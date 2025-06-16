using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class FrmCategoryList : DevExpress.XtraEditors.XtraForm
    {
        public FrmCategoryList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FrmCategory frmCategory = new FrmCategory();
            frmCategory.ShowDialog();
            dto = bll.Select();
            dataGridView1.DataSource = dto.Categories;
        }
        CategoryDTO dto=new CategoryDTO();
        CategoryBLL bll= new CategoryBLL();
        private void FrmCategoryList_Load(object sender, EventArgs e)
        {
            dto = bll.Select();
            dataGridView1.DataSource = dto.Categories;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Category Name";
        }

        private void txtCategoryName_TextChanged(object sender, EventArgs e)
        {
            List<CategoryDetailDTO> list = dto.Categories;
            list=list.Where(x=>x.CategoryName.Contains(txtCategoryName.Text)).ToList();
            dataGridView1.DataSource=list;
        }

        CategoryDetailDTO detail=new CategoryDetailDTO();
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail = new CategoryDetailDTO();
            detail.ID= Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            detail.CategoryName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.ID == 0)

                MessageBox.Show("Please Select a Category from table");
            else
            { 
                FrmCategory frm = new FrmCategory();
                frm.detail = detail;
                frm.isUpdate = true;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                bll = new CategoryBLL();
                dto = bll.Select();
                dataGridView1.DataSource = dto.Categories;
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (detail.ID == 0)
            {
                MessageBox.Show("Please select a category from the table");
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (bll.Delete(detail))
                    {
                        MessageBox.Show("Category was deleted");
                        bll = new CategoryBLL();
                        dto = bll.Select();
                        dataGridView1.DataSource = dto.Categories;
                        txtCategoryName.Clear();
                    }
                }
            }

        }
    }
}