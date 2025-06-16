using System;
using System.Collections.Generic;
using System.Linq;

namespace Erp_V1.BLL
{
    /// <summary>
    /// Implements a simple ARIMA–style forecaster using linear trend extrapolation.
    /// In production, consider using a fully–featured ARIMA library.
    /// </summary>
    public class ArimaForecaster : IForecaster
    {
        public ForecastResult Forecast(List<TimeSeriesData> series, PredictionParameters parameters)
        {
            int n = series.Count;
            if (n < 2)
                throw new InvalidOperationException("Insufficient data for ARIMA forecast.");

            // Compute linear trend based on first and last data points
            double slope = (series.Last().Quantity - series.First().Quantity) / (double)(n - 1);
            double forecast = series.Last().Quantity + (slope * parameters.ForecastHorizon);

            // Simple confidence interval (adjust as needed)
            return new ForecastResult
            {
                Forecast = forecast,
                ConfidenceUpper = forecast * 1.1,
                ConfidenceLower = forecast * 0.9
            };
        }
    }
}
