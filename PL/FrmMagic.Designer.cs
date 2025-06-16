namespace Erp_V1
{
    partial class FrmMagic
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtEntityDefinition = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageDTO = new System.Windows.Forms.TabPage();
            this.txtDTOCode = new System.Windows.Forms.TextBox();
            this.tabPageDAOInterface = new System.Windows.Forms.TabPage();
            this.txtDAOInterfaceCode = new System.Windows.Forms.TextBox();
            this.tabPageDAOImplementation = new System.Windows.Forms.TabPage();
            this.txtDAOImplementationCode = new System.Windows.Forms.TextBox();
            this.tabPageBLLInterface = new System.Windows.Forms.TabPage();
            this.txtBLLInterfaceCode = new System.Windows.Forms.TextBox();
            this.tabPageBLLImplementation = new System.Windows.Forms.TabPage();
            this.txtBLLImplementationCode = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPageDTO.SuspendLayout();
            this.tabPageDAOInterface.SuspendLayout();
            this.tabPageDAOImplementation.SuspendLayout();
            this.tabPageBLLInterface.SuspendLayout();
            this.tabPageBLLImplementation.SuspendLayout();
            this.SuspendLayout();
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Entity Definition (JSON):";
            //
            // txtEntityDefinition
            //
            this.txtEntityDefinition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntityDefinition.Location = new System.Drawing.Point(16, 30);
            this.txtEntityDefinition.Multiline = true;
            this.txtEntityDefinition.Name = "txtEntityDefinition";
            this.txtEntityDefinition.Size = new System.Drawing.Size(956, 155);
            this.txtEntityDefinition.TabIndex = 1;
            this.txtEntityDefinition.Text = "{\r\n  \"entityName\": \"YourEntityName\",\r\n  \"properties\": [\r\n    {\"name\": \"ID\", \"dataType\": \"int\", \"isPrimaryKey\": true},\r\n    {\"name\": \"PropertyName1\", \"dataType\": \"string\"},\r\n    {\"name\": \"PropertyName2\", \"dataType\": \"int\"}\r\n  ]\r\n}";
            //
            // btnGenerate
            //
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Location = new System.Drawing.Point(878, 191);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(94, 23);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "Generate Code";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            //
            // tabControl1
            //
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageDTO);
            this.tabControl1.Controls.Add(this.tabPageDAOInterface);
            this.tabControl1.Controls.Add(this.tabPageDAOImplementation);
            this.tabControl1.Controls.Add(this.tabPageBLLInterface);
            this.tabControl1.Controls.Add(this.tabPageBLLImplementation);
            this.tabControl1.Location = new System.Drawing.Point(16, 220);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(956, 430);
            this.tabControl1.TabIndex = 3;
            //
            // tabPageDTO
            //
            this.tabPageDTO.Controls.Add(this.txtDTOCode);
            this.tabPageDTO.Location = new System.Drawing.Point(4, 22);
            this.tabPageDTO.Name = "tabPageDTO";
            this.tabPageDTO.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDTO.Size = new System.Drawing.Size(948, 404);
            this.tabPageDTO.TabIndex = 0;
            this.tabPageDTO.Text = "DTO Code";
            this.tabPageDTO.UseVisualStyleBackColor = true;
            //
            // txtDTOCode
            //
            this.txtDTOCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDTOCode.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDTOCode.Location = new System.Drawing.Point(3, 3);
            this.txtDTOCode.Multiline = true;
            this.txtDTOCode.Name = "txtDTOCode";
            this.txtDTOCode.ReadOnly = true;
            this.txtDTOCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDTOCode.Size = new System.Drawing.Size(942, 398);
            this.txtDTOCode.TabIndex = 0;
            //
            // tabPageDAOInterface
            //
            this.tabPageDAOInterface.Controls.Add(this.txtDAOInterfaceCode);
            this.tabPageDAOInterface.Location = new System.Drawing.Point(4, 22);
            this.tabPageDAOInterface.Name = "tabPageDAOInterface";
            this.tabPageDAOInterface.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDAOInterface.Size = new System.Drawing.Size(948, 404);
            this.tabPageDAOInterface.TabIndex = 1;
            this.tabPageDAOInterface.Text = "DAO Interface Code";
            this.tabPageDAOInterface.UseVisualStyleBackColor = true;
            //
            // txtDAOInterfaceCode
            //
            this.txtDAOInterfaceCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDAOInterfaceCode.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDAOInterfaceCode.Location = new System.Drawing.Point(3, 3);
            this.txtDAOInterfaceCode.Multiline = true;
            this.txtDAOInterfaceCode.Name = "txtDAOInterfaceCode";
            this.txtDAOInterfaceCode.ReadOnly = true;
            this.txtDAOInterfaceCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDAOInterfaceCode.Size = new System.Drawing.Size(942, 398);
            this.txtDAOInterfaceCode.TabIndex = 1;
            //
            // tabPageDAOImplementation
            //
            this.tabPageDAOImplementation.Controls.Add(this.txtDAOImplementationCode);
            this.tabPageDAOImplementation.Location = new System.Drawing.Point(4, 22);
            this.tabPageDAOImplementation.Name = "tabPageDAOImplementation";
            this.tabPageDAOImplementation.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDAOImplementation.Size = new System.Drawing.Size(948, 404);
            this.tabPageDAOImplementation.TabIndex = 2;
            this.tabPageDAOImplementation.Text = "DAO Implementation Code";
            this.tabPageDAOImplementation.UseVisualStyleBackColor = true;
            //
            // txtDAOImplementationCode
            //
            this.txtDAOImplementationCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDAOImplementationCode.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDAOImplementationCode.Location = new System.Drawing.Point(3, 3);
            this.txtDAOImplementationCode.Multiline = true;
            this.txtDAOImplementationCode.Name = "txtDAOImplementationCode";
            this.txtDAOImplementationCode.ReadOnly = true;
            this.txtDAOImplementationCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDAOImplementationCode.Size = new System.Drawing.Size(942, 398);
            this.txtDAOImplementationCode.TabIndex = 1;
            //
            // tabPageBLLInterface
            //
            this.tabPageBLLInterface.Controls.Add(this.txtBLLInterfaceCode);
            this.tabPageBLLInterface.Location = new System.Drawing.Point(4, 22);
            this.tabPageBLLInterface.Name = "tabPageBLLInterface";
            this.tabPageBLLInterface.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBLLInterface.Size = new System.Drawing.Size(948, 404);
            this.tabPageBLLInterface.TabIndex = 3;
            this.tabPageBLLInterface.Text = "BLL Interface Code";
            this.tabPageBLLInterface.UseVisualStyleBackColor = true;
            //
            // txtBLLInterfaceCode
            //
            this.txtBLLInterfaceCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBLLInterfaceCode.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBLLInterfaceCode.Location = new System.Drawing.Point(3, 3);
            this.txtBLLInterfaceCode.Multiline = true;
            this.txtBLLInterfaceCode.Name = "txtBLLInterfaceCode";
            this.txtBLLInterfaceCode.ReadOnly = true;
            this.txtBLLInterfaceCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBLLInterfaceCode.Size = new System.Drawing.Size(942, 398);
            this.txtBLLInterfaceCode.TabIndex = 1;
            //
            // tabPageBLLImplementation
            //
            this.tabPageBLLImplementation.Controls.Add(this.txtBLLImplementationCode);
            this.tabPageBLLImplementation.Location = new System.Drawing.Point(4, 22);
            this.tabPageBLLImplementation.Name = "tabPageBLLImplementation";
            this.tabPageBLLImplementation.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBLLImplementation.Size = new System.Drawing.Size(948, 404);
            this.tabPageBLLImplementation.TabIndex = 4;
            this.tabPageBLLImplementation.Text = "BLL Implementation Code";
            this.tabPageBLLImplementation.UseVisualStyleBackColor = true;
            //
            // txtBLLImplementationCode
            //
            this.txtBLLImplementationCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBLLImplementationCode.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBLLImplementationCode.Location = new System.Drawing.Point(3, 3);
            this.txtBLLImplementationCode.Multiline = true;
            this.txtBLLImplementationCode.Name = "txtBLLImplementationCode";
            this.txtBLLImplementationCode.ReadOnly = true;
            this.txtBLLImplementationCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBLLImplementationCode.Size = new System.Drawing.Size(942, 398);
            this.txtBLLImplementationCode.TabIndex = 1;
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 662);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.txtEntityDefinition);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "CRUD Code Generator";
            this.tabControl1.ResumeLayout(false);
            this.tabPageDTO.ResumeLayout(false);
            this.tabPageDTO.PerformLayout();
            this.tabPageDAOInterface.ResumeLayout(false);
            this.tabPageDAOInterface.PerformLayout();
            this.tabPageDAOImplementation.ResumeLayout(false);
            this.tabPageDAOImplementation.PerformLayout();
            this.tabPageBLLInterface.ResumeLayout(false);
            this.tabPageBLLInterface.PerformLayout();
            this.tabPageBLLImplementation.ResumeLayout(false);
            this.tabPageBLLImplementation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEntityDefinition;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageDTO;
        private System.Windows.Forms.TabPage tabPageDAOInterface;
        private System.Windows.Forms.TabPage tabPageDAOImplementation;
        private System.Windows.Forms.TabPage tabPageBLLInterface;
        private System.Windows.Forms.TabPage tabPageBLLImplementation;
        private System.Windows.Forms.TextBox txtDTOCode;
        private System.Windows.Forms.TextBox txtDAOInterfaceCode;
        private System.Windows.Forms.TextBox txtDAOImplementationCode;
        private System.Windows.Forms.TextBox txtBLLInterfaceCode;
        private System.Windows.Forms.TextBox txtBLLImplementationCode;
    }
}