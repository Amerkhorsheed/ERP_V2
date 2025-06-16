using DevExpress.XtraEditors;
using System;
using System.Linq;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class FrmPayment : XtraForm
    {
        // Business logic objects.
        private SalesBLL salesBll = new SalesBLL();
        private CustomerBLL customerBll = new CustomerBLL();

        // Data transfer objects.
        private SalesDTO salesDto = new SalesDTO();
        private CustomerDTO customerDto = new CustomerDTO();

        // The sales detail that we will build from the user’s input.
        private SalesDetailDTO saleDetail = new SalesDetailDTO();

        // Calculated total price (Price * SalesAmount)
        private int totalPrice = 0;

        public FrmPayment()
        {
            InitializeComponent();
        }

        private void FrmPayment_Load(object sender, EventArgs e)
        {
            try
            {
                // Load data for products and customers.
                salesDto = salesBll.Select();
                customerDto = customerBll.Select();

                // Setup product grid.
                gridProduct.DataSource = salesDto.Products;
                gridProduct.Columns["ProductName"].HeaderText = "Product Name";
                gridProduct.Columns["CategoryName"].HeaderText = "Category Name";
                gridProduct.Columns["stockAmount"].HeaderText = "Stock Amount";
                gridProduct.Columns["price"].Visible = false;
                gridProduct.Columns["ProductID"].Visible = false;
                gridProduct.Columns["CategoryID"].Visible = false;
                gridProduct.Columns["isCategoryDeleted"].Visible = false;
                gridProduct.Columns["Sale_Price"].HeaderText = "Sales Price";
                gridProduct.Columns["MinQty"].HeaderText = "Max Sale Qty";
                gridProduct.Columns["MaxDiscount"].HeaderText = "Max Discount";

                // Setup customer grid.
                gridCustomer.DataSource = customerDto.Customers;
                if (gridCustomer.Columns.Count > 0)
                    gridCustomer.Columns[0].Visible = false;
                if (gridCustomer.Columns.Count > 1)
                    gridCustomer.Columns[1].HeaderText = "Customer Name";

                // Set default payment option to full.
                radFullPayment.Checked = true;
                txtPaidAmount.Enabled = false;
                txtRemaining.Enabled = false;
                txtPaidAmount.Text = "0";
                txtRemaining.Text = "0";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // When a product row is selected, update saleDetail with product info.
        private void gridProduct_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= gridProduct.Rows.Count)
                    return;

                var row = gridProduct.Rows[e.RowIndex];
                var product = row.DataBoundItem as ProductDetailDTO;
                if (product != null)
                {
                    saleDetail.ProductID = product.ProductID;
                    saleDetail.ProductName = product.ProductName;
                    saleDetail.StockAmount = product.stockAmount;
                    saleDetail.Price = (int)product.Sale_Price;
                    saleDetail.MinQty = product.MinQty;
                    saleDetail.MaxDiscount = product.MaxDiscount;

                    txtProductName.Text = saleDetail.ProductName;
                    txtPrice.Text = saleDetail.Price.ToString();
                    txtStock.Text = saleDetail.StockAmount.ToString();
                }
                else
                {
                    XtraMessageBox.Show("Selected product data is invalid.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error selecting product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // When a customer row is selected, update saleDetail with customer info.
        private void gridCustomer_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= gridCustomer.Rows.Count)
                    return;

                var row = gridCustomer.Rows[e.RowIndex];
                if (row.Cells.Count < 2)
                {
                    XtraMessageBox.Show("Customer data is incomplete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var customerNameObj = row.Cells[1].Value;
                if (customerNameObj == null)
                {
                    XtraMessageBox.Show("Customer name is missing.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                saleDetail.CustomerName = customerNameObj.ToString();

                var customerIDObj = row.Cells[0].Value;
                if (customerIDObj == null || !int.TryParse(customerIDObj.ToString(), out int customerId))
                {
                    XtraMessageBox.Show("Customer ID is missing or invalid.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                saleDetail.CustomerID = customerId;

                txtCustomerName.Text = saleDetail.CustomerName;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error selecting customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // When the user enters the sales quantity, recalc total price.
        private void txtSalesAmount_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtSalesAmount.Text, out int qty))
            {
                totalPrice = saleDetail.Price * qty;
                lblTotalPrice.Text = $"Total Price: {totalPrice}";
            }
            else
            {
                lblTotalPrice.Text = "Total Price: 0";
            }
        }

        // Payment radio buttons.
        private void radFullPayment_CheckedChanged(object sender, EventArgs e)
        {
            if (radFullPayment.Checked)
            {
                txtPaidAmount.Enabled = false;
                txtRemaining.Enabled = false;
                // For full payment, paid equals total.
                txtPaidAmount.Text = totalPrice.ToString();
                txtRemaining.Text = "0";
            }
        }

        private void radPartialPayment_CheckedChanged(object sender, EventArgs e)
        {
            if (radPartialPayment.Checked)
            {
                txtPaidAmount.Enabled = true;
                txtRemaining.Enabled = false; // Auto-calculated.
                txtPaidAmount.Text = string.Empty;
                txtRemaining.Text = string.Empty;
            }
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtPaidAmount.Text, out int paid))
            {
                int remaining = totalPrice - paid;
                if (remaining < 0)
                    remaining = 0;
                txtRemaining.Text = remaining.ToString();
            }
            else
            {
                txtRemaining.Text = string.Empty;
            }
        }

        // Save button – creates a sale and updates the customer's outstanding (baky) amount.
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate product selection.
                if (saleDetail.ProductID == 0)
                {
                    XtraMessageBox.Show("Please select a product.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Validate customer selection.
                if (saleDetail.CustomerID == 0)
                {
                    XtraMessageBox.Show("Please select a customer.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Validate sales quantity.
                if (string.IsNullOrWhiteSpace(txtSalesAmount.Text) || !int.TryParse(txtSalesAmount.Text, out int salesQty))
                {
                    XtraMessageBox.Show("Please enter a valid sales quantity.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (salesQty > saleDetail.MinQty)
                {
                    XtraMessageBox.Show($"Sales quantity cannot exceed {saleDetail.MinQty}.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (salesQty > saleDetail.StockAmount)
                {
                    XtraMessageBox.Show("Not enough stock for the selected product.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                saleDetail.SalesAmount = salesQty;
                // Parse discount if provided.
                float enteredDiscount = 0;
                if (!string.IsNullOrWhiteSpace(txtDiscount.Text) && !float.TryParse(txtDiscount.Text, out enteredDiscount))
                {
                    XtraMessageBox.Show("Invalid discount value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (enteredDiscount > saleDetail.MaxDiscount)
                {
                    XtraMessageBox.Show($"Discount cannot exceed {saleDetail.MaxDiscount}.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                saleDetail.MaxDiscount = enteredDiscount;

                // Calculate total price.
                totalPrice = saleDetail.Price * salesQty;
                int paidAmount = 0, remaining = 0;
                if (radFullPayment.Checked)
                {
                    paidAmount = totalPrice;
                    remaining = 0;
                    // For full payment, Price remains the full sales price.
                }
                else if (radPartialPayment.Checked)
                {
                    if (!int.TryParse(txtPaidAmount.Text, out paidAmount))
                    {
                        XtraMessageBox.Show("Please enter a valid paid amount.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (paidAmount <= 0 || paidAmount >= totalPrice)
                    {
                        XtraMessageBox.Show("For partial payment, the paid amount must be greater than zero and less than the total price.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    remaining = totalPrice - paidAmount;
                    // IMPORTANT: For partial payment, update the Price field so profit is calculated based on the actual payment.
                    saleDetail.Price = paidAmount;
                }
                saleDetail.Madfou3 = paidAmount;
                saleDetail.Baky = remaining;
                saleDetail.SalesDate = DateTime.Today;

                // Insert the sale.
                if (salesBll.Insert(saleDetail))
                {
                    // Reload the customer from the database to ensure fresh data.
                    var latestCustomerDto = customerBll.Select();
                    var cust = latestCustomerDto.Customers.FirstOrDefault(c => c.ID == saleDetail.CustomerID);
                    long currentBaky = cust != null && cust.baky.HasValue ? cust.baky.Value : 0;
                    // Accumulate the new outstanding amount.
                    long newBaky = currentBaky + remaining;

                    // Build updated customer DTO.
                    CustomerDetailDTO updatedCustomer = new CustomerDetailDTO
                    {
                        ID = saleDetail.CustomerID,
                        CustomerName = cust.CustomerName,
                        Cust_Address = cust.Cust_Address,
                        Cust_Phone = cust.Cust_Phone,
                        Notes = cust.Notes,
                        baky = newBaky
                    };

                    // Update the customer record.
                    if (!customerBll.Update(updatedCustomer))
                    {
                        XtraMessageBox.Show("Sale recorded, but failed to update customer outstanding balance.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        XtraMessageBox.Show("Sale recorded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show("Failed to record sale.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error saving sale: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
