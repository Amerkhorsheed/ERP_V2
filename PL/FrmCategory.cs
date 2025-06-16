using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class FrmCategory : DevExpress.XtraEditors.XtraForm
    {
        public FrmCategory()
        {
            InitializeComponent();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        CategoryBLL bll = new CategoryBLL();
        public CategoryDetailDTO detail = new CategoryDetailDTO();
        public bool isUpdate = false;

        private void FrmCategory_Load(object sender, EventArgs e)
        {
            if (isUpdate)
                txtCategoryName.Text = detail.CategoryName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate category name using GeneralN.IsValidCategoryName method
            string categoryName = txtCategoryName.Text.Trim();
            if (!GeneralN.IsValidCategoryName(categoryName))
            {
                MessageBox.Show("Invalid Category Name. It should only contain letters and spaces.");
                return;
            }

            if (categoryName == "")
            {
                MessageBox.Show("Category Name is Empty");
                return;
            }

            if (!isUpdate) // Add
            {
                CategoryDetailDTO category = new CategoryDetailDTO { CategoryName = categoryName };
                if (bll.Insert(category))
                {
                    MessageBox.Show("Category Was Added");
                    txtCategoryName.Clear();
                }
            }
            else if (isUpdate) // Update
            {
                if (detail.CategoryName == categoryName)
                {
                    MessageBox.Show("There is No Change");
                }
                else
                {
                    detail.CategoryName = categoryName;
                    if (bll.Update(detail))
                    {
                        MessageBox.Show("Category Was Updated");
                        this.Close();
                    }
                }
            }
        }

        private void FrmCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent the 'ding' sound when Enter is pressed
                btnSave_Click(sender, e);
            }
        }
        private void labelControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
