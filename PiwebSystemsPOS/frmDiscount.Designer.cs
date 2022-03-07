namespace PiwebSystemsPOS
{
    partial class frmDiscount
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
            this.rdPercentage = new MetroFramework.Controls.MetroRadioButton();
            this.rdFixedAmount = new MetroFramework.Controls.MetroRadioButton();
            this.cmbiType = new MetroFramework.Controls.MetroComboBox();
            this.txtFixedAmount = new MetroFramework.Controls.MetroTextBox();
            this.txtPercentage = new MetroFramework.Controls.MetroTextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rdPercentage
            // 
            this.rdPercentage.AutoSize = true;
            this.rdPercentage.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rdPercentage.Location = new System.Drawing.Point(287, 120);
            this.rdPercentage.Name = "rdPercentage";
            this.rdPercentage.Size = new System.Drawing.Size(92, 19);
            this.rdPercentage.TabIndex = 0;
            this.rdPercentage.Text = "Percentage";
            this.rdPercentage.UseSelectable = true;
            this.rdPercentage.CheckedChanged += new System.EventHandler(this.rdPercentage_CheckedChanged);
            // 
            // rdFixedAmount
            // 
            this.rdFixedAmount.AutoSize = true;
            this.rdFixedAmount.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rdFixedAmount.Location = new System.Drawing.Point(441, 120);
            this.rdFixedAmount.Name = "rdFixedAmount";
            this.rdFixedAmount.Size = new System.Drawing.Size(110, 19);
            this.rdFixedAmount.TabIndex = 0;
            this.rdFixedAmount.Text = "Fixed Amount";
            this.rdFixedAmount.UseSelectable = true;
            this.rdFixedAmount.CheckedChanged += new System.EventHandler(this.rdFixedAmount_CheckedChanged);
            // 
            // cmbiType
            // 
            this.cmbiType.FormattingEnabled = true;
            this.cmbiType.ItemHeight = 23;
            this.cmbiType.Items.AddRange(new object[] {
            "discount from the last sold item",
            "discount from the sub total"});
            this.cmbiType.Location = new System.Drawing.Point(227, 191);
            this.cmbiType.Name = "cmbiType";
            this.cmbiType.Size = new System.Drawing.Size(378, 29);
            this.cmbiType.TabIndex = 1;
            this.cmbiType.UseSelectable = true;
            // 
            // txtFixedAmount
            // 
            // 
            // 
            // 
            this.txtFixedAmount.CustomButton.Image = null;
            this.txtFixedAmount.CustomButton.Location = new System.Drawing.Point(340, 1);
            this.txtFixedAmount.CustomButton.Name = "";
            this.txtFixedAmount.CustomButton.Size = new System.Drawing.Size(43, 43);
            this.txtFixedAmount.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtFixedAmount.CustomButton.TabIndex = 1;
            this.txtFixedAmount.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtFixedAmount.CustomButton.UseSelectable = true;
            this.txtFixedAmount.CustomButton.Visible = false;
            this.txtFixedAmount.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtFixedAmount.Lines = new string[0];
            this.txtFixedAmount.Location = new System.Drawing.Point(227, 250);
            this.txtFixedAmount.MaxLength = 32767;
            this.txtFixedAmount.Name = "txtFixedAmount";
            this.txtFixedAmount.PasswordChar = '\0';
            this.txtFixedAmount.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtFixedAmount.SelectedText = "";
            this.txtFixedAmount.SelectionLength = 0;
            this.txtFixedAmount.SelectionStart = 0;
            this.txtFixedAmount.ShortcutsEnabled = true;
            this.txtFixedAmount.Size = new System.Drawing.Size(384, 45);
            this.txtFixedAmount.TabIndex = 2;
            this.txtFixedAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFixedAmount.UseSelectable = true;
            this.txtFixedAmount.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtFixedAmount.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtPercentage
            // 
            // 
            // 
            // 
            this.txtPercentage.CustomButton.Image = null;
            this.txtPercentage.CustomButton.Location = new System.Drawing.Point(340, 1);
            this.txtPercentage.CustomButton.Name = "";
            this.txtPercentage.CustomButton.Size = new System.Drawing.Size(43, 43);
            this.txtPercentage.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtPercentage.CustomButton.TabIndex = 1;
            this.txtPercentage.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPercentage.CustomButton.UseSelectable = true;
            this.txtPercentage.CustomButton.Visible = false;
            this.txtPercentage.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtPercentage.Lines = new string[0];
            this.txtPercentage.Location = new System.Drawing.Point(227, 310);
            this.txtPercentage.MaxLength = 32767;
            this.txtPercentage.Name = "txtPercentage";
            this.txtPercentage.PasswordChar = '\0';
            this.txtPercentage.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPercentage.SelectedText = "";
            this.txtPercentage.SelectionLength = 0;
            this.txtPercentage.SelectionStart = 0;
            this.txtPercentage.ShortcutsEnabled = true;
            this.txtPercentage.Size = new System.Drawing.Size(384, 45);
            this.txtPercentage.TabIndex = 2;
            this.txtPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPercentage.UseSelectable = true;
            this.txtPercentage.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtPercentage.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(333, 386);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(128, 61);
            this.btnOk.TabIndex = 15;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(477, 386);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 61);
            this.button1.TabIndex = 15;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 28);
            this.label1.TabIndex = 16;
            this.label1.Text = "Discount Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(40, 261);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 28);
            this.label2.TabIndex = 16;
            this.label2.Text = "Fixed Amount";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(44, 318);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 28);
            this.label3.TabIndex = 16;
            this.label3.Text = "Percentage (%)";
            // 
            // frmDiscount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 472);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtPercentage);
            this.Controls.Add(this.txtFixedAmount);
            this.Controls.Add(this.cmbiType);
            this.Controls.Add(this.rdFixedAmount);
            this.Controls.Add(this.rdPercentage);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDiscount";
            this.Resizable = false;
            this.Text = "Discount";
            this.Load += new System.EventHandler(this.frmDiscount_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroRadioButton rdPercentage;
        private MetroFramework.Controls.MetroRadioButton rdFixedAmount;
        private MetroFramework.Controls.MetroComboBox cmbiType;
        private MetroFramework.Controls.MetroTextBox txtFixedAmount;
        private MetroFramework.Controls.MetroTextBox txtPercentage;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}