namespace Erp_V1
{
    partial class FrmAi
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAi));
            this.lblAssistantTitle = new System.Windows.Forms.Label();
            this.txtAIResponse = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.pbAI = new System.Windows.Forms.PictureBox();
            this.btnSendRequest = new System.Windows.Forms.Button();
            this.txtParameter = new System.Windows.Forms.TextBox();
            this.cmbQuestionType = new System.Windows.Forms.ComboBox();
            this.lblParameterPrompt = new System.Windows.Forms.Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAI)).BeginInit();
            this.panelContent.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAssistantTitle
            // 
            this.lblAssistantTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAssistantTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssistantTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(46)))), ((int)(((byte)(109)))));
            this.lblAssistantTitle.Location = new System.Drawing.Point(60, 0);
            this.lblAssistantTitle.Name = "lblAssistantTitle";
            this.lblAssistantTitle.Size = new System.Drawing.Size(490, 80);
            this.lblAssistantTitle.TabIndex = 0;
            this.lblAssistantTitle.Text = "ERP Cognitive Assistant";
            this.lblAssistantTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAIResponse
            // 
            this.txtAIResponse.BackColor = System.Drawing.Color.White;
            this.txtAIResponse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAIResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAIResponse.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAIResponse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtAIResponse.Location = new System.Drawing.Point(20, 168);
            this.txtAIResponse.Margin = new System.Windows.Forms.Padding(4);
            this.txtAIResponse.Multiline = true;
            this.txtAIResponse.Name = "txtAIResponse";
            this.txtAIResponse.ReadOnly = true;
            this.txtAIResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAIResponse.Size = new System.Drawing.Size(510, 242);
            this.txtAIResponse.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblStatus.Location = new System.Drawing.Point(20, 410);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lblStatus.Size = new System.Drawing.Size(510, 25);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Ready";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.Color.White;
            this.panelContainer.Controls.Add(this.panelContent);
            this.panelContainer.Controls.Add(this.panelHeader);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(550, 500);
            this.panelContainer.TabIndex = 5;
            // 
            // pbAI
            // 
            this.pbAI.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbAI.Image = ((System.Drawing.Image)(resources.GetObject("pbAI.Image")));
            this.pbAI.Location = new System.Drawing.Point(0, 0);
            this.pbAI.Name = "pbAI";
            this.pbAI.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.pbAI.Size = new System.Drawing.Size(60, 80);
            this.pbAI.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbAI.TabIndex = 1;
            this.pbAI.TabStop = false;
            // 
            // btnSendRequest
            // 
            this.btnSendRequest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(46)))), ((int)(((byte)(109)))));
            this.btnSendRequest.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSendRequest.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(46)))), ((int)(((byte)(109)))));
            this.btnSendRequest.FlatAppearance.BorderSize = 0;
            this.btnSendRequest.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(29)))), ((int)(((byte)(69)))));
            this.btnSendRequest.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnSendRequest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendRequest.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendRequest.ForeColor = System.Drawing.Color.White;
            this.btnSendRequest.Image = ((System.Drawing.Image)(resources.GetObject("btnSendRequest.Image")));
            this.btnSendRequest.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendRequest.Location = new System.Drawing.Point(20, 113);
            this.btnSendRequest.Name = "btnSendRequest";
            this.btnSendRequest.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSendRequest.Size = new System.Drawing.Size(510, 45);
            this.btnSendRequest.TabIndex = 7;
            this.btnSendRequest.Text = "Generate Insight";
            this.btnSendRequest.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnSendRequest.UseVisualStyleBackColor = false;
            this.btnSendRequest.Click += new System.EventHandler(this.btnSendRequest_Click);
            // 
            // txtParameter
            // 
            this.txtParameter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtParameter.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtParameter.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtParameter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtParameter.Location = new System.Drawing.Point(20, 83);
            this.txtParameter.Name = "txtParameter";
            this.txtParameter.Size = new System.Drawing.Size(510, 30);
            this.txtParameter.TabIndex = 6;
            // 
            // cmbQuestionType
            // 
            this.cmbQuestionType.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbQuestionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuestionType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbQuestionType.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cmbQuestionType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbQuestionType.FormattingEnabled = true;
            this.cmbQuestionType.Items.AddRange(new object[] {
            "Get stock level of product...",
            "Customer details for...",
            "Sales report summary",
            "General ERP question..."});
            this.cmbQuestionType.Location = new System.Drawing.Point(20, 52);
            this.cmbQuestionType.Name = "cmbQuestionType";
            this.cmbQuestionType.Size = new System.Drawing.Size(510, 31);
            this.cmbQuestionType.TabIndex = 5;
            this.cmbQuestionType.SelectedIndexChanged += new System.EventHandler(this.cmbQuestionType_SelectedIndexChanged);
            // 
            // lblParameterPrompt
            // 
            this.lblParameterPrompt.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblParameterPrompt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParameterPrompt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblParameterPrompt.Location = new System.Drawing.Point(20, 20);
            this.lblParameterPrompt.Name = "lblParameterPrompt";
            this.lblParameterPrompt.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lblParameterPrompt.Size = new System.Drawing.Size(510, 32);
            this.lblParameterPrompt.TabIndex = 8;
            this.lblParameterPrompt.Text = "Select inquiry type:";
            this.lblParameterPrompt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.txtAIResponse);
            this.panelContent.Controls.Add(this.lblStatus);
            this.panelContent.Controls.Add(this.btnSendRequest);
            this.panelContent.Controls.Add(this.txtParameter);
            this.panelContent.Controls.Add(this.cmbQuestionType);
            this.panelContent.Controls.Add(this.lblParameterPrompt);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 80);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.panelContent.Size = new System.Drawing.Size(550, 420);
            this.panelContent.TabIndex = 9;
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblAssistantTitle);
            this.panelHeader.Controls.Add(this.pbAI);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(550, 80);
            this.panelHeader.TabIndex = 0;
            // 
            // FrmAi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(550, 500);
            this.Controls.Add(this.panelContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(568, 547);
            this.Name = "FrmAi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cognitive ERP Assistant";
            this.panelContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbAI)).EndInit();
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblAssistantTitle;
        private System.Windows.Forms.TextBox txtAIResponse;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.PictureBox pbAI;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Button btnSendRequest;
        private System.Windows.Forms.TextBox txtParameter;
        private System.Windows.Forms.ComboBox cmbQuestionType;
        private System.Windows.Forms.Label lblParameterPrompt;
        private System.Windows.Forms.Panel panelContent;
    }
}