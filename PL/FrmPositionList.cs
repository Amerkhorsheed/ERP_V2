using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class FrmPositionList : Form
    {
        private readonly PositionBLL _bll = new PositionBLL();
        private PositionDTO _dto = new PositionDTO();
        private PositionDetailDTO _detail;
        public FrmPositionList()
        {
            InitializeComponent();
        }

        private void FrmPositionList_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }
        private void LoadGrid()
        {
            _dto = _bll.Select();
            dataGridView1.DataSource = _dto.Positions;
            // Hide internal ID and department ID columns
            dataGridView1.Columns[nameof(PositionDetailDTO.PositionID)].Visible = false;
            dataGridView1.Columns[nameof(PositionDetailDTO.DepartmentID)].Visible = false;

            // Set user-friendly headers
            dataGridView1.Columns[nameof(PositionDetailDTO.PositionName)].HeaderText = "Position Name";
            dataGridView1.Columns[nameof(PositionDetailDTO.DepartmentName)].HeaderText = "Department";
        }

        private void txtPositionFilter_TextChanged(object sender, EventArgs e)
        {
            var filtered = _dto.Positions
                .Where(x => x.PositionName.IndexOf(txtPositionFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
            dataGridView1.DataSource = filtered;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _detail = dataGridView1.Rows[e.RowIndex].DataBoundItem as PositionDetailDTO;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var frm = new FrmPosition();
            frm.ShowDialog();
            LoadGrid();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_detail == null || _detail.PositionID == 0)
            {
                XtraMessageBox.Show("Please select a position to update.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var frm = new FrmPosition { Detail = _detail, IsUpdate = true };
            frm.ShowDialog();
            LoadGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_detail == null || _detail.PositionID == 0)
            {
                XtraMessageBox.Show("Please select a position to delete.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var result = XtraMessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes && _bll.Delete(_detail))
            {
                XtraMessageBox.Show("Position deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
