namespace PiwebSystemsPOS
{
    partial class frmLicGenerate
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
            this.txtProductID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbLicType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtExperienceDays = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProductKey = new System.Windows.Forms.TextBox();
            this.btnGenerateKey = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Product ID :";
            // 
            // txtProductID
            // 
            this.txtProductID.Location = new System.Drawing.Point(168, 74);
            this.txtProductID.Name = "txtProductID";
            this.txtProductID.Size = new System.Drawing.Size(353, 29);
            this.txtProductID.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "License Type :";
            // 
            // cmbLicType
            // 
            this.cmbLicType.FormattingEnabled = true;
            this.cmbLicType.Items.AddRange(new object[] {
            "Full",
            "Trial"});
            this.cmbLicType.Location = new System.Drawing.Point(168, 109);
            this.cmbLicType.Name = "cmbLicType";
            this.cmbLicType.Size = new System.Drawing.Size(172, 29);
            this.cmbLicType.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Experience Days :";
            // 
            // txtExperienceDays
            // 
            this.txtExperienceDays.Location = new System.Drawing.Point(168, 144);
            this.txtExperienceDays.Name = "txtExperienceDays";
            this.txtExperienceDays.Size = new System.Drawing.Size(172, 29);
            this.txtExperienceDays.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "Product Key :";
            // 
            // txtProductKey
            // 
            this.txtProductKey.Location = new System.Drawing.Point(168, 179);
            this.txtProductKey.Name = "txtProductKey";
            this.txtProductKey.ReadOnly = true;
            this.txtProductKey.Size = new System.Drawing.Size(353, 29);
            this.txtProductKey.TabIndex = 1;
            // 
            // btnGenerateKey
            // 
            this.btnGenerateKey.Location = new System.Drawing.Point(391, 227);
            this.btnGenerateKey.Name = "btnGenerateKey";
            this.btnGenerateKey.Size = new System.Drawing.Size(130, 44);
            this.btnGenerateKey.TabIndex = 3;
            this.btnGenerateKey.Text = "Generate";
            this.btnGenerateKey.UseVisualStyleBackColor = true;
            this.btnGenerateKey.Click += new System.EventHandler(this.btnGenerateKey_Click);
            // 
            // frmLicGenerate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 288);
            this.Controls.Add(this.btnGenerateKey);
            this.Controls.Add(this.cmbLicType);
            this.Controls.Add(this.txtExperienceDays);
            this.Controls.Add(this.txtProductKey);
            this.Controls.Add(this.txtProductID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLicGenerate";
            this.Resizable = false;
            this.Text = "Generate";
            this.Load += new System.EventHandler(this.frmLicGenerate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProductID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbLicType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtExperienceDays;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtProductKey;
        private System.Windows.Forms.Button btnGenerateKey;
    }
}