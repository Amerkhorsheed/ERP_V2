using DevExpress.XtraEditors;
using System;
using System.Linq;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class FrmAddStock : XtraForm
    {
        // Business logic and data objects
        private readonly ProductBLL _productBLL = new ProductBLL();
        private ProductDTO _productDTO = new ProductDTO();
        private ProductDetailDTO _selectedProduct = new ProductDetailDTO();
        private bool _comboInitialized = false;

        public FrmAddStock()
        {
            InitializeComponent();
        }

        #region Form Load & Initialization

        private void FrmAddStock_Load(object sender, EventArgs e)
        {
            try
            {
                _productDTO = _productBLL.Select();
                InitializeDataGridView();
                InitializeCategoryComboBox();
            }
            catch (Exception ex)
            {
                ShowError($"Initialization failed: {ex.Message}");
            }
        }

        private void InitializeDataGridView()
        {
            dataGridView1.DataSource = _productDTO.Products;

            // Ensure expected columns exist
            if (dataGridView1.Columns.Count < 6)
            {
                ShowError("Grid columns configuration mismatch.");
                return;
            }

            // Set header texts using property names
            dataGridView1.Columns["ProductName"].HeaderText = "Product Name";
            dataGridView1.Columns["CategoryName"].HeaderText = "Category Name";
            dataGridView1.Columns["stockAmount"].HeaderText = "Stock Amount";
            dataGridView1.Columns["price"].HeaderText = "Price";

            // Hide internal fields
            dataGridView1.Columns["ProductID"].Visible = false;
            dataGridView1.Columns["CategoryID"].Visible = false;
            if (dataGridView1.Columns.Contains("isCategoryDeleted"))
                dataGridView1.Columns["isCategoryDeleted"].Visible = false;
        }

        private void InitializeCategoryComboBox()
        {
            cmbCategory.DataSource = _productDTO.Categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "ID";
            cmbCategory.SelectedIndex = -1;
            _comboInitialized = _productDTO.Categories.Count > 0;
        }

        #endregion

        #region Event Handlers

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_comboInitialized || cmbCategory.SelectedValue == null)
                return;

            try
            {
                if (int.TryParse(cmbCategory.SelectedValue.ToString(), out int categoryId))
                {
                    var filteredProducts = _productDTO.Products
                        .Where(x => x.CategoryID == categoryId)
                        .ToList();
                    dataGridView1.DataSource = filteredProducts;
                    ClearInputFields(filteredProducts.Count == 0);
                }
            }
            catch (Exception ex)
            {
                ShowError($"Filter error: {ex.Message}");
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.RowCount)
                return;

            try
            {
                // Retrieve the complete product object.
                _selectedProduct = dataGridView1.Rows[e.RowIndex].DataBoundItem as ProductDetailDTO;
                if (_selectedProduct != null)
                {
                    txtProductName.Text = _selectedProduct.ProductName;
                    txtPrice.Text = _selectedProduct.price.ToString();
                    txtStock.Text = _selectedProduct.stockAmount.ToString();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Row selection error: {ex.Message}");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                if (int.TryParse(txtStock.Text, out int stockToAdd))
                {
                    // Ensure the product name is updated (if user has modified it).
                    _selectedProduct.ProductName = txtProductName.Text.Trim();

                    // Add the new stock amount to the current stock.
                    _selectedProduct.stockAmount += stockToAdd;

                    // Attempt to update the product.
                    if (_productBLL.Update(_selectedProduct))
                    {
                        ShowSuccess("Stock updated successfully.");
                        RefreshData();
                        // Clear only the stock textbox, keeping product details intact.
                        ClearInputFields(clearAll: false);
                    }
                    else
                    {
                        ShowWarning("Stock update did not complete. Please try again.");
                    }
                }
                else
                {
                    ShowWarning("Please enter a valid numeric stock amount.");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Update failed: {ex.Message}");
            }
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow digits and control keys only.
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Helper Methods

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                ShowWarning("Please select a product from the table.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtStock.Text))
            {
                ShowWarning("Please enter a stock amount.");
                return false;
            }
            return true;
        }

        private void RefreshData()
        {
            _productDTO = _productBLL.Select();
            dataGridView1.DataSource = _productDTO.Products;
        }

        /// <summary>
        /// Clears the input fields.
        /// If clearAll is true, clears product name and price as well; otherwise, only clears the stock textbox.
        /// </summary>
        /// <param name="clearAll">If true, clears all fields.</param>
        private void ClearInputFields(bool clearAll = true)
        {
            if (clearAll)
            {
                txtProductName.Clear();
                txtPrice.Clear();
            }
            txtStock.Clear();
        }

        private string SafeGetString(DataGridViewCell cell)
        {
            return cell?.Value?.ToString() ?? string.Empty;
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }
}
