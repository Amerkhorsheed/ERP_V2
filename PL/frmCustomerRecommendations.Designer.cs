namespace Erp_V1
{
    partial class frmProductRecommendations
    {
        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.Button btnGetRecommendations;
        private System.Windows.Forms.DataGridView dgvRecommendations;

        private void InitializeComponent()
        {
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.btnGetRecommendations = new System.Windows.Forms.Button();
            this.dgvRecommendations = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecommendations)).BeginInit();
            this.SuspendLayout();
            // 
            // cboCustomer
            // 
            this.cboCustomer.FormattingEnabled = true;
            this.cboCustomer.Location = new System.Drawing.Point(12, 12);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(200, 21);
            this.cboCustomer.TabIndex = 0;
            // 
            // btnGetRecommendations
            // 
            this.btnGetRecommendations.Location = new System.Drawing.Point(230, 10);
            this.btnGetRecommendations.Name = "btnGetRecommendations";
            this.btnGetRecommendations.Size = new System.Drawing.Size(150, 23);
            this.btnGetRecommendations.TabIndex = 1;
            this.btnGetRecommendations.Text = "Get Recommendations";
            this.btnGetRecommendations.UseVisualStyleBackColor = true;
            this.btnGetRecommendations.Click += new System.EventHandler(this.btnGetRecommendations_Click);
            // 
            // dgvRecommendations
            // 
            this.dgvRecommendations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecommendations.Location = new System.Drawing.Point(12, 50);
            this.dgvRecommendations.Name = "dgvRecommendations";
            this.dgvRecommendations.Size = new System.Drawing.Size(600, 300);
            this.dgvRecommendations.TabIndex = 2;
            // 
            // frmProductRecommendations
            // 
            this.ClientSize = new System.Drawing.Size(630, 370);
            this.Controls.Add(this.dgvRecommendations);
            this.Controls.Add(this.btnGetRecommendations);
            this.Controls.Add(this.cboCustomer);
            this.Name = "frmProductRecommendations";
            this.Text = "Product Recommendations";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecommendations)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
