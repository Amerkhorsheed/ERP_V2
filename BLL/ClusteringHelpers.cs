// File: ClusteringHelpers.cs

using Erp_V1.DAL.DTO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Erp_V1.BLL
{
    /// <summary>
    /// Professional-grade static helper class for advanced TF-IDF analysis, 
    /// enhanced silhouette scoring, and intelligent keyword extraction.
    /// </summary>
    public static class ClusteringHelpers
    {
        #region Constants
        private const int MIN_WORD_LENGTH = 3; // Minimum word length for keyword extraction and TF-IDF
        private const int TOP_KEYWORDS_COUNT = 5; // Number of top keywords to extract per cluster
        private const double EPSILON = 1e-10; // Small value for numerical stability
        #endregion

        #region Public Methods
        /// <summary>
        /// Computes enhanced silhouette score using TF-IDF vectors with cosine distance.
        /// </summary>
        public static async Task<double> CalculateSilhouetteScoreAsync(
            List<ReturnAnalysisResult> results,
            CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                try
                {
                    if (results == null || results.Count < 2)
                        return 0.0;

                    // Filter out empty or invalid processed reasons
                    var validResults = results
                        .Where(r => !string.IsNullOrWhiteSpace(r.ProcessedReason))
                        .ToList();

                    if (validResults.Count < 2)
                        return 0.0;

                    // Check for single cluster
                    var uniqueClusters = validResults.Select(r => r.ClusterID).Distinct().Count();
                    if (uniqueClusters <= 1)
                        return 0.0;

                    var documents = validResults.Select(r => r.ProcessedReason).ToList();
                    if (!documents.Any()) return 0.0;

                    var vectors = TfidfVectorizer.TransformAll(documents);
                    if (vectors == null || !vectors.Any() || vectors.All(v => v.Length == 0)) return 0.0;


                    var clusterAssignments = validResults
                        .Select((r, idx) => (Vector: vectors[idx], Cluster: r.ClusterID))
                        .Where(item => item.Vector != null && item.Vector.Length > 0) // Ensure vectors are valid
                        .ToList();

                    if (clusterAssignments.Count < 2 || clusterAssignments.Select(ca => ca.Cluster).Distinct().Count() <= 1)
                        return 0.0;


                    return ComputeEnhancedSilhouette(clusterAssignments);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Silhouette calculation error: {ex.Message}");
                    return 0.0; // Return neutral score on error
                }
            }, cancellationToken);
        }

        /// <summary>
        /// Extracts intelligent keywords with frequency analysis and relevance scoring.
        /// Utilizes a cache for performance.
        /// </summary>
        public static Dictionary<uint, List<string>> ExtractClusterKeywords(
            List<ReturnAnalysisResult> results,
            ConcurrentDictionary<uint, List<string>> keywordCache)
        {
            if (results == null || !results.Any())
                return new Dictionary<uint, List<string>>();

            return results
                .Where(r => !string.IsNullOrWhiteSpace(r.ProcessedReason))
                .GroupBy(r => r.ClusterID)
                .ToDictionary(
                    grp => grp.Key,
                    grp => keywordCache.GetOrAdd(grp.Key, key =>
                        ExtractSmartKeywords(grp.ToList())
                    )
                );
        }
        #endregion

        #region Private Helper Methods
        /// <summary>
        /// Computes enhanced silhouette with cosine distance and numerical stability.
        /// </summary>
        private static double ComputeEnhancedSilhouette(List<(float[] Vector, uint Cluster)> data)
        {
            if (data == null || data.Count == 0) return 0.0;

            var clusterGroups = data.GroupBy(d => d.Cluster).ToList();
            if (clusterGroups.Count <= 1) return 0.0;

            var clusters = clusterGroups.ToDictionary(g => g.Key, g => g.ToList());
            double totalScore = 0.0;
            int validPoints = 0;

            foreach (var current in data)
            {
                if (current.Vector == null || current.Vector.Length == 0) continue;

                var currentClusterData = clusters[current.Cluster];

                // Skip single-point clusters for more stable calculation of 'a'
                // 'a' is not well-defined for a single-point cluster.
                if (currentClusterData.Count <= 1)
                {
                    // Assign a neutral or slightly negative score for points in singleton clusters if they are the only ones.
                    // However, if other clusters exist, 'b' can be calculated.
                    // For simplicity here, we can skip them or assign silhouette score of 0.
                    // If all clusters are singletons, the previous check clusterGroups.Count <= 1 handles it.
                    // If this specific point is in a singleton cluster but others are not, its 'a' is effectively 0 or undefined.
                    // Let's assign 0 for such points for 'a'.
                }


                // a_i = average distance to others in same cluster
                var sameClusterPoints = currentClusterData
                    .Where(x => !ReferenceEquals(x, current) && x.Vector != null && x.Vector.Length > 0)
                    .ToList();

                double a = 0.0;
                if (sameClusterPoints.Any())
                {
                    a = sameClusterPoints.Average(x => CosineSimilarityDistance(current.Vector, x.Vector));
                }
                // If the point is the only one in its cluster, 'a' is typically treated as 0.

                // b_i = minimum average distance to any other cluster
                double b = double.MaxValue;
                bool otherClusterExists = false;
                foreach (var kvp in clusters)
                {
                    if (kvp.Key == current.Cluster) continue;

                    var otherClusterData = kvp.Value.Where(x => x.Vector != null && x.Vector.Length > 0).ToList();
                    if (otherClusterData.Any())
                    {
                        otherClusterExists = true;
                        double avgDistToCluster = otherClusterData
                            .Average(x => CosineSimilarityDistance(current.Vector, x.Vector));
                        b = Math.Min(b, avgDistToCluster);
                    }
                }

                if (!otherClusterExists) // Should not happen if clusterGroups.Count > 1
                {
                    b = 0; // Or handle as an error/special case. Max distance implies good separation if a=0.
                           // If there are no other clusters, silhouette is 0 (already handled by clusterGroups.Count <= 1)
                           // This means 'b' cannot be calculated, so the point's silhouette is undefined, typically 0.
                }


                // Compute silhouette coefficient with numerical stability
                if (b == double.MaxValue) b = 1.0; // If no other clusters to compare (e.g. all points ended up in one valid cluster after filtering)
                                                   // This case should ideally be caught by initial checks.
                                                   // If b remains MaxValue, it means no other valid cluster points found.

                double maxValue = Math.Max(a, b);
                double silhouetteCoeff;
                if (maxValue < EPSILON) // If a and b are both near zero (e.g. point is very close to all other points or singleton)
                {
                    silhouetteCoeff = 0.0;
                }
                else if (currentClusterData.Count <= 1 && !otherClusterExists) // Point in singleton and no other clusters
                {
                    silhouetteCoeff = 0.0;
                }
                else if (currentClusterData.Count <= 1) // Point in singleton cluster, 'a' is 0. Silhouette is 1 if b > 0.
                {
                    silhouetteCoeff = (b - 0) / Math.Max(0, b); // effectively 1 if b > 0, 0 if b is 0.
                    if (b < EPSILON) silhouetteCoeff = 0.0; // if b is also 0.
                    else silhouetteCoeff = 1.0; // well separated from other clusters (b is the measure of that)
                }
                else
                {
                    silhouetteCoeff = (b - a) / maxValue;
                }

                totalScore += silhouetteCoeff;
                validPoints++;
            }

            return validPoints > 0 ? totalScore / validPoints : 0.0;
        }

        /// <summary>
        /// Computes cosine similarity distance (1 - cosine similarity).
        /// Result is between 0 (identical) and 2 (opposite, if vectors can have negative components),
        /// or 0 and 1 for non-negative vectors like TF-IDF.
        /// </summary>
        private static double CosineSimilarityDistance(float[] v1, float[] v2)
        {
            if (v1 == null || v2 == null || v1.Length == 0 || v2.Length == 0 || v1.Length != v2.Length)
                return 1.0; // Max distance for invalid/mismatched inputs or if one is zero vector effectively

            double dotProduct = 0.0;
            double norm1 = 0.0;
            double norm2 = 0.0;

            for (int i = 0; i < v1.Length; i++)
            {
                dotProduct += v1[i] * v2[i];
                norm1 += v1[i] * v1[i];
                norm2 += v2[i] * v2[i];
            }

            double norm1Sqrt = Math.Sqrt(norm1);
            double norm2Sqrt = Math.Sqrt(norm2);

            if (norm1Sqrt < EPSILON || norm2Sqrt < EPSILON) // Check for zero vectors
                return 1.0; // Max distance if one vector is effectively zero

            double similarity = dotProduct / (norm1Sqrt * norm2Sqrt);

            // Ensure similarity is within [-1, 1] due to potential floating point inaccuracies
            similarity = Math.Max(-1.0, Math.Min(1.0, similarity));

            return 1.0 - similarity; // Cosine distance for non-negative vectors ranges [0, 1]
        }

        /// <summary>
        /// Extracts relevant keywords from a list of return analysis results for a specific cluster
        /// using frequency analysis of processed reasons.
        /// </summary>
        private static List<string> ExtractSmartKeywords(List<ReturnAnalysisResult> clusterResults)
        {
            if (clusterResults == null || !clusterResults.Any())
                return new List<string> { "general", "item", "issue" }; // Default keywords

            var wordFrequencies = new ConcurrentDictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            Parallel.ForEach(clusterResults, result =>
            {
                if (!string.IsNullOrWhiteSpace(result.ProcessedReason))
                {
                    // ProcessedReason is assumed to be already tokenized by spaces, cleaned, and filtered by length
                    var words = result.ProcessedReason.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    // .Where(w => w.Length >= MIN_WORD_LENGTH); // Redundant if ProcessedReason is truly processed
                    foreach (var word in words)
                    {
                        wordFrequencies.AddOrUpdate(word, 1, (key, count) => count + 1);
                    }
                }
            });

            if (wordFrequencies.IsEmpty)
                return new List<string> { "general", "item", "issue" };

            var topKeywords = wordFrequencies
                .OrderByDescending(kvp => kvp.Value)
                .ThenBy(kvp => kvp.Key) // Consistent ordering for ties
                .Select(kvp => kvp.Key)
                .Take(TOP_KEYWORDS_COUNT)
                .ToList();

            if (!topKeywords.Any()) // Should be covered by IsEmpty check above
            {
                return new List<string> { "general", "item", "issue" };
            }

            return topKeywords;
        }
        #endregion

        #region TF-IDF Vectorizer
        /// <summary>
        /// Provides TF-IDF vectorization capabilities.
        /// Assumes documents are already preprocessed (tokenized by space, stopwords removed, etc.).
        /// </summary>
        private static class TfidfVectorizer
        {
            private static List<string> TokenizeText(string text)
            {
                // Text is assumed to be from ProcessedReason: already lowercased, punctuation removed,
                // stopwords removed, numbers removed, and words filtered by min length.
                return text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            public static List<float[]> TransformAll(List<string> documents)
            {
                if (documents == null || !documents.Any())
                    return new List<float[]>();

                var tokenizedDocuments = documents.Select(TokenizeText).ToList();

                var vocabularySet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var docTokens in tokenizedDocuments)
                {
                    foreach (var token in docTokens)
                    {
                        vocabularySet.Add(token);
                    }
                }
                var vocabList = vocabularySet.ToList();
                var vocabIndexMap = vocabList.Select((word, idx) => new { word, idx })
                                             .ToDictionary(p => p.word, p => p.idx, StringComparer.OrdinalIgnoreCase);

                if (!vocabList.Any())
                {
                    return documents.Select(d => Array.Empty<float>()).ToList();
                }

                // Calculate IDF
                var idf = new double[vocabList.Count];
                int N = tokenizedDocuments.Count;
                for (int i = 0; i < vocabList.Count; i++)
                {
                    string term = vocabList[i];
                    int docsContainingTerm = tokenizedDocuments.Count(docTokens => docTokens.Contains(term, StringComparer.OrdinalIgnoreCase));
                    // Adding 1 to N in numerator and 1 to docsContainingTerm in denominator (IDF smoothing)
                    // Standard: Math.Log((double)N / docsContainingTerm)
                    // Smoothed (common): Math.Log((double)N / (1 + docsContainingTerm)) or Math.Log(1 + ((double)N / docsContainingTerm))
                    idf[i] = Math.Log((double)N / (1 + docsContainingTerm)) + 1.0; // Plus 1 to ensure positive IDF for terms in all docs
                }

                var tfidfVectors = new List<float[]>();
                foreach (var docTokens in tokenizedDocuments)
                {
                    var vector = new float[vocabList.Count];
                    if (!docTokens.Any())
                    {
                        tfidfVectors.Add(vector); // Add a zero vector for empty documents
                        continue;
                    }

                    var tf = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
                    foreach (var token in docTokens)
                    {
                        if (tf.ContainsKey(token))
                            tf[token]++;
                        else
                            tf[token] = 1;
                    }

                    int totalTermsInDoc = docTokens.Count;
                    foreach (var token in tf.Keys.ToList()) // Normalize TF
                    {
                        tf[token] = tf[token] / totalTermsInDoc;
                    }

                    for (int i = 0; i < vocabList.Count; i++)
                    {
                        string term = vocabList[i];
                        double termTf = tf.TryGetValue(term, out var tfVal) ? tfVal : 0.0;
                        vector[i] = (float)(termTf * idf[i]);
                    }
                    tfidfVectors.Add(vector);
                }
                return tfidfVectors;
            }
        }
        #endregion
    }
}