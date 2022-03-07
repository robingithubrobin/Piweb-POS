namespace PiwebSystemsPOS
{
    partial class frmCreateUser
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.chkIsActive = new MetroFramework.Controls.MetroCheckBox();
            this.calExpiryDate = new MetroFramework.Controls.MetroDateTime();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.chkMob2WhatsApp = new System.Windows.Forms.CheckBox();
            this.chkWhatsApp = new System.Windows.Forms.CheckBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMobile2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.cmbWorkStation = new System.Windows.Forms.ComboBox();
            this.chkUseLoginCred = new MetroFramework.Controls.MetroCheckBox();
            this.btnSetClerkPsw = new System.Windows.Forms.Button();
            this.label39 = new System.Windows.Forms.Label();
            this.edtClerkName = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.edtClerkPsw = new System.Windows.Forms.TextBox();
            this.EditID = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.metroTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EditID)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.metroTabControl1);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1234, 784);
            this.panel1.TabIndex = 1;
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Controls.Add(this.metroTabPage3);
            this.metroTabControl1.Location = new System.Drawing.Point(3, 3);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 2;
            this.metroTabControl1.Size = new System.Drawing.Size(1228, 693);
            this.metroTabControl1.TabIndex = 13;
            this.metroTabControl1.UseSelectable = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.chkIsActive);
            this.metroTabPage1.Controls.Add(this.calExpiryDate);
            this.metroTabPage1.Controls.Add(this.cmbRole);
            this.metroTabPage1.Controls.Add(this.txtPassword);
            this.metroTabPage1.Controls.Add(this.label4);
            this.metroTabPage1.Controls.Add(this.label3);
            this.metroTabPage1.Controls.Add(this.label2);
            this.metroTabPage1.Controls.Add(this.txtFullName);
            this.metroTabPage1.Controls.Add(this.label1);
            this.metroTabPage1.Controls.Add(this.txtName);
            this.metroTabPage1.Controls.Add(this.lblName);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 6;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(1220, 651);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "General";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 8;
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkIsActive.Location = new System.Drawing.Point(570, 73);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(62, 19);
            this.chkIsActive.TabIndex = 11;
            this.chkIsActive.Text = "Active";
            this.chkIsActive.UseSelectable = true;
            // 
            // calExpiryDate
            // 
            this.calExpiryDate.Location = new System.Drawing.Point(570, 18);
            this.calExpiryDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.calExpiryDate.Name = "calExpiryDate";
            this.calExpiryDate.Size = new System.Drawing.Size(208, 31);
            this.calExpiryDate.TabIndex = 10;
            // 
            // cmbRole
            // 
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Items.AddRange(new object[] {
            "Manager",
            "Supervisor",
            "Cashier",
            "Administrator"});
            this.cmbRole.Location = new System.Drawing.Point(139, 131);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(281, 33);
            this.cmbRole.TabIndex = 9;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(139, 94);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(281, 31);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(463, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "Expiry Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(3, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "User Role";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(3, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Password";
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(139, 57);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(281, 31);
            this.txtFullName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(3, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Full Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(139, 20);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(281, 31);
            this.txtName.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Location = new System.Drawing.Point(3, 23);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(99, 25);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "User Name";
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.chkMob2WhatsApp);
            this.metroTabPage2.Controls.Add(this.chkWhatsApp);
            this.metroTabPage2.Controls.Add(this.txtEmail);
            this.metroTabPage2.Controls.Add(this.label9);
            this.metroTabPage2.Controls.Add(this.txtMobile2);
            this.metroTabPage2.Controls.Add(this.label11);
            this.metroTabPage2.Controls.Add(this.txtMobile);
            this.metroTabPage2.Controls.Add(this.label12);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 6;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(1220, 651);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Communication";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 8;
            // 
            // chkMob2WhatsApp
            // 
            this.chkMob2WhatsApp.AutoSize = true;
            this.chkMob2WhatsApp.BackColor = System.Drawing.Color.Transparent;
            this.chkMob2WhatsApp.Location = new System.Drawing.Point(426, 62);
            this.chkMob2WhatsApp.Name = "chkMob2WhatsApp";
            this.chkMob2WhatsApp.Size = new System.Drawing.Size(122, 29);
            this.chkMob2WhatsApp.TabIndex = 20;
            this.chkMob2WhatsApp.Text = "WhatsApp";
            this.chkMob2WhatsApp.UseVisualStyleBackColor = false;
            this.chkMob2WhatsApp.Visible = false;
            // 
            // chkWhatsApp
            // 
            this.chkWhatsApp.AutoSize = true;
            this.chkWhatsApp.BackColor = System.Drawing.Color.Transparent;
            this.chkWhatsApp.Location = new System.Drawing.Point(426, 25);
            this.chkWhatsApp.Name = "chkWhatsApp";
            this.chkWhatsApp.Size = new System.Drawing.Size(122, 29);
            this.chkWhatsApp.TabIndex = 20;
            this.chkWhatsApp.Text = "WhatsApp";
            this.chkWhatsApp.UseVisualStyleBackColor = false;
            this.chkWhatsApp.Visible = false;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(139, 96);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(281, 31);
            this.txtEmail.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(3, 99);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 25);
            this.label9.TabIndex = 19;
            this.label9.Text = "Email";
            // 
            // txtMobile2
            // 
            this.txtMobile2.Location = new System.Drawing.Point(139, 59);
            this.txtMobile2.Name = "txtMobile2";
            this.txtMobile2.Size = new System.Drawing.Size(281, 31);
            this.txtMobile2.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(3, 62);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 25);
            this.label11.TabIndex = 17;
            this.label11.Text = "Phone 2";
            // 
            // txtMobile
            // 
            this.txtMobile.Location = new System.Drawing.Point(139, 22);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(281, 31);
            this.txtMobile.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(3, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 25);
            this.label12.TabIndex = 16;
            this.label12.Text = "Phone";
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.Controls.Add(this.cmbWorkStation);
            this.metroTabPage3.Controls.Add(this.chkUseLoginCred);
            this.metroTabPage3.Controls.Add(this.btnSetClerkPsw);
            this.metroTabPage3.Controls.Add(this.label39);
            this.metroTabPage3.Controls.Add(this.edtClerkName);
            this.metroTabPage3.Controls.Add(this.label38);
            this.metroTabPage3.Controls.Add(this.edtClerkPsw);
            this.metroTabPage3.Controls.Add(this.EditID);
            this.metroTabPage3.Controls.Add(this.label5);
            this.metroTabPage3.Controls.Add(this.label37);
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.HorizontalScrollbarSize = 6;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(1220, 651);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "Set Clerk";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            this.metroTabPage3.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.VerticalScrollbarSize = 8;
            // 
            // cmbWorkStation
            // 
            this.cmbWorkStation.FormattingEnabled = true;
            this.cmbWorkStation.Location = new System.Drawing.Point(164, 24);
            this.cmbWorkStation.Name = "cmbWorkStation";
            this.cmbWorkStation.Size = new System.Drawing.Size(321, 33);
            this.cmbWorkStation.TabIndex = 66;
            // 
            // chkUseLoginCred
            // 
            this.chkUseLoginCred.AutoSize = true;
            this.chkUseLoginCred.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.chkUseLoginCred.Location = new System.Drawing.Point(164, 186);
            this.chkUseLoginCred.Name = "chkUseLoginCred";
            this.chkUseLoginCred.Size = new System.Drawing.Size(158, 19);
            this.chkUseLoginCred.TabIndex = 65;
            this.chkUseLoginCred.Text = "Use Login Credentials";
            this.chkUseLoginCred.UseSelectable = true;
            this.chkUseLoginCred.CheckedChanged += new System.EventHandler(this.chkUseLoginCred_CheckedChanged);
            // 
            // btnSetClerkPsw
            // 
            this.btnSetClerkPsw.Location = new System.Drawing.Point(164, 232);
            this.btnSetClerkPsw.Name = "btnSetClerkPsw";
            this.btnSetClerkPsw.Size = new System.Drawing.Size(158, 34);
            this.btnSetClerkPsw.TabIndex = 64;
            this.btnSetClerkPsw.Text = "Set Clerk";
            this.btnSetClerkPsw.UseVisualStyleBackColor = true;
            this.btnSetClerkPsw.Click += new System.EventHandler(this.btnSetClerkPsw_Click);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.label39.Location = new System.Drawing.Point(4, 106);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(103, 25);
            this.label39.TabIndex = 63;
            this.label39.Text = "Clerk Name";
            // 
            // edtClerkName
            // 
            this.edtClerkName.Location = new System.Drawing.Point(164, 100);
            this.edtClerkName.MaxLength = 10;
            this.edtClerkName.Name = "edtClerkName";
            this.edtClerkName.Size = new System.Drawing.Size(321, 31);
            this.edtClerkName.TabIndex = 62;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.label38.Location = new System.Drawing.Point(3, 140);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(131, 25);
            this.label38.TabIndex = 61;
            this.label38.Text = "Clerk Password";
            // 
            // edtClerkPsw
            // 
            this.edtClerkPsw.Location = new System.Drawing.Point(164, 137);
            this.edtClerkPsw.MaxLength = 6;
            this.edtClerkPsw.Name = "edtClerkPsw";
            this.edtClerkPsw.Size = new System.Drawing.Size(321, 31);
            this.edtClerkPsw.TabIndex = 60;
            this.edtClerkPsw.UseSystemPasswordChar = true;
            // 
            // EditID
            // 
            this.EditID.Location = new System.Drawing.Point(164, 63);
            this.EditID.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.EditID.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.EditID.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.EditID.Name = "EditID";
            this.EditID.Size = new System.Drawing.Size(69, 31);
            this.EditID.TabIndex = 59;
            this.EditID.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(4, 32);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 25);
            this.label5.TabIndex = 58;
            this.label5.Text = "WorkStation ID";
            this.label5.Click += new System.EventHandler(this.label37_Click);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.BackColor = System.Drawing.Color.Transparent;
            this.label37.Location = new System.Drawing.Point(4, 65);
            this.label37.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(74, 25);
            this.label37.TabIndex = 58;
            this.label37.Text = "Clerk ID";
            this.label37.Click += new System.EventHandler(this.label37_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(997, 752);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(114, 29);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1117, 752);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 29);
            this.button1.TabIndex = 12;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // frmCreateUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 864);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCreateUser";
            this.Resizable = false;
            this.Text = "New User";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmCreateUser_Load);
            this.panel1.ResumeLayout(false);
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage1.PerformLayout();
            this.metroTabPage2.ResumeLayout(false);
            this.metroTabPage2.PerformLayout();
            this.metroTabPage3.ResumeLayout(false);
            this.metroTabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EditID)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private System.Windows.Forms.CheckBox chkMob2WhatsApp;
        private System.Windows.Forms.CheckBox chkWhatsApp;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMobile2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.Label label12;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Label label3;
        private MetroFramework.Controls.MetroCheckBox chkIsActive;
        private MetroFramework.Controls.MetroDateTime calExpiryDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSetClerkPsw;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox edtClerkName;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox edtClerkPsw;
        private System.Windows.Forms.NumericUpDown EditID;
        private System.Windows.Forms.Label label37;
        private MetroFramework.Controls.MetroCheckBox chkUseLoginCred;
        private System.Windows.Forms.ComboBox cmbWorkStation;
        private System.Windows.Forms.Label label5;



    }
}