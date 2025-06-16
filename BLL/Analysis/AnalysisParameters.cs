namespace Erp_V1.BLL
{
    /// <summary>
    /// Parameters for performing time series analysis.
    /// </summary>
    public class AnalysisParameters
    {
        public int SeasonalityPeriod { get; set; }
        public int TrendWindow { get; set; }
    }
}
