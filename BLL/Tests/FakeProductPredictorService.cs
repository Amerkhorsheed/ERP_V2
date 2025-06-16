using System;
using System.Collections.Generic;
using Erp_V1.DAL.DTO;
using Erp_V1.BLL;

namespace Erp_V1.BLL.Tests
{
    /// <summary>
    /// A fake predictor that returns a dummy prediction regardless of input.
    /// This allows testing of any consuming logic (or simply verifying that
    /// GenerateProductForecasts returns a non‐empty list when valid parameters are provided).
    /// </summary>
    public class FakeProductPredictorService : ProductPredictorService
    {
        public override List<ProductPredictionDTO> GenerateProductForecasts(PredictionParameters parameters)
        {
            // Instead of going through DAOs and forecasters,
            // we simply return a dummy prediction.

            // Simulate valid forecasting parameters
            parameters.Validate();

            var prediction = new ProductPredictionDTO
            {
                ProductID = 1,
                ProductName = "Test Product",
                PredictedSales = 200,
                ConfidenceIntervalUpper = 220,
                ConfidenceIntervalLower = 180,
                TrendSlope = 1.2,
                ProductHealth = "Healthy",
                RecommendedActions = new List<string>
                {
                    "Maintain current marketing efforts.",
                    "Consider gradual price increases."
                },
                // A composite score based on our dummy values:
                PredictionScore = (200 * 0.7) + (1.2 * 0.3)
            };

            return new List<ProductPredictionDTO> { prediction };
        }
        public FakeProductPredictorService() : base(null, null) 
        {
            
        }
    }
}
