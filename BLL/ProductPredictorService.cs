using System;
using System.Collections.Generic;
using System.Linq;
using Erp_V1.DAL.DTO;
using Erp_V1.DAL.DAO;
using Microsoft.ML;

namespace Erp_V1.BLL
{

    /// <summary>
    /// Provides advanced product forecasting using hybrid ARIMA–LSTM techniques.
    /// </summary>
    public class ProductPredictorService : IProductPredictorService, IDisposable
    {
        private readonly SalesDAO _salesDao = new SalesDAO();
        private readonly ProductDAO _productDao = new ProductDAO();
        private readonly MLContext _mlContext = new MLContext(seed: 1);
        private readonly IForecaster _arimaForecaster;
        private readonly IForecaster _lstmForecaster;
        private readonly IHybridForecaster _hybridForecaster;
        private readonly ITimeSeriesAnalyzer _timeSeriesAnalyzer;

        public ProductPredictorService(SalesDAO salesDao, ProductDAO productDao)
        {
            _salesDao = salesDao;
            _productDao = productDao;
            _arimaForecaster = new ArimaForecaster();
            _lstmForecaster = new LstmForecaster(_mlContext);
            _hybridForecaster = new HybridForecaster();
            _timeSeriesAnalyzer = new TimeSeriesAnalyzer();
        }

        /// <summary>
        /// Generates forecasts for each active product.
        /// </summary>
        /// <param name="parameters">The prediction configuration parameters.</param>
        /// <returns>A list of product predictions sorted by prediction score.</returns>
        public virtual List<ProductPredictionDTO> GenerateProductForecasts(PredictionParameters parameters)
        {
            parameters.Validate();

            try
            {
                var salesData = RetrieveCleanSalesData();
                var products = _productDao.Select().Where(p => !p.isCategoryDeleted).ToList();

                System.Diagnostics.Debug.WriteLine($"DEBUG: Total Products: {products.Count}");
                System.Diagnostics.Debug.WriteLine($"DEBUG: Total Sales Records: {salesData.Count}");

                var predictions = new List<ProductPredictionDTO>();

                foreach (var product in products)
                {
                    try
                    {
                        // Get sales for the product, ordered by date.
                        var productSales = salesData
                            .Where(s => s.ProductID == product.ProductID)
                            .OrderBy(s => s.SalesDate)
                            .ToList();

                        System.Diagnostics.Debug.WriteLine($"DEBUG: Processing Product {product.ProductID}: {product.ProductName} (Sales Count: {productSales.Count})");

                        if (!IsValidProductData(productSales, parameters.MinimumDataPoints))
                        {
                            System.Diagnostics.Debug.WriteLine($"WARN: Insufficient data for product {product.ProductID}");
                            continue;
                        }

                        // Transform the sales data into a time series.
                        var timeSeries = TransformToTimeSeries(productSales);
                        var analysis = _timeSeriesAnalyzer.Analyze(timeSeries, new AnalysisParameters
                        {
                            SeasonalityPeriod = parameters.SeasonalityPeriod,
                            TrendWindow = parameters.TrendWindow
                        });

                        // Obtain forecasts from ARIMA and LSTM forecasters.
                        ForecastResult arimaForecast = _arimaForecaster.Forecast(timeSeries, parameters);
                        ForecastResult lstmForecast = _lstmForecaster.Forecast(timeSeries, parameters);
                        ForecastResult hybridForecast = _hybridForecaster.CombineForecasts(arimaForecast, lstmForecast);

                        // Compose the final prediction DTO.
                        var prediction = ComposePredictionResult(product, hybridForecast, analysis);
                        predictions.Add(prediction);
                    }
                    catch (Exception exProd)
                    {
                        System.Diagnostics.Debug.WriteLine($"ERROR: Forecast failed for product {product.ProductID}: {exProd.Message}");
                    }
                }

                return predictions.OrderByDescending(p => p.PredictionScore).ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CRITICAL ERROR in GenerateProductForecasts: {ex}");
                throw;
            }
        }

        /// <summary>
        /// Retrieves cleaned sales data from the past 2 years for active products.
        /// </summary>
        private List<SalesDetailDTO> RetrieveCleanSalesData()
        {
            return _salesDao.Select()
                .Where(s => !s.isProductDeleted &&
                            !s.isCategoryDeleted &&
                            s.SalesDate > DateTime.Today.AddYears(-2))
                .ToList();
        }

        /// <summary>
        /// Determines whether the sales data for a product is sufficient.
        /// </summary>
        private bool IsValidProductData(List<SalesDetailDTO> sales, int minDataPoints)
        {
            // Must have at least the minimum number of records and one sale in the past year.
            return sales.Count >= minDataPoints &&
                   sales.Any(s => s.SalesDate > DateTime.Today.AddYears(-1));
        }

        /// <summary>
        /// Converts raw sales details into aggregated time series data.
        /// </summary>
        private List<TimeSeriesData> TransformToTimeSeries(List<SalesDetailDTO> sales)
        {
            return sales.GroupBy(s => s.SalesDate.Date)
                        .Select(g => new TimeSeriesData
                        {
                            Date = g.Key,
                            Quantity = g.Sum(s => s.SalesAmount),
                            AvgPrice = g.Average(s => s.Price),
                            Transactions = g.Count()
                        })
                        .OrderBy(t => t.Date)
                        .ToList();
        }

        /// <summary>
        /// Composes the prediction result for a product based on the forecast and time series analysis.
        /// </summary>
        private ProductPredictionDTO ComposePredictionResult(ProductDetailDTO product, ForecastResult forecast, TimeSeriesAnalysis analysis)
        {
            // Calculate a composite prediction score.
            double predictionScore = (forecast.Forecast * 0.7) + (analysis.TrendSlope * 0.3);
            string productHealth = predictionScore > 1000 ? "Healthy" : "Attention Required";

            return new ProductPredictionDTO
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                PredictedSales = forecast.Forecast,
                ConfidenceIntervalUpper = forecast.ConfidenceUpper,
                ConfidenceIntervalLower = forecast.ConfidenceLower,
                TrendSlope = analysis.TrendSlope,
                ProductHealth = productHealth,
                RecommendedActions = GenerateRecommendations(productHealth),
                PredictionScore = predictionScore
            };
        }

        /// <summary>
        /// Generates a list of recommendations based on the product health.
        /// </summary>
        private List<string> GenerateRecommendations(string productHealth)
        {
            if (productHealth == "Healthy")
            {
                return new List<string>
                {
                    "Maintain current marketing efforts.",
                    "Consider gradual price increases."
                };
            }
            else
            {
                return new List<string>
                {
                    "Review and optimize marketing strategy.",
                    "Adjust inventory levels.",
                    "Investigate customer feedback."
                };
            }
        }

        public void Dispose()
        {
            _salesDao.Dispose();
            _productDao.Dispose();
        }
    }
}
