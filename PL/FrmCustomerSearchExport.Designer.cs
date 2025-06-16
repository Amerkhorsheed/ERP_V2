namespace Erp_V1
{
    partial class FrmCustomerSearchExport
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.LabelControl lblSearch;
        private DevExpress.XtraEditors.SimpleButton btnExportAll;
        private DevExpress.XtraEditors.SimpleButton btnExportSelected;
        private System.Windows.Forms.DataGridView dgvCustomers;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;

        /// <summary>
        /// Clean up any resources.
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblSearch = new DevExpress.XtraEditors.LabelControl();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.dgvCustomers = new System.Windows.Forms.DataGridView();
            this.btnExportAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnExportSelected = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel.Controls.Add(this.lblSearch, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.txtSearch, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.dgvCustomers, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.btnExportAll, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.btnExportSelected, 1, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(800, 520);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // lblSearch
            // 
            this.lblSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSearch.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSearch.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblSearch.Appearance.Options.UseFont = true;
            this.lblSearch.Appearance.Options.UseForeColor = true;
            this.lblSearch.Location = new System.Drawing.Point(13, 18);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(58, 23);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search:";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSearch.Location = new System.Drawing.Point(559, 19);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Properties.Appearance.Options.UseFont = true;
            this.txtSearch.Properties.NullValuePrompt = "Enter customer name...";
            this.txtSearch.Size = new System.Drawing.Size(200, 30);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.EditValueChanged += new System.EventHandler(this.txtSearch_EditValueChanged);
            // 
            // dgvCustomers
            // 
            this.dgvCustomers.AllowUserToAddRows = false;
            this.dgvCustomers.AllowUserToDeleteRows = false;
            this.dgvCustomers.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel.SetColumnSpan(this.dgvCustomers, 2);
            this.dgvCustomers.Location = new System.Drawing.Point(13, 55);
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.ReadOnly = true;
            this.dgvCustomers.RowHeadersWidth = 51;
            this.dgvCustomers.Size = new System.Drawing.Size(774, 400);
            this.dgvCustomers.TabIndex = 2;
            // 
            // btnExportAll
            // 
            this.btnExportAll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExportAll.Location = new System.Drawing.Point(208, 467);
            this.btnExportAll.Name = "btnExportAll";
            this.btnExportAll.Size = new System.Drawing.Size(150, 35);
            this.btnExportAll.TabIndex = 3;
            this.btnExportAll.Text = "Export All";
            this.btnExportAll.Click += new System.EventHandler(this.btnExportAll_Click);
            // 
            // btnExportSelected
            // 
            this.btnExportSelected.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExportSelected.Location = new System.Drawing.Point(598, 467);
            this.btnExportSelected.Name = "btnExportSelected";
            this.btnExportSelected.Size = new System.Drawing.Size(150, 35);
            this.btnExportSelected.TabIndex = 4;
            this.btnExportSelected.Text = "Export Selected";
            this.btnExportSelected.Click += new System.EventHandler(this.btnExportSelected_Click);
            // 
            // FrmCustomerSearchExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 520);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "FrmCustomerSearchExport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Search & Export";
            this.Load += new System.EventHandler(this.FrmCustomerSearchExport_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
