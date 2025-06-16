namespace Erp_V1
{
    partial class frmMultiSales
    {
        private System.ComponentModel.IContainer components = null;
        //private DevExpress.XtraEditors.XtraForm form;
        private System.Windows.Forms.ComboBox cboProduct;
        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.DateTimePicker dtpSalesDate;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Button btnAddSale;
        private System.Windows.Forms.Button btnLoadSales;
        private System.Windows.Forms.DataGridView dgvSalesDetails;
        private System.Windows.Forms.DataGridView dgvSales;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalesID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductNameSales;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategoryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalesAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPriceSales;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalesDate;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cboProduct = new System.Windows.Forms.ComboBox();
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.dtpSalesDate = new System.Windows.Forms.DateTimePicker();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnAddSale = new System.Windows.Forms.Button();
            this.btnLoadSales = new System.Windows.Forms.Button();
            this.dgvSalesDetails = new System.Windows.Forms.DataGridView();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSales = new System.Windows.Forms.DataGridView();
            this.colSalesID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductNameSales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalesAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPriceSales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalesDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).BeginInit();
            this.SuspendLayout();
            // 
            // cboProduct
            // 
            this.cboProduct.FormattingEnabled = true;
            this.cboProduct.Location = new System.Drawing.Point(12, 12);
            this.cboProduct.Name = "cboProduct";
            this.cboProduct.Size = new System.Drawing.Size(200, 24);
            this.cboProduct.TabIndex = 0;
            // 
            // cboCustomer
            // 
            this.cboCustomer.FormattingEnabled = true;
            this.cboCustomer.Location = new System.Drawing.Point(12, 42);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(200, 24);
            this.cboCustomer.TabIndex = 1;
            // 
            // cboCategory
            // 
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(12, 72);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(200, 24);
            this.cboCategory.TabIndex = 2;
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(52, 101);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(100, 22);
            this.txtQuantity.TabIndex = 3;
            this.txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantity_KeyPress);
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(198, 101);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(126, 22);
            this.txtPrice.TabIndex = 4;
            this.txtPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrice_KeyPress);
            // 
            // dtpSalesDate
            // 
            this.dtpSalesDate.Location = new System.Drawing.Point(12, 130);
            this.dtpSalesDate.Name = "dtpSalesDate";
            this.dtpSalesDate.Size = new System.Drawing.Size(200, 22);
            this.dtpSalesDate.TabIndex = 5;
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Location = new System.Drawing.Point(12, 158);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(100, 30);
            this.btnAddProduct.TabIndex = 6;
            this.btnAddProduct.Text = "Add Product";
            this.btnAddProduct.UseVisualStyleBackColor = true;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // btnAddSale
            // 
            this.btnAddSale.Location = new System.Drawing.Point(118, 158);
            this.btnAddSale.Name = "btnAddSale";
            this.btnAddSale.Size = new System.Drawing.Size(100, 30);
            this.btnAddSale.TabIndex = 7;
            this.btnAddSale.Text = "Add Sale";
            this.btnAddSale.UseVisualStyleBackColor = true;
            this.btnAddSale.Click += new System.EventHandler(this.btnAddSale_Click);
            // 
            // btnLoadSales
            // 
            this.btnLoadSales.Location = new System.Drawing.Point(224, 158);
            this.btnLoadSales.Name = "btnLoadSales";
            this.btnLoadSales.Size = new System.Drawing.Size(100, 30);
            this.btnLoadSales.TabIndex = 8;
            this.btnLoadSales.Text = "Load Sales";
            this.btnLoadSales.UseVisualStyleBackColor = true;
            this.btnLoadSales.Click += new System.EventHandler(this.btnLoadSales_Click);
            // 
            // dgvSalesDetails
            // 
            this.dgvSalesDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalesDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProductName,
            this.colQuantity,
            this.colPrice});
            this.dgvSalesDetails.Location = new System.Drawing.Point(12, 194);
            this.dgvSalesDetails.Name = "dgvSalesDetails";
            this.dgvSalesDetails.RowHeadersWidth = 51;
            this.dgvSalesDetails.RowTemplate.Height = 24;
            this.dgvSalesDetails.Size = new System.Drawing.Size(776, 150);
            this.dgvSalesDetails.TabIndex = 9;
            // 
            // colProductName
            // 
            this.colProductName.MinimumWidth = 6;
            this.colProductName.Name = "colProductName";
            this.colProductName.Width = 125;
            // 
            // colQuantity
            // 
            this.colQuantity.MinimumWidth = 6;
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Width = 125;
            // 
            // colPrice
            // 
            this.colPrice.MinimumWidth = 6;
            this.colPrice.Name = "colPrice";
            this.colPrice.Width = 125;
            // 
            // dgvSales
            // 
            this.dgvSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSalesID,
            this.colCustomerName,
            this.colProductNameSales,
            this.colCategoryName,
            this.colSalesAmount,
            this.colPriceSales,
            this.colSalesDate});
            this.dgvSales.Location = new System.Drawing.Point(12, 350);
            this.dgvSales.Name = "dgvSales";
            this.dgvSales.RowHeadersWidth = 51;
            this.dgvSales.RowTemplate.Height = 24;
            this.dgvSales.Size = new System.Drawing.Size(776, 150);
            this.dgvSales.TabIndex = 10;
            // 
            // colSalesID
            // 
            this.colSalesID.MinimumWidth = 6;
            this.colSalesID.Name = "colSalesID";
            this.colSalesID.Width = 125;
            // 
            // colCustomerName
            // 
            this.colCustomerName.MinimumWidth = 6;
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.Width = 125;
            // 
            // colProductNameSales
            // 
            this.colProductNameSales.MinimumWidth = 6;
            this.colProductNameSales.Name = "colProductNameSales";
            this.colProductNameSales.Width = 125;
            // 
            // colCategoryName
            // 
            this.colCategoryName.MinimumWidth = 6;
            this.colCategoryName.Name = "colCategoryName";
            this.colCategoryName.Width = 125;
            // 
            // colSalesAmount
            // 
            this.colSalesAmount.MinimumWidth = 6;
            this.colSalesAmount.Name = "colSalesAmount";
            this.colSalesAmount.Width = 125;
            // 
            // colPriceSales
            // 
            this.colPriceSales.MinimumWidth = 6;
            this.colPriceSales.Name = "colPriceSales";
            this.colPriceSales.Width = 125;
            // 
            // colSalesDate
            // 
            this.colSalesDate.MinimumWidth = 6;
            this.colSalesDate.Name = "colSalesDate";
            this.colSalesDate.Width = 125;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 102);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(34, 22);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "Quy";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(158, 101);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(34, 22);
            this.textBox2.TabIndex = 12;
            this.textBox2.Text = "Pcs";
            // 
            // frmMultiSales
            // 
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dgvSales);
            this.Controls.Add(this.dgvSalesDetails);
            this.Controls.Add(this.btnLoadSales);
            this.Controls.Add(this.btnAddSale);
            this.Controls.Add(this.btnAddProduct);
            this.Controls.Add(this.dtpSalesDate);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.cboCustomer);
            this.Controls.Add(this.cboProduct);
            this.Name = "frmMultiSales";
            this.Text = "Multi Sales";
            this.Load += new System.EventHandler(this.frmMultiSales_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
    }
}
