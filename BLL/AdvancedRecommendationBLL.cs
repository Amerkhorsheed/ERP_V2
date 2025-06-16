//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using Accord.MachineLearning;
//using Accord.Math.Distances;
//using Accord.Statistics;
//using Erp_V1.DAL.DTO;
//using Erp_V1.DAL.DAO;
//using Microsoft.Extensions.Caching.Memory;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;

//namespace Erp_V1.BLL
//{
//    /// <summary>
//    /// Configuration options for the recommendation engine.
//    /// </summary>
//    public class RecommendationOptions
//    {
//        public int MaxRecommendations { get; set; } = 10;
//        public double ClusterTolerance { get; set; } = 0.01;
//        public int MaxClusterIterations { get; set; } = 500;
//        public int MinDataPointsForClustering { get; set; } = 10;
//        public double SimilarityThreshold { get; set; } = 0.7;
//        public TimeSpan CacheDuration { get; set; } = TimeSpan.FromMinutes(15);
//        public bool EnableHybridFiltering { get; set; } = true;
//        public double ContentBasedWeight { get; set; } = 0.4;
//        public double CollaborativeWeight { get; set; } = 0.6;
//    }

//    /// <summary>
//    /// Represents a customer purchase pattern for analysis.
//    /// </summary>
//    public class CustomerProfile
//    {
//        public string CustomerName { get; set; }
//        public List<int> PurchasedProductIds { get; set; } = new List<int>();
//        public List<int> PreferredCategories { get; set; } = new List<int>();
//        public double AverageSpending { get; set; }
//        public double SpendingVariance { get; set; }
//        public DateTime LastPurchaseDate { get; set; }
//        public int TotalPurchases { get; set; }
//        public double[] FeatureVector { get; set; }
//    }

//    /// <summary>
//    /// Represents a product recommendation with confidence score.
//    /// </summary>
//    public class ProductRecommendation
//    {
//        public ProductDetailDTO Product { get; set; }
//        public double ConfidenceScore { get; set; }
//        public string RecommendationReason { get; set; }
//        public RecommendationType Type { get; set; }
//    }

//    /// <summary>
//    /// Types of recommendation algorithms used.
//    /// </summary>
//    public enum RecommendationType
//    {
//        ContentBased,
//        CollaborativeFiltering,
//        Hybrid,
//        PopularityBased,
//        CategoryBased
//    }

//    /// <summary>
//    /// Advanced product recommendation engine using multiple ML algorithms.
//    /// Implements collaborative filtering, content-based filtering, and hybrid approaches.
//    /// </summary>
//    public class AdvancedRecommendationBLL
//    {
//        private readonly SalesDAO _salesDao;
//        private readonly ProductDAO _productDao;
//        private readonly ILogger<AdvancedRecommendationBLL> _logger;
//        private readonly RecommendationOptions _options;

//        public AdvancedRecommendationBLL(
//            SalesDAO salesDao,
//            ProductDAO productDao,
//            ILogger<AdvancedRecommendationBLL> logger,
//            IOptions<RecommendationOptions> options)
//        {
//            _salesDao = salesDao ?? throw new ArgumentNullException(nameof(salesDao));
//            _productDao = productDao ?? throw new ArgumentNullException(nameof(productDao));
//            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
//        }

//        /// <summary>
//        /// Generates personalized product recommendations using hybrid filtering approach.
//        /// </summary>
//        /// <param name="customerName">Target customer name</param>
//        /// <returns>List of recommended products with confidence scores</returns>
//        public async Task<IEnumerable<ProductRecommendation>> GetPersonalizedRecommendationsAsync(string customerName)
//        {
//            if (string.IsNullOrWhiteSpace(customerName))
//                throw new ArgumentException("Customer name cannot be empty.", nameof(customerName));

//            _logger.LogInformation("Generating personalized recommendations for customer: {CustomerName}", customerName);

//            try
//            {
//                var allSales = await Task.Run(() => _salesDao.Select().Where(s => !s.isProductDeleted && !s.isCustomerDeleted).ToList());
//                var allProducts = await Task.Run(() => _productDao.Select().Where(p => !p.isCategoryDeleted && p.stockAmount > 0).ToList());

//                var customerProfile = BuildCustomerProfile(customerName, allSales);
//                if (customerProfile.TotalPurchases == 0)
//                {
//                    _logger.LogInformation("New customer detected. Returning popularity-based recommendations.");
//                    return await GetPopularityBasedRecommendations(allSales, allProducts);
//                }

//                var recommendations = new List<ProductRecommendation>();

//                if (_options.EnableHybridFiltering)
//                {
//                    // Hybrid approach combining multiple algorithms
//                    var collaborativeRecs = await GetCollaborativeFilteringRecommendations(customerProfile, allSales, allProducts);
//                    var contentBasedRecs = await GetContentBasedRecommendations(customerProfile, allProducts);

//                    recommendations.AddRange(
//                        collaborativeRecs.Select(r => new ProductRecommendation
//                        {
//                            Product = r.Product,
//                            ConfidenceScore = r.ConfidenceScore * _options.CollaborativeWeight,
//                            RecommendationReason = $"Similar customers also purchased this item. {r.RecommendationReason}",
//                            Type = RecommendationType.Hybrid
//                        }));

//                    recommendations.AddRange(
//                        contentBasedRecs.Select(r => new ProductRecommendation
//                        {
//                            Product = r.Product,
//                            ConfidenceScore = r.ConfidenceScore * _options.ContentBasedWeight,
//                            RecommendationReason = $"Based on your purchase history. {r.RecommendationReason}",
//                            Type = RecommendationType.Hybrid
//                        }));
//                }
//                else
//                {
//                    recommendations.AddRange(await GetCollaborativeFilteringRecommendations(customerProfile, allSales, allProducts));
//                }

//                // Merge, deduplicate, and rank recommendations
//                var finalRecommendations = MergeAndRankRecommendations(recommendations, customerProfile);

//                _logger.LogInformation("Generated {Count} recommendations for customer: {CustomerName}",
//                    finalRecommendations.Count(), customerName);

//                return finalRecommendations.Take(_options.MaxRecommendations);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error generating recommendations for customer: {CustomerName}", customerName);
//                throw new InvalidOperationException($"Failed to generate recommendations for customer {customerName}", ex);
//            }
//        }

//        /// <summary>
//        /// Builds a comprehensive customer profile for personalization.
//        /// </summary>
//        private CustomerProfile BuildCustomerProfile(string customerName, List<SalesDetailDTO> allSales)
//        {
//            var customerSales = allSales
//                .Where(s => s.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase))
//                .ToList();

//            if (!customerSales.Any())
//            {
//                return new CustomerProfile
//                {
//                    CustomerName = customerName,
//                    FeatureVector = new double[0]
//                };
//            }

//            var purchasedProducts = customerSales.Select(s => s.ProductID).Distinct().ToList();
//            var preferredCategories = customerSales
//                .GroupBy(s => s.CategoryID)
//                .OrderByDescending(g => g.Count())
//                .Select(g => g.Key)
//                .ToList();

//            var prices = customerSales.Select(s => (double)s.Price).ToArray();
//            var avgSpending = prices.Average();
//            var spendingVariance = prices.Variance();

//            // Create feature vector for similarity calculations
//            var categoryFrequency = customerSales
//                .GroupBy(s => s.CategoryID)
//                .ToDictionary(g => g.Key, g => (double)g.Count() / customerSales.Count);

//            var featureVector = new List<double>
//            {
//                avgSpending,
//                spendingVariance,
//                customerSales.Count,
//                preferredCategories.Count,
//                (DateTime.Now - customerSales.Max(s => s.SalesDate)).TotalDays
//            };

//            // Add category preferences to feature vector
//            var allCategories = allSales.Select(s => s.CategoryID).Distinct().OrderBy(x => x).ToList();
//            foreach (var categoryId in allCategories)
//            {
//                featureVector.Add(categoryFrequency.GetValueOrDefault(categoryId, 0.0));
//            }

//            return new CustomerProfile
//            {
//                CustomerName = customerName,
//                PurchasedProductIds = purchasedProducts,
//                PreferredCategories = preferredCategories,
//                AverageSpending = avgSpending,
//                SpendingVariance = spendingVariance,
//                LastPurchaseDate = customerSales.Max(s => s.SalesDate),
//                TotalPurchases = customerSales.Count,
//                FeatureVector = featureVector.ToArray()
//            };
//        }

//        /// <summary>
//        /// Implements collaborative filtering using advanced clustering and similarity measures.
//        /// </summary>
//        private async Task<IEnumerable<ProductRecommendation>> GetCollaborativeFilteringRecommendations(
//            CustomerProfile targetCustomer, List<SalesDetailDTO> allSales, List<ProductDetailDTO> allProducts)
//        {
//            var customerProfiles = await Task.Run(() =>
//                allSales.GroupBy(s => s.CustomerName)
//                       .Where(g => g.Key != targetCustomer.CustomerName)
//                       .Select(g => BuildCustomerProfile(g.Key, allSales))
//                       .Where(p => p.TotalPurchases > 0)
//                       .ToList());

//            if (customerProfiles.Count < 2)
//            {
//                return await GetContentBasedRecommendations(targetCustomer, allProducts);
//            }

//            // Find similar customers using cosine similarity
//            var similarCustomers = FindSimilarCustomers(targetCustomer, customerProfiles);

//            if (!similarCustomers.Any())
//            {
//                return await GetPopularityBasedRecommendations(allSales, allProducts);
//            }

//            // Generate recommendations based on similar customers' purchases
//            var recommendationScores = new Dictionary<int, double>();
//            var recommendationReasons = new Dictionary<int, string>();

//            foreach (var (customer, similarity) in similarCustomers)
//            {
//                var theirPurchases = customer.PurchasedProductIds
//                    .Except(targetCustomer.PurchasedProductIds)
//                    .ToList();

//                foreach (var productId in theirPurchases)
//                {
//                    if (recommendationScores.ContainsKey(productId))
//                    {
//                        recommendationScores[productId] += similarity;
//                    }
//                    else
//                    {
//                        recommendationScores[productId] = similarity;
//                        recommendationReasons[productId] = $"Recommended by {similarCustomers.Count()} similar customers";
//                    }
//                }
//            }

//            var recommendations = recommendationScores
//                .Join(allProducts,
//                      kvp => kvp.Key,
//                      p => p.ProductID,
//                      (kvp, product) => new ProductRecommendation
//                      {
//                          Product = product,
//                          ConfidenceScore = Math.Min(kvp.Value, 1.0),
//                          RecommendationReason = recommendationReasons[kvp.Key],
//                          Type = RecommendationType.CollaborativeFiltering
//                      })
//                .Where(r => r.ConfidenceScore >= _options.SimilarityThreshold)
//                .OrderByDescending(r => r.ConfidenceScore);

//            return recommendations;
//        }

//        /// <summary>
//        /// Implements content-based filtering using product features and customer preferences.
//        /// </summary>
//        private async Task<IEnumerable<ProductRecommendation>> GetContentBasedRecommendations(
//            CustomerProfile customerProfile, List<ProductDetailDTO> allProducts)
//        {
//            if (!customerProfile.PreferredCategories.Any())
//            {
//                return Enumerable.Empty<ProductRecommendation>();
//            }

//            var availableProducts = allProducts
//                .Where(p => !customerProfile.PurchasedProductIds.Contains(p.ProductID))
//                .ToList();

//            var recommendations = new List<ProductRecommendation>();

//            foreach (var product in availableProducts)
//            {
//                var score = CalculateContentBasedScore(product, customerProfile);
//                if (score > 0.3) // Minimum threshold
//                {
//                    recommendations.Add(new ProductRecommendation
//                    {
//                        Product = product,
//                        ConfidenceScore = score,
//                        RecommendationReason = GetContentBasedReason(product, customerProfile),
//                        Type = RecommendationType.ContentBased
//                    });
//                }
//            }

//            return recommendations.OrderByDescending(r => r.ConfidenceScore);
//        }

//        /// <summary>
//        /// Provides fallback recommendations based on overall popularity.
//        /// </summary>
//        private async Task<IEnumerable<ProductRecommendation>> GetPopularityBasedRecommendations(
//            List<SalesDetailDTO> allSales, List<ProductDetailDTO> allProducts)
//        {
//            var productPopularity = allSales
//                .GroupBy(s => s.ProductID)
//                .ToDictionary(g => g.Key, g => new { Count = g.Count(), AvgRating = g.Average(s => s.Price) });

//            var recommendations = allProducts
//                .Where(p => productPopularity.ContainsKey(p.ProductID))
//                .Select(p => new ProductRecommendation
//                {
//                    Product = p,
//                    ConfidenceScore = Math.Min(productPopularity[p.ProductID].Count / 100.0, 1.0),
//                    RecommendationReason = $"Popular item - purchased by {productPopularity[p.ProductID].Count} customers",
//                    Type = RecommendationType.PopularityBased
//                })
//                .OrderByDescending(r => r.ConfidenceScore)
//                .Take(_options.MaxRecommendations);

//            return recommendations;
//        }

//        /// <summary>
//        /// Finds customers similar to the target customer using advanced similarity measures.
//        /// </summary>
//        private IEnumerable<(CustomerProfile Customer, double Similarity)> FindSimilarCustomers(
//            CustomerProfile targetCustomer, List<CustomerProfile> allCustomers)
//        {
//            if (targetCustomer.FeatureVector.Length == 0)
//                return Enumerable.Empty<(CustomerProfile, double)>();

//            var similarities = new List<(CustomerProfile Customer, double Similarity)>();
//            var cosine = new Cosine();

//            foreach (var otherCustomer in allCustomers)
//            {
//                if (otherCustomer.FeatureVector.Length != targetCustomer.FeatureVector.Length)
//                    continue;

//                try
//                {
//                    var similarity = 1.0 - cosine.Distance(targetCustomer.FeatureVector, otherCustomer.FeatureVector);

//                    // Add Jaccard similarity for purchased products
//                    var intersection = targetCustomer.PurchasedProductIds.Intersect(otherCustomer.PurchasedProductIds).Count();
//                    var union = targetCustomer.PurchasedProductIds.Union(otherCustomer.PurchasedProductIds).Count();
//                    var jaccardSimilarity = union > 0 ? (double)intersection / union : 0.0;

//                    // Combine similarities
//                    var combinedSimilarity = (similarity * 0.7) + (jaccardSimilarity * 0.3);

//                    if (combinedSimilarity >= _options.SimilarityThreshold)
//                    {
//                        similarities.Add((otherCustomer, combinedSimilarity));
//                    }
//                }
//                catch (Exception ex)
//                {
//                    _logger.LogWarning(ex, "Error calculating similarity for customer: {CustomerName}", otherCustomer.CustomerName);
//                }
//            }

//            return similarities.OrderByDescending(s => s.Similarity).Take(10);
//        }

//        /// <summary>
//        /// Calculates content-based recommendation score for a product.
//        /// </summary>
//        private double CalculateContentBasedScore(ProductDetailDTO product, CustomerProfile customerProfile)
//        {
//            double score = 0.0;

//            // Category preference score
//            if (customerProfile.PreferredCategories.Contains(product.CategoryID))
//            {
//                var categoryRank = customerProfile.PreferredCategories.IndexOf(product.CategoryID);
//                score += (customerProfile.PreferredCategories.Count - categoryRank) / (double)customerProfile.PreferredCategories.Count * 0.6;
//            }

//            // Price preference score
//            var priceDifference = Math.Abs(product.price - customerProfile.AverageSpending);
//            var priceScore = Math.Max(0, 1.0 - (priceDifference / customerProfile.AverageSpending));
//            score += priceScore * 0.4;

//            return Math.Min(score, 1.0);
//        }

//        /// <summary>
//        /// Generates explanation for content-based recommendations.
//        /// </summary>
//        private string GetContentBasedReason(ProductDetailDTO product, CustomerProfile customerProfile)
//        {
//            var reasons = new List<string>();

//            if (customerProfile.PreferredCategories.Contains(product.CategoryID))
//            {
//                reasons.Add($"matches your preferred {product.CategoryName} category");
//            }

//            var priceDifference = Math.Abs(product.price - customerProfile.AverageSpending);
//            if (priceDifference / customerProfile.AverageSpending <= 0.2)
//            {
//                reasons.Add("fits your typical spending range");
//            }

//            return reasons.Any() ? string.Join(" and ", reasons) : "similar to your previous purchases";
//        }

//        /// <summary>
//        /// Merges and ranks recommendations from different algorithms.
//        /// </summary>
//        private IEnumerable<ProductRecommendation> MergeAndRankRecommendations(
//            List<ProductRecommendation> recommendations, CustomerProfile customerProfile)
//        {
//            return recommendations
//                .GroupBy(r => r.Product.ProductID)
//                .Select(g => new ProductRecommendation
//                {
//                    Product = g.First().Product,
//                    ConfidenceScore = g.Sum(r => r.ConfidenceScore) / g.Count(),
//                    RecommendationReason = string.Join("; ", g.Select(r => r.RecommendationReason).Distinct()),
//                    Type = g.Count() > 1 ? RecommendationType.Hybrid : g.First().Type
//                })
//                .Where(r => r.Product.stockAmount > 0)
//                .OrderByDescending(r => r.ConfidenceScore)
//                .ThenByDescending(r => r.Product.Sale_Price > 0 ? r.Product.Sale_Price : r.Product.price);
//        }

//        /// <summary>
//        /// Determines optimal number of clusters using the elbow method with improved implementation.
//        /// </summary>
//        public int DetermineOptimalClusters(double[][] data)
//        {
//            if (data.Length < _options.MinDataPointsForClustering)
//                return 1;

//            const int maxClusters = 15;
//            int maxK = Math.Min(data.Length / 2, maxClusters);
//            var wcss = new double[maxK];

//            for (int k = 1; k <= maxK; k++)
//            {
//                try
//                {
//                    var kmeans = new KMeans(k, new SquareEuclidean())
//                    {
//                        Tolerance = _options.ClusterTolerance,
//                        MaxIterations = _options.MaxClusterIterations,
//                        UseSeeding = Seeding.KMeansPlusPlus
//                    };

//                    var clusters = kmeans.Learn(data);
//                    wcss[k - 1] = CalculateWCSS(data, clusters);
//                }
//                catch (Exception ex)
//                {
//                    _logger.LogWarning(ex, "Error in clustering with k={K}", k);
//                    wcss[k - 1] = double.MaxValue;
//                }
//            }

//            // Find elbow using rate of change
//            for (int i = 2; i < maxK - 1; i++)
//            {
//                var slope1 = wcss[i - 2] - wcss[i - 1];
//                var slope2 = wcss[i - 1] - wcss[i];

//                if (slope1 > 0 && slope2 > 0 && slope1 / slope2 > 2.0)
//                {
//                    return i;
//                }
//            }

//            return Math.Min(3, maxK);
//        }

//        /// <summary>
//        /// Calculates Within-Cluster Sum of Squares for elbow method.
//        /// </summary>
//        private double CalculateWCSS(double[][] data, KMeans clusters)
//        {
//            double wcss = 0.0;
//            var distance = new SquareEuclidean();

//            for (int i = 0; i < data.Length; i++)
//            {
//                var clusterIndex = clusters.Decide(data[i]);
//                var centroid = clusters.Centroids[clusterIndex];
//                wcss += distance.Distance(data[i], centroid);
//            }

//            return wcss;
//        }

//        /// <summary>
//        /// Gets available products (legacy compatibility).
//        /// </summary>
//        public List<ProductDetailDTO> AvailableProducts => _productDao.Select();
//    }

//    /// <summary>
//    /// Enhanced service layer with advanced caching and monitoring.
//    /// </summary>
//    public class EnhancedRecommendationService
//    {
//        private readonly AdvancedRecommendationBLL _bll;
//        private readonly ILogger<EnhancedRecommendationService> _logger;
//        private readonly IMemoryCache _cache;
//        private readonly RecommendationOptions _options;

//        public EnhancedRecommendationService(
//            AdvancedRecommendationBLL bll,
//            ILogger<EnhancedRecommendationService> logger,
//            IMemoryCache cache,
//            IOptions<RecommendationOptions> options)
//        {
//            _bll = bll ?? throw new ArgumentNullException(nameof(bll));
//            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
//            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
//        }

//        /// <summary>
//        /// Gets personalized recommendations with advanced caching strategy.
//        /// </summary>
//        public async Task<IEnumerable<ProductRecommendation>> GetRecommendationsAsync(
//            string customerName, CancellationToken cancellationToken = default)
//        {
//            if (string.IsNullOrWhiteSpace(customerName))
//                throw new ArgumentException("Customer name is required.", nameof(customerName));

//            var cacheKey = $"advanced_recs_{customerName.ToLowerInvariant()}";

//            if (_cache.TryGetValue(cacheKey, out IEnumerable<ProductRecommendation> cachedResult))
//            {
//                _logger.LogInformation("Cache hit for customer: {CustomerName}", customerName);
//                return cachedResult;
//            }

//            _logger.LogInformation("Generating advanced recommendations for: {CustomerName}", customerName);

//            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
//            var recommendations = await _bll.GetPersonalizedRecommendationsAsync(customerName);
//            stopwatch.Stop();

//            _logger.LogInformation("Generated {Count} recommendations in {ElapsedMs}ms for: {CustomerName}",
//                recommendations.Count(), stopwatch.ElapsedMilliseconds, customerName);

//            var cacheOptions = new MemoryCacheEntryOptions
//            {
//                AbsoluteExpirationRelativeToNow = _options.CacheDuration,
//                SlidingExpiration = TimeSpan.FromMinutes(5),
//                Priority = CacheItemPriority.Normal
//            };

//            _cache.Set(cacheKey, recommendations, cacheOptions);
//            return recommendations;
//        }

//        /// <summary>
//        /// Gets available products.
//        /// </summary>
//        public List<ProductDetailDTO> AvailableProducts => _bll.AvailableProducts;

//        /// <summary>
//        /// Clears recommendation cache for a specific customer.
//        /// </summary>
//        public void ClearCustomerCache(string customerName)
//        {
//            if (!string.IsNullOrWhiteSpace(customerName))
//            {
//                var cacheKey = $"advanced_recs_{customerName.ToLowerInvariant()}";
//                _cache.Remove(cacheKey);
//                _logger.LogInformation("Cleared cache for customer: {CustomerName}", customerName);
//            }
//        }
//    }

//    /// <summary>
//    /// Enhanced service collection extensions.
//    /// </summary>
//    public static class EnhancedServiceCollectionExtensions
//    {
//        /// <summary>
//        /// Registers advanced recommendation services with configuration.
//        /// </summary>
//        public static IServiceCollection AddAdvancedRecommendationServices(
//            this IServiceCollection services,
//            Action<RecommendationOptions> configureOptions = null)
//        {
//            // Configure options
//            if (configureOptions != null)
//            {
//                services.Configure(configureOptions);
//            }
//            else
//            {
//                services.Configure<RecommendationOptions>(options => { });
//            }

//            // Register services
//            services.AddScoped<AdvancedRecommendationBLL>();
//            services.AddScoped<EnhancedRecommendationService>();

//            // Add memory cache if not already registered
//            services.AddMemoryCache();

//            return services;
//        }
//    }
//}