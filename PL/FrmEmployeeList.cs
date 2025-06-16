using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.DAL.DTO;

namespace Erp_V1
{
    public partial class FrmEmployeeList : Form
    {
        private readonly EmployeeBLL _employeeBll = new EmployeeBLL();
        private List<EmployeeDetailDTO> _employeesCache = new List<EmployeeDetailDTO>();

        public FrmEmployeeList()
        {
            InitializeComponent();
            // We load employees in the Load event to ensure the form is fully initialized
        }

        private void FrmEmployeeList_Load(object sender, EventArgs e)
        {
            LoadAllEmployees(); // Load data when the form loads
        }

        private void LoadAllEmployees(string searchKeyword = "")
        {
            try
            {
                var dto = _employeeBll.Select();
                _employeesCache = dto.Employees; // Cache for filtering
                BindToGrid(_employeesCache, searchKeyword);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load employees:\n{ex.Message}", "Loading Error 😢", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindToGrid(List<EmployeeDetailDTO> list, string keyword = "")
        {
            IEnumerable<EmployeeDetailDTO> filtered = list;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var lower = keyword.Trim().ToLower();
                filtered = list.Where(e =>
                    e.UserNo.ToString().Contains(lower) ||
                    (!string.IsNullOrEmpty(e.Name) && e.Name.ToLower().Contains(lower)) ||
                    (!string.IsNullOrEmpty(e.Surname) && e.Surname.ToLower().Contains(lower)) ||
                    (!string.IsNullOrEmpty(e.DepartmentName) && e.DepartmentName.ToLower().Contains(lower)) ||
                    (!string.IsNullOrEmpty(e.PositionName) && e.PositionName.ToLower().Contains(lower)) ||
                    (!string.IsNullOrEmpty(e.RoleName) && e.RoleName.ToLower().Contains(lower))
                );
            }

            // Prepare data for the grid
            var displayList = filtered
                .Select(e => new
                {
                    e.EmployeeID,
                    e.UserNo,
                    e.Name,
                    e.Surname,
                    e.DepartmentName,
                    e.PositionName,
                    e.RoleName,
                    Salary = e.Salary.ToString("C"), // Format as currency
                    BirthDay = e.BirthDay?.ToString("dd MMM yyyy"), // Better date format
                    e.Address,
                    e.ImagePath // Keep for data access, but hide it
                })
                .OrderBy(e => e.UserNo)
                .ToList();

            dgvEmployees.DataSource = null; // Clear previous data
            dgvEmployees.DataSource = displayList;

            // Configure columns (best done after setting DataSource)
            if (dgvEmployees.Columns.Count > 0)
            {
                dgvEmployees.Columns["EmployeeID"].Visible = false; // Usually hidden
                dgvEmployees.Columns["ImagePath"].Visible = false;
                dgvEmployees.Columns["Address"].Visible = false; // Can be long, maybe hide by default

                dgvEmployees.Columns["UserNo"].HeaderText = "User #";
                dgvEmployees.Columns["Name"].HeaderText = "First Name";
                dgvEmployees.Columns["Surname"].HeaderText = "Last Name";
                dgvEmployees.Columns["DepartmentName"].HeaderText = "Department";
                dgvEmployees.Columns["PositionName"].HeaderText = "Position";
                dgvEmployees.Columns["RoleName"].HeaderText = "Role";
                dgvEmployees.Columns["Salary"].HeaderText = "Salary";
                dgvEmployees.Columns["BirthDay"].HeaderText = "Birthday";

                // Adjust column fill weights or set fixed widths if needed
                dgvEmployees.Columns["UserNo"].FillWeight = 50;
                dgvEmployees.Columns["Name"].FillWeight = 100;
                dgvEmployees.Columns["Surname"].FillWeight = 100;
                dgvEmployees.Columns["DepartmentName"].FillWeight = 120;
                dgvEmployees.Columns["PositionName"].FillWeight = 120;
                dgvEmployees.Columns["RoleName"].FillWeight = 80;
                dgvEmployees.Columns["Salary"].FillWeight = 70;
                dgvEmployees.Columns["BirthDay"].FillWeight = 80;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Use the modernized FrmEmployee
            using (var frm = new FrmEmployee())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadAllEmployees(txtSearch.Text); // Refresh list
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.CurrentRow == null)
            {
                MessageBox.Show("Please select an employee to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedId = Convert.ToInt32(dgvEmployees.CurrentRow.Cells["EmployeeID"].Value);
            var dto = _employeesCache.FirstOrDefault(x => x.EmployeeID == selectedId);

            if (dto == null)
            {
                MessageBox.Show("Could not find the selected employee data.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Use the modernized FrmEmployee in Edit mode
            using (var frm = new FrmEmployee(dto))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadAllEmployees(txtSearch.Text); // Refresh list
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.CurrentRow == null)
            {
                MessageBox.Show("Please select an employee to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedId = Convert.ToInt32(dgvEmployees.CurrentRow.Cells["EmployeeID"].Value);
            var dto = _employeesCache.FirstOrDefault(x => x.EmployeeID == selectedId);

            if (dto == null)
            {
                MessageBox.Show("Could not find the selected employee data.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirm = MessageBox.Show($"🛑 Are you absolutely sure you want to delete:\n\n" +
                                          $"    {dto.Name} {dto.Surname} (User #{dto.UserNo})?\n\n" +
                                          $"This action might be irreversible!",
                                          "Confirm Deletion 💀",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Warning,
                                          MessageBoxDefaultButton.Button2); // Default to No

            if (confirm != DialogResult.Yes) return;

            try
            {
                // Assuming your BLL Delete method takes the DTO or just the ID
                bool success = _employeeBll.Delete(dto);
                if (success)
                {
                    MessageBox.Show("Employee has been deleted.", "Success ✅", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllEmployees(txtSearch.Text); // Refresh list
                }
                else
                {
                    MessageBox.Show("Delete operation failed.", "Error ❌", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting:\n{ex.Message}", "Exception 💥", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            BindToGrid(_employeesCache, txtSearch.Text); // Filter as user types
        }

        private void dgvEmployees_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // On double-click, trigger the Edit button's action
            if (e.RowIndex >= 0) // Ensure it's not the header row
            {
                btnEdit.PerformClick();
            }
        }
    }
}