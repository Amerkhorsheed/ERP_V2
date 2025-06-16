using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class frmProductRecommendations : Form
    {
        private RecommendationEngine recommendationEngine = new RecommendationEngine();
        private CustomerBLL customerBLL = new CustomerBLL();

        public frmProductRecommendations()
        {
            InitializeComponent();
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            var customers = customerBLL.Select().Customers;
            cboCustomer.DataSource = customers;
            cboCustomer.DisplayMember = "CustomerName";
            cboCustomer.ValueMember = "ID";
        }

        private void btnGetRecommendations_Click(object sender, EventArgs e)
        {
            // Check if a customer is selected
            if (cboCustomer.SelectedItem != null)
            {
                var selectedCustomer = (CustomerDetailDTO)cboCustomer.SelectedItem;

                // Get recommendations for the selected customer
                var recommendations = recommendationEngine.GetRecommendationsForCustomer(selectedCustomer.ID);

                if (recommendations != null && recommendations.Count > 0)
                {
                    dgvRecommendations.DataSource = recommendations;

                    // Adjust column display if needed
                    dgvRecommendations.Columns["ProductID"].Visible = false;
                    dgvRecommendations.Columns["CategoryName"].HeaderText = "Category";
                    dgvRecommendations.Columns["ProductName"].HeaderText = "Product";
                    dgvRecommendations.Columns["Price"].HeaderText = "Price";
                }
                else
                {
                    MessageBox.Show("No product recommendations available for this customer.");
                }
            }
            else
            {
                MessageBox.Show("Please select a customer.");
            }
        }
    }
}
