using System.Collections.Generic;

namespace Erp_V1.BLL
{
    /// <summary>
    /// Interface for a forecasting model.
    /// </summary>
    public interface IForecaster
    {
        ForecastResult Forecast(List<TimeSeriesData> series, PredictionParameters parameters);
    }
}
