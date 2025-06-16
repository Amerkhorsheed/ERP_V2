//using System;
//using System.Linq;
//using System.Collections.Generic;
//using Microsoft.ML;
//using Erp_V1.DAL;
//using Erp_V1.DAL.DAO;
//using Erp_V1.DAL.DTO;
//using Erp_V1.BLL.Forecasters;

//namespace Erp_V1.BLL
//{
//    /// <summary>
//    /// Advanced product prediction engine using hybrid ARIMA-LSTM architecture.
//    /// </summary>
//    public class ProductPredictorBLL : IDisposable
//    {
//        #region Dependencies
//        private readonly SalesDAO _salesDao = new SalesDAO();
//        private readonly ProductDAO _productDao = new ProductDAO();
//        private readonly MLContext _mlContext = new MLContext(seed: 1);
//        #endregion

//        #region Core Prediction Engine
//        /// <summary>
//        /// Generates product forecasts with confidence intervals and recommendations.
//        /// </summary>
//        /// <param name="parameters">Prediction configuration parameters.</param>
//        /// <returns>Sorted list of product predictions.</returns>
//        public List<ProductPredictionDTO> GenerateProductForecasts(PredictionParameters parameters)
//        {
//            ValidateParameters(parameters);

//            var salesData = RetrieveCleanSalesData();
//            var products = _productDao.Select().Where(p => !p.isCategoryDeleted).ToList();

//            return products.AsParallel().Select(p =>
//                    ProcessProductPrediction(p, salesData, parameters))
//                .Where(p => p != null)
//                .OrderByDescending(p => p.PredictionScore)
//                .ToList();
//        }

//        private ProductPredictionDTO ProcessProductPrediction(ProductDetailDTO product,
//            List<SalesDetailDTO> salesData, PredictionParameters parameters)
//        {
//            try
//            {
//                var productSales = salesData
//                    .Where(s => s.ProductID == product.ProductID)
//                    .OrderBy(s => s.SalesDate)
//                    .ToList();

//                if (!ValidateProductData(productSales, parameters.MinimumDataPoints))
//                    return null;

//                var timeSeries = TransformToTimeSeries(productSales, parameters);
//                var analysis = PerformTimeSeriesAnalysis(timeSeries, parameters);
//                var forecast = GenerateHybridForecast(timeSeries, parameters);

//                return ComposePredictionResult(product, forecast, analysis, parameters);
//            }
//            catch (Exception ex)
//            {
//                LogPredictionError(product.ProductID, ex);
//                return null;
//            }
//        }
//        #endregion

//        #region Forecasting Algorithms
//        private ForecastResult GenerateHybridForecast(List<TimeSeriesData> data,
//            PredictionParameters parameters)
//        {
//            var arimaForecast = new ArimaForecaster().Forecast(data, parameters);
//            var lstmForecast = new LstmForecaster(_mlContext).Forecast(data, parameters);

//            return new HybridForecaster().CombineForecasts(arimaForecast, lstmForecast);
//        }

//        private TimeSeriesAnalysis PerformTimeSeriesAnalysis(List<TimeSeriesData> data,
//            PredictionParameters parameters)
//        {
//            return new TimeSeriesAnalyzer().Analyze(data,
//                new AnalysisParameters
//                {
//                    SeasonalityPeriod = parameters.SeasonalityPeriod,
//                    TrendWindow = parameters.TrendWindow
//                });
//        }
//        #endregion

//        #region Helper Methods
//        private List<TimeSeriesData> TransformToTimeSeries(List<SalesDetailDTO> sales,
//            PredictionParameters parameters)
//        {
//            return sales.GroupBy(s => s.SalesDate.Date)
//                .Select(g => new TimeSeriesData
//                {
//                    Date = g.Key,
//                    Quantity = g.Sum(s => s.SalesAmount),
//                    AvgPrice = g.Average(s => s.Price),
//                    Transactions = g.Count()
//                })
//                .OrderBy(d => d.Date)
//                .ToList();
//        }

//        private List<SalesDetailDTO> RetrieveCleanSalesData()
//        {
//            return _salesDao.Select()
//                .Where(s => !s.isProductDeleted &&
//                           !s.isCategoryDeleted &&
//                           s.SalesDate > DateTime.Today.AddYears(-2))
//                .ToList();
//        }
//        #endregion

//        #region Validation & Error Handling
//        private void ValidateParameters(PredictionParameters parameters)
//        {
//            if (parameters.ForecastHorizon < 7 || parameters.ForecastHorizon > 365)
//                throw new ArgumentException("Invalid forecast horizon");

//            if (parameters.ConfidenceLevel < 80 || parameters.ConfidenceLevel > 99)
//                throw new ArgumentException("Invalid confidence level");
//        }

//        private bool ValidateProductData(List<SalesDetailDTO> sales, int minDataPoints)
//        {
//            return sales.Count >= minDataPoints &&
//                   sales.Any(s => s.SalesDate > DateTime.Today.AddMonths(-3));
//        }

//        private void LogPredictionError(int productId, Exception ex)
//        {
//            // Replace with enterprise logging as needed.
//            Console.WriteLine($"Error processing product {productId}: {ex.Message}");
//        }

//        private ProductPredictionDTO ComposePredictionResult(ProductDetailDTO product,
//            ForecastResult forecast, TimeSeriesAnalysis analysis, PredictionParameters parameters)
//        {
//            // Sample logic: calculate a prediction score and determine product health.
//            double predictionScore = forecast.Forecast * analysis.TrendSlope; // Simplified score
//            string productHealth = predictionScore > 1000 ? "Healthy" : "Needs Attention";

//            return new ProductPredictionDTO
//            {
//                ProductID = product.ProductID,
//                ProductName = product.ProductName,
//                PredictedSales = forecast.Forecast,
//                ConfidenceIntervalUpper = forecast.ConfidenceUpper,
//                ConfidenceIntervalLower = forecast.ConfidenceLower,
//                TrendSlope = analysis.TrendSlope,
//                ProductHealth = productHealth,
//                RecommendedActions = new List<string> { "Review marketing strategy", "Optimize inventory levels" },
//                PredictionScore = predictionScore
//            };
//        }
//        #endregion

//        public void Dispose()
//        {
//            _salesDao.Dispose();
//            _productDao.Dispose();
//        }
//    }

//    #region Supporting Classes
//    public class PredictionParameters
//    {
//        public int ForecastHorizon { get; set; } = 30;
//        public int ConfidenceLevel { get; set; } = 95;
//        public int MinimumDataPoints { get; set; } = 30;
//        public int SeasonalityPeriod { get; set; } = 7;
//        public int TrendWindow { get; set; } = 30;
//    }

//    public class TimeSeriesData
//    {
//        public DateTime Date { get; set; }
//        public double Quantity { get; set; }
//        public double AvgPrice { get; set; }
//        public int Transactions { get; set; }
//    }

//    public class TimeSeriesAnalysis
//    {
//        public double TrendSlope { get; set; }
//        // Additional analysis properties can be added here.
//    }

//    public class AnalysisParameters
//    {
//        public int SeasonalityPeriod { get; set; }
//        public int TrendWindow { get; set; }
//    }
//    #endregion
//}
