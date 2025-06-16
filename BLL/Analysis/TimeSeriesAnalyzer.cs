using System.Collections.Generic;
using System.Linq;

namespace Erp_V1.BLL
{
    /// <summary>
    /// Provides a basic time series analysis by computing a simple trend slope.
    /// </summary>
    public class TimeSeriesAnalyzer : ITimeSeriesAnalyzer
    {
        public TimeSeriesAnalysis Analyze(List<TimeSeriesData> data, AnalysisParameters parameters)
        {
            if (data == null || data.Count < 2)
                return new TimeSeriesAnalysis { TrendSlope = 0 };

            int n = data.Count;
            double slope = (data.Last().Quantity - data.First().Quantity) / (double)(n - 1);
            return new TimeSeriesAnalysis { TrendSlope = slope };
        }
    }
}
