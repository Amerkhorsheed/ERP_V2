using System;

namespace Erp_V1.BLL
{
    /// <summary>
    /// Represents the result of a forecast including a prediction and confidence bounds.
    /// </summary>
    public class ForecastResult
    {
        public double Forecast { get; set; }
        public double ConfidenceUpper { get; set; }
        public double ConfidenceLower { get; set; }

        /// <summary>
        /// Converts float array forecasts to double values.
        /// </summary>
        public static ForecastResult FromFloatOutput(ForecastOutput output)
        {
            return new ForecastResult
            {
                // Take the first value or calculate an average if needed
                Forecast = output.Forecast?.Length > 0 ? Convert.ToDouble(output.Forecast[0]) : 0,
                ConfidenceUpper = output.ConfidenceUpperBound?.Length > 0 ?
                    Convert.ToDouble(output.ConfidenceUpperBound[0]) : 0,
                ConfidenceLower = output.ConfidenceLowerBound?.Length > 0 ?
                    Convert.ToDouble(output.ConfidenceLowerBound[0]) : 0
            };
        }
    }
}
