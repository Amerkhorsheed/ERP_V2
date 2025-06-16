namespace Erp_V1.BLL
{
    /// <summary>
    /// Interface for combining multiple forecast results.
    /// </summary>
    public interface IHybridForecaster
    {
        ForecastResult CombineForecasts(ForecastResult forecast1, ForecastResult forecast2);
    }
}
