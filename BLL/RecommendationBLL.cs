using System;
using System.Collections.Generic;
using System.Linq;
using Accord.MachineLearning;
using Accord.Math.Distances;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using Microsoft.Extensions.Logging;

namespace Erp_V1.BLL
{
    public sealed class RecommendationBLL : IRecommendationEngine, IDisposable
    {
        private const int MinDataPoints = 10;
        private const int MaxClusters = 15;

        private readonly SalesDAO _salesDao;
        private readonly ProductDAO _productDao;
        private readonly ILogger<RecommendationBLL> _logger;
        private bool _disposed;

        public RecommendationBLL(
            SalesDAO salesDao,
            ProductDAO productDao,
            ILogger<RecommendationBLL> logger)
        {
            _salesDao = salesDao ?? throw new ArgumentNullException(nameof(salesDao));
            _productDao = productDao ?? throw new ArgumentNullException(nameof(productDao));
            _logger = logger;
        }

        public List<ProductDetailDTO> AvailableProducts => _productDao.Select();

        public int DetermineOptimalK(double[][] data)
        {
            if (data == null || data.Length < MinDataPoints)
                return 1;

            // If all category IDs identical, no meaningful clustering
            var distinctCats = data.Select(r => r[1]).Distinct().Count();
            if (distinctCats <= 1)
                return 1;

            int upperK = Math.Min(MaxClusters, data.Length - 1);
            var diagnostics = EvaluateClusters(data, upperK);
            if (diagnostics.Count == 0)
                return 1;

            return diagnostics.OrderByDescending(d => d.SilhouetteScore).First().K;
        }

        public List<ClusteringDiagnostics> EvaluateClusters(double[][] data, int maxK)
        {
            var results = new List<ClusteringDiagnostics>();
            if (data == null || data.Length < 2)
                return results;

            int upperK = Math.Min(maxK, data.Length - 1);
            var distCalc = new SquareEuclidean();

            for (int k = 2; k <= upperK; k++)
            {
                try
                {
                    var kmeans = new KMeans(k)
                    {
                        Distance = distCalc,
                        Tolerance = 0.01,
                        MaxIterations = 500,
                        ParallelOptions = { MaxDegreeOfParallelism = Environment.ProcessorCount }
                    };
                    var model = kmeans.Learn(data);
                    var labels = data.Select(r => model.Decide(r)).ToArray();
                    if (labels.Distinct().Count() < 2)
                        continue;

                    double score = ComputeSilhouette(data, labels, distCalc);
                    results.Add(new ClusteringDiagnostics(k, score));
                }
                catch
                {
                    // ignore and continue
                }
            }

            return results.OrderBy(d => d.K).ToList();
        }

        public List<ProductDetailDTO> GetRecommendationsForCustomer(string customerName)
        {
            if (string.IsNullOrWhiteSpace(customerName))
                return new List<ProductDetailDTO>();

            var allSales = _salesDao.Select() ?? new List<SalesDetailDTO>();
            var uniqueCustomers = allSales.Select(s => s.CustomerName).Distinct().ToList();

            // Build purchased set for this customer
            var purchasedSet = new HashSet<int>(
                allSales
                    .Where(s => s.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase))
                    .Select(s => s.ProductID)
            );

            // If no purchase history at all, return empty
            if (purchasedSet.Count == 0)
                return new List<ProductDetailDTO>();

            // If insufficient distinct customers, return _all_ non-purchased products
            if (uniqueCustomers.Count < MinDataPoints)
            {
                return AvailableProducts
                    .Where(p => !purchasedSet.Contains(p.ProductID))
                    .ToList();
            }

            // Build purchase matrix [customers x products]
            var (matrix, custIndex, prodIndex) = BuildMatrix(allSales, uniqueCustomers);
            int targetRow = custIndex[customerName];

            int optimalK = DetermineOptimalK(matrix);
            // If clustering not meaningful, return all non-purchased
            if (optimalK <= 1)
            {
                return AvailableProducts
                    .Where(p => !purchasedSet.Contains(p.ProductID))
                    .ToList();
            }

            var (model, labels) = TrainKMeans(matrix, optimalK);
            var recs = GenerateRecommendations(matrix, labels, targetRow, prodIndex, purchasedSet);

            return recs;
        }

        private (double[][] matrix, Dictionary<string, int> custIndex, Dictionary<int, int> prodIndex)
            BuildMatrix(List<SalesDetailDTO> sales, List<string> customers)
        {
            var products = AvailableProducts.Select(p => p.ProductID).Distinct().ToList();
            int rows = customers.Count, cols = products.Count;

            var matrix = new double[rows][];
            for (int i = 0; i < rows; i++)
                matrix[i] = new double[cols];

            var custIndex = customers.Select((c, i) => (c, i))
                            .ToDictionary(x => x.c, x => x.i);
            var prodIndex = products.Select((p, i) => (p, i))
                            .ToDictionary(x => x.p, x => x.i);

            foreach (var s in sales)
            {
                if (!custIndex.TryGetValue(s.CustomerName, out int r)) continue;
                if (!prodIndex.TryGetValue(s.ProductID, out int c)) continue;
                matrix[r][c] += s.SalesAmount;
            }

            return (matrix, custIndex, prodIndex);
        }

        private (KMeansClusterCollection model, int[] labels) TrainKMeans(double[][] data, int k)
        {
            var distCalc = new SquareEuclidean();
            var kmeans = new KMeans(k)
            {
                Distance = distCalc,
                Tolerance = 0.01,
                MaxIterations = 500,
                ParallelOptions = { MaxDegreeOfParallelism = Environment.ProcessorCount }
            };
            var model = kmeans.Learn(data);
            var labels = data.Select(r => model.Decide(r)).ToArray();
            return (model, labels);
        }

        private List<ProductDetailDTO> GenerateRecommendations(
            double[][] matrix,
            int[] labels,
            int targetRow,
            Dictionary<int, int> prodIndex,
            HashSet<int> purchasedSet)
        {
            int targetCluster = labels[targetRow];
            var scores = new Dictionary<int, double>();
            var productMap = AvailableProducts.ToDictionary(p => p.ProductID);

            for (int r = 0; r < matrix.Length; r++)
            {
                if (r == targetRow || labels[r] != targetCluster)
                    continue;

                for (int c = 0; c < matrix[r].Length; c++)
                {
                    double amt = matrix[r][c];
                    if (amt <= 0) continue;
                    int pid = prodIndex.First(x => x.Value == c).Key;
                    if (purchasedSet.Contains(pid)) continue;

                    if (!scores.ContainsKey(pid))
                        scores[pid] = 0;
                    scores[pid] += amt;
                }
            }

            var recommendations = scores
                .OrderByDescending(kvp => kvp.Value)
                .Select(kvp => productMap[kvp.Key])
                .ToList();

            return recommendations;
        }

        private double ComputeSilhouette(
            double[][] data,
            int[] labels,
            SquareEuclidean distanceCalc)
        {
            int n = data.Length;
            double total = 0;
            var clusters = new Dictionary<int, List<int>>();
            for (int i = 0; i < n; i++)
            {
                int lbl = labels[i];
                if (!clusters.ContainsKey(lbl))
                    clusters[lbl] = new List<int>();
                clusters[lbl].Add(i);
            }

            for (int i = 0; i < n; i++)
            {
                int lbl = labels[i];
                var own = clusters[lbl].Where(j => j != i).ToList();
                double a = own.Count > 0
                    ? own.Select(j => distanceCalc.Distance(data[i], data[j])).Average()
                    : 0.0;

                double b = clusters
                    .Where(kvp => kvp.Key != lbl)
                    .Select(kvp => kvp.Value.Select(j => distanceCalc.Distance(data[i], data[j])).DefaultIfEmpty(0.0).Average())
                    .DefaultIfEmpty(0.0)
                    .Min();

                total += (Math.Max(a, b) > 0) ? (b - a) / Math.Max(a, b) : 0;
            }

            return total / n;
        }

        public void Dispose()
        {
            if (_disposed) return;
            if (_salesDao is IDisposable s) s.Dispose();
            if (_productDao is IDisposable p) p.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
