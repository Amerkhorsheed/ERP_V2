using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class frmMultiSales : DevExpress.XtraEditors.XtraForm
    {
        private SalesBLL salesBLL = new SalesBLL();
        private ProductBLL productBLL = new ProductBLL();
        private CustomerBLL customerBLL = new CustomerBLL();
        private CategoryBLL categoryBLL = new CategoryBLL();

        public frmMultiSales()
        {
            InitializeComponent();
            LoadComboBoxes();
            InitializeSalesDetailsGrid();
        }

        private void LoadComboBoxes()
        {
            // Load products
            var products = productBLL.Select().Products;
            cboProduct.DataSource = products;
            cboProduct.DisplayMember = "ProductName";
            cboProduct.ValueMember = "ProductID";

            // Load customers
            var customers = customerBLL.Select().Customers;
            cboCustomer.DataSource = customers;
            cboCustomer.DisplayMember = "CustomerName";
            cboCustomer.ValueMember = "ID";

            // Load categories
            var categories = categoryBLL.Select().Categories;
            cboCategory.DataSource = categories;
            cboCategory.DisplayMember = "CategoryName";
            cboCategory.ValueMember = "ID";
        }

        private void InitializeSalesDetailsGrid()
        {
            dgvSalesDetails.Columns.Clear();

            dgvSalesDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductName",
                HeaderText = "Product Name",
                Width = 200
            });

            dgvSalesDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                HeaderText = "Quantity",
                Width = 100
            });

            dgvSalesDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Price",
                HeaderText = "Price",
                Width = 100
            });
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboProduct.SelectedItem != null)
                {
                    var selectedProduct = (ProductDetailDTO)cboProduct.SelectedItem;

                    if (int.TryParse(txtQuantity.Text, out int quantity) && decimal.TryParse(txtPrice.Text, out decimal price))
                    {
                        dgvSalesDetails.Rows.Add(
                            selectedProduct.ProductName,
                            quantity,
                            price
                        );
                    }
                    else
                    {
                        MessageBox.Show("Please enter valid numeric values for Quantity and Price.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddSale_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedCustomer = (CustomerDetailDTO)cboCustomer.SelectedItem;
                var salesDetails = new List<SalesDetailDTO>();

                foreach (DataGridViewRow row in dgvSalesDetails.Rows)
                {
                    if (row.IsNewRow) continue;

                    var productName = row.Cells["ProductName"].Value.ToString();
                    if (int.TryParse(row.Cells["Quantity"].Value.ToString(), out int quantity) &&
                        decimal.TryParse(row.Cells["Price"].Value.ToString(), out decimal price))
                    {
                        var product = productBLL.Select().Products.FirstOrDefault(p => p.ProductName == productName);

                        if (product != null)
                        {
                            SalesDetailDTO salesDetail = new SalesDetailDTO
                            {
                                ProductID = product.ProductID,
                                CustomerID = selectedCustomer.ID,
                                CategoryID = product.CategoryID,
                                SalesAmount = quantity,
                                Price = (int)price,
                                SalesDate = dtpSalesDate.Value,
                                StockAmount = product.stockAmount
                            };

                            salesDetails.Add(salesDetail);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid quantity or price in the sales details grid.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Stop further processing if there's an error
                    }
                }

                foreach (var detail in salesDetails)
                {
                    salesBLL.Insert(detail);

                    // Update stock
                    var product = productBLL.Select().Products.FirstOrDefault(p => p.ProductID == detail.ProductID);
                    if (product != null)
                    {
                        product.stockAmount -= detail.SalesAmount;
                        productBLL.Update(product);
                    }
                }

                MessageBox.Show("Sales added successfully!");
                LoadSalesData();  // Refresh sales data grid
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the sale: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoadSales_Click(object sender, EventArgs e)
        {
            LoadSalesData();
        }

        private void LoadSalesData()
        {
            try
            {
                SalesDTO salesDTO = salesBLL.Select();
                dgvSales.DataSource = salesDTO.Sales.Select(s => new
                {
                    s.SalesID,
                    s.CustomerName,
                    s.ProductName,
                    s.CategoryName,
                    s.SalesAmount,
                    s.Price,
                    s.SalesDate
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading sales data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmMultiSales_Load(object sender, EventArgs e)
        {

        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numbers for the Quantity field
            e.Handled = General.isNumber(e);
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numbers for the Price field
            e.Handled = General.isNumber(e);
        }

    }
}
