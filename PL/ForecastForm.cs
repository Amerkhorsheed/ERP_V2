using System;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DAO;

namespace Erp_V1
{
    /// <summary>
    /// Main forecasting dashboard form.
    /// </summary>
    public partial class ForecastForm : Form
    {
        private ProductPredictorService _predictorService;

        public ForecastForm()
        {
            InitializeComponent();
            _predictorService = new ProductPredictorService(new SalesDAO(), new ProductDAO());
            SetupForm();
        }

        private void SetupForm()
        {
            // Set custom form properties for a modern look.
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BackColor = System.Drawing.Color.WhiteSmoke;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                // Create forecast parameters from user inputs.
                var parameters = new PredictionParameters
                {
                    ForecastHorizon = int.Parse(txtHorizon.Text),
                    ConfidenceLevel = int.Parse(txtConfidence.Text),
                    MinimumDataPoints = int.Parse(txtMinData.Text),
                    SeasonalityPeriod = int.Parse(txtSeasonality.Text),
                    TrendWindow = int.Parse(txtTrend.Text)
                };

                // Generate forecasts and bind to grid.
                var predictions = _predictorService.GenerateProductForecasts(parameters);
                dataGridViewForecast.DataSource = predictions;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating forecast: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
