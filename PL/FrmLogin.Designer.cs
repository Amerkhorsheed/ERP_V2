namespace Erp_V1
{
    partial class FrmLogin
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
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.lblSlogan = new System.Windows.Forms.Label();
            this.lblAppName = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.pnlPassword = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.picPassword = new System.Windows.Forms.PictureBox();
            this.pnlUsername = new System.Windows.Forms.Panel();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.picUsername = new System.Windows.Forms.PictureBox();
            this.lblLogin = new System.Windows.Forms.Label();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.pnlRight.SuspendLayout();
            this.pnlPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPassword)).BeginInit();
            this.pnlUsername.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUsername)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(30)))), ((int)(((byte)(48)))));
            this.pnlLeft.Controls.Add(this.lblSlogan);
            this.pnlLeft.Controls.Add(this.lblAppName);
            this.pnlLeft.Controls.Add(this.pictureBoxLogo);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(350, 530);
            this.pnlLeft.TabIndex = 0;
            // 
            // lblSlogan
            // 
            this.lblSlogan.AutoSize = true;
            this.lblSlogan.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSlogan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblSlogan.Location = new System.Drawing.Point(50, 320);
            this.lblSlogan.Name = "lblSlogan";
            this.lblSlogan.Size = new System.Drawing.Size(243, 28);
            this.lblSlogan.TabIndex = 2;
            this.lblSlogan.Text = "Unlock Your Potential Today";
            this.lblSlogan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAppName
            // 
            this.lblAppName.AutoSize = true;
            this.lblAppName.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppName.ForeColor = System.Drawing.Color.White;
            this.lblAppName.Location = new System.Drawing.Point(60, 260);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(223, 50);
            this.lblAppName.TabIndex = 1;
            this.lblAppName.Text = "ERP System";
            this.lblAppName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = global::Erp_V1.Properties.Resources.icons8_customer_100__3_1;
            this.pictureBoxLogo.Location = new System.Drawing.Point(100, 100);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(150, 150);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogo.TabIndex = 0;
            this.pictureBoxLogo.TabStop = false;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.pnlRight.Controls.Add(this.btnClose);
            this.pnlRight.Controls.Add(this.btnLogin);
            this.pnlRight.Controls.Add(this.pnlPassword);
            this.pnlRight.Controls.Add(this.pnlUsername);
            this.pnlRight.Controls.Add(this.lblLogin);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(350, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(400, 530);
            this.pnlRight.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnClose.FlatAppearance.BorderSize = 2;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnClose.Location = new System.Drawing.Point(50, 410);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(300, 45);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(50, 350);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(300, 50);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "LOGIN";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // pnlPassword
            // 
            this.pnlPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.pnlPassword.Controls.Add(this.txtPassword);
            this.pnlPassword.Controls.Add(this.picPassword);
            this.pnlPassword.Location = new System.Drawing.Point(50, 260);
            this.pnlPassword.Name = "pnlPassword";
            this.pnlPassword.Size = new System.Drawing.Size(300, 45);
            this.pnlPassword.TabIndex = 4;
            this.pnlPassword.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Paint_Underline);
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.Color.White;
            this.txtPassword.Location = new System.Drawing.Point(40, 8);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(250, 27);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.Text = "Password";
            // 
            // picPassword
            // 
            this.picPassword.Location = new System.Drawing.Point(5, 8);
            this.picPassword.Name = "picPassword";
            this.picPassword.Size = new System.Drawing.Size(24, 24);
            this.picPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPassword.TabIndex = 0;
            this.picPassword.TabStop = false;
            // 
            // pnlUsername
            // 
            this.pnlUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.pnlUsername.Controls.Add(this.txtUsername);
            this.pnlUsername.Controls.Add(this.picUsername);
            this.pnlUsername.Location = new System.Drawing.Point(50, 190);
            this.pnlUsername.Name = "pnlUsername";
            this.pnlUsername.Size = new System.Drawing.Size(300, 45);
            this.pnlUsername.TabIndex = 3;
            this.pnlUsername.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Paint_Underline);
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.ForeColor = System.Drawing.Color.White;
            this.txtUsername.Location = new System.Drawing.Point(40, 8);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(250, 27);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.Text = "Username";
            // 
            // picUsername
            // 
            this.picUsername.Location = new System.Drawing.Point(5, 8);
            this.picUsername.Name = "picUsername";
            this.picUsername.Size = new System.Drawing.Size(24, 24);
            this.picUsername.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picUsername.TabIndex = 0;
            this.picUsername.TabStop = false;
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogin.ForeColor = System.Drawing.Color.White;
            this.lblLogin.Location = new System.Drawing.Point(40, 80);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(297, 59);
            this.lblLogin.TabIndex = 0;
            this.lblLogin.Text = "Secure Access";
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 530);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmLogin";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.pnlPassword.ResumeLayout(false);
            this.pnlPassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPassword)).EndInit();
            this.pnlUsername.ResumeLayout(false);
            this.pnlUsername.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUsername)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Label lblSlogan;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Panel pnlUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.PictureBox picUsername;
        private System.Windows.Forms.Panel pnlPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.PictureBox picPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnClose;
    }
}





//namespace Erp_V1
//{
//    partial class FrmLogin
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.pnlLeft = new System.Windows.Forms.Panel();
//            this.lblWelcome = new System.Windows.Forms.Label();
//            this.pnlRight = new System.Windows.Forms.Panel();
//            this.btnClose = new System.Windows.Forms.Button();
//            this.btnLogin = new System.Windows.Forms.Button();
//            this.txtPassword = new System.Windows.Forms.TextBox();
//            this.txtUsername = new System.Windows.Forms.TextBox();
//            this.lblPassword = new System.Windows.Forms.Label();
//            this.lblUsername = new System.Windows.Forms.Label();
//            this.lblLogin = new System.Windows.Forms.Label();
//            this.pictureBox1 = new System.Windows.Forms.PictureBox();
//            this.pnlLeft.SuspendLayout();
//            this.pnlRight.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
//            this.SuspendLayout();
//            // 
//            // pnlLeft
//            // 
//            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
//            this.pnlLeft.Controls.Add(this.lblWelcome);
//            this.pnlLeft.Controls.Add(this.pictureBox1);
//            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
//            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
//            this.pnlLeft.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
//            this.pnlLeft.Name = "pnlLeft";
//            this.pnlLeft.Size = new System.Drawing.Size(400, 652);
//            this.pnlLeft.TabIndex = 0;
//            // 
//            // lblWelcome
//            // 
//            this.lblWelcome.AutoSize = true;
//            this.lblWelcome.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lblWelcome.ForeColor = System.Drawing.Color.White;
//            this.lblWelcome.Location = new System.Drawing.Point(71, 278);
//            this.lblWelcome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
//            this.lblWelcome.Name = "lblWelcome";
//            this.lblWelcome.Size = new System.Drawing.Size(202, 56);
//            this.lblWelcome.TabIndex = 1;
//            this.lblWelcome.Text = "Welcome to the \r\nERP System";
//            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
//            // 
//            // pnlRight
//            // 
//            this.pnlRight.BackColor = System.Drawing.SystemColors.Control;
//            this.pnlRight.Controls.Add(this.btnClose);
//            this.pnlRight.Controls.Add(this.btnLogin);
//            this.pnlRight.Controls.Add(this.txtPassword);
//            this.pnlRight.Controls.Add(this.txtUsername);
//            this.pnlRight.Controls.Add(this.lblPassword);
//            this.pnlRight.Controls.Add(this.lblUsername);
//            this.pnlRight.Controls.Add(this.lblLogin);
//            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.pnlRight.Location = new System.Drawing.Point(400, 0);
//            this.pnlRight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
//            this.pnlRight.Name = "pnlRight";
//            this.pnlRight.Size = new System.Drawing.Size(600, 652);
//            this.pnlRight.TabIndex = 1;
//            // 
//            // btnClose
//            // 
//            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
//            this.btnClose.FlatAppearance.BorderSize = 0;
//            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.btnClose.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.btnClose.ForeColor = System.Drawing.Color.White;
//            this.btnClose.Location = new System.Drawing.Point(317, 395);
//            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
//            this.btnClose.Name = "btnClose";
//            this.btnClose.Size = new System.Drawing.Size(197, 49);
//            this.btnClose.TabIndex = 6;
//            this.btnClose.Text = "Close";
//            this.btnClose.UseVisualStyleBackColor = false;
//            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
//            // 
//            // btnLogin
//            // 
//            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
//            this.btnLogin.FlatAppearance.BorderSize = 0;
//            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.btnLogin.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.btnLogin.ForeColor = System.Drawing.Color.White;
//            this.btnLogin.Location = new System.Drawing.Point(85, 395);
//            this.btnLogin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
//            this.btnLogin.Name = "btnLogin";
//            this.btnLogin.Size = new System.Drawing.Size(197, 49);
//            this.btnLogin.TabIndex = 5;
//            this.btnLogin.Text = "Login";
//            this.btnLogin.UseVisualStyleBackColor = false;
//            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
//            // 
//            // txtPassword
//            // 
//            this.txtPassword.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.txtPassword.Location = new System.Drawing.Point(85, 318);
//            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
//            this.txtPassword.Name = "txtPassword";
//            this.txtPassword.PasswordChar = '*';
//            this.txtPassword.Size = new System.Drawing.Size(428, 32);
//            this.txtPassword.TabIndex = 4;
//            // 
//            // txtUsername
//            // 
//            this.txtUsername.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.txtUsername.Location = new System.Drawing.Point(85, 224);
//            this.txtUsername.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
//            this.txtUsername.Name = "txtUsername";
//            this.txtUsername.Size = new System.Drawing.Size(428, 32);
//            this.txtUsername.TabIndex = 3;
//            // 
//            // lblPassword
//            // 
//            this.lblPassword.AutoSize = true;
//            this.lblPassword.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lblPassword.Location = new System.Drawing.Point(80, 288);
//            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
//            this.lblPassword.Name = "lblPassword";
//            this.lblPassword.Size = new System.Drawing.Size(103, 23);
//            this.lblPassword.TabIndex = 2;
//            this.lblPassword.Text = "Password";
//            // 
//            // lblUsername
//            // 
//            this.lblUsername.AutoSize = true;
//            this.lblUsername.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lblUsername.Location = new System.Drawing.Point(80, 194);
//            this.lblUsername.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
//            this.lblUsername.Name = "lblUsername";
//            this.lblUsername.Size = new System.Drawing.Size(108, 23);
//            this.lblUsername.TabIndex = 1;
//            this.lblUsername.Text = "Username";
//            // 
//            // lblLogin
//            // 
//            this.lblLogin.AutoSize = true;
//            this.lblLogin.Font = new System.Drawing.Font("Century Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lblLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
//            this.lblLogin.Location = new System.Drawing.Point(77, 122);
//            this.lblLogin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
//            this.lblLogin.Name = "lblLogin";
//            this.lblLogin.Size = new System.Drawing.Size(327, 44);
//            this.lblLogin.TabIndex = 0;
//            this.lblLogin.Text = "Login to continue";
//            // 
//            // pictureBox1
//            // 
//            this.pictureBox1.Image = global::Erp_V1.Properties.Resources.photo_2024_09_27_04_37_05;
//            this.pictureBox1.Location = new System.Drawing.Point(97, 137);
//            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
//            this.pictureBox1.Name = "pictureBox1";
//            this.pictureBox1.Size = new System.Drawing.Size(147, 135);
//            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
//            this.pictureBox1.TabIndex = 0;
//            this.pictureBox1.TabStop = false;
//            // 
//            // FrmLogin
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(1000, 652);
//            this.Controls.Add(this.pnlRight);
//            this.Controls.Add(this.pnlLeft);
//            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
//            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
//            this.Name = "FrmLogin";
//            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
//            this.Text = "FrmLogin";
//            this.Load += new System.EventHandler(this.FrmLogin_Load);
//            this.pnlLeft.ResumeLayout(false);
//            this.pnlLeft.PerformLayout();
//            this.pnlRight.ResumeLayout(false);
//            this.pnlRight.PerformLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
//            this.ResumeLayout(false);

//        }

//        #endregion

//        private System.Windows.Forms.Panel pnlLeft;
//        private System.Windows.Forms.PictureBox pictureBox1;
//        private System.Windows.Forms.Panel pnlRight;
//        private System.Windows.Forms.Label lblWelcome;
//        private System.Windows.Forms.Label lblLogin;
//        private System.Windows.Forms.TextBox txtUsername;
//        private System.Windows.Forms.Label lblPassword;
//        private System.Windows.Forms.Label lblUsername;
//        private System.Windows.Forms.TextBox txtPassword;
//        private System.Windows.Forms.Button btnLogin;
//        private System.Windows.Forms.Button btnClose;
//    }
//}