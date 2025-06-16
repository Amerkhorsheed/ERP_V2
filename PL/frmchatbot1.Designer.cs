namespace Erp_V1
{
    partial class frmchatbot1
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
            this.txtChatDisplay = new System.Windows.Forms.TextBox();
            this.txtUserInput = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.pnlInputArea = new System.Windows.Forms.Panel();
            this.btnVoiceInput = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlChatDisplayArea = new System.Windows.Forms.Panel();
            this.pnlInputArea.SuspendLayout();
            this.pnlChatDisplayArea.SuspendLayout();
            this.SuspendLayout();
            //
            // txtChatDisplay
            //
            this.txtChatDisplay.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtChatDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtChatDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChatDisplay.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtChatDisplay.Location = new System.Drawing.Point(10, 10);
            this.txtChatDisplay.Multiline = true;
            this.txtChatDisplay.Name = "txtChatDisplay";
            this.txtChatDisplay.ReadOnly = true;
            this.txtChatDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChatDisplay.Size = new System.Drawing.Size(578, 329);
            this.txtChatDisplay.TabIndex = 0;
            //
            // txtUserInput
            //
            this.txtUserInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserInput.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtUserInput.Location = new System.Drawing.Point(3, 3);
            this.txtUserInput.Name = "txtUserInput";
            this.txtUserInput.Size = new System.Drawing.Size(429, 27);
            this.txtUserInput.TabIndex = 1;
            this.txtUserInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserInput_KeyPress);
            //
            // btnSend
            //
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSend.FlatAppearance.BorderSize = 0;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(438, 2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(70, 28);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            //
            // pnlInputArea
            //
            this.pnlInputArea.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlInputArea.Controls.Add(this.btnVoiceInput);
            this.pnlInputArea.Controls.Add(this.btnSend);
            this.pnlInputArea.Controls.Add(this.txtUserInput);
            this.pnlInputArea.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInputArea.Location = new System.Drawing.Point(0, 374);
            this.pnlInputArea.Name = "pnlInputArea";
            this.pnlInputArea.Size = new System.Drawing.Size(598, 34);
            this.pnlInputArea.TabIndex = 3;
            //
            // btnVoiceInput
            //
            this.btnVoiceInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVoiceInput.BackColor = System.Drawing.Color.ForestGreen;
            this.btnVoiceInput.FlatAppearance.BorderSize = 0;
            this.btnVoiceInput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoiceInput.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnVoiceInput.ForeColor = System.Drawing.Color.White;
            this.btnVoiceInput.Location = new System.Drawing.Point(514, 2);
            this.btnVoiceInput.Name = "btnVoiceInput";
            this.btnVoiceInput.Size = new System.Drawing.Size(72, 28);
            this.btnVoiceInput.TabIndex = 3;
            this.btnVoiceInput.Text = "Voice";
            this.btnVoiceInput.UseVisualStyleBackColor = false;
            this.btnVoiceInput.Click += new System.EventHandler(this.btnVoiceInput_Click);
            //
            // lblStatus
            //
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblStatus.Location = new System.Drawing.Point(0, 358);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(598, 16);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Status: Ready";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlChatDisplayArea
            //
            this.pnlChatDisplayArea.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlChatDisplayArea.Controls.Add(this.txtChatDisplay);
            this.pnlChatDisplayArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChatDisplayArea.Location = new System.Drawing.Point(0, 0);
            this.pnlChatDisplayArea.Name = "pnlChatDisplayArea";
            this.pnlChatDisplayArea.Padding = new System.Windows.Forms.Padding(10);
            this.pnlChatDisplayArea.Size = new System.Drawing.Size(598, 358);
            this.pnlChatDisplayArea.TabIndex = 5;
            //
            // frmchatbot1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(598, 408);
            this.Controls.Add(this.pnlChatDisplayArea);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.pnlInputArea);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmchatbot1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Professional Chatbot";
            this.pnlInputArea.ResumeLayout(false);
            this.pnlInputArea.PerformLayout();
            this.pnlChatDisplayArea.ResumeLayout(false);
            this.pnlChatDisplayArea.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtChatDisplay;
        private System.Windows.Forms.TextBox txtUserInput;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Panel pnlInputArea;
        private System.Windows.Forms.Button btnVoiceInput;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlChatDisplayArea;
    }
}