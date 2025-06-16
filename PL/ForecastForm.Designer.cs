namespace Erp_V1
{
    partial class ForecastForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewForecast;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblHorizon;
        private System.Windows.Forms.TextBox txtHorizon;
        private System.Windows.Forms.Label lblConfidence;
        private System.Windows.Forms.TextBox txtConfidence;
        private System.Windows.Forms.Label lblMinData;
        private System.Windows.Forms.TextBox txtMinData;
        private System.Windows.Forms.Label lblSeasonality;
        private System.Windows.Forms.TextBox txtSeasonality;
        private System.Windows.Forms.Label lblTrend;
        private System.Windows.Forms.TextBox txtTrend;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel inputTableLayout;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dataGridViewForecast = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblHorizon = new System.Windows.Forms.Label();
            this.txtHorizon = new System.Windows.Forms.TextBox();
            this.lblConfidence = new System.Windows.Forms.Label();
            this.txtConfidence = new System.Windows.Forms.TextBox();
            this.lblMinData = new System.Windows.Forms.Label();
            this.txtMinData = new System.Windows.Forms.TextBox();
            this.lblSeasonality = new System.Windows.Forms.Label();
            this.txtSeasonality = new System.Windows.Forms.TextBox();
            this.lblTrend = new System.Windows.Forms.Label();
            this.txtTrend = new System.Windows.Forms.TextBox();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.inputTableLayout = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewForecast)).BeginInit();
            this.headerPanel.SuspendLayout();
            this.inputTableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewForecast
            // 
            this.dataGridViewForecast.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewForecast.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewForecast.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewForecast.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewForecast.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewForecast.Location = new System.Drawing.Point(12, 160);
            this.dataGridViewForecast.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewForecast.Name = "dataGridViewForecast";
            this.dataGridViewForecast.RowHeadersWidth = 51;
            this.dataGridViewForecast.RowTemplate.Height = 28;
            this.dataGridViewForecast.Size = new System.Drawing.Size(876, 200);
            this.dataGridViewForecast.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(737, 66);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 27);
            this.btnRefresh.TabIndex = 10;
            this.btnRefresh.Text = "Generate";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblHorizon
            // 
            this.lblHorizon.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblHorizon.AutoSize = true;
            this.lblHorizon.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHorizon.Location = new System.Drawing.Point(80, 4);
            this.lblHorizon.Name = "lblHorizon";
            this.lblHorizon.Size = new System.Drawing.Size(142, 23);
            this.lblHorizon.TabIndex = 0;
            this.lblHorizon.Text = "Forecast Horizon:";
            // 
            // txtHorizon
            // 
            this.txtHorizon.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtHorizon.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHorizon.Location = new System.Drawing.Point(228, 2);
            this.txtHorizon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtHorizon.Name = "txtHorizon";
            this.txtHorizon.Size = new System.Drawing.Size(80, 30);
            this.txtHorizon.TabIndex = 1;
            this.txtHorizon.Text = "30";
            // 
            // lblConfidence
            // 
            this.lblConfidence.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblConfidence.AutoSize = true;
            this.lblConfidence.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblConfidence.Location = new System.Drawing.Point(500, 4);
            this.lblConfidence.Name = "lblConfidence";
            this.lblConfidence.Size = new System.Drawing.Size(172, 23);
            this.lblConfidence.TabIndex = 2;
            this.lblConfidence.Text = "Confidence Level (%):";
            // 
            // txtConfidence
            // 
            this.txtConfidence.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtConfidence.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtConfidence.Location = new System.Drawing.Point(678, 2);
            this.txtConfidence.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtConfidence.Name = "txtConfidence";
            this.txtConfidence.Size = new System.Drawing.Size(80, 30);
            this.txtConfidence.TabIndex = 3;
            this.txtConfidence.Text = "95";
            // 
            // lblMinData
            // 
            this.lblMinData.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblMinData.AutoSize = true;
            this.lblMinData.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMinData.Location = new System.Drawing.Point(43, 36);
            this.lblMinData.Name = "lblMinData";
            this.lblMinData.Size = new System.Drawing.Size(179, 23);
            this.lblMinData.TabIndex = 4;
            this.lblMinData.Text = "Minimum Data Points:";
            // 
            // txtMinData
            // 
            this.txtMinData.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtMinData.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMinData.Location = new System.Drawing.Point(228, 34);
            this.txtMinData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMinData.Name = "txtMinData";
            this.txtMinData.Size = new System.Drawing.Size(80, 30);
            this.txtMinData.TabIndex = 5;
            this.txtMinData.Text = "30";
            // 
            // lblSeasonality
            // 
            this.lblSeasonality.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSeasonality.AutoSize = true;
            this.lblSeasonality.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSeasonality.Location = new System.Drawing.Point(520, 36);
            this.lblSeasonality.Name = "lblSeasonality";
            this.lblSeasonality.Size = new System.Drawing.Size(152, 23);
            this.lblSeasonality.TabIndex = 6;
            this.lblSeasonality.Text = "Seasonality Period:";
            // 
            // txtSeasonality
            // 
            this.txtSeasonality.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSeasonality.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSeasonality.Location = new System.Drawing.Point(678, 34);
            this.txtSeasonality.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSeasonality.Name = "txtSeasonality";
            this.txtSeasonality.Size = new System.Drawing.Size(80, 30);
            this.txtSeasonality.TabIndex = 7;
            this.txtSeasonality.Text = "7";
            // 
            // lblTrend
            // 
            this.lblTrend.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTrend.AutoSize = true;
            this.lblTrend.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTrend.Location = new System.Drawing.Point(98, 68);
            this.lblTrend.Name = "lblTrend";
            this.lblTrend.Size = new System.Drawing.Size(124, 23);
            this.lblTrend.TabIndex = 8;
            this.lblTrend.Text = "Trend Window:";
            // 
            // txtTrend
            // 
            this.txtTrend.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtTrend.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTrend.Location = new System.Drawing.Point(228, 66);
            this.txtTrend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTrend.Name = "txtTrend";
            this.txtTrend.Size = new System.Drawing.Size(80, 30);
            this.txtTrend.TabIndex = 9;
            this.txtTrend.Text = "30";
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.headerPanel.Controls.Add(this.lblTitle);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(900, 48);
            this.headerPanel.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(330, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(292, 41);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Product Forecasting";
            // 
            // inputTableLayout
            // 
            this.inputTableLayout.ColumnCount = 4;
            this.inputTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.inputTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.inputTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.inputTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.inputTableLayout.Controls.Add(this.lblHorizon, 0, 0);
            this.inputTableLayout.Controls.Add(this.txtHorizon, 1, 0);
            this.inputTableLayout.Controls.Add(this.lblConfidence, 2, 0);
            this.inputTableLayout.Controls.Add(this.txtConfidence, 3, 0);
            this.inputTableLayout.Controls.Add(this.lblMinData, 0, 1);
            this.inputTableLayout.Controls.Add(this.txtMinData, 1, 1);
            this.inputTableLayout.Controls.Add(this.lblSeasonality, 2, 1);
            this.inputTableLayout.Controls.Add(this.txtSeasonality, 3, 1);
            this.inputTableLayout.Controls.Add(this.lblTrend, 0, 2);
            this.inputTableLayout.Controls.Add(this.txtTrend, 1, 2);
            this.inputTableLayout.Controls.Add(this.btnRefresh, 3, 2);
            this.inputTableLayout.Dock = System.Windows.Forms.DockStyle.Top;
            this.inputTableLayout.Location = new System.Drawing.Point(0, 48);
            this.inputTableLayout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.inputTableLayout.Name = "inputTableLayout";
            this.inputTableLayout.RowCount = 3;
            this.inputTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.inputTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.inputTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.inputTableLayout.Size = new System.Drawing.Size(900, 96);
            this.inputTableLayout.TabIndex = 1;
            // 
            // ForecastForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 376);
            this.Controls.Add(this.dataGridViewForecast);
            this.Controls.Add(this.inputTableLayout);
            this.Controls.Add(this.headerPanel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ForecastForm";
            this.Text = "Product Forecast Dashboard";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewForecast)).EndInit();
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.inputTableLayout.ResumeLayout(false);
            this.inputTableLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
