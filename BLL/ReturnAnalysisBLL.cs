using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using Microsoft.ML;
using Microsoft.ML.Data;               // ← Added for ColumnNameAttribute
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms.Text;

namespace Erp_V1.BLL
{
    /// <summary>
    /// Professional-grade return analysis service that coordinates preprocessing, 
    /// ML.NET clustering, silhouette scoring, and keyword extraction with enhanced accuracy.
    /// </summary>
    public class ReturnAnalysisBLL
    {
        #region Constants
        private const int SEED_VALUE = 42;
        private const int MIN_WORD_LENGTH = 3;
        private const int MAX_ITERATIONS = 200;
        private const int MIN_CLUSTER_COUNT = 2;
        private const int MAX_CLUSTER_TO_TEST = 5;
        private const double MIN_SILHOUETTE_THRESHOLD = 0.1;
        private const int MAX_TOKENS_PER_DOCUMENT = 100;
        private const int TOP_KEYWORDS_COUNT = 5;
        #endregion

        #region Fields
        private readonly ReturnDAO _returnDao;
        private readonly MLContext _mlContext;
        private ITransformer _model;
        private readonly HashSet<string> _stopWords;
        private readonly ConcurrentDictionary<uint, List<string>> _keywordCache;
        #endregion

        #region Stop Words
        private static readonly string[] DefaultStopWords =
        {
            "a","about","above","after","again","against","all","am","an","and","any",
            "are","as","at","be","because","been","before","being","below",
            "between","both","but","by","can","cannot","could","did",
            "do","does","doing","don't","down","during","each","few",
            "for","from","further","had","has","have","having",
            "he","he'd","he'll","he's","her","here","hers","herself","him",
            "himself","his","how","i","i'd","i'll","i'm","i've","if","in",
            "into","is","it","it's","its","itself","me","more","most",
            "my","myself","no","nor","not","of","off","on","once","only",
            "or","other","ought","our","ours","ourselves","out","over","own","same",
            "she","she'd","she'll","she's","should","so","some",
            "such","than","that","that's","the","their","theirs","them","themselves",
            "then","there","there's","these","they","they'd","they'll","they're","they've",
            "this","those","through","to","too","under","until","up","very","was",
            "we","we'd","we'll","we're","we've","were","what","when","where",
            "which","while","who","whom","why","with","would","you","you'd","you'll",
            "you're","you've","your","yours","yourself","yourselves"
        };
        #endregion

        #region Constructors
        public ReturnAnalysisBLL(ReturnDAO returnDao)
        {
            _returnDao = returnDao ?? throw new ArgumentNullException(nameof(returnDao));
            _mlContext = new MLContext(SEED_VALUE);
            _stopWords = new HashSet<string>(DefaultStopWords, StringComparer.OrdinalIgnoreCase);
            _keywordCache = new ConcurrentDictionary<uint, List<string>>();
        }

        public ReturnAnalysisBLL() : this(new ReturnDAO()) { }
        #endregion

        #region Public Methods
        public async Task<ReturnAnalysisResultsDTO> AnalyzeReturnReasonsAsync(
            ReturnAnalysisConfigDTO config,
            CancellationToken cancellationToken = default)
        {
            ValidateConfig(config);

            try
            {
                var returnDto = await Task.Run(() => Select(), cancellationToken);
                var returnDetails = returnDto?.Returns;
                if (returnDetails == null || !returnDetails.Any())
                    return CreateEmptyResults();

                var processedData = await ProcessReturnReasonsAsync(returnDetails, config, cancellationToken);
                if (processedData.Count < MIN_CLUSTER_COUNT)
                    return CreateEmptyResults();

                int maxPossible = CalculateMaxClusters(processedData.Count);
                var findResult = await FindOptimalClusterCountAsync(processedData, maxPossible, cancellationToken);
                int optimalK = findResult.OptimalClusterCount;

                config.ClusterCount = optimalK;
                _model = await TrainModelAsync(processedData, optimalK, cancellationToken);

                var dataView = _mlContext.Data.LoadFromEnumerable(processedData);
                var predictions = _model.Transform(dataView);
                var clusteringResults = ProcessPredictions(processedData, predictions);

                double silhouetteScore = await ClusteringHelpers.CalculateSilhouetteScoreAsync(
                    clusteringResults, cancellationToken);

                var normalizedResults = NormalizeClusterAssignments(clusteringResults, optimalK);
                var clusterKeywords = ExtractNormalizedKeywords(normalizedResults, optimalK);

                AssignClusterDescriptions(normalizedResults, clusterKeywords);

                var clusterMetrics = new Dictionary<int, double> { { optimalK, silhouetteScore } };
                return new ReturnAnalysisResultsDTO
                {
                    Results = normalizedResults
                        .Select(r => new ReturnAnalysisDTO
                        {
                            ReturnID = r.ReturnID,
                            OriginalReason = r.OriginalReason,
                            ProcessedReason = r.ProcessedReason,
                            ClusterID = r.ClusterID,
                            ClusterDescription = r.ClusterDescription,
                            Confidence = r.Confidence,
                            ReturnDate = r.ReturnDate,
                            CustomerID = r.CustomerID,
                            ProductID = r.ProductID
                        }).ToList(),
                    ClusterMetrics = clusterMetrics,
                    ClusterKeywords = clusterKeywords,
                    QualityScore = silhouetteScore
                };
            }
            catch (Exception ex)
            {
                // TODO: Implement professional logging (e.g., Serilog, NLog)
                throw new InvalidOperationException($"Analysis failed: {ex.Message}", ex);
            }
        }

        public ReturnDTO Select()
        {
            try
            {
                var returnDetails = _returnDao.Select();
                return new ReturnDTO { Returns = returnDetails ?? new List<ReturnDetailDTO>() };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to fetch return data: {ex.Message}", ex);
            }
        }

        public async Task<List<ReturnReasonData>> ProcessReturnReasonsAsync(
            List<ReturnDetailDTO> returns,
            ReturnAnalysisConfigDTO config,
            CancellationToken cancellationToken)
        {
            if (returns == null) return new List<ReturnReasonData>();

            return await Task.Run(() =>
                returns
                    .AsParallel() // Process in parallel for performance
                    .Where(r => !string.IsNullOrWhiteSpace(r.ReturnReason) && r.ReturnReason.Trim().Length >= config.MinWordLength)
                    .Select(r => new ReturnReasonData
                    {
                        ReturnID = r.ReturnID,
                        Reason = PreprocessTextEnhanced(r.ReturnReason, config.MinWordLength, config.StopWords),
                        OriginalReason = r.ReturnReason,
                        ReturnDate = r.ReturnDate,
                        CustomerID = r.CustomerID,
                        ProductID = r.ProductID
                    })
                    .Where(r => !string.IsNullOrWhiteSpace(r.Reason))
                    .ToList()
            , cancellationToken);
        }

        public async Task<List<ReturnReasonData>> ProcessReturnReasonsAsync(
            List<ReturnDetailDTO> returns)
        {
            var cfg = new ReturnAnalysisConfigDTO
            {
                MinWordLength = MIN_WORD_LENGTH,
                StopWords = new HashSet<string>(_stopWords)
            };
            return await ProcessReturnReasonsAsync(returns, cfg, CancellationToken.None);
        }

        public async Task<(int OptimalClusterCount, Dictionary<int, double> ClusterMetrics)> FindOptimalClusterCountAsync(
            List<ReturnReasonData> data,
            int maxClustersToTest,
            CancellationToken cancellationToken)
        {
            if (data == null || data.Count < MIN_CLUSTER_COUNT)
            {
                return (data?.Count > 0 ? 1 : MIN_CLUSTER_COUNT, new Dictionary<int, double>());
            }

            int actualMaxClusters = Math.Min(Math.Min(maxClustersToTest, data.Count), MAX_CLUSTER_TO_TEST);
            actualMaxClusters = Math.Max(actualMaxClusters, MIN_CLUSTER_COUNT);

            if (data.Count < MIN_CLUSTER_COUNT)
            {
                return (1, new Dictionary<int, double>());
            }

            var metrics = new ConcurrentDictionary<int, double>();
            var dataView = _mlContext.Data.LoadFromEnumerable(data);

            var tasks = Enumerable.Range(MIN_CLUSTER_COUNT, actualMaxClusters - MIN_CLUSTER_COUNT + 1)
                .Select(k => Task.Run(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    try
                    {
                        var pipeline = BuildAnalysisPipeline(k);
                        var model = pipeline.Fit(dataView);
                        var preds = model.Transform(dataView);
                        var interimResults = ProcessPredictions(data, preds);
                        double score = await ClusteringHelpers.CalculateSilhouetteScoreAsync(interimResults, cancellationToken);
                        metrics.TryAdd(k, score);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error training for K={k}: {ex.Message}");
                        metrics.TryAdd(k, -1.0); // Indicate failure
                    }
                }, cancellationToken));

            await Task.WhenAll(tasks);

            if (!metrics.Any(m => m.Value > -1.0))
            {
                return (data.Count >= MIN_CLUSTER_COUNT ? MIN_CLUSTER_COUNT : 1, new Dictionary<int, double>(metrics));
            }

            var validMetrics = metrics.Where(kvp => kvp.Value > -1.0).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            if (!validMetrics.Any()) return (MIN_CLUSTER_COUNT, new Dictionary<int, double>());

            int bestK = validMetrics
                .OrderByDescending(kvp => kvp.Value)
                .FirstOrDefault(kvp => kvp.Value >= MIN_SILHOUETTE_THRESHOLD)
                .Key;

            if (bestK == 0) // No cluster met the threshold
            {
                bestK = validMetrics.OrderByDescending(kvp => kvp.Value).First().Key;
            }

            return (bestK, new Dictionary<int, double>(validMetrics));
        }

        public Task<double> CalculateSilhouetteScoreAsync(
            List<ReturnAnalysisResult> dtoResults,
            CancellationToken cancellationToken)
        {
            return ClusteringHelpers.CalculateSilhouetteScoreAsync(dtoResults, cancellationToken);
        }

        // **FIX APPLIED HERE**
        // Removed the third argument 'TOP_KEYWORDS_COUNT' as no overload of the method accepts it.
        public Dictionary<uint, List<string>> IdentifyClusterKeywords(
            List<ReturnAnalysisResult> dtoResults)
        {
            return ClusteringHelpers.ExtractClusterKeywords(dtoResults, _keywordCache);
        }
        #endregion

        #region Private Helper Methods
        private void ValidateConfig(ReturnAnalysisConfigDTO config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config), "Configuration object cannot be null.");
        }

        private int CalculateMaxClusters(int dataCount)
        {
            if (dataCount < 1) return MIN_CLUSTER_COUNT;
            int maxPossible = Math.Min(MAX_CLUSTER_TO_TEST, dataCount / 2);
            return Math.Max(MIN_CLUSTER_COUNT, maxPossible);
        }

        private string PreprocessTextEnhanced(string text, int minWordLength, HashSet<string> stopWords)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            var effectiveStopWords = stopWords ?? _stopWords;

            string cleaned = text.ToLowerInvariant();
            cleaned = Regex.Replace(cleaned, @"[^\w\s']", " ", RegexOptions.Compiled);
            cleaned = Regex.Replace(cleaned, @"\s+", " ", RegexOptions.Compiled).Trim();

            var tokens = cleaned
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(w => w.Length >= minWordLength && !effectiveStopWords.Contains(w) && !double.TryParse(w, out _))
                .Distinct()
                .Take(MAX_TOKENS_PER_DOCUMENT);

            return string.Join(" ", tokens);
        }

        public IEstimator<ITransformer> BuildAnalysisPipeline(int clusterCount)
        {
            var textPipeline = _mlContext.Transforms.Text.FeaturizeText(
                outputColumnName: "Features",
                options: new TextFeaturizingEstimator.Options
                {
                    WordFeatureExtractor = new WordBagEstimator.Options
                    {
                        NgramLength = 2,
                        UseAllLengths = true,
                        MaximumNgramsCount = new[] { 10000, 10000 }
                    },
                    CharFeatureExtractor = new WordBagEstimator.Options
                    {
                        NgramLength = 3,
                        UseAllLengths = false,
                        MaximumNgramsCount = new[] { 16000 }
                    },
                    OutputTokensColumnName = "Tokens"
                },
                inputColumnNames: nameof(ReturnReasonData.Reason));

            var kmeansTrainer = _mlContext.Clustering.Trainers.KMeans(new KMeansTrainer.Options
            {
                NumberOfClusters = clusterCount,
                FeatureColumnName = "Features",
                MaximumNumberOfIterations = MAX_ITERATIONS,
                InitializationAlgorithm = KMeansTrainer.InitializationAlgorithm.KMeansPlusPlus,
                OptimizationTolerance = 1e-6f
            });

            return textPipeline.Append(kmeansTrainer);
        }

        private async Task<ITransformer> TrainModelAsync(
            List<ReturnReasonData> data,
            int clusterCount,
            CancellationToken cancellationToken)
        {
            if (data == null || !data.Any())
                throw new ArgumentException("Training data cannot be null or empty.", nameof(data));

            if (clusterCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(clusterCount), "Cluster count must be greater than zero.");

            var pipeline = BuildAnalysisPipeline(clusterCount);
            return await Task.Run(() =>
            {
                try
                {
                    var dataView = _mlContext.Data.LoadFromEnumerable(data);
                    if (!dataView.Schema.Any()) throw new InvalidOperationException("DataView schema is empty after loading data.");
                    return pipeline.Fit(dataView);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Model training failed for K={clusterCount}. Ensure data is adequate. Original error: {ex.Message}", ex);
                }
            }, cancellationToken);
        }

        public List<ReturnAnalysisResult> ProcessPredictions(
            List<ReturnReasonData> data,
            IDataView predictions)
        {
            var predictedData = _mlContext.Data.CreateEnumerable<ReturnClusterPrediction>(
                predictions, reuseRowObject: false).ToList();

            return data.Zip(predictedData, (original, prediction) => new ReturnAnalysisResult
            {
                ReturnID = original.ReturnID,
                OriginalReason = original.OriginalReason,
                ProcessedReason = original.Reason,
                ClusterID = prediction.PredictedClusterId,
                Confidence = CalculateKMeansConfidence(prediction.Score, prediction.PredictedClusterId),
                ReturnDate = original.ReturnDate,
                CustomerID = original.CustomerID,
                ProductID = original.ProductID
            }).ToList();
        }

        private float CalculateKMeansConfidence(float[] distances, uint predictedClusterId)
        {
            if (distances == null || distances.Length == 0) return 0f;

            int predictedIndex = (int)predictedClusterId - 1;
            if (predictedIndex < 0 || predictedIndex >= distances.Length) return 0f;

            if (distances.Length == 1) return 1f;

            float ownDistance = distances[predictedIndex];
            float nearestOtherDistance = distances.Where((d, i) => i != predictedIndex).DefaultIfEmpty(float.MaxValue).Min();

            if (nearestOtherDistance < 1e-9)
            {
                return (ownDistance < 1e-9) ? 0.5f : 0f;
            }

            float confidence = (nearestOtherDistance - ownDistance) / nearestOtherDistance;
            return Math.Max(0f, Math.Min(1f, confidence)); // Clamp to [0, 1]
        }

        private List<ReturnAnalysisResult> NormalizeClusterAssignments(
            List<ReturnAnalysisResult> results,
            int targetClusterCount)
        {
            if (!results.Any() || targetClusterCount <= 0) return results;

            var topKClustersIds = results
                .GroupBy(r => r.ClusterID)
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key)
                .Select(g => g.Key)
                .Take(targetClusterCount)
                .ToList();

            if (!topKClustersIds.Any()) return results;

            var clusterIdMapping = topKClustersIds
                .Select((oldId, index) => new { OldId = oldId, NewId = (uint)(index + 1) })
                .ToDictionary(map => map.OldId, map => map.NewId);

            uint fallbackClusterId = clusterIdMapping.First().Value;

            foreach (var result in results)
            {
                result.ClusterID = clusterIdMapping.TryGetValue(result.ClusterID, out var newId) ? newId : fallbackClusterId;
            }
            return results;
        }

        private Dictionary<uint, List<string>> ExtractNormalizedKeywords(
            List<ReturnAnalysisResult> normalizedResults,
            int targetClusterCount)
        {
            // **FIX APPLIED HERE**
            // Removed the third argument 'TOP_KEYWORDS_COUNT' as no overload of the method accepts it.
            var keywordsByCluster = ClusteringHelpers.ExtractClusterKeywords(normalizedResults, _keywordCache);
            var finalKeywords = new Dictionary<uint, List<string>>();

            for (uint i = 1; i <= targetClusterCount; i++)
            {
                finalKeywords[i] = keywordsByCluster.TryGetValue(i, out var keywords) && keywords.Any()
                    ? keywords
                    : new List<string> { "general", "return", "miscellaneous" };
            }
            return finalKeywords;
        }

        private void AssignClusterDescriptions(
            List<ReturnAnalysisResult> results,
            Dictionary<uint, List<string>> clusterKeywords)
        {
            foreach (var result in results)
            {
                result.ClusterDescription = clusterKeywords.TryGetValue(result.ClusterID, out var keywords) && keywords.Any()
                    ? CreateProfessionalDescription(keywords)
                    : "General Return Category";
            }
        }

        private string CreateProfessionalDescription(List<string> keywords)
        {
            if (keywords == null || !keywords.Any())
                return "General Return Issues";

            var keywordSet = new HashSet<string>(keywords.Select(k => k.ToLowerInvariant()));

            if (keywordSet.Overlaps(new[] { "broken", "damaged", "cracked", "defective", "faulty" }))
                return "Product Damage & Defects";
            if (keywordSet.Overlaps(new[] { "wrong", "size", "fit", "large", "small", "color", "item" }))
                return "Incorrect Item / Sizing / Color";
            if (keywordSet.Overlaps(new[] { "shipping", "delivery", "arrived", "package", "late", "box" }))
                return "Shipping & Delivery Problems";
            if (keywordSet.Overlaps(new[] { "quality", "cheap", "poor", "material", "performance" }))
                return "Product Quality/Performance Concerns";
            if (keywordSet.Overlaps(new[] { "missing", "parts", "incomplete", "component" }))
                return "Missing Parts / Incomplete Item";
            if (keywordSet.Overlaps(new[] { "description", "match", "expected", "different", "picture" }))
                return "Not as Described / Pictured";
            if (keywordSet.Overlaps(new[] { "wanted", "needed", "longer", "mind", "changed", "mistake" }))
                return "Order Error / No Longer Needed";
            if (keywordSet.Overlaps(new[] { "work", "function", "compatible", "connect", "stopped" }))
                return "Functionality/Compatibility Issues";

            var topThree = keywordSet.Take(3).Select(k => char.ToUpper(k[0]) + k.Substring(1));
            return $"Issues related to: {string.Join(", ", topThree)}";
        }

        private ReturnAnalysisResultsDTO CreateEmptyResults()
        {
            return new ReturnAnalysisResultsDTO
            {
                Results = new List<ReturnAnalysisDTO>(),
                ClusterMetrics = new Dictionary<int, double>(),
                ClusterKeywords = new Dictionary<uint, List<string>>(),
                QualityScore = 0
            };
        }
        #endregion
    }

    /// <summary>
    /// Represents the prediction output from the K-Means clustering model.
    /// </summary>
    public class ReturnClusterPrediction
    {
        /// <summary>
        /// The ID of the predicted cluster (1-based).
        /// </summary>
        [ColumnName("PredictedLabel")]           // ← Map ML.NET's “PredictedLabel” → PredictedClusterId
        public uint PredictedClusterId { get; set; }

        /// <summary>
        /// An array of floats representing the squared Euclidean distances to each cluster centroid.
        /// The length of the array is equal to the number of clusters (K).
        /// </summary>
        [ColumnName("Score")]                    // ← Map ML.NET's “Score” → Score[]
        public float[] Score { get; set; }
    }
}
