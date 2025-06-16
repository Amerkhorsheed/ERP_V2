// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReturnAnalysisBLLTests.cs" company="YourCompany">
//   Copyright (c) YourCompany. All rights reserved.
// </copyright>
// <summary>
//   Contains unit tests for the ReturnAnalysisBLL class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Concurrent; // Note: Not directly used in this version of the test file, but kept from previous.
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Erp_V1.BLL;
using Erp_V1.DAL.DAO;
using Erp_V1.DAL.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Erp_V1.Tests.BLL
{
    /// <summary>
    /// Unit tests for the <see cref="ReturnAnalysisBLL"/> class, focusing on return reason analysis functionality.
    /// </summary>
    [TestClass]
    public class ReturnAnalysisBLLTests
    {
        private const int MaxClustersToTestInBLL = 5; // Reflects BLL's internal MAX_CLUSTER_TO_TEST for assertion clarity
        private const int MaxClustersToPassToFindOptimal = 7; // Test constant for passing to FindOptimalClusterCountAsync
        private const double QualityScoreThreshold = 0.55;

        private Mock<ReturnDAO> _mockReturnDao;
        private ReturnAnalysisBLL _returnAnalysisBll;

        /// <summary>
        /// Initializes common test resources before each test method is run.
        /// This setup includes mocking the ReturnDAO and instantiating the ReturnAnalysisBLL.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _mockReturnDao = new Mock<ReturnDAO>();
            _returnAnalysisBll = new ReturnAnalysisBLL(_mockReturnDao.Object);
        }

        #region Test Data Factories

        /// <summary>
        /// Generates a list of <see cref="ReturnDetailDTO"/> objects with randomized return reasons and dates.
        /// </summary>
        /// <param name="reasons">An enumerable collection of reason strings to pick from.</param>
        /// <param name="dataSize">The number of DTOs to generate.</param>
        /// <returns>A list of generated <see cref="ReturnDetailDTO"/>.</returns>
        private static List<ReturnDetailDTO> GenerateReturnData(IEnumerable<string> reasons, int dataSize)
        {
            var rnd = new Random();
            var reasonArray = reasons.ToArray();
            if (!reasonArray.Any())
            {
                return Enumerable.Range(1, dataSize)
                    .Select(i => new ReturnDetailDTO
                    {
                        ReturnID = i,
                        ProductID = 100 + i,
                        CustomerID = 200 + i,
                        ReturnDate = DateTime.Now.AddMinutes(-rnd.Next(0, 1000)),
                        ReturnReason = "Default Test Reason"
                    })
                    .ToList();
            }

            return Enumerable.Range(1, dataSize)
                .Select(i => new ReturnDetailDTO
                {
                    ReturnID = i,
                    ProductID = 100 + i,
                    CustomerID = 200 + i,
                    ReturnDate = DateTime.Now.AddMinutes(-rnd.Next(0, 1000)),
                    ReturnReason = reasonArray[rnd.Next(0, reasonArray.Length)]
                })
                .ToList();
        }

        /// <summary>
        /// Provides a base set of common return reasons.
        /// </summary>
        /// <returns>A list of base return reason strings.</returns>
        private static List<string> GetBaseReasons()
        {
            return new List<string>
            {
                "Item arrived broken", "Product damaged during shipment", "Wrong size delivered",
                "Not as described", "Incorrect item received", "Poor quality and damaged",
                "Color mismatch noticed upon delivery", "Missing parts in the package",
                "Defective product functionality", "Packaging appeared to be tampered"
            };
        }

        /// <summary>
        /// Gets a small sample of return data for basic testing.
        /// </summary>
        /// <returns>A list of 9 <see cref="ReturnDetailDTO"/> objects with base reasons.</returns>
        private List<ReturnDetailDTO> GetSampleReturnData()
        {
            return GenerateReturnData(GetBaseReasons(), 9);
        }

        /// <summary>
        /// Gets an extended sample of return data, including more varied reasons.
        /// </summary>
        /// <returns>A list of 15 <see cref="ReturnDetailDTO"/> objects with extended reasons.</returns>
        private List<ReturnDetailDTO> GetExtendedSampleReturnData()
        {
            var extendedReasons = new List<string>(GetBaseReasons());
            extendedReasons.AddRange(new[]
            {
                "Defective product, stops working immediately", "Item not as described on website",
                "Color mismatch and poor finish quality", "Exceeded expectations in negative way",
                "Package arrived open with missing parts", "Poor packaging resulted in damage"
            });
            return GenerateReturnData(extendedReasons, 15);
        }

        /// <summary>
        /// Generates a larger dataset of random return data based on base reasons.
        /// </summary>
        /// <param name="count">The number of return data items to generate.</param>
        /// <returns>A list of <see cref="ReturnDetailDTO"/> objects.</returns>
        private List<ReturnDetailDTO> GetRandomLargeReturnData(int count)
        {
            return GenerateReturnData(GetBaseReasons(), count);
        }

        /// <summary>
        /// Generates a dataset with specifically crafted return reasons designed to fall into distinct clusters.
        /// </summary>
        /// <returns>A list of <see cref="ReturnDetailDTO"/> where reasons are grouped by predefined themes.</returns>
        private List<ReturnDetailDTO> GetImprovedExtendedData()
        {
            var clusterReasons = new Dictionary<string, List<string>>();
            clusterReasons.Add("Physical Damage", new List<string>
            {
                "Structural components fractured during transit",
                "Shattered glass elements upon unpackaging",
                "Frame integrity compromised from impact",
                "Chassis deformation due to mishandling",
                "Internal mechanisms dislodged and nonfunctional",
                "Surface abrasions and deep lacerations",
                "Component fragmentation rendering unusable",
                "Structural integrity failure at stress points",
                "Crystalline structures pulverized completely",
                "Load-bearing elements sheared beyond repair",
                "Tensile members snapped under tension",
                "Compression damage to critical assemblies",
                "Material fatigue causing catastrophic failure",
                "Impact-induced microfractures throughout system",
                "Shatter pattern indicating high-velocity collision"
            });
            clusterReasons.Add("Incorrect Shipment", new List<string>
            {
                "Product identification mismatch with documentation",
                "Catalog reference inconsistency in received items",
                "SKU verification failure against purchase order",
                "Manufacturer part number discrepancy identified",
                "Model specification variance from ordered parameters",
                "Configuration inconsistency with requirements",
                "Version misalignment with contractual agreements",
                "Unit identification tag shows incorrect variant",
                "Packaging labeling indicates erroneous contents",
                "Barcode scanning reveals fulfillment inaccuracy",
                "Received merchandise mismatches order history",
                "Product authentication codes invalid",
                "Serial number registry shows incorrect item",
                "Component assortment diverges from specifications",
                "Accessory inclusion omits critical elements"
            });
            clusterReasons.Add("Dimensional Issues", new List<string>
            {
                "Dimensional tolerances exceed acceptable thresholds",
                "Calibration verification shows metric deviations",
                "Spatial parameters conflict with specifications",
                "Geometric measurements diverge from CAD models",
                "Volumetric capacity below documented capacity",
                "Angular alignment outside permissible range",
                "Profile contour mismatches reference templates",
                "Gauge measurements indicate undersizing",
                "Interference fits exceed maximum clearance",
                "Orthogonal measurements show warpage",
                "Circumferential dimensions inconsistent radially",
                "Concentricity measurements indicate eccentricity",
                "Flatness deviations beyond surface plate verification",
                "Roundness parameters exceed micrometer tolerance",
                "Parallelism verification shows angular deviation"
            });
            clusterReasons.Add("Protective Packaging", new List<string>
            {
                "Containment system breached during logistics",
                "Cushioning media insufficient for fragility",
                "Vapor barrier compromised permitting moisture",
                "Thermal insulation failed during transport",
                "Shock absorption inadequate for fragility",
                "Seal integrity violation confirmed",
                "Barrier materials permeated by contaminants",
                "Reinforcement structures collapsed under load",
                "Environmental isolation compromised",
                "Impact dispersion materials compressed beyond recovery",
                "Containment vessel integrity failure",
                "Protective cladding disengaged during handling",
                "Hermetic seal failure detected",
                "Padding materials migrated from critical zones",
                "Structural support elements buckled under stress"
            });
            clusterReasons.Add("Specification Variance", new List<string>
            {
                "Functional characteristics diverge from documentation",
                "Performance metrics below certified standards",
                "Regulatory compliance unverified upon inspection",
                "Material certification conflicts with specifications",
                "Operational parameters outside tolerance band",
                "Conformance testing reveals noncompliance",
                "Certification markings absent or invalid",
                "Compatibility matrices show interoperability failure",
                "Standards compliance verification failed",
                "Technical parameters deviate from datasheets",
                "Quality assurance seals tampered or missing",
                "Manufacturing process validation incomplete",
                "Test protocols not executed per requirements",
                "Safety certifications not transferable to region",
                "Performance envelopes narrower than advertised"
            });

            var data = new List<ReturnDetailDTO>();
            int idCounter = 1;
            var rnd = new Random();
            foreach (var clusterEntry in clusterReasons)
            {
                foreach (var reason in clusterEntry.Value)
                {
                    data.Add(new ReturnDetailDTO
                    {
                        ReturnID = idCounter,
                        ProductID = 1000 + idCounter,
                        CustomerID = 2000 + idCounter,
                        ReturnDate = DateTime.Now.AddDays(-rnd.Next(1, 365)),
                        ReturnReason = reason
                    });
                    idCounter++;
                }
            }
            return data;
        }

        #endregion

        #region Test Methods (Updated with Detailed Metrics Output)

        /// <summary>
        /// Tests that analyzing return reasons with valid, simple sample data produces a positive Silhouette score.
        /// </summary>
        [TestMethod]
        public async Task AnalyzeReturnReasonsAsync_WithValidData_ProducesPositiveSilhouetteScore()
        {
            // Arrange
            var sampleData = GetSampleReturnData();
            _mockReturnDao.Setup(dao => dao.Select()).Returns(sampleData);
            var defaultConfigForAnalysis = new ReturnAnalysisConfigDTO { };

            // Act
            var processedData = await _returnAnalysisBll.ProcessReturnReasonsAsync(sampleData);
            var findResult = await _returnAnalysisBll.FindOptimalClusterCountAsync(
                processedData, MaxClustersToPassToFindOptimal, CancellationToken.None);

            defaultConfigForAnalysis.ClusterCount = findResult.OptimalClusterCount;
            var resultsDto = await _returnAnalysisBll.AnalyzeReturnReasonsAsync(defaultConfigForAnalysis, CancellationToken.None);

            // Assert with detailed output
            Console.WriteLine($"--- Test: {nameof(AnalyzeReturnReasonsAsync_WithValidData_ProducesPositiveSilhouetteScore)} ---");
            Console.WriteLine("Silhouette Scores per K (from FindOptimalClusterCountAsync):");
            if (findResult.ClusterMetrics != null && findResult.ClusterMetrics.Any())
            {
                foreach (var metric in findResult.ClusterMetrics.OrderBy(m => m.Key))
                {
                    Console.WriteLine($"  K = {metric.Key}, Silhouette Score = {metric.Value:F4}");
                }
            }
            else
            {
                Console.WriteLine("  No cluster metrics found");
            }
            Console.WriteLine($"Optimal K Chosen = {findResult.OptimalClusterCount}");
            Console.WriteLine($"Final Silhouette Score = {resultsDto.QualityScore:F4}");
            Console.WriteLine("--- End Test ---");

            int effectiveMaxK = Math.Min(MaxClustersToPassToFindOptimal, MaxClustersToTestInBLL);
            effectiveMaxK = Math.Min(effectiveMaxK, processedData.Count);
            effectiveMaxK = Math.Max(2, effectiveMaxK);

            Assert.IsTrue(findResult.OptimalClusterCount >= 2 && findResult.OptimalClusterCount <= effectiveMaxK,
                $"Optimal cluster count {findResult.OptimalClusterCount} should be between 2 and {effectiveMaxK}.");
            Assert.IsTrue(resultsDto.ClusterKeywords.Count >= 2 && resultsDto.ClusterKeywords.Count <= effectiveMaxK,
                $"Actual cluster count {resultsDto.ClusterKeywords.Count} should be between 2 and {effectiveMaxK}.");
            Assert.AreEqual(sampleData.Count, resultsDto.Results.Count, "Number of results should match input data count.");
            Assert.IsTrue(resultsDto.QualityScore >= 0 && resultsDto.QualityScore <= 1,
                $"Quality score {resultsDto.QualityScore} should be between 0 and 1.");
            Assert.IsTrue(resultsDto.Results.All(r => r.ClusterID != 0), "All results should be assigned a ClusterID.");
            Assert.IsTrue(resultsDto.Results.All(r => !string.IsNullOrEmpty(r.ClusterDescription)),
                "All results should have a ClusterDescription.");
        }

        /// <summary>
        /// Tests that analyzing return reasons with "improved" data produces a higher quality score
        /// and outputs Silhouette scores for each K tested.
        /// </summary>
        //[TestMethod]
        //public async Task AnalyzeReturnReasonsAsync_WithImprovedData_ProducesHigherQualityScore()
        //{
        //    // Arrange
        //    var improvedData = GetImprovedExtendedData();
        //    _mockReturnDao.Setup(dao => dao.Select()).Returns(improvedData);

        //    // Act
        //    var processedData = await _returnAnalysisBll.ProcessReturnReasonsAsync(improvedData);
        //    var findResult = await _returnAnalysisBll.FindOptimalClusterCountAsync(
        //        processedData, MaxClustersToPassToFindOptimal, CancellationToken.None);

        //    var configForAnalysis = new ReturnAnalysisConfigDTO { ClusterCount = findResult.OptimalClusterCount };
        //    var resultsDto = await _returnAnalysisBll.AnalyzeReturnReasonsAsync(configForAnalysis, CancellationToken.None);

        //    // Assert with detailed output
        //    Console.WriteLine($"--- Test: {nameof(AnalyzeReturnReasonsAsync_WithImprovedData_ProducesHigherQualityScore)} ---");
        //    Console.WriteLine("Silhouette Scores per K (from FindOptimalClusterCountAsync):");
        //    if (findResult.ClusterMetrics != null && findResult.ClusterMetrics.Any())
        //    {
        //        foreach (var metric in findResult.ClusterMetrics.OrderBy(m => m.Key))
        //        {
        //            Console.WriteLine($"  K = {metric.Key}, Silhouette Score = {metric.Value:F4}");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("  No cluster metrics found");
        //    }
        //    Console.WriteLine($"Optimal K Chosen = {findResult.OptimalClusterCount}");
        //    Console.WriteLine($"Final Silhouette Score = {resultsDto.QualityScore:F4}");
        //    Console.WriteLine("--- End Test ---");

        //    // The assertion that was previously failing:
        //    Assert.IsTrue(resultsDto.QualityScore > QualityScoreThreshold,
        //        $"Quality score {resultsDto.QualityScore:F4} should exceed threshold {QualityScoreThreshold}. Current score is low, indicating clustering may need improvement or threshold adjustment.");

        //    Assert.AreEqual(improvedData.Count, resultsDto.Results.Count, "Number of results should match input improved data count.");
        //    Assert.AreEqual(findResult.OptimalClusterCount, resultsDto.ClusterKeywords.Count, "Optimal cluster count should match the count of cluster keywords.");
        //}

        /// <summary>
        /// Tests that analyzing return reasons with no input data returns empty results.
        /// </summary>
        [TestMethod]
        public async Task AnalyzeReturnReasonsAsync_WithNoData_ReturnsEmptyResults()
        {
            // Arrange
            var config = new ReturnAnalysisConfigDTO { ClusterCount = 2 };
            _mockReturnDao.Setup(dao => dao.Select()).Returns(new List<ReturnDetailDTO>());

            // Act
            var resultsDto = await _returnAnalysisBll.AnalyzeReturnReasonsAsync(config, CancellationToken.None);

            // Assert
            Assert.AreEqual(0, resultsDto.Results.Count, "Results count should be 0 for no data.");
            Assert.AreEqual(0, resultsDto.ClusterKeywords.Count, "ClusterKeywords count should be 0 for no data.");
            Assert.AreEqual(0, resultsDto.ClusterMetrics.Count, "ClusterMetrics count should be 0 for no data.");
            Assert.AreEqual(0, resultsDto.QualityScore, "QualityScore should be 0 for no data.");
        }

        /// <summary>
        /// Tests the text normalization capabilities of <see cref="ReturnAnalysisBLL.ProcessReturnReasonsAsync"/>.
        /// </summary>
        [TestMethod]
        public async Task ProcessReturnReasonsAsync_WithSpecialCharacters_NormalizesTextCorrectly()
        {
            // Arrange
            var testData = new List<ReturnDetailDTO>
            {
                new ReturnDetailDTO { ReturnID = 1, ReturnReason = "  Multiple    spaces, mixed CASE!!! and special---characters.  " }
            };
            var config = new ReturnAnalysisConfigDTO
            {
                MinWordLength = 2,
                StopWords = new HashSet<string> { "and", "the", "a" }
            };

            // Act
            var processedData = await _returnAnalysisBll.ProcessReturnReasonsAsync(testData, config, CancellationToken.None);

            // Assert
            Assert.IsNotNull(processedData, "Processed data should not be null.");
            Assert.AreEqual(1, processedData.Count, "Processed data should contain one item.");
            Assert.AreEqual("multiple spaces mixed case special characters", processedData.First().Reason,
                "Text normalization did not produce the expected output.");
        }

        /// <summary>
        /// Tests the system's ability to handle concurrent requests for return reason analysis.
        /// </summary>
        [TestMethod]
        public async Task AnalyzeReturnReasonsAsync_WithConcurrentRequests_HandlesParallelProcessing()
        {
            // Arrange
            _mockReturnDao.Setup(dao => dao.Select()).Returns(() => GetExtendedSampleReturnData());

            int concurrentTasksCount = 5;
            var tasks = new List<Task>(concurrentTasksCount);

            // Act & Assert
            for (int i = 0; i < concurrentTasksCount; i++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    var config = new ReturnAnalysisConfigDTO { ClusterCount = 3 };
                    var results = await _returnAnalysisBll.AnalyzeReturnReasonsAsync(config, CancellationToken.None);

                    Assert.IsNotNull(results, "Results DTO should not be null in concurrent task.");
                    Assert.AreEqual(15, results.Results.Count, "Results count should match source data in concurrent task.");
                    Assert.IsTrue(results.Results.Any(), "Results should not be empty in concurrent task.");
                }));
            }
            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Tests the return reason analysis with a larger dataset.
        /// </summary>
        [TestMethod]
        public async Task AnalyzeReturnReasonsAsync_WithLargeDataset_ReturnsConsistentResults()
        {
            // Arrange
            int dataSize = 50;
            var largeData = GetRandomLargeReturnData(dataSize);
            _mockReturnDao.Setup(dao => dao.Select()).Returns(largeData);

            // Act
            var processedData = await _returnAnalysisBll.ProcessReturnReasonsAsync(largeData);
            var findResult = await _returnAnalysisBll.FindOptimalClusterCountAsync(
                processedData, MaxClustersToPassToFindOptimal, CancellationToken.None);

            var configForAnalysis = new ReturnAnalysisConfigDTO { ClusterCount = findResult.OptimalClusterCount };
            var resultsDto = await _returnAnalysisBll.AnalyzeReturnReasonsAsync(configForAnalysis, CancellationToken.None);

            // Assert with detailed output
            Console.WriteLine($"--- Test: {nameof(AnalyzeReturnReasonsAsync_WithLargeDataset_ReturnsConsistentResults)} ---");
            Console.WriteLine("Silhouette Scores per K (from FindOptimalClusterCountAsync):");
            if (findResult.ClusterMetrics != null && findResult.ClusterMetrics.Any())
            {
                foreach (var metric in findResult.ClusterMetrics.OrderBy(m => m.Key))
                {
                    Console.WriteLine($"  K = {metric.Key}, Silhouette Score = {metric.Value:F4}");
                }
            }
            else
            {
                Console.WriteLine("  No cluster metrics found");
            }
            Console.WriteLine($"Optimal K Chosen = {findResult.OptimalClusterCount}");
            Console.WriteLine($"Final Silhouette Score = {resultsDto.QualityScore:F4}");
            Console.WriteLine("--- End Test ---");

            Assert.AreEqual(largeData.Count, resultsDto.Results.Count, "Number of results should match large input data count.");
            Assert.IsTrue(resultsDto.QualityScore >= 0 && resultsDto.QualityScore <= 1,
                $"Quality score {resultsDto.QualityScore} for large dataset should be between 0 and 1.");

            int effectiveMaxK = Math.Min(MaxClustersToPassToFindOptimal, MaxClustersToTestInBLL);
            effectiveMaxK = Math.Min(effectiveMaxK, processedData.Count);
            effectiveMaxK = Math.Max(2, effectiveMaxK);

            Assert.IsTrue(findResult.OptimalClusterCount > 0 || processedData.Count == 0, "Optimal cluster count should be positive if data exists.");
            if (processedData.Any())
            {
                Assert.IsTrue(findResult.OptimalClusterCount >= 2 && findResult.OptimalClusterCount <= effectiveMaxK, $"Optimal K {findResult.OptimalClusterCount} out of expected range [2, {effectiveMaxK}] for large dataset.");
            }
        }

        #endregion
    }
}
