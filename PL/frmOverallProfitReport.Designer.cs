namespace Erp_V1
{
    partial class frmOverallProfitReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvProfitDetails;
        private DevExpress.XtraCharts.ChartControl chartControlCategory;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblOverallProfit;

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
            this.dgvProfitDetails = new System.Windows.Forms.DataGridView();
            this.chartControlCategory = new DevExpress.XtraCharts.ChartControl();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblOverallProfit = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProfitDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlCategory)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvProfitDetails
            // 
            this.dgvProfitDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProfitDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProfitDetails.Location = new System.Drawing.Point(12, 60);
            this.dgvProfitDetails.Name = "dgvProfitDetails";
            this.dgvProfitDetails.Size = new System.Drawing.Size(760, 200);
            this.dgvProfitDetails.TabIndex = 0;
            // 
            // chartControlCategory
            // 
            this.chartControlCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                    | System.Windows.Forms.AnchorStyles.Left)
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.chartControlCategory.Location = new System.Drawing.Point(12, 280);
            this.chartControlCategory.Name = "chartControlCategory";
            this.chartControlCategory.Size = new System.Drawing.Size(760, 280);
            this.chartControlCategory.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 40);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Overall Profit Report";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOverallProfit
            // 
            this.lblOverallProfit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOverallProfit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblOverallProfit.Location = new System.Drawing.Point(418, 9);
            this.lblOverallProfit.Name = "lblOverallProfit";
            this.lblOverallProfit.Size = new System.Drawing.Size(354, 40);
            this.lblOverallProfit.TabIndex = 3;
            this.lblOverallProfit.Text = "Overall Profit: ";
            this.lblOverallProfit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmOverallProfitReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 581);
            this.Controls.Add(this.lblOverallProfit);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.chartControlCategory);
            this.Controls.Add(this.dgvProfitDetails);
            this.Name = "frmOverallProfitReport";
            this.Text = "Overall Profit Report";
            this.Load += new System.EventHandler(this.frmOverallProfitReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProfitDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControlCategory)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
