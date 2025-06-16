using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class FrmPartialPayment : XtraForm
    {
        // Business logic instance for customers.
        private CustomerBLL customerBll = new CustomerBLL();

        // List of customers with outstanding balance.
        private List<CustomerDetailDTO> outstandingCustomers = new List<CustomerDetailDTO>();

        public FrmPartialPayment()
        {
            InitializeComponent();
        }

        private void FrmPartialPayment_Load(object sender, EventArgs e)
        {
            try
            {
                // Load all customers then filter those with an outstanding balance (baky > 0).
                var customers = customerBll.Select().Customers;
                outstandingCustomers = customers.Where(c => c.baky.HasValue && c.baky.Value > 0).ToList();
                dgvCustomers.DataSource = outstandingCustomers;

                // Setup DataGridView column headers.
                dgvCustomers.Columns[0].Visible = false;
                //dgvCustomers.Columns["ID"].HeaderText = "Customer ID";
                dgvCustomers.Columns["CustomerName"].HeaderText = "Customer Name";
                dgvCustomers.Columns[2].Visible = false;
                dgvCustomers.Columns[3].Visible = false;
                dgvCustomers.Columns[4].Visible = false;
                //dgvCustomers.Columns["Cust_Address"].HeaderText = "Address";
                //dgvCustomers.Columns["Cust_Phone"].HeaderText = "Phone";
                //dgvCustomers.Columns["Notes"].HeaderText = "Notes";
                dgvCustomers.Columns["baky"].HeaderText = "Outstanding Amount";

                // Clear payment details.
                ClearPaymentDetails();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error loading customers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // When the user selects a customer from the grid.
        private void dgvCustomers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvCustomers.Rows.Count)
                return;

            var row = dgvCustomers.Rows[e.RowIndex];
            var customer = row.DataBoundItem as CustomerDetailDTO;
            if (customer != null)
            {
                txtSelectedCustomer.Text = customer.CustomerName;
                txtCurrentOutstanding.Text = customer.baky.HasValue ? customer.baky.Value.ToString() : "0";
            }
            ClearPaymentInput();
        }

        // When the payment type changes.
        private void rbFullPayment_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFullPayment.Checked)
            {
                // In full payment mode, disable payment amount input and set to current outstanding.
                txtPaymentAmount.Enabled = false;
                if (decimal.TryParse(txtCurrentOutstanding.Text, out decimal outstanding))
                {
                    txtPaymentAmount.Text = outstanding.ToString();
                    txtNewOutstanding.Text = "0";
                }
            }
        }

        private void rbPartialPayment_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPartialPayment.Checked)
            {
                // Enable partial payment input.
                txtPaymentAmount.Enabled = true;
                txtPaymentAmount.Text = string.Empty;
                txtNewOutstanding.Text = string.Empty;
            }
        }

        // When the payment amount is entered/changed.
        private void txtPaymentAmount_TextChanged(object sender, EventArgs e)
        {
            if (rbPartialPayment.Checked)
            {
                if (decimal.TryParse(txtCurrentOutstanding.Text, out decimal currentOutstanding) &&
                    decimal.TryParse(txtPaymentAmount.Text, out decimal payAmount))
                {
                    decimal newOutstanding = currentOutstanding - payAmount;
                    if (newOutstanding < 0)
                        newOutstanding = 0;
                    txtNewOutstanding.Text = newOutstanding.ToString();
                }
                else
                {
                    txtNewOutstanding.Text = string.Empty;
                }
            }
        }

        // Save the payment.
        private void btnSavePayment_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate that a customer is selected.
                if (string.IsNullOrWhiteSpace(txtSelectedCustomer.Text))
                {
                    XtraMessageBox.Show("Please select a customer.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get the selected customer.
                int selectedCustomerId = Convert.ToInt32(dgvCustomers.CurrentRow.Cells["ID"].Value);
                var customer = outstandingCustomers.FirstOrDefault(c => c.ID == selectedCustomerId);
                if (customer == null)
                {
                    XtraMessageBox.Show("Selected customer not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                decimal currentOutstanding = customer.baky.HasValue ? customer.baky.Value : 0;
                decimal paymentAmount = 0;

                if (rbFullPayment.Checked)
                {
                    paymentAmount = currentOutstanding;
                }
                else if (rbPartialPayment.Checked)
                {
                    if (!decimal.TryParse(txtPaymentAmount.Text, out paymentAmount))
                    {
                        XtraMessageBox.Show("Please enter a valid payment amount.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (paymentAmount <= 0 || paymentAmount > currentOutstanding)
                    {
                        XtraMessageBox.Show("Payment amount must be greater than zero and not exceed the outstanding amount.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    XtraMessageBox.Show("Please select a payment type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Calculate new outstanding.
                decimal newOutstanding = currentOutstanding - paymentAmount;

                // Update the customer's outstanding balance.
                customer.baky = (long?)newOutstanding;
                if (customerBll.Update(customer))
                {
                    XtraMessageBox.Show("Payment recorded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Refresh the grid.
                    FrmPartialPayment_Load(null, null);
                }
                else
                {
                    XtraMessageBox.Show("Failed to update the outstanding amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error saving payment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearPaymentDetails()
        {
            txtSelectedCustomer.Text = string.Empty;
            txtCurrentOutstanding.Text = string.Empty;
            ClearPaymentInput();
        }

        private void ClearPaymentInput()
        {
            rbFullPayment.Checked = true;
            rbPartialPayment.Checked = false;
            txtPaymentAmount.Text = string.Empty;
            txtPaymentAmount.Enabled = false;
            txtNewOutstanding.Text = string.Empty;
        }
    }
}
