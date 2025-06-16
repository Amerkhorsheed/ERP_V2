using System;

namespace Erp_V1.BLL
{
    /// <summary>
    /// Encapsulates configuration parameters for product forecasting.
    /// </summary>
    public class PredictionParameters
    {
        public int ForecastHorizon { get; set; } = 30;          // Days to forecast
        public int ConfidenceLevel { get; set; } = 90;            // Confidence level percentage
        public int MinimumDataPoints { get; set; } = 10;          // Minimum records needed
        public int SeasonalityPeriod { get; set; } = 7;           // Seasonality (e.g., weekly)
        public int TrendWindow { get; set; } = 14;                // Number of days for trend estimation

        /// <summary>
        /// Validates the parameters.
        /// </summary>
        public void Validate()
        {
            if (ForecastHorizon < 7 || ForecastHorizon > 365)
                throw new ArgumentException("Forecast horizon must be between 7 and 365 days.");
            if (ConfidenceLevel < 80 || ConfidenceLevel > 99)
                throw new ArgumentException("Confidence level must be between 80 and 99.");
            if (MinimumDataPoints < 10)
                throw new ArgumentException("Minimum data points must be at least 10.");
        }
    }
}
