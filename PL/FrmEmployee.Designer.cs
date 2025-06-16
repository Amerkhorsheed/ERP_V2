namespace Erp_V1
{
    partial class FrmEmployee
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // --- Modern Design Setup ---
            var bgColor = System.Drawing.Color.FromArgb(245, 248, 250);
            var panelColor = System.Drawing.Color.White;
            var primaryColor = System.Drawing.Color.FromArgb(0, 150, 136); // Teal
            var accentColor = System.Drawing.Color.FromArgb(255, 152, 0); // Orange
            var textColor = System.Drawing.Color.FromArgb(55, 71, 79);
            var whiteColor = System.Drawing.Color.White;
            var mainFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            var labelFont = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            var buttonFont = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            // --- Control Initialization ---
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlImageContainer = new System.Windows.Forms.Panel();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.btnBrowseImage = new System.Windows.Forms.Button();
            this.lblUserNo = new System.Windows.Forms.Label();
            this.txtUserNo = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblSurname = new System.Windows.Forms.Label();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.lblBirthDay = new System.Windows.Forms.Label();
            this.dtpBirthDay = new System.Windows.Forms.DateTimePicker();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblSalary = new System.Windows.Forms.Label();
            this.txtSalary = new System.Windows.Forms.TextBox();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.lblPosition = new System.Windows.Forms.Label();
            this.cmbPosition = new System.Windows.Forms.ComboBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.pnlLeft.SuspendLayout();
            this.pnlImageContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.pnlRight.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = panelColor;
            this.pnlLeft.Controls.Add(this.pnlImageContainer);
            this.pnlLeft.Controls.Add(this.lblUserNo);
            this.pnlLeft.Controls.Add(this.txtUserNo);
            this.pnlLeft.Controls.Add(this.lblName);
            this.pnlLeft.Controls.Add(this.txtName);
            this.pnlLeft.Controls.Add(this.lblSurname);
            this.pnlLeft.Controls.Add(this.txtSurname);
            this.pnlLeft.Controls.Add(this.lblPassword);
            this.pnlLeft.Controls.Add(this.txtPassword);
            this.pnlLeft.Location = new System.Drawing.Point(15, 15);
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(10);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(340, 480);
            this.pnlLeft.TabIndex = 0;
            // 
            // pnlImageContainer
            // 
            this.pnlImageContainer.BackColor = primaryColor;
            this.pnlImageContainer.Controls.Add(this.picImage);
            this.pnlImageContainer.Controls.Add(this.btnBrowseImage);
            this.pnlImageContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlImageContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlImageContainer.Name = "pnlImageContainer";
            this.pnlImageContainer.Size = new System.Drawing.Size(340, 160);
            this.pnlImageContainer.TabIndex = 8;
            // 
            // picImage
            // 
            this.picImage.BackColor = System.Drawing.Color.Gainsboro;
            this.picImage.Location = new System.Drawing.Point(115, 15);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(110, 110);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picImage.TabIndex = 13;
            this.picImage.TabStop = false;
            // 
            // btnBrowseImage
            // 
            this.btnBrowseImage.BackColor = accentColor;
            this.btnBrowseImage.FlatAppearance.BorderSize = 0;
            this.btnBrowseImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseImage.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseImage.ForeColor = whiteColor;
            this.btnBrowseImage.Location = new System.Drawing.Point(125, 128);
            this.btnBrowseImage.Name = "btnBrowseImage";
            this.btnBrowseImage.Size = new System.Drawing.Size(90, 25);
            this.btnBrowseImage.TabIndex = 15;
            this.btnBrowseImage.Text = "BROWSE...";
            this.btnBrowseImage.UseVisualStyleBackColor = false;
            this.btnBrowseImage.Click += new System.EventHandler(this.btnBrowseImage_Click);
            // 
            // lblUserNo
            // 
            this.lblUserNo.AutoSize = true;
            this.lblUserNo.Font = labelFont;
            this.lblUserNo.ForeColor = textColor;
            this.lblUserNo.Location = new System.Drawing.Point(40, 185);
            this.lblUserNo.Name = "lblUserNo";
            this.lblUserNo.Size = new System.Drawing.Size(68, 19);
            this.lblUserNo.TabIndex = 0;
            this.lblUserNo.Text = "User No *";
            // 
            // txtUserNo
            // 
            this.txtUserNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserNo.Font = mainFont;
            this.txtUserNo.Location = new System.Drawing.Point(44, 208);
            this.txtUserNo.MaxLength = 6;
            this.txtUserNo.Name = "txtUserNo";
            this.txtUserNo.Size = new System.Drawing.Size(250, 25);
            this.txtUserNo.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = labelFont;
            this.lblName.ForeColor = textColor;
            this.lblName.Location = new System.Drawing.Point(40, 250);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(56, 19);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name *";
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = mainFont;
            this.txtName.Location = new System.Drawing.Point(44, 273);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(250, 25);
            this.txtName.TabIndex = 3;
            // 
            // lblSurname
            // 
            this.lblSurname.AutoSize = true;
            this.lblSurname.Font = labelFont;
            this.lblSurname.ForeColor = textColor;
            this.lblSurname.Location = new System.Drawing.Point(40, 315);
            this.lblSurname.Name = "lblSurname";
            this.lblSurname.Size = new System.Drawing.Size(77, 19);
            this.lblSurname.TabIndex = 4;
            this.lblSurname.Text = "Surname *";
            // 
            // txtSurname
            // 
            this.txtSurname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSurname.Font = mainFont;
            this.txtSurname.Location = new System.Drawing.Point(44, 338);
            this.txtSurname.MaxLength = 50;
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(250, 25);
            this.txtSurname.TabIndex = 5;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = labelFont;
            this.lblPassword.ForeColor = textColor;
            this.lblPassword.Location = new System.Drawing.Point(40, 380);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(79, 19);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "Password *";
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = mainFont;
            this.txtPassword.Location = new System.Drawing.Point(44, 403);
            this.txtPassword.MaxLength = 20;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(250, 25);
            this.txtPassword.TabIndex = 7;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = panelColor;
            this.pnlRight.Controls.Add(this.lblBirthDay);
            this.pnlRight.Controls.Add(this.dtpBirthDay);
            this.pnlRight.Controls.Add(this.lblAddress);
            this.pnlRight.Controls.Add(this.txtAddress);
            this.pnlRight.Controls.Add(this.lblSalary);
            this.pnlRight.Controls.Add(this.txtSalary);
            this.pnlRight.Controls.Add(this.lblDepartment);
            this.pnlRight.Controls.Add(this.cmbDepartment);
            this.pnlRight.Controls.Add(this.lblPosition);
            this.pnlRight.Controls.Add(this.cmbPosition);
            this.pnlRight.Controls.Add(this.lblRole);
            this.pnlRight.Controls.Add(this.cmbRole);
            this.pnlRight.Location = new System.Drawing.Point(375, 15);
            this.pnlRight.Margin = new System.Windows.Forms.Padding(10);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(340, 480);
            this.pnlRight.TabIndex = 1;
            // 
            // lblBirthDay
            // 
            this.lblBirthDay.AutoSize = true;
            this.lblBirthDay.Font = labelFont;
            this.lblBirthDay.ForeColor = textColor;
            this.lblBirthDay.Location = new System.Drawing.Point(40, 30);
            this.lblBirthDay.Name = "lblBirthDay";
            this.lblBirthDay.Size = new System.Drawing.Size(69, 19);
            this.lblBirthDay.TabIndex = 8;
            this.lblBirthDay.Text = "Birth Day";
            // 
            // dtpBirthDay
            // 
            this.dtpBirthDay.Font = mainFont;
            this.dtpBirthDay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBirthDay.Location = new System.Drawing.Point(44, 53);
            this.dtpBirthDay.Name = "dtpBirthDay";
            this.dtpBirthDay.Size = new System.Drawing.Size(250, 25);
            this.dtpBirthDay.TabIndex = 9;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = labelFont;
            this.lblAddress.ForeColor = textColor;
            this.lblAddress.Location = new System.Drawing.Point(40, 95);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(68, 19);
            this.lblAddress.TabIndex = 10;
            this.lblAddress.Text = "Address *";
            // 
            // txtAddress
            // 
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Font = mainFont;
            this.txtAddress.Location = new System.Drawing.Point(44, 118);
            this.txtAddress.MaxLength = 200;
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAddress.Size = new System.Drawing.Size(250, 70);
            this.txtAddress.TabIndex = 11;
            // 
            // lblSalary
            // 
            this.lblSalary.AutoSize = true;
            this.lblSalary.Font = labelFont;
            this.lblSalary.ForeColor = textColor;
            this.lblSalary.Location = new System.Drawing.Point(40, 205);
            this.lblSalary.Name = "lblSalary";
            this.lblSalary.Size = new System.Drawing.Size(58, 19);
            this.lblSalary.TabIndex = 16;
            this.lblSalary.Text = "Salary *";
            // 
            // txtSalary
            // 
            this.txtSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSalary.Font = mainFont;
            this.txtSalary.Location = new System.Drawing.Point(44, 228);
            this.txtSalary.MaxLength = 10;
            this.txtSalary.Name = "txtSalary";
            this.txtSalary.Size = new System.Drawing.Size(250, 25);
            this.txtSalary.TabIndex = 17;
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Font = labelFont;
            this.lblDepartment.ForeColor = textColor;
            this.lblDepartment.Location = new System.Drawing.Point(40, 270);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(96, 19);
            this.lblDepartment.TabIndex = 18;
            this.lblDepartment.Text = "Department *";
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.FlatStyle = System.Windows.Forms.FlatStyle.System; // Use system for better dropdown rendering
            this.cmbDepartment.Font = mainFont;
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Location = new System.Drawing.Point(44, 293);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(250, 25);
            this.cmbDepartment.TabIndex = 19;
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.Font = labelFont;
            this.lblPosition.ForeColor = textColor;
            this.lblPosition.Location = new System.Drawing.Point(40, 335);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(71, 19);
            this.lblPosition.TabIndex = 20;
            this.lblPosition.Text = "Position *";
            // 
            // cmbPosition
            // 
            this.cmbPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPosition.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbPosition.Font = mainFont;
            this.cmbPosition.FormattingEnabled = true;
            this.cmbPosition.Location = new System.Drawing.Point(44, 358);
            this.cmbPosition.Name = "cmbPosition";
            this.cmbPosition.Size = new System.Drawing.Size(250, 25);
            this.cmbPosition.TabIndex = 21;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = labelFont;
            this.lblRole.ForeColor = textColor;
            this.lblRole.Location = new System.Drawing.Point(40, 400);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(48, 19);
            this.lblRole.TabIndex = 22;
            this.lblRole.Text = "Role *";
            // 
            // cmbRole
            // 
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbRole.Font = mainFont;
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Location = new System.Drawing.Point(44, 423);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(250, 25);
            this.cmbRole.TabIndex = 23;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = primaryColor;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = buttonFont;
            this.btnSave.ForeColor = whiteColor;
            this.btnSave.Location = new System.Drawing.Point(485, 510);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 40);
            this.btnSave.TabIndex = 24;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = buttonFont;
            this.btnCancel.ForeColor = textColor;
            this.btnCancel.Location = new System.Drawing.Point(605, 510);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 40);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtImagePath
            // 
            this.txtImagePath.Location = new System.Drawing.Point(-500, -500); // Hidden
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.ReadOnly = true;
            this.txtImagePath.Size = new System.Drawing.Size(100, 20);
            this.txtImagePath.TabIndex = 14;
            this.txtImagePath.Visible = false;
            // 
            // FrmEmployee
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None; // Use None for precise control
            this.BackColor = bgColor;
            this.ClientSize = new System.Drawing.Size(730, 565);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.txtImagePath);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEmployee";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Employee Details";
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.pnlImageContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // --- All Controls Declared ---
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlImageContainer;
        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.Button btnBrowseImage;
        private System.Windows.Forms.Label lblUserNo;
        private System.Windows.Forms.TextBox txtUserNo;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblSurname;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblBirthDay;
        private System.Windows.Forms.DateTimePicker dtpBirthDay;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblSalary;
        private System.Windows.Forms.TextBox txtSalary;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.ComboBox cmbPosition;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtImagePath;
    }
}