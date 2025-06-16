using DevExpress.XtraEditors;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using DocumentFormat;
using DevExpress.ClipboardSource.SpreadsheetML;

namespace Erp_V1
{
    public partial class FrmCustomerSearchExport : XtraForm
    {
        #region Private Members

        // Business logic layer for customer operations.
        private readonly CustomerBLL _customerBLL = new CustomerBLL();
        // Local cache for all customer data.
        private List<CustomerDetailDTO> _allCustomers = new List<CustomerDetailDTO>();

        #endregion

        #region Constructor & Form Load

        public FrmCustomerSearchExport()
        {
            InitializeComponent();
            ApplyModernStyling();
        }

        private async void FrmCustomerSearchExport_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadCustomersAsync();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error loading customers:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Data Loading & Filtering

        /// <summary>
        /// Asynchronously loads customers from the business logic layer.
        /// </summary>
        private async Task LoadCustomersAsync()
        {
            try
            {
                await Task.Run(() =>
                {
                    var customerDto = _customerBLL.Select();
                    _allCustomers = customerDto.Customers;
                });

                // Update the DataGridView on the UI thread.
                dgvCustomers.Invoke(new Action(() =>
                {
                    dgvCustomers.DataSource = _allCustomers;
                    ConfigureDataGrid();
                }));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load customers.", ex);
            }
        }

        /// <summary>
        /// Filters the customer list based on user input.
        /// </summary>
        private void txtSearch_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = txtSearch.Text.Trim().ToLower();
                var filtered = _allCustomers
                    .Where(c => c.CustomerName.ToLower().Contains(searchTerm))
                    .ToList();
                dgvCustomers.DataSource = filtered;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error filtering customers:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region DataGridView Configuration & Styling

        /// <summary>
        /// Configures the DataGridView columns and header styles.
        /// </summary>
        private void ConfigureDataGrid()
        {
            try
            {
                // Hide internal columns.
                if (dgvCustomers.Columns["ID"] != null)
                    dgvCustomers.Columns["ID"].Visible = false;
                if (dgvCustomers.Columns["Notes"] != null)
                    dgvCustomers.Columns["Notes"].Visible = false;

                // Set header texts.
                if (dgvCustomers.Columns["CustomerName"] != null)
                    dgvCustomers.Columns["CustomerName"].HeaderText = "Customer Name";
                if (dgvCustomers.Columns["Cust_Address"] != null)
                    dgvCustomers.Columns["Cust_Address"].HeaderText = "Address";
                if (dgvCustomers.Columns["Cust_Phone"] != null)
                    dgvCustomers.Columns["Cust_Phone"].HeaderText = "Phone";
                if (dgvCustomers.Columns["baky"] != null)
                    dgvCustomers.Columns["baky"].HeaderText = "Outstanding Amount";

                // Apply header style.
                dgvCustomers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204);
                dgvCustomers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvCustomers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error configuring grid:\n{ex.Message}",
                    "Grid Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Export Functionality

        /// <summary>
        /// Exports all (or filtered) customer data to an Excel file.
        /// </summary>
        private void btnExportAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCustomers.DataSource is List<CustomerDetailDTO> list && list.Any())
                {
                    ExportToExcel(list);
                }
                else
                {
                    XtraMessageBox.Show("No customer data available to export.",
                        "Export Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error exporting data:\n{ex.Message}",
                    "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Exports the selected customer's data to an Excel file.
        /// </summary>
        private void btnExportSelected_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCustomers.CurrentRow != null)
                {
                    var selectedCustomer = dgvCustomers.CurrentRow.DataBoundItem as CustomerDetailDTO;
                    if (selectedCustomer != null)
                    {
                        ExportToExcel(new List<CustomerDetailDTO> { selectedCustomer });
                    }
                    else
                    {
                        XtraMessageBox.Show("No customer selected.",
                            "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error exporting selected customer:\n{ex.Message}",
                    "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Exports the specified customer list to an Excel file using ClosedXML.
        /// </summary>
        private void ExportToExcel(List<CustomerDetailDTO> customers)
        {
            try
            {
                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = "Excel Files (*.xlsx)|*.xlsx";
                    dlg.Title = "Export to Excel";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Customers");
                            // Write header.
                            worksheet.Cell(1, 1).Value = "Customer Name";
                            worksheet.Cell(1, 2).Value = "Address";
                            worksheet.Cell(1, 3).Value = "Phone";
                            worksheet.Cell(1, 4).Value = "Outstanding Amount";
                            int row = 2;
                            foreach (var customer in customers)
                            {
                                worksheet.Cell(row, 1).Value = customer.CustomerName;
                                worksheet.Cell(row, 2).Value = customer.Cust_Address;
                                worksheet.Cell(row, 3).Value = customer.Cust_Phone;
                                worksheet.Cell(row, 4).Value = customer.baky;
                                row++;
                            }
                            workbook.SaveAs(dlg.FileName);
                        }
                        XtraMessageBox.Show("Export completed successfully.",
                            "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error during export:\n{ex.Message}",
                    "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Styling

        /// <summary>
        /// Applies modern styling to the form and its controls.
        /// </summary>
        private void ApplyModernStyling()
        {
            try
            {
                // Form styling.
                this.BackColor = Color.WhiteSmoke;
                this.Font = new Font("Segoe UI", 10);
                // Style search TextEdit.
                txtSearch.Properties.Appearance.BackColor = Color.White;
                txtSearch.Properties.Appearance.ForeColor = Color.Black;
                txtSearch.Properties.Appearance.Font = new Font("Segoe UI", 10);
                txtSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
                // Style export buttons.
                StyleButton(btnExportAll);
                StyleButton(btnExportSelected);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error applying styling:\n{ex.Message}",
                    "Styling Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Applies consistent styling to a DevExpress SimpleButton.
        /// </summary>
        private void StyleButton(SimpleButton button)
        {
            if (button != null)
            {
                button.Appearance.BackColor = Color.FromArgb(0, 122, 204);
                button.Appearance.ForeColor = Color.White;
                button.Appearance.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                button.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            }
        }

        #endregion
    }
}
