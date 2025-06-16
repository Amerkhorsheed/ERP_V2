//// File: BLL/RecommendationEngine.cs
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Accord.MachineLearning;
//using Accord.Math.Distances;
//using Accord.Math.Random;
//using Erp_V1.DAL.DAO;
//using Erp_V1.DAL.DTO;
//using Microsoft.Extensions.Logging;

//namespace Erp_V1.BLL
//{
//    /// <summary>
//    /// Clusters customers’ purchase patterns via K-means (random initialization) 
//    /// and returns recommendations based on cluster neighbors.
//    /// </summary>
//    public sealed class RecommendationEngine : IRecommendationEngine, IDisposable
//    {
//        private const int MinDataPoints = 10;       // Minimum distinct customers to run clustering
//        private const int MaxClusters = 15;         // Maximum K to attempt
//        private const int RecommendationCount = 5;  // Number of products to recommend
//        private const int SilhouetteSampleSize = 5000;

//        private readonly SalesDAO _salesDao;
//        private readonly ProductDAO _productDao;
//        private readonly ILogger<RecommendationEngine> _logger;
//        private bool _disposed;

//        /// <summary>
//        /// Seed for deterministic runs; set to a non‐negative integer. Default = 0.
//        /// </summary>
//        public int RandomSeed { get; set; }

//        /// <inheritdoc />
//        public List<ProductDetailDTO> AvailableProducts
//        {
//            get { return _productDao.Select(); }
//        }

//        public RecommendationEngine(
//            SalesDAO salesDao,
//            ProductDAO productDao,
//            ILogger<RecommendationEngine> logger)
//        {
//            if (salesDao == null) throw new ArgumentNullException(nameof(salesDao));
//            if (productDao == null) throw new ArgumentNullException(nameof(productDao));

//            _salesDao = salesDao;
//            _productDao = productDao;
//            _logger = logger;
//            RandomSeed = 0;
//        }

//        /// <inheritdoc />
//        public int DetermineOptimalK(double[][] data)
//        {
//            ValidateDataMatrix(data);

//            if (data.Length < MinDataPoints)
//            {
//                _logger?.LogWarning(
//                    "Not enough data points ({Count}) for clustering; returning K=1",
//                    data.Length);
//                return 1;
//            }

//            int upperK = Math.Min(MaxClusters, data.Length - 1);
//            List<ClusteringDiagnostics> diagnostics = EvaluateClusters(data, upperK);

//            if (diagnostics == null || diagnostics.Count == 0)
//            {
//                _logger?.LogWarning(
//                    "No valid clusters for data length {Length}; returning K=1",
//                    data.Length);
//                return 1;
//            }

//            ClusteringDiagnostics best = MaxBySilhouette(diagnostics);
//            return best != null ? best.K : 1;
//        }

//        /// <inheritdoc />
//        public List<ClusteringDiagnostics> EvaluateClusters(double[][] data, int maxK)
//        {
//            ValidateDataMatrix(data);

//            var results = new List<ClusteringDiagnostics>();
//            if (data.Length < 2) return results;

//            int upperK = Math.Min(maxK, data.Length - 1);
//            Generator.Seed = RandomSeed;

//            // Pre‐allocate a distance calculator
//            var distanceCalc = new SquareEuclidean();

//            Parallel.For(
//                2,
//                upperK + 1,
//                new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
//                k =>
//                {
//                    try
//                    {
//                        // Build and train KMeans with random initialization
//                        var kmeans = new KMeans(k)
//                        {
//                            Distance = distanceCalc,
//                            Tolerance = 0.01,
//                            MaxIterations = 500,
//                            ParallelOptions = { MaxDegreeOfParallelism = Environment.ProcessorCount }
//                        };

//                        // Use the same seed for reproducibility
//                        kmeans.Random = new Accord.Math.Random.Generator(Generator.Seed);

//                        KMeansClusterCollection model = kmeans.Learn(data);
//                        int[] labels = model.Decide(data);

//                        if (labels.Distinct().Count() < 2) return;

//                        double score = CalculateSilhouetteScore(data, labels, distanceCalc);
//                        lock (results)
//                        {
//                            results.Add(new ClusteringDiagnostics(k, score));
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        _logger?.LogError(
//                            ex,
//                            "Error clustering with K={KValue} (data length={Length})",
//                            k,
//                            data.Length);
//                    }
//                });

//            return results.OrderBy(d => d.K).ToList();
//        }

//        /// <inheritdoc />
//        public List<ProductDetailDTO> GetRecommendationsForCustomer(string customerName)
//        {
//            if (string.IsNullOrWhiteSpace(customerName))
//                throw new ArgumentException(
//                    "Customer name must be specified", nameof(customerName));

//            List<SalesDetailDTO> allSales = _salesDao.Select();
//            List<string> uniqueCustomers = allSales
//                .Select(s => s.CustomerName)
//                .Distinct()
//                .ToList();

//            if (!uniqueCustomers.Contains(customerName) || uniqueCustomers.Count < MinDataPoints)
//            {
//                _logger?.LogInformation(
//                    "Fallback to top sellers for {Customer}", customerName);
//                return GetTopSellingProducts(RecommendationCount, allSales);
//            }

//            var matrixData = BuildPurchaseMatrix(allSales, uniqueCustomers);
//            double[][] matrix = matrixData.Item1;
//            Dictionary<string, int> customerIndex = matrixData.Item2;
//            Dictionary<int, int> productIndex = matrixData.Item3;

//            int targetRow = customerIndex[customerName];
//            int optimalK = DetermineOptimalK(matrix);

//            if (optimalK <= 1)
//            {
//                _logger?.LogWarning(
//                    "Optimal K ≤ 1 for {Customer}; falling back to top sellers",
//                    customerName);
//                return GetTopSellingProducts(RecommendationCount, allSales);
//            }

//            var trained = TrainKMeans(matrix, optimalK);
//            int[] labels = trained.Item2;

//            return GenerateRecommendations(matrix, labels, targetRow, productIndex, allSales);
//        }

//        #region ───── Private Helpers ─────

//        /// <summary>
//        /// Trains a KMeans model (random init) and returns (model, labels).
//        /// </summary>
//        private Tuple<KMeansClusterCollection, int[]> TrainKMeans(double[][] data, int k)
//        {
//            Generator.Seed = RandomSeed;

//            var distanceCalc = new SquareEuclidean();
//            var kmeans = new KMeans(k)
//            {
//                Distance = distanceCalc,
//                Tolerance = 0.01,
//                MaxIterations = 500,
//                ParallelOptions = { MaxDegreeOfParallelism = Environment.ProcessorCount }
//            };

//            kmeans.Random = new Accord.Math.Random.Generator(Generator.Seed);
//            KMeansClusterCollection model = kmeans.Learn(data);
//            int[] labels = model.Decide(data);
//            return Tuple.Create(model, labels);
//        }

//        /// <summary>
//        /// Computes silhouette score for each point and returns the average.
//        /// </summary>
//        private double CalculateSilhouetteScore(
//            double[][] data,
//            int[] labels,
//            SquareEuclidean distanceCalc)
//        {
//            if (data.Length == 0) return 0.0;

//            double[][] sampleData = data;
//            int[] sampleLabels = labels;
//            if (data.Length > SilhouetteSampleSize)
//            {
//                int[] indices = Vector.Range(data.Length).Shuffled().Take(SilhouetteSampleSize).ToArray();
//                sampleData = new double[indices.Length][];
//                sampleLabels = new int[indices.Length];
//                for (int i = 0; i < indices.Length; i++)
//                {
//                    sampleData[i] = data[indices[i]];
//                    sampleLabels[i] = labels[indices[i]];
//                }
//            }

//            return ComputeParallelSilhouette(sampleData, sampleLabels, distanceCalc);
//        }

//        /// <summary>
//        /// Parallelized silhouette: average over all points of (b−a)/max(a,b).
//        /// </summary>
//        private double ComputeParallelSilhouette(
//            double[][] data,
//            int[] labels,
//            SquareEuclidean distanceCalc)
//        {
//            var clusterMap = BuildClusterMap(labels);
//            object lockObj = new object();
//            double totalScore = 0.0;

//            Parallel.For(
//                0,
//                data.Length,
//                () => 0.0,
//                (i, state, partial) =>
//                {
//                    int cluster = labels[i];
//                    double a = ComputeIntraClusterDistance(data, i, cluster, clusterMap, distanceCalc);
//                    double b = ComputeNearestClusterDistance(data, i, cluster, clusterMap, distanceCalc);

//                    double denom = Math.Max(a, b);
//                    if (denom < double.Epsilon) return partial;

//                    double s = (b - a) / denom;
//                    return partial + s;
//                },
//                partial =>
//                {
//                    lock (lockObj)
//                    {
//                        totalScore += partial;
//                    }
//                });

//            return totalScore / data.Length;
//        }

//        /// <summary>
//        /// Maps each cluster label to a list of indices belonging to that cluster.
//        /// </summary>
//        private Dictionary<int, List<int>> BuildClusterMap(int[] labels)
//        {
//            var map = new Dictionary<int, List<int>>();
//            for (int i = 0; i < labels.Length; i++)
//            {
//                int lbl = labels[i];
//                if (!map.ContainsKey(lbl))
//                    map[lbl] = new List<int>();
//                map[lbl].Add(i);
//            }
//            return map;
//        }

//        /// <summary>
//        /// Average distance from point index to all other points in the same cluster.
//        /// </summary>
//        private double ComputeIntraClusterDistance(
//            double[][] data,
//            int index,
//            int cluster,
//            Dictionary<int, List<int>> clusterMap,
//            SquareEuclidean distanceCalc)
//        {
//            List<int> members = clusterMap[cluster];
//            if (members.Count <= 1) return 0.0;

//            double sum = 0.0;
//            int count = 0;
//            foreach (int j in members)
//            {
//                if (j == index) continue;
//                sum += distanceCalc.Distance(data[index], data[j]);
//                count++;
//            }
//            return count > 0 ? (sum / count) : 0.0;
//        }

//        /// <summary>
//        /// Lowest average distance from point index to any other cluster’s members.
//        /// </summary>
//        private double ComputeNearestClusterDistance(
//            double[][] data,
//            int index,
//            int currentCluster,
//            Dictionary<int, List<int>> clusterMap,
//            SquareEuclidean distanceCalc)
//        {
//            double minAvg = double.MaxValue;
//            foreach (var kvp in clusterMap)
//            {
//                int other = kvp.Key;
//                if (other == currentCluster) continue;

//                List<int> members = kvp.Value;
//                if (members.Count == 0) continue;

//                double sum = 0.0;
//                foreach (int j in members)
//                    sum += distanceCalc.Distance(data[index], data[j]);

//                double avg = sum / members.Count;
//                if (avg < minAvg) minAvg = avg;
//            }
//            return minAvg < double.MaxValue ? minAvg : 0.0;
//        }

//        /// <summary>
//        /// Returns the diagnostics entry with the highest silhouette score.
//        /// </summary>
//        private static ClusteringDiagnostics MaxBySilhouette(List<ClusteringDiagnostics> list)
//        {
//            if (list == null || list.Count == 0) return null;

//            ClusteringDiagnostics best = null;
//            double bestScore = double.NegativeInfinity;
//            foreach (var d in list)
//            {
//                if (d.SilhouetteScore > bestScore)
//                {
//                    bestScore = d.SilhouetteScore;
//                    best = d;
//                }
//            }
//            return best;
//        }

//        /// <summary>
//        /// Builds a [customers × products] matrix where entry (i,j) = sum of sales amount.
//        /// </summary>
//        private Tuple<double[][], Dictionary<string, int>, Dictionary<int, int>> BuildPurchaseMatrix(
//            List<SalesDetailDTO> sales,
//            List<string> customers)
//        {
//            List<int> products = _productDao
//                .Select()
//                .Select(p => p.ProductID)
//                .Distinct()
//                .ToList();

//            int rowCount = customers.Count;
//            int colCount = products.Count;

//            double[][] matrix = new double[rowCount][];
//            for (int i = 0; i < rowCount; i++)
//                matrix[i] = new double[colCount];

//            var custIndex = new Dictionary<string, int>();
//            for (int i = 0; i < customers.Count; i++)
//                custIndex[customers[i]] = i;

//            var prodIndex = new Dictionary<int, int>();
//            for (int i = 0; i < products.Count; i++)
//                prodIndex[products[i]] = i;

//            foreach (var s in sales)
//            {
//                int r, c;
//                if (custIndex.TryGetValue(s.CustomerName, out r)
//                    && prodIndex.TryGetValue(s.ProductID, out c))
//                {
//                    matrix[r][c] += s.SalesAmount;
//                }
//            }

//            return Tuple.Create(matrix, custIndex, prodIndex);
//        }

//        /// <summary>
//        /// Recommends products by scoring cluster‐neighbor purchases and falling back to top sellers.
//        /// </summary>
//        private List<ProductDetailDTO> GenerateRecommendations(
//            double[][] matrix,
//            int[] labels,
//            int targetRow,
//            Dictionary<int, int> productIndex,
//            List<SalesDetailDTO> allSales)
//        {
//            int targetCluster = labels[targetRow];
//            var productScores = new Dictionary<int, double>();

//            var allProductsMap = _productDao
//                .Select()
//                .ToDictionary(p => p.ProductID);

//            for (int r = 0; r < matrix.Length; r++)
//            {
//                if (r == targetRow || labels[r] != targetCluster) continue;

//                for (int c = 0; c < matrix[r].Length; c++)
//                {
//                    double amt = matrix[r][c];
//                    if (amt <= 0.0) continue;

//                    int pid = productIndex.First(kvp => kvp.Value == c).Key;
//                    double existing = productScores.ContainsKey(pid) ? productScores[pid] : 0.0;
//                    productScores[pid] = existing + amt;
//                }
//            }

//            var purchasedByTarget = new HashSet<int>();
//            for (int c = 0; c < matrix[targetRow].Length; c++)
//            {
//                if (matrix[targetRow][c] > 0.0)
//                {
//                    int pid = productIndex.First(kvp => kvp.Value == c).Key;
//                    purchasedByTarget.Add(pid);
//                }
//            }

//            var recommendations = productScores
//                .Where(kvp => !purchasedByTarget.Contains(kvp.Key))
//                .OrderByDescending(kvp => kvp.Value)
//                .Take(RecommendationCount)
//                .Select(kvp => allProductsMap[kvp.Key])
//                .ToList();

//            if (recommendations.Count < RecommendationCount)
//            {
//                int needed = RecommendationCount - recommendations.Count;
//                var topSellers = GetTopSellingProducts(needed, allSales)
//                    .Where(p => !purchasedByTarget.Contains(p.ProductID))
//                    .Take(needed);
//                recommendations.AddRange(topSellers);
//            }

//            return recommendations;
//        }

//        /// <summary>
//        /// Returns the top N selling products across all sales.
//        /// </summary>
//        private List<ProductDetailDTO> GetTopSellingProducts(
//            int count,
//            IEnumerable<SalesDetailDTO> sales)
//        {
//            var topGroups = sales
//                .GroupBy(s => s.ProductID)
//                .Select(g => new
//                {
//                    ProductID = g.Key,
//                    TotalSales = g.Sum(s => s.SalesAmount)
//                })
//                .OrderByDescending(x => x.TotalSales)
//                .Take(count)
//                .ToList();

//            var products = _productDao.Select();
//            var result = (from t in topGroups
//                          join p in products on t.ProductID equals p.ProductID
//                          select p)
//                         .ToList();

//            return result;
//        }

//        /// <summary>
//        /// Ensures the data matrix is non‐null and has no null rows.
//        /// </summary>
//        private void ValidateDataMatrix(double[][] data)
//        {
//            if (data == null)
//                throw new ArgumentNullException(nameof(data), "Data matrix cannot be null.");

//            for (int i = 0; i < data.Length; i++)
//            {
//                if (data[i] == null)
//                    throw new ArgumentException("Data matrix contains a null row at index " + i);
//            }
//        }

//        #endregion

//        /// <inheritdoc />
//        public void Dispose()
//        {
//            if (_disposed) return;
//            if (_salesDao is IDisposable sd) sd.Dispose();
//            if (_productDao is IDisposable pd) pd.Dispose();
//            _disposed = true;
//            GC.SuppressFinalize(this);
//        }
//    }
//}
