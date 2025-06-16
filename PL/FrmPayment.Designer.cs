namespace Erp_V1
{
    partial class FrmPayment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Product Information Controls
        private System.Windows.Forms.GroupBox grpProduct;
        private System.Windows.Forms.DataGridView gridProduct;
        private DevExpress.XtraEditors.TextEdit txtProductName;
        private DevExpress.XtraEditors.TextEdit txtPrice;
        private DevExpress.XtraEditors.TextEdit txtStock;
        private DevExpress.XtraEditors.LabelControl lblProductName;
        private DevExpress.XtraEditors.LabelControl lblPrice;
        private DevExpress.XtraEditors.LabelControl lblStock;
        #endregion

        #region Customer Information Controls
        private System.Windows.Forms.GroupBox grpCustomer;
        private System.Windows.Forms.DataGridView gridCustomer;
        private DevExpress.XtraEditors.TextEdit txtCustomerName;
        private DevExpress.XtraEditors.LabelControl lblCustomerName;
        #endregion

        #region Sales Details Controls
        private System.Windows.Forms.GroupBox grpSales;
        private DevExpress.XtraEditors.TextEdit txtSalesAmount;
        private DevExpress.XtraEditors.LabelControl lblSalesAmount;
        private DevExpress.XtraEditors.TextEdit txtDiscount;
        private DevExpress.XtraEditors.LabelControl lblDiscount;
        private DevExpress.XtraEditors.LabelControl lblTotalPrice;
        #endregion

        #region Payment Section Controls
        private System.Windows.Forms.GroupBox grpPayment;
        private System.Windows.Forms.RadioButton radFullPayment;
        private System.Windows.Forms.RadioButton radPartialPayment;
        private DevExpress.XtraEditors.TextEdit txtPaidAmount;
        private DevExpress.XtraEditors.LabelControl lblPaid;
        private DevExpress.XtraEditors.TextEdit txtRemaining;
        private DevExpress.XtraEditors.LabelControl lblRemaining;
        #endregion

        #region Action Buttons
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        #endregion

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPayment));
            this.grpProduct = new System.Windows.Forms.GroupBox();
            this.gridProduct = new System.Windows.Forms.DataGridView();
            this.txtProductName = new DevExpress.XtraEditors.TextEdit();
            this.txtPrice = new DevExpress.XtraEditors.TextEdit();
            this.txtStock = new DevExpress.XtraEditors.TextEdit();
            this.lblProductName = new DevExpress.XtraEditors.LabelControl();
            this.lblPrice = new DevExpress.XtraEditors.LabelControl();
            this.lblStock = new DevExpress.XtraEditors.LabelControl();
            this.grpCustomer = new System.Windows.Forms.GroupBox();
            this.gridCustomer = new System.Windows.Forms.DataGridView();
            this.txtCustomerName = new DevExpress.XtraEditors.TextEdit();
            this.lblCustomerName = new DevExpress.XtraEditors.LabelControl();
            this.grpSales = new System.Windows.Forms.GroupBox();
            this.txtSalesAmount = new DevExpress.XtraEditors.TextEdit();
            this.lblSalesAmount = new DevExpress.XtraEditors.LabelControl();
            this.txtDiscount = new DevExpress.XtraEditors.TextEdit();
            this.lblDiscount = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalPrice = new DevExpress.XtraEditors.LabelControl();
            this.grpPayment = new System.Windows.Forms.GroupBox();
            this.radFullPayment = new System.Windows.Forms.RadioButton();
            this.radPartialPayment = new System.Windows.Forms.RadioButton();
            this.txtPaidAmount = new DevExpress.XtraEditors.TextEdit();
            this.lblPaid = new DevExpress.XtraEditors.LabelControl();
            this.txtRemaining = new DevExpress.XtraEditors.TextEdit();
            this.lblRemaining = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.grpProduct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStock.Properties)).BeginInit();
            this.grpCustomer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).BeginInit();
            this.grpSales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalesAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscount.Properties)).BeginInit();
            this.grpPayment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaidAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemaining.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grpProduct
            // 
            this.grpProduct.Controls.Add(this.gridProduct);
            this.grpProduct.Controls.Add(this.txtProductName);
            this.grpProduct.Controls.Add(this.txtPrice);
            this.grpProduct.Controls.Add(this.txtStock);
            this.grpProduct.Controls.Add(this.lblProductName);
            this.grpProduct.Controls.Add(this.lblPrice);
            this.grpProduct.Controls.Add(this.lblStock);
            this.grpProduct.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.grpProduct.Location = new System.Drawing.Point(12, 4);
            this.grpProduct.Name = "grpProduct";
            this.grpProduct.Size = new System.Drawing.Size(785, 268);
            this.grpProduct.TabIndex = 0;
            this.grpProduct.TabStop = false;
            this.grpProduct.Text = "Product Information";
            // 
            // gridProduct
            // 
            this.gridProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProduct.Location = new System.Drawing.Point(16, 25);
            this.gridProduct.Name = "gridProduct";
            this.gridProduct.RowHeadersWidth = 51;
            this.gridProduct.Size = new System.Drawing.Size(763, 150);
            this.gridProduct.TabIndex = 0;
            this.gridProduct.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridProduct_RowEnter);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(130, 177);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtProductName.Properties.Appearance.Options.UseFont = true;
            this.txtProductName.Properties.ReadOnly = true;
            this.txtProductName.Size = new System.Drawing.Size(206, 26);
            this.txtProductName.TabIndex = 2;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(130, 207);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPrice.Properties.Appearance.Options.UseFont = true;
            this.txtPrice.Properties.ReadOnly = true;
            this.txtPrice.Size = new System.Drawing.Size(185, 26);
            this.txtPrice.TabIndex = 4;
            // 
            // txtStock
            // 
            this.txtStock.Location = new System.Drawing.Point(130, 237);
            this.txtStock.Name = "txtStock";
            this.txtStock.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtStock.Properties.Appearance.Options.UseFont = true;
            this.txtStock.Properties.ReadOnly = true;
            this.txtStock.Size = new System.Drawing.Size(160, 26);
            this.txtStock.TabIndex = 6;
            // 
            // lblProductName
            // 
            this.lblProductName.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblProductName.Appearance.Options.UseFont = true;
            this.lblProductName.Location = new System.Drawing.Point(16, 180);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(105, 20);
            this.lblProductName.TabIndex = 1;
            this.lblProductName.Text = "Product Name:";
            // 
            // lblPrice
            // 
            this.lblPrice.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrice.Appearance.Options.UseFont = true;
            this.lblPrice.Location = new System.Drawing.Point(16, 210);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(38, 20);
            this.lblPrice.TabIndex = 3;
            this.lblPrice.Text = "Price:";
            // 
            // lblStock
            // 
            this.lblStock.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStock.Appearance.Options.UseFont = true;
            this.lblStock.Location = new System.Drawing.Point(16, 240);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(104, 20);
            this.lblStock.TabIndex = 5;
            this.lblStock.Text = "Stock Amount:";
            // 
            // grpCustomer
            // 
            this.grpCustomer.Controls.Add(this.gridCustomer);
            this.grpCustomer.Controls.Add(this.txtCustomerName);
            this.grpCustomer.Controls.Add(this.lblCustomerName);
            this.grpCustomer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.grpCustomer.Location = new System.Drawing.Point(12, 280);
            this.grpCustomer.Name = "grpCustomer";
            this.grpCustomer.Size = new System.Drawing.Size(785, 220);
            this.grpCustomer.TabIndex = 7;
            this.grpCustomer.TabStop = false;
            this.grpCustomer.Text = "Customer Information";
            // 
            // gridCustomer
            // 
            this.gridCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCustomer.Location = new System.Drawing.Point(16, 25);
            this.gridCustomer.Name = "gridCustomer";
            this.gridCustomer.RowHeadersWidth = 51;
            this.gridCustomer.Size = new System.Drawing.Size(763, 150);
            this.gridCustomer.TabIndex = 0;
            this.gridCustomer.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridCustomer_RowEnter);
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(140, 183);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCustomerName.Properties.Appearance.Options.UseFont = true;
            this.txtCustomerName.Properties.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(196, 26);
            this.txtCustomerName.TabIndex = 9;
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCustomerName.Appearance.Options.UseFont = true;
            this.lblCustomerName.Location = new System.Drawing.Point(16, 186);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(118, 20);
            this.lblCustomerName.TabIndex = 8;
            this.lblCustomerName.Text = "Customer Name:";
            // 
            // grpSales
            // 
            this.grpSales.Controls.Add(this.txtSalesAmount);
            this.grpSales.Controls.Add(this.lblSalesAmount);
            this.grpSales.Controls.Add(this.txtDiscount);
            this.grpSales.Controls.Add(this.lblDiscount);
            this.grpSales.Controls.Add(this.lblTotalPrice);
            this.grpSales.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpSales.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.grpSales.Location = new System.Drawing.Point(12, 504);
            this.grpSales.Name = "grpSales";
            this.grpSales.Size = new System.Drawing.Size(683, 101);
            this.grpSales.TabIndex = 10;
            this.grpSales.TabStop = false;
            this.grpSales.Text = "Sales Details";
            // 
            // txtSalesAmount
            // 
            this.txtSalesAmount.Location = new System.Drawing.Point(130, 27);
            this.txtSalesAmount.Name = "txtSalesAmount";
            this.txtSalesAmount.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSalesAmount.Properties.Appearance.Options.UseFont = true;
            this.txtSalesAmount.Size = new System.Drawing.Size(185, 26);
            this.txtSalesAmount.TabIndex = 11;
            this.txtSalesAmount.TextChanged += new System.EventHandler(this.txtSalesAmount_TextChanged);
            // 
            // lblSalesAmount
            // 
            this.lblSalesAmount.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSalesAmount.Appearance.Options.UseFont = true;
            this.lblSalesAmount.Location = new System.Drawing.Point(16, 30);
            this.lblSalesAmount.Name = "lblSalesAmount";
            this.lblSalesAmount.Size = new System.Drawing.Size(101, 20);
            this.lblSalesAmount.TabIndex = 10;
            this.lblSalesAmount.Text = "Sales Amount:";
            // 
            // txtDiscount
            // 
            this.txtDiscount.Location = new System.Drawing.Point(100, 57);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDiscount.Properties.Appearance.Options.UseFont = true;
            this.txtDiscount.Size = new System.Drawing.Size(160, 26);
            this.txtDiscount.TabIndex = 13;
            // 
            // lblDiscount
            // 
            this.lblDiscount.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDiscount.Appearance.Options.UseFont = true;
            this.lblDiscount.Location = new System.Drawing.Point(16, 60);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(66, 20);
            this.lblDiscount.TabIndex = 12;
            this.lblDiscount.Text = "Discount:";
            // 
            // lblTotalPrice
            // 
            this.lblTotalPrice.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalPrice.Appearance.ForeColor = System.Drawing.Color.Teal;
            this.lblTotalPrice.Appearance.Options.UseFont = true;
            this.lblTotalPrice.Appearance.Options.UseForeColor = true;
            this.lblTotalPrice.Location = new System.Drawing.Point(334, 30);
            this.lblTotalPrice.Name = "lblTotalPrice";
            this.lblTotalPrice.Size = new System.Drawing.Size(91, 20);
            this.lblTotalPrice.TabIndex = 14;
            this.lblTotalPrice.Text = "Total Price: 0";
            // 
            // grpPayment
            // 
            this.grpPayment.Controls.Add(this.radFullPayment);
            this.grpPayment.Controls.Add(this.radPartialPayment);
            this.grpPayment.Controls.Add(this.txtPaidAmount);
            this.grpPayment.Controls.Add(this.lblPaid);
            this.grpPayment.Controls.Add(this.txtRemaining);
            this.grpPayment.Controls.Add(this.lblRemaining);
            this.grpPayment.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpPayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.grpPayment.Location = new System.Drawing.Point(12, 611);
            this.grpPayment.Name = "grpPayment";
            this.grpPayment.Size = new System.Drawing.Size(683, 100);
            this.grpPayment.TabIndex = 15;
            this.grpPayment.TabStop = false;
            this.grpPayment.Text = "Payment Section";
            // 
            // radFullPayment
            // 
            this.radFullPayment.Location = new System.Drawing.Point(16, 30);
            this.radFullPayment.Name = "radFullPayment";
            this.radFullPayment.Size = new System.Drawing.Size(100, 20);
            this.radFullPayment.TabIndex = 15;
            this.radFullPayment.TabStop = true;
            this.radFullPayment.Text = "Full Payment";
            this.radFullPayment.UseVisualStyleBackColor = true;
            this.radFullPayment.CheckedChanged += new System.EventHandler(this.radFullPayment_CheckedChanged);
            // 
            // radPartialPayment
            // 
            this.radPartialPayment.Location = new System.Drawing.Point(130, 30);
            this.radPartialPayment.Name = "radPartialPayment";
            this.radPartialPayment.Size = new System.Drawing.Size(120, 20);
            this.radPartialPayment.TabIndex = 16;
            this.radPartialPayment.Text = "Partial Payment";
            this.radPartialPayment.UseVisualStyleBackColor = true;
            this.radPartialPayment.CheckedChanged += new System.EventHandler(this.radPartialPayment_CheckedChanged);
            // 
            // txtPaidAmount
            // 
            this.txtPaidAmount.Location = new System.Drawing.Point(130, 57);
            this.txtPaidAmount.Name = "txtPaidAmount";
            this.txtPaidAmount.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPaidAmount.Properties.Appearance.Options.UseFont = true;
            this.txtPaidAmount.Size = new System.Drawing.Size(160, 26);
            this.txtPaidAmount.TabIndex = 18;
            this.txtPaidAmount.TextChanged += new System.EventHandler(this.txtPaidAmount_TextChanged);
            // 
            // lblPaid
            // 
            this.lblPaid.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPaid.Appearance.Options.UseFont = true;
            this.lblPaid.Location = new System.Drawing.Point(16, 60);
            this.lblPaid.Name = "lblPaid";
            this.lblPaid.Size = new System.Drawing.Size(96, 20);
            this.lblPaid.TabIndex = 17;
            this.lblPaid.Text = "Amount Paid:";
            // 
            // txtRemaining
            // 
            this.txtRemaining.Location = new System.Drawing.Point(460, 57);
            this.txtRemaining.Name = "txtRemaining";
            this.txtRemaining.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtRemaining.Properties.Appearance.Options.UseFont = true;
            this.txtRemaining.Size = new System.Drawing.Size(150, 26);
            this.txtRemaining.TabIndex = 20;
            // 
            // lblRemaining
            // 
            this.lblRemaining.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRemaining.Appearance.Options.UseFont = true;
            this.lblRemaining.Location = new System.Drawing.Point(310, 60);
            this.lblRemaining.Name = "lblRemaining";
            this.lblRemaining.Size = new System.Drawing.Size(137, 20);
            this.lblRemaining.TabIndex = 19;
            this.lblRemaining.Text = "Remaining Balance:";
            // 
            // btnSave
            // 
            this.btnSave.Appearance.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnSave.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSave.ImageOptions.SvgImage")));
            this.btnSave.Location = new System.Drawing.Point(12, 717);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(164, 45);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "Save Payment";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.BackColor = System.Drawing.Color.IndianRed;
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.Options.UseBackColor = true;
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnCancel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCancel.ImageOptions.SvgImage")));
            this.btnCancel.Location = new System.Drawing.Point(633, 717);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(164, 45);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmPayment
            // 
            this.ClientSize = new System.Drawing.Size(822, 766);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpPayment);
            this.Controls.Add(this.grpSales);
            this.Controls.Add(this.grpCustomer);
            this.Controls.Add(this.grpProduct);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Payment";
            this.Load += new System.EventHandler(this.FrmPayment_Load);
            this.grpProduct.ResumeLayout(false);
            this.grpProduct.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStock.Properties)).EndInit();
            this.grpCustomer.ResumeLayout(false);
            this.grpCustomer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).EndInit();
            this.grpSales.ResumeLayout(false);
            this.grpSales.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalesAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscount.Properties)).EndInit();
            this.grpPayment.ResumeLayout(false);
            this.grpPayment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaidAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemaining.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
