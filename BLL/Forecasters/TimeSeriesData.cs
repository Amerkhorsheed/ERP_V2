using System;

namespace Erp_V1.BLL
{
    /// <summary>
    /// Represents a single time series data point.
    /// </summary>
    public class TimeSeriesData
    {
        public DateTime Date { get; set; }
        public float Quantity { get; set; }
        public double AvgPrice { get; set; }
        public int Transactions { get; set; }
    }
}
