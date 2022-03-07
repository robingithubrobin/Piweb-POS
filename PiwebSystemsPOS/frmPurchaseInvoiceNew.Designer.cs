namespace PiwebSystemsPOS
{
    partial class frmPurchaseInvoiceNew
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
            this.InvoiceDate = new MetroFramework.Controls.MetroDateTime();
            this.ReceivingDate = new MetroFramework.Controls.MetroDateTime();
            this.cmbSupplier = new MetroFramework.Controls.MetroComboBox();
            this.cmbInvoiceType = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.txtRefNo = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InvoiceDate
            // 
            this.InvoiceDate.Location = new System.Drawing.Point(298, 138);
            this.InvoiceDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.InvoiceDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.InvoiceDate.Name = "InvoiceDate";
            this.InvoiceDate.Size = new System.Drawing.Size(428, 29);
            this.InvoiceDate.TabIndex = 12;
            // 
            // ReceivingDate
            // 
            this.ReceivingDate.Location = new System.Drawing.Point(298, 369);
            this.ReceivingDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ReceivingDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.ReceivingDate.Name = "ReceivingDate";
            this.ReceivingDate.Size = new System.Drawing.Size(428, 29);
            this.ReceivingDate.TabIndex = 13;
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.ItemHeight = 23;
            this.cmbSupplier.Location = new System.Drawing.Point(298, 258);
            this.cmbSupplier.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(428, 29);
            this.cmbSupplier.TabIndex = 10;
            this.cmbSupplier.UseSelectable = true;
            // 
            // cmbInvoiceType
            // 
            this.cmbInvoiceType.FormattingEnabled = true;
            this.cmbInvoiceType.ItemHeight = 23;
            this.cmbInvoiceType.Location = new System.Drawing.Point(298, 198);
            this.cmbInvoiceType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbInvoiceType.Name = "cmbInvoiceType";
            this.cmbInvoiceType.Size = new System.Drawing.Size(428, 29);
            this.cmbInvoiceType.TabIndex = 11;
            this.cmbInvoiceType.UseSelectable = true;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.ForeColor = System.Drawing.Color.Red;
            this.metroLabel4.Location = new System.Drawing.Point(39, 274);
            this.metroLabel4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(61, 19);
            this.metroLabel4.TabIndex = 4;
            this.metroLabel4.Text = "Supplier:";
            this.metroLabel4.UseCustomForeColor = true;
            // 
            // txtRefNo
            // 
            // 
            // 
            // 
            this.txtRefNo.CustomButton.Image = null;
            this.txtRefNo.CustomButton.Location = new System.Drawing.Point(594, 2);
            this.txtRefNo.CustomButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRefNo.CustomButton.Name = "";
            this.txtRefNo.CustomButton.Size = new System.Drawing.Size(50, 51);
            this.txtRefNo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtRefNo.CustomButton.TabIndex = 1;
            this.txtRefNo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtRefNo.CustomButton.UseSelectable = true;
            this.txtRefNo.CustomButton.Visible = false;
            this.txtRefNo.Lines = new string[0];
            this.txtRefNo.Location = new System.Drawing.Point(298, 318);
            this.txtRefNo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRefNo.MaxLength = 32767;
            this.txtRefNo.Name = "txtRefNo";
            this.txtRefNo.PasswordChar = '\0';
            this.txtRefNo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtRefNo.SelectedText = "";
            this.txtRefNo.SelectionLength = 0;
            this.txtRefNo.SelectionStart = 0;
            this.txtRefNo.ShortcutsEnabled = true;
            this.txtRefNo.Size = new System.Drawing.Size(430, 35);
            this.txtRefNo.TabIndex = 9;
            this.txtRefNo.UseSelectable = true;
            this.txtRefNo.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtRefNo.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.ForeColor = System.Drawing.Color.Red;
            this.metroLabel3.Location = new System.Drawing.Point(39, 214);
            this.metroLabel3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(139, 19);
            this.metroLabel3.TabIndex = 5;
            this.metroLabel3.Text = "Purchase Order Type:";
            this.metroLabel3.UseCustomForeColor = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.ForeColor = System.Drawing.Color.Red;
            this.metroLabel2.Location = new System.Drawing.Point(39, 154);
            this.metroLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(140, 19);
            this.metroLabel2.TabIndex = 6;
            this.metroLabel2.Text = "Purchase Order Date:";
            this.metroLabel2.UseCustomForeColor = true;
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel5.ForeColor = System.Drawing.Color.Red;
            this.metroLabel5.Location = new System.Drawing.Point(39, 385);
            this.metroLabel5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(102, 19);
            this.metroLabel5.TabIndex = 7;
            this.metroLabel5.Text = "Receiving Date:";
            this.metroLabel5.UseCustomForeColor = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(39, 318);
            this.metroLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(143, 19);
            this.metroLabel1.TabIndex = 8;
            this.metroLabel1.Text = "Reference Invoice No.:";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(229)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = global::PiwebSystemsPOS.Properties.Resources.check_icon;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(560, 482);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(170, 65);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "  OK";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // frmPurchaseInvoiceNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 582);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.InvoiceDate);
            this.Controls.Add(this.ReceivingDate);
            this.Controls.Add(this.cmbSupplier);
            this.Controls.Add(this.cmbInvoiceType);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.txtRefNo);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.metroLabel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPurchaseInvoiceNew";
            this.Padding = new System.Windows.Forms.Padding(30, 92, 30, 31);
            this.Resizable = false;
            this.Text = "Create Purchase Order";
            this.Load += new System.EventHandler(this.frmPurchaseInvoiceNew_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroDateTime InvoiceDate;
        private MetroFramework.Controls.MetroDateTime ReceivingDate;
        private MetroFramework.Controls.MetroComboBox cmbSupplier;
        private MetroFramework.Controls.MetroComboBox cmbInvoiceType;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroTextBox txtRefNo;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.Button btnAdd;

    }
}