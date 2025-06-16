using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class frmDepartmentSearch : Form
    {
        private readonly DepartmentBLL _departmentBLL = new DepartmentBLL();
        private List<DepartmentDetailDTO> _allDepartments;

        public DepartmentDetailDTO SelectedDepartment { get; private set; }

        public frmDepartmentSearch()
        {
            InitializeComponent();
            Text = "Search Departments";
            BackColor = Color.FromArgb(34, 40, 49);

            dgvSearchResults.EnableHeadersVisualStyles = false;
            dgvSearchResults.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(57, 62, 70);
            dgvSearchResults.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSearchResults.RowTemplate.Height = 28;
            dgvSearchResults.RowsDefaultCellStyle.BackColor = Color.FromArgb(50, 54, 61);
            dgvSearchResults.RowsDefaultCellStyle.ForeColor = Color.White;
        }

        private void frmDepartmentSearch_Load(object sender, EventArgs e)
        {
            _allDepartments = _departmentBLL.Select()
                .Departments
                .Where(d => !d.IsDeleted)
                .ToList();

            dgvSearchResults.DataSource = _allDepartments;
            FormatGrid();
        }

        private void FormatGrid()
        {
            // Rename your displayed columns
            //dgvDepartments.Columns["DepartmentID"].HeaderText = "ID";
            dgvSearchResults.Columns["DepartmentName"].HeaderText = "Name";

            // Hide any other auto‑generated properties (IsDeleted, DeletedDate, etc.)
            foreach (var key in new[] { "DepartmentID", "IsDeleted", "DeletedDate" })
            {
                if (dgvSearchResults.Columns.Contains(key))
                    dgvSearchResults.Columns[key].Visible = false;
            }

            // Stretch only the visible ones
            foreach (DataGridViewColumn col in dgvSearchResults.Columns)
            {
                if (col.Visible)
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var query = txtSearch.Text.Trim().ToLower();
            var filtered = string.IsNullOrEmpty(query)
                ? _allDepartments
                : _allDepartments.Where(d => d.DepartmentName.ToLower().Contains(query)).ToList();

            dgvSearchResults.DataSource = filtered;
        }

        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}