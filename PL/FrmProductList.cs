using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class FrmProductList : DevExpress.XtraEditors.XtraForm
    {
        public FrmProductList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FrmProduct frm = new FrmProduct();
            frm.dto = dto;
            frm.ShowDialog();
            dto = bll.Select();
            dataGridView1.DataSource = dto.Products;
            CleanFilters();
        }

        ProductBLL bll = new ProductBLL();
        ProductDTO dto = new ProductDTO();

        private void FrmProductList_Load(object sender, EventArgs e)
        {
            dto = bll.Select();
            cmbCategory.DataSource = dto.Categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "ID";
            cmbCategory.SelectedIndex = -1;
            dataGridView1.DataSource = dto.Products;
            dataGridView1.Columns[0].HeaderText = "Product Name";
            dataGridView1.Columns[1].HeaderText = "Category Name";
            dataGridView1.Columns[2].HeaderText = "Stock Amount";
            dataGridView1.Columns[3].HeaderText = "Price";
            // Hide columns that are not needed in the grid view.
            dataGridView1.Columns[4].Visible = false; // ProductID
            dataGridView1.Columns[5].Visible = false; // CategoryID
            dataGridView1.Columns[6].Visible = false; // isCategoryDeleted
            // The new fields (Sale_Price, MinQty, MaxDiscount) will be available in the DTO even if not displayed.
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Start with the full list of products
            List<ProductDetailDTO> list = dto.Products;

            // Filter by Product Name if the textbox is not empty
            if (txtProductName.Text.Trim() != "")
            {
                list = list.Where(x => x.ProductName.Contains(txtProductName.Text.Trim())).ToList();
            }

            // Filter by Category if a category is selected
            if (cmbCategory.SelectedIndex != -1)
            {
                list = list.Where(x => x.CategoryID == Convert.ToInt32(cmbCategory.SelectedValue)).ToList();
            }

            // Filter by Price if the price textbox is not empty
            if (txtPrice.Text.Trim() != "")
            {
                int price = Convert.ToInt32(txtPrice.Text);
                if (rbPriceEquals.Checked)
                    list = list.Where(x => x.price == price).ToList();
                else if (rbPriceMore.Checked)
                    list = list.Where(x => x.price > price).ToList();
                else if (rbPriceLess.Checked)
                    list = list.Where(x => x.price < price).ToList();
                else
                    MessageBox.Show("Please select a criterion from the price group");
            }

            // Filter by Stock if the stock textbox is not empty
            if (txtStock.Text.Trim() != "")
            {
                int stock = Convert.ToInt32(txtStock.Text);
                if (rbStockEqual.Checked)
                    list = list.Where(x => x.stockAmount == stock).ToList();
                else if (rbStockMore.Checked)
                    list = list.Where(x => x.stockAmount > stock).ToList();
                else if (rbStockLess.Checked)
                    list = list.Where(x => x.stockAmount < stock).ToList();
                else
                    MessageBox.Show("Please select a criterion from the stock group");
            }

            // Update the dataGridView with the filtered list
            dataGridView1.DataSource = list;
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            CleanFilters();
        }

        private void CleanFilters()
        {
            txtPrice.Clear();
            txtStock.Clear();
            txtProductName.Clear();
            cmbCategory.SelectedIndex = -1;
            rbPriceEquals.Checked = false;
            rbPriceLess.Checked = false;
            rbStockEqual.Checked = false;
            rbStockMore.Checked = false;
            rbStockLess.Checked = false;
            rbPriceMore.Checked = false;
            dataGridView1.DataSource = dto.Products;
        }

        // Use DataBoundItem to capture the full product DTO including new fields.
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail = dataGridView1.Rows[e.RowIndex].DataBoundItem as ProductDetailDTO;
        }

        ProductDetailDTO detail = new ProductDetailDTO();

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.ProductID == 0)
                MessageBox.Show("Please Select a Product From Table");
            else
            {
                FrmProduct frm = new FrmProduct();
                frm.isUpdate = true;
                frm.detail = detail;
                frm.dto = dto;
                this.Hide();
                frm.ShowDialog();
                this.Visible = false;
                bll = new ProductBLL();
                dto = bll.Select();
                dataGridView1.DataSource = dto.Products;
                CleanFilters();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (detail.ProductID == 0)
                MessageBox.Show("Please Select a product from table");
            else
            {
                DialogResult result = MessageBox.Show("Are you Sure?", "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (bll.Delete(detail))
                    {
                        MessageBox.Show("Product Was Deleted");
                        bll = new ProductBLL();
                        dto = bll.Select();
                        dataGridView1.DataSource = dto.Products;
                        cmbCategory.DataSource = dto.Categories;
                        CleanFilters();
                    }
                }
            }
        }
    }
}
