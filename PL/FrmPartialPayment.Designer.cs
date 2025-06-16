namespace Erp_V1
{
    partial class FrmPartialPayment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Customer DataGridView.
        private System.Windows.Forms.DataGridView dgvCustomers;

        // Customer details controls.
        private DevExpress.XtraEditors.LabelControl lblSelectedCustomer;
        private DevExpress.XtraEditors.TextEdit txtSelectedCustomer;
        private DevExpress.XtraEditors.LabelControl lblCurrentOutstanding;
        private DevExpress.XtraEditors.TextEdit txtCurrentOutstanding;

        // Payment group controls.
        private System.Windows.Forms.GroupBox grpPayment;
        private System.Windows.Forms.RadioButton rbFullPayment;
        private System.Windows.Forms.RadioButton rbPartialPayment;
        private DevExpress.XtraEditors.LabelControl lblPaymentAmount;
        private DevExpress.XtraEditors.TextEdit txtPaymentAmount;
        private DevExpress.XtraEditors.LabelControl lblNewOutstanding;
        private DevExpress.XtraEditors.TextEdit txtNewOutstanding;

        // Action buttons.
        private DevExpress.XtraEditors.SimpleButton btnSavePayment;
        private DevExpress.XtraEditors.SimpleButton btnCancel;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPartialPayment));
            this.dgvCustomers = new System.Windows.Forms.DataGridView();
            this.lblSelectedCustomer = new DevExpress.XtraEditors.LabelControl();
            this.txtSelectedCustomer = new DevExpress.XtraEditors.TextEdit();
            this.lblCurrentOutstanding = new DevExpress.XtraEditors.LabelControl();
            this.txtCurrentOutstanding = new DevExpress.XtraEditors.TextEdit();
            this.grpPayment = new System.Windows.Forms.GroupBox();
            this.rbFullPayment = new System.Windows.Forms.RadioButton();
            this.rbPartialPayment = new System.Windows.Forms.RadioButton();
            this.lblPaymentAmount = new DevExpress.XtraEditors.LabelControl();
            this.txtPaymentAmount = new DevExpress.XtraEditors.TextEdit();
            this.lblNewOutstanding = new DevExpress.XtraEditors.LabelControl();
            this.txtNewOutstanding = new DevExpress.XtraEditors.TextEdit();
            this.btnSavePayment = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelectedCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrentOutstanding.Properties)).BeginInit();
            this.grpPayment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaymentAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewOutstanding.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCustomers
            // 
            this.dgvCustomers.AllowUserToAddRows = false;
            this.dgvCustomers.AllowUserToDeleteRows = false;
            this.dgvCustomers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomers.Location = new System.Drawing.Point(20, 20);
            this.dgvCustomers.MultiSelect = false;
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.ReadOnly = true;
            this.dgvCustomers.RowHeadersWidth = 51;
            this.dgvCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustomers.Size = new System.Drawing.Size(647, 200);
            this.dgvCustomers.TabIndex = 0;
            this.dgvCustomers.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustomers_RowEnter);
            // 
            // lblSelectedCustomer
            // 
            this.lblSelectedCustomer.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSelectedCustomer.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblSelectedCustomer.Appearance.Options.UseFont = true;
            this.lblSelectedCustomer.Appearance.Options.UseForeColor = true;
            this.lblSelectedCustomer.Location = new System.Drawing.Point(20, 235);
            this.lblSelectedCustomer.Name = "lblSelectedCustomer";
            this.lblSelectedCustomer.Size = new System.Drawing.Size(134, 20);
            this.lblSelectedCustomer.TabIndex = 1;
            this.lblSelectedCustomer.Text = "Selected Customer:";
            // 
            // txtSelectedCustomer
            // 
            this.txtSelectedCustomer.Location = new System.Drawing.Point(160, 232);
            this.txtSelectedCustomer.Name = "txtSelectedCustomer";
            this.txtSelectedCustomer.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSelectedCustomer.Properties.Appearance.Options.UseFont = true;
            this.txtSelectedCustomer.Properties.ReadOnly = true;
            this.txtSelectedCustomer.Size = new System.Drawing.Size(200, 26);
            this.txtSelectedCustomer.TabIndex = 2;
            // 
            // lblCurrentOutstanding
            // 
            this.lblCurrentOutstanding.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCurrentOutstanding.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblCurrentOutstanding.Appearance.Options.UseFont = true;
            this.lblCurrentOutstanding.Appearance.Options.UseForeColor = true;
            this.lblCurrentOutstanding.Location = new System.Drawing.Point(364, 235);
            this.lblCurrentOutstanding.Name = "lblCurrentOutstanding";
            this.lblCurrentOutstanding.Size = new System.Drawing.Size(148, 20);
            this.lblCurrentOutstanding.TabIndex = 3;
            this.lblCurrentOutstanding.Text = "Current Outstanding:";
            // 
            // txtCurrentOutstanding
            // 
            this.txtCurrentOutstanding.Location = new System.Drawing.Point(517, 232);
            this.txtCurrentOutstanding.Name = "txtCurrentOutstanding";
            this.txtCurrentOutstanding.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCurrentOutstanding.Properties.Appearance.Options.UseFont = true;
            this.txtCurrentOutstanding.Properties.ReadOnly = true;
            this.txtCurrentOutstanding.Size = new System.Drawing.Size(150, 26);
            this.txtCurrentOutstanding.TabIndex = 4;
            // 
            // grpPayment
            // 
            this.grpPayment.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grpPayment.Controls.Add(this.rbFullPayment);
            this.grpPayment.Controls.Add(this.rbPartialPayment);
            this.grpPayment.Controls.Add(this.lblPaymentAmount);
            this.grpPayment.Controls.Add(this.txtPaymentAmount);
            this.grpPayment.Controls.Add(this.lblNewOutstanding);
            this.grpPayment.Controls.Add(this.txtNewOutstanding);
            this.grpPayment.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpPayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.grpPayment.Location = new System.Drawing.Point(20, 270);
            this.grpPayment.Name = "grpPayment";
            this.grpPayment.Size = new System.Drawing.Size(620, 150);
            this.grpPayment.TabIndex = 5;
            this.grpPayment.TabStop = false;
            this.grpPayment.Text = "Payment Details";
            // 
            // rbFullPayment
            // 
            this.rbFullPayment.AutoSize = true;
            this.rbFullPayment.Location = new System.Drawing.Point(20, 30);
            this.rbFullPayment.Name = "rbFullPayment";
            this.rbFullPayment.Size = new System.Drawing.Size(121, 24);
            this.rbFullPayment.TabIndex = 0;
            this.rbFullPayment.TabStop = true;
            this.rbFullPayment.Text = "Full Payment";
            this.rbFullPayment.UseVisualStyleBackColor = true;
            this.rbFullPayment.CheckedChanged += new System.EventHandler(this.rbFullPayment_CheckedChanged);
            // 
            // rbPartialPayment
            // 
            this.rbPartialPayment.AutoSize = true;
            this.rbPartialPayment.Location = new System.Drawing.Point(150, 30);
            this.rbPartialPayment.Name = "rbPartialPayment";
            this.rbPartialPayment.Size = new System.Drawing.Size(141, 24);
            this.rbPartialPayment.TabIndex = 1;
            this.rbPartialPayment.TabStop = true;
            this.rbPartialPayment.Text = "Partial Payment";
            this.rbPartialPayment.UseVisualStyleBackColor = true;
            this.rbPartialPayment.CheckedChanged += new System.EventHandler(this.rbPartialPayment_CheckedChanged);
            // 
            // lblPaymentAmount
            // 
            this.lblPaymentAmount.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPaymentAmount.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblPaymentAmount.Appearance.Options.UseFont = true;
            this.lblPaymentAmount.Appearance.Options.UseForeColor = true;
            this.lblPaymentAmount.Location = new System.Drawing.Point(20, 70);
            this.lblPaymentAmount.Name = "lblPaymentAmount";
            this.lblPaymentAmount.Size = new System.Drawing.Size(128, 20);
            this.lblPaymentAmount.TabIndex = 2;
            this.lblPaymentAmount.Text = "Payment Amount:";
            // 
            // txtPaymentAmount
            // 
            this.txtPaymentAmount.Location = new System.Drawing.Point(153, 67);
            this.txtPaymentAmount.Name = "txtPaymentAmount";
            this.txtPaymentAmount.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPaymentAmount.Properties.Appearance.Options.UseFont = true;
            this.txtPaymentAmount.Size = new System.Drawing.Size(188, 26);
            this.txtPaymentAmount.TabIndex = 3;
            this.txtPaymentAmount.TextChanged += new System.EventHandler(this.txtPaymentAmount_TextChanged);
            // 
            // lblNewOutstanding
            // 
            this.lblNewOutstanding.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNewOutstanding.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblNewOutstanding.Appearance.Options.UseFont = true;
            this.lblNewOutstanding.Appearance.Options.UseForeColor = true;
            this.lblNewOutstanding.Location = new System.Drawing.Point(20, 110);
            this.lblNewOutstanding.Name = "lblNewOutstanding";
            this.lblNewOutstanding.Size = new System.Drawing.Size(127, 20);
            this.lblNewOutstanding.TabIndex = 4;
            this.lblNewOutstanding.Text = "New Outstanding:";
            // 
            // txtNewOutstanding
            // 
            this.txtNewOutstanding.Location = new System.Drawing.Point(152, 107);
            this.txtNewOutstanding.Name = "txtNewOutstanding";
            this.txtNewOutstanding.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNewOutstanding.Properties.Appearance.Options.UseFont = true;
            this.txtNewOutstanding.Properties.ReadOnly = true;
            this.txtNewOutstanding.Size = new System.Drawing.Size(189, 26);
            this.txtNewOutstanding.TabIndex = 5;
            // 
            // btnSavePayment
            // 
            this.btnSavePayment.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSavePayment.Appearance.Options.UseFont = true;
            this.btnSavePayment.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSavePayment.ImageOptions.SvgImage")));
            this.btnSavePayment.Location = new System.Drawing.Point(20, 440);
            this.btnSavePayment.Name = "btnSavePayment";
            this.btnSavePayment.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnSavePayment.Size = new System.Drawing.Size(154, 40);
            this.btnSavePayment.TabIndex = 6;
            this.btnSavePayment.Text = "Save Payment";
            this.btnSavePayment.Click += new System.EventHandler(this.btnSavePayment_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCancel.ImageOptions.SvgImage")));
            this.btnCancel.Location = new System.Drawing.Point(500, 440);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnCancel.Size = new System.Drawing.Size(140, 40);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmPartialPayment
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 540);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSavePayment);
            this.Controls.Add(this.grpPayment);
            this.Controls.Add(this.txtCurrentOutstanding);
            this.Controls.Add(this.lblCurrentOutstanding);
            this.Controls.Add(this.txtSelectedCustomer);
            this.Controls.Add(this.lblSelectedCustomer);
            this.Controls.Add(this.dgvCustomers);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmPartialPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Partial Payment Management";
            this.Load += new System.EventHandler(this.FrmPartialPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelectedCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrentOutstanding.Properties)).EndInit();
            this.grpPayment.ResumeLayout(false);
            this.grpPayment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaymentAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewOutstanding.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
