using System;
using System.Drawing;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Erp_V1
{
    public partial class FrmAi : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private const string ApiEndpoint = "http://127.0.0.1:5000/ai_assistant";

        public FrmAi()
        {
            InitializeComponent();
            txtAIResponse.ReadOnly = true;
            ApplyRoundedCorners(btnSendRequest, 8);
            this.StartPosition = FormStartPosition.CenterScreen;
            cmbQuestionType.SelectedIndex = 0; // Select the first question type by default
        }

        private async void btnSendRequest_Click(object sender, EventArgs e)
        {
            string questionType = cmbQuestionType.SelectedItem?.ToString();
            string parameter = txtParameter.Text.Trim();
            string userRequest = "";

            if (string.IsNullOrEmpty(questionType))
            {
                MessageBox.Show("Please select a question type.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(parameter) && (questionType != "Sales report summary" && questionType != "General ERP question...")) // Parameter required for certain question types
            {
                MessageBox.Show("Please enter the required parameter.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (questionType)
            {
                case "Get stock level of product...":
                    userRequest = $"Stock level of product {parameter}";
                    break;
                case "Customer details for...":
                    userRequest = $"Customer details for {parameter}";
                    break;
                case "Sales report summary":
                    userRequest = "Sales report summary";
                    break;
                case "General ERP question...":
                    userRequest = parameter; // Parameter box is for general question in this case
                    break;
                default:
                    MessageBox.Show("Invalid question type selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }


            lblStatus.ForeColor = Color.DarkBlue;
            lblStatus.Text = "Sending request to AI...";
            txtAIResponse.Text = "";
            txtAIResponse.BackColor = Color.White;

            try
            {
                var requestData = new { request = userRequest };
                string jsonData = JsonSerializer.Serialize(requestData);
                var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(ApiEndpoint, content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonDocument.Parse(responseBody).RootElement;
                string aiResponse = jsonResponse.GetProperty("response").GetString();

                txtAIResponse.Text = aiResponse;
                txtAIResponse.BackColor = Color.WhiteSmoke;
                lblStatus.ForeColor = Color.DarkGreen;
                lblStatus.Text = "Request successful. AI response received.";
            }
            catch (HttpRequestException ex)
            {
                HandleRequestError($"Network error: {ex.Message}", Color.Red);
            }
            catch (JsonException ex)
            {
                HandleRequestError($"Error parsing AI response: {ex.Message}", Color.Red);
            }
            catch (Exception ex)
            {
                HandleRequestError($"Unexpected error: {ex.Message}", Color.Red);
            }
        }

        private void HandleRequestError(string errorMessage, Color statusColor)
        {
            txtAIResponse.Text = $"Error: {errorMessage}";
            txtAIResponse.BackColor = Color.LightCoral;
            lblStatus.ForeColor = statusColor;
            lblStatus.Text = "Request failed. Please check your request and backend.";
            MessageBox.Show(errorMessage, "AI Assistant Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void ApplyRoundedCorners(Button button, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.StartFigure();
            path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
            path.AddArc(new Rectangle(button.Width - radius, 0, radius, radius), 270, 90);
            path.AddArc(new Rectangle(button.Width - radius, button.Height - radius, radius, radius), 0, 90);
            path.AddArc(new Rectangle(0, button.Height - radius, radius, radius), 90, 90);
            path.CloseFigure();
            button.Region = new Region(path);
        }

        private void cmbQuestionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedQuestion = cmbQuestionType.SelectedItem?.ToString();
            txtParameter.Text = ""; // Clear parameter textbox when question type changes
            if (selectedQuestion == "Get stock level of product...")
            {
                lblParameterPrompt.Text = "Enter product name:";
            }
            else if (selectedQuestion == "Customer details for...")
            {
                lblParameterPrompt.Text = "Enter customer name:";
            }
            else if (selectedQuestion == "Sales report summary")
            {
                lblParameterPrompt.Text = ""; // No parameter needed
                txtParameter.Enabled = false; // Disable parameter textbox
            }
            else if (selectedQuestion == "General ERP question...")
            {
                lblParameterPrompt.Text = "Enter your question:";
                txtParameter.Enabled = true; // Enable parameter textbox for general questions
            }
            else
            {
                lblParameterPrompt.Text = "Enter parameter:"; // Default prompt
                txtParameter.Enabled = true; // Ensure textbox is enabled by default for other types
            }
        }

        private void FrmAi_Load(object sender, EventArgs e)
        {

        }

        
    }
}