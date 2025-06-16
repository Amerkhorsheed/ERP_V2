using DevExpress.XtraEditors;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class FrmPosition : XtraForm
    {
        private readonly PositionBLL _positionBll = new PositionBLL();
        private readonly DepartmentBLL _departmentBll = new DepartmentBLL();

        public PositionDetailDTO Detail { get; set; } = new PositionDetailDTO();
        public bool IsUpdate { get; set; } = false;

        public FrmPosition()
        {
            InitializeComponent();
            Load += FrmPosition_Load;
            KeyPreview = true;
            KeyDown += FrmPosition_KeyDown;
        }

        private void FrmPosition_Load(object sender, EventArgs e)
        {
            // Load departments
            var deptDto = _departmentBll.Select();
            lookupDepartment.Properties.DataSource = deptDto.Departments;
            lookupDepartment.Properties.DisplayMember = nameof(DepartmentDetailDTO.DepartmentName);
            lookupDepartment.Properties.ValueMember = nameof(DepartmentDetailDTO.DepartmentID);
            lookupDepartment.Properties.NullText = "[Select Department]";

            if (IsUpdate)
            {
                txtPositionName.Text = Detail.PositionName;
                lookupDepartment.EditValue = Detail.DepartmentID;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var name = txtPositionName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                XtraMessageBox.Show("Position name cannot be empty.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPositionName.Focus();
                return;
            }

            // Validate: letters and spaces only
            if (!Regex.IsMatch(name, "^[A-Za-z ]+$"))
            {
                XtraMessageBox.Show("Position name must contain only letters and spaces.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPositionName.Focus();
                return;
            }

            if (lookupDepartment.EditValue == null)
            {
                XtraMessageBox.Show("Please select a department.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lookupDepartment.ShowPopup();
                return;
            }

            var dto = new PositionDetailDTO
            {
                PositionID = Detail.PositionID,
                PositionName = name,
                DepartmentID = (int)lookupDepartment.EditValue
            };

            bool success;
            if (!IsUpdate)
            {
                success = _positionBll.Insert(dto);
                if (success)
                {
                    XtraMessageBox.Show("Position added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                }
                else
                {
                    XtraMessageBox.Show("Failed to add position.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (dto.PositionName != Detail.PositionName || dto.DepartmentID != Detail.DepartmentID)
                {
                    Detail.PositionName = dto.PositionName;
                    Detail.DepartmentID = dto.DepartmentID;
                }

                success = _positionBll.Update(Detail);
                if (success)
                {
                    XtraMessageBox.Show("Position updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    XtraMessageBox.Show("Failed to update position.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmPosition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSave_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void ClearForm()
        {
            txtPositionName.Clear();
            lookupDepartment.EditValue = null;
            txtPositionName.Focus();
        }
    }
}