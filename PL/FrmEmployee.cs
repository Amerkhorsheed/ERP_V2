using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using System;
using System.Drawing; // Needed for Image handling if you copy/resize
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class FrmEmployee : Form
    {
        private readonly EmployeeBLL _employeeBll = new EmployeeBLL();
        private EmployeeDetailDTO _currentEmployeeDto = null;
        private bool _isEditMode = false;
        private string _tempImagePath = ""; // To store chosen image path

        public FrmEmployee()
        {
            InitializeComponent();
            LoadLookups();
            Text = "Add New Employee";
            btnSave.Text = "SAVE";
        }

        /// <summary>
        /// Constructor overload for “Edit” mode. Pass in an existing DTO.
        /// </summary>
        public FrmEmployee(EmployeeDetailDTO dto) : this()
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            _currentEmployeeDto = dto;
            _isEditMode = true;
            Text = $"Edit Employee (ID: {dto.EmployeeID})";
            btnSave.Text = "UPDATE";
            txtUserNo.ReadOnly = true; // Usually, UserNo shouldn't be editable
            PopulateFields(dto);
        }

        private void LoadLookups()
        {
            try
            {
                var employeeDto = _employeeBll.Select(); // Assuming this fetches lookups

                cmbDepartment.DataSource = employeeDto.Departments;
                cmbDepartment.DisplayMember = "DepartmentName";
                cmbDepartment.ValueMember = "DepartmentID";
                cmbDepartment.SelectedIndex = -1; // Start with no selection

                cmbPosition.DataSource = employeeDto.Positions;
                cmbPosition.DisplayMember = "PositionName";
                cmbPosition.ValueMember = "PositionID";
                cmbPosition.SelectedIndex = -1;

                cmbRole.DataSource = employeeDto.Roles;
                cmbRole.DisplayMember = "RoleName";
                cmbRole.ValueMember = "RoleID";
                cmbRole.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading initial data: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close(); // Can't proceed if lookups fail
            }
        }

        private void PopulateFields(EmployeeDetailDTO dto)
        {
            txtUserNo.Text = dto.UserNo.ToString();
            txtName.Text = dto.Name;
            txtSurname.Text = dto.Surname;
            txtPassword.Text = dto.Password;
            dtpBirthDay.Value = dto.BirthDay ?? DateTime.Today;
            txtAddress.Text = dto.Address;
            txtSalary.Text = dto.Salary.ToString();

            // Safely set ComboBoxes
            cmbDepartment.SelectedValue = dto.DepartmentID;
            cmbPosition.SelectedValue = dto.PositionID;
            cmbRole.SelectedValue = dto.RoleID;

            // Load image
            txtImagePath.Text = dto.ImagePath; // Store original path
            _tempImagePath = dto.ImagePath;    // Store original path in temp too
            LoadImage(_tempImagePath);
        }

        private void LoadImage(string imagePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    picImage.ImageLocation = imagePath;
                }
                else
                {
                    picImage.ImageLocation = null; // Clear image if path is bad
                    picImage.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load image: {ex.Message}", "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                picImage.ImageLocation = null;
                picImage.Image = null;
            }
        }


        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                ofd.Title = "Select Employee Picture";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _tempImagePath = ofd.FileName; // Use temp path
                    LoadImage(_tempImagePath);
                }
            }
        }

        private bool ValidateInputs()
        {
            // UserNo
            if (!int.TryParse(txtUserNo.Text.Trim(), out int userNo) || userNo <= 0)
            {
                MessageBox.Show("Please enter a valid, positive numeric UserNo.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserNo.Focus();
                return false;
            }

            // Check uniqueness only if adding a new user
            // You'll need a BLL method like `IsUserNoUnique(userNo, currentId)`
            // if (!_isEditMode && !_employeeBll.IsUserNoUnique(userNo))
            // {
            //     MessageBox.Show("This UserNo already exists. Please choose another.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //     txtUserNo.Focus();
            //     return false;
            // }

            // Name & Surname
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSurname.Text))
            {
                MessageBox.Show("Surname cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSurname.Focus();
                return false;
            }

            // Password (optional: add complexity check)
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Password cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            // Address
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Address cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAddress.Focus();
                return false;
            }

            // Salary
            if (!int.TryParse(txtSalary.Text.Trim(), out int salary) || salary < 0)
            {
                MessageBox.Show("Please enter a valid, non-negative numeric salary.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSalary.Focus();
                return false;
            }

            // ComboBoxes
            if (cmbDepartment.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Department.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDepartment.Focus();
                return false;
            }
            if (cmbPosition.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Position.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPosition.Focus();
                return false;
            }
            if (cmbRole.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRole.Focus();
                return false;
            }

            return true; // All checks passed
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                return; // Stop if validation fails
            }

            // --- Optional: Handle Image Copying ---
            // It's best practice to copy the image to a standard location
            // and save *that* path, rather than the original browse path.
            // Example:
            // string finalImagePath = _tempImagePath;
            // if (!string.IsNullOrEmpty(_tempImagePath) && (_tempImagePath != txtImagePath.Text || _isEditMode))
            // {
            //     string imageDir = Path.Combine(Application.StartupPath, "EmployeeImages");
            //     if (!Directory.Exists(imageDir)) Directory.CreateDirectory(imageDir);
            //     string newFileName = $"{txtUserNo.Text.Trim()}_{Path.GetExtension(_tempImagePath)}";
            //     finalImagePath = Path.Combine(imageDir, newFileName);
            //     try { File.Copy(_tempImagePath, finalImagePath, true); }
            //     catch (Exception ex) { /* Handle copy error */ }
            // }
            // For now, we'll just use the _tempImagePath
            string finalImagePath = _tempImagePath;


            // Build DTO
            var dto = new EmployeeDetailDTO
            {
                UserNo = int.Parse(txtUserNo.Text.Trim()),
                Name = txtName.Text.Trim(),
                Surname = txtSurname.Text.Trim(),
                Password = txtPassword.Text, // Keep password as is (no trimming)
                BirthDay = dtpBirthDay.Value.Date,
                Address = txtAddress.Text.Trim(),
                ImagePath = finalImagePath, // Use the (potentially copied) path
                Salary = int.Parse(txtSalary.Text.Trim()),
                DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue),
                PositionID = Convert.ToInt32(cmbPosition.SelectedValue),
                RoleID = Convert.ToInt32(cmbRole.SelectedValue),
                // Names are usually looked up, but included if your BLL needs them
                DepartmentName = cmbDepartment.Text,
                PositionName = cmbPosition.Text,
                RoleName = cmbRole.Text
            };

            bool success;
            try
            {
                if (_isEditMode)
                {
                    dto.EmployeeID = _currentEmployeeDto.EmployeeID;
                    success = _employeeBll.Update(dto);
                }
                else
                {
                    success = _employeeBll.Insert(dto);
                }

                if (success)
                {
                    MessageBox.Show(_isEditMode ? "Employee updated successfully." : "Employee added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK; // Signal success to the calling form
                    Close();
                }
                else
                {
                    MessageBox.Show("Operation failed. An issue occurred during saving.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred:\n{ex.Message}", "Critical Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}