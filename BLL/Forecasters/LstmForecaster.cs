using System;
using System.Collections.Generic;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;

namespace Erp_V1.BLL
{
    public class LstmForecaster : IForecaster
    {
        private readonly MLContext _mlContext;

        public LstmForecaster(MLContext mlContext)
        {
            _mlContext = mlContext;
        }

        public ForecastResult Forecast(List<TimeSeriesData> series, PredictionParameters parameters)
        {
            var dataView = _mlContext.Data.LoadFromEnumerable(series);

            var pipeline = _mlContext.Forecasting.ForecastBySsa(
                outputColumnName: nameof(ForecastOutput.Forecast),
                inputColumnName: nameof(TimeSeriesData.Quantity),
                windowSize: parameters.SeasonalityPeriod * 2,
                seriesLength: parameters.SeasonalityPeriod * 4,
                trainSize: series.Count,
                horizon: parameters.ForecastHorizon,
                confidenceLevel: parameters.ConfidenceLevel / 100f,
                // Explicitly specify confidence interval column names
                confidenceLowerBoundColumn: nameof(ForecastOutput.ConfidenceLowerBound),
                confidenceUpperBoundColumn: nameof(ForecastOutput.ConfidenceUpperBound));

            var model = pipeline.Fit(dataView);
            var forecastEngine = model.CreateTimeSeriesEngine<TimeSeriesData, ForecastOutput>(_mlContext);
            ForecastOutput output = forecastEngine.Predict();

            return new ForecastResult
            {
                Forecast = Convert.ToDouble(output.Forecast[parameters.ForecastHorizon - 1]),
                ConfidenceUpper = Convert.ToDouble(output.ConfidenceUpperBound[parameters.ForecastHorizon - 1]),
                ConfidenceLower = Convert.ToDouble(output.ConfidenceLowerBound[parameters.ForecastHorizon - 1])
            };
        }
    }

    public class ForecastOutput
    {
        public float[] Forecast { get; set; }
        public float[] ConfidenceUpperBound { get; set; }
        public float[] ConfidenceLowerBound { get; set; }
    }
}
