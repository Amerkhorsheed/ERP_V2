using System.Collections.Generic;

namespace Erp_V1.DAL.DTO
{
    public class ProductPredictionDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double PredictedSales { get; set; }
        public double ConfidenceIntervalUpper { get; set; }
        public double ConfidenceIntervalLower { get; set; }
        public double TrendSlope { get; set; }
        public string ProductHealth { get; set; }
        public List<string> RecommendedActions { get; set; }
        public double PredictionScore { get; set; }

        public string ConfidenceInterval =>
            $"{ConfidenceIntervalLower:N0} - {ConfidenceIntervalUpper:N0}";
    }
}