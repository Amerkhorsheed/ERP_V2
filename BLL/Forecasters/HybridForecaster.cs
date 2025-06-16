namespace Erp_V1.BLL
{
    /// <summary>
    /// Combines forecasts from multiple models using a weighted average.
    /// </summary>
    public class HybridForecaster : IHybridForecaster
    {
        public ForecastResult CombineForecasts(ForecastResult forecast1, ForecastResult forecast2)
        {
            return new ForecastResult
            {
                Forecast = (forecast1.Forecast + forecast2.Forecast) / 2,
                ConfidenceUpper = (forecast1.ConfidenceUpper + forecast2.ConfidenceUpper) / 2,
                ConfidenceLower = (forecast1.ConfidenceLower + forecast2.ConfidenceLower) / 2
            };
        }
    }
}
