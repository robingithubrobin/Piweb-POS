namespace PiwebSystemsPOS
{
    partial class frmLicRegister
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLicRegister));
            this.label1 = new System.Windows.Forms.Label();
            this.txtProductID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProductKey = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnUserKeyboard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Product ID :";
            // 
            // txtProductID
            // 
            this.txtProductID.Location = new System.Drawing.Point(143, 94);
            this.txtProductID.Name = "txtProductID";
            this.txtProductID.ReadOnly = true;
            this.txtProductID.Size = new System.Drawing.Size(298, 29);
            this.txtProductID.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Product Key :";
            // 
            // txtProductKey
            // 
            this.txtProductKey.Location = new System.Drawing.Point(143, 141);
            this.txtProductKey.Name = "txtProductKey";
            this.txtProductKey.Size = new System.Drawing.Size(298, 29);
            this.txtProductKey.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(367, 189);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 39);
            this.button1.TabIndex = 2;
            this.button1.Text = "Activate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnUserKeyboard
            // 
            this.btnUserKeyboard.FlatAppearance.BorderSize = 0;
            this.btnUserKeyboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserKeyboard.Image = ((System.Drawing.Image)(resources.GetObject("btnUserKeyboard.Image")));
            this.btnUserKeyboard.Location = new System.Drawing.Point(447, 132);
            this.btnUserKeyboard.Name = "btnUserKeyboard";
            this.btnUserKeyboard.Size = new System.Drawing.Size(69, 44);
            this.btnUserKeyboard.TabIndex = 4;
            this.btnUserKeyboard.UseVisualStyleBackColor = true;
            this.btnUserKeyboard.Click += new System.EventHandler(this.btnUserKeyboard_Click);
            // 
            // frmLicRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 255);
            this.Controls.Add(this.btnUserKeyboard);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtProductKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtProductID);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLicRegister";
            this.Resizable = false;
            this.Text = "Activate License";
            this.Load += new System.EventHandler(this.frmLicRegister_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProductID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProductKey;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnUserKeyboard;
    }
}