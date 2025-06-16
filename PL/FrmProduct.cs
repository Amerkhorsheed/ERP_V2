using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class FrmProduct : DevExpress.XtraEditors.XtraForm
    {
        public FrmProduct()
        {
            InitializeComponent();
        }

        // Product Data Transfer Object (DTO) and BLL
        public ProductDTO dto = new ProductDTO();
        public ProductDetailDTO detail = new ProductDetailDTO();
        public bool isUpdate = false; // Flag to determine whether we are updating or adding a product

        // Product Business Logic Layer (BLL)
        ProductBLL bll = new ProductBLL();

        private void FrmProduct_Load(object sender, EventArgs e)
        {
            // Load categories and product details for update
            dto = bll.Select();
            cmbCategory.DataSource = dto.Categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "ID";
            cmbCategory.SelectedIndex = -1;

            if (isUpdate)
            {
                // Populate the form with the product details if updating
                txtProductName.Text = detail.ProductName;
                txtPrice.Text = detail.price.ToString();
                cmbCategory.SelectedValue = detail.CategoryID;
                // Populate the new fields
                txtBuyprice.Text = detail.Sale_Price.ToString();
                txtQtysale.Text = detail.MinQty.ToString();
                txtDiscount.Text = detail.MaxDiscount.ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Close the form
            this.Close();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numbers for the price field
            e.Handled = General.isNumber(e);
        }

        private void txtProductName_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            // Validate product name using GeneralN.IsValidProductName
            e.Handled = !GeneralN.IsValidName(e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Declare the priceValue variable here
            int priceValue;

            // Validate required inputs
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Product Name is empty. Please enter a product name.");
                return;
            }
            else if (cmbCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a category.");
                return;
            }
            else if (string.IsNullOrWhiteSpace(txtPrice.Text) ||
                     !int.TryParse(txtPrice.Text, out priceValue) || priceValue <= 0)
            {
                MessageBox.Show("Invalid price. Please enter a valid positive number.");
                return;
            }

            // Validate new fields using TryParse; if a field is left empty assume its current value remains.
            float salePriceValue = detail.Sale_Price;
            float minQtyValue = detail.MinQty;
            float maxDiscountValue = detail.MaxDiscount;
            if (!string.IsNullOrWhiteSpace(txtBuyprice.Text))
            {
                if (!float.TryParse(txtBuyprice.Text, out salePriceValue))
                {
                    MessageBox.Show("Invalid buy price. Please enter a valid number.");
                    return;
                }
            }
            if (!string.IsNullOrWhiteSpace(txtQtysale.Text))
            {
                if (!float.TryParse(txtQtysale.Text, out minQtyValue))
                {
                    MessageBox.Show("Invalid quantity sale. Please enter a valid number.");
                    return;
                }
            }
            if (!string.IsNullOrWhiteSpace(txtDiscount.Text))
            {
                if (!float.TryParse(txtDiscount.Text, out maxDiscountValue))
                {
                    MessageBox.Show("Invalid discount. Please enter a valid number.");
                    return;
                }
            }

            // Handle Add or Update operations
            if (!isUpdate) // Add new product
            {
                ProductDetailDTO product = new ProductDetailDTO
                {
                    ProductName = txtProductName.Text,
                    CategoryID = Convert.ToInt32(cmbCategory.SelectedValue),
                    price = priceValue,
                    Sale_Price = salePriceValue,
                    MinQty = minQtyValue,
                    MaxDiscount = maxDiscountValue
                };

                if (bll.Insert(product))
                {
                    MessageBox.Show("Product was added successfully.");
                    ResetFields();
                }
            }
            else // Update existing product
            {
                bool changesMade = false;

                // Check each field and update only if different
                if (detail.ProductName != txtProductName.Text)
                {
                    detail.ProductName = txtProductName.Text;
                    changesMade = true;
                }

                int newCategoryID = Convert.ToInt32(cmbCategory.SelectedValue);
                if (detail.CategoryID != newCategoryID)
                {
                    detail.CategoryID = newCategoryID;
                    changesMade = true;
                }

                if (detail.price != priceValue)
                {
                    detail.price = priceValue;
                    changesMade = true;
                }

                if (detail.Sale_Price != salePriceValue)
                {
                    detail.Sale_Price = salePriceValue;
                    changesMade = true;
                }

                if (detail.MinQty != minQtyValue)
                {
                    detail.MinQty = minQtyValue;
                    changesMade = true;
                }

                if (detail.MaxDiscount != maxDiscountValue)
                {
                    detail.MaxDiscount = maxDiscountValue;
                    changesMade = true;
                }

                if (!changesMade)
                {
                    MessageBox.Show("There are no changes to save.");
                    return;
                }

                if (bll.Update(detail))
                {
                    MessageBox.Show("Product was updated successfully.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update Product.");
                }
            }
        }

        private void FrmProduct_KeyDown(object sender, KeyEventArgs e)
        {
            // Allow the user to press Enter to trigger the Save action
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSave_Click(sender, e);
            }
        }

        private void ResetFields()
        {
            // Clear input fields after adding a product
            txtProductName.Clear();
            txtPrice.Clear();
            txtBuyprice.Clear();
            txtQtysale.Clear();
            txtDiscount.Clear();
            cmbCategory.SelectedIndex = -1;
        }
    }
}
