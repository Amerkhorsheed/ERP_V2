using System.Collections.Generic;

namespace Erp_V1.BLL
{
    /// <summary>
    /// Interface for time series analysis.
    /// </summary>
    public interface ITimeSeriesAnalyzer
    {
        TimeSeriesAnalysis Analyze(List<TimeSeriesData> data, AnalysisParameters parameters);
    }
}
