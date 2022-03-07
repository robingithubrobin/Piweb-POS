namespace PiwebSystemsPOS
{
    partial class ucConfigurations
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tileBusinessSetup = new MetroFramework.Controls.MetroTile();
            this.tileManageUsers = new MetroFramework.Controls.MetroTile();
            this.tilePrinterSettings = new MetroFramework.Controls.MetroTile();
            this.tileDeviceRegister = new MetroFramework.Controls.MetroTile();
            this.SuspendLayout();
            // 
            // tileBusinessSetup
            // 
            this.tileBusinessSetup.ActiveControl = null;
            this.tileBusinessSetup.Location = new System.Drawing.Point(26, 30);
            this.tileBusinessSetup.Name = "tileBusinessSetup";
            this.tileBusinessSetup.Size = new System.Drawing.Size(160, 114);
            this.tileBusinessSetup.TabIndex = 1;
            this.tileBusinessSetup.Text = "Business Setup";
            this.tileBusinessSetup.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.tileBusinessSetup.UseSelectable = true;
            this.tileBusinessSetup.Click += new System.EventHandler(this.tileBusinessSetup_Click);
            // 
            // tileManageUsers
            // 
            this.tileManageUsers.ActiveControl = null;
            this.tileManageUsers.Location = new System.Drawing.Point(192, 30);
            this.tileManageUsers.Name = "tileManageUsers";
            this.tileManageUsers.Size = new System.Drawing.Size(160, 114);
            this.tileManageUsers.TabIndex = 2;
            this.tileManageUsers.Text = "Manage Users";
            this.tileManageUsers.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.tileManageUsers.UseSelectable = true;
            this.tileManageUsers.Click += new System.EventHandler(this.tileManageUsers_Click);
            // 
            // tilePrinterSettings
            // 
            this.tilePrinterSettings.ActiveControl = null;
            this.tilePrinterSettings.Location = new System.Drawing.Point(26, 150);
            this.tilePrinterSettings.Name = "tilePrinterSettings";
            this.tilePrinterSettings.Size = new System.Drawing.Size(160, 114);
            this.tilePrinterSettings.TabIndex = 2;
            this.tilePrinterSettings.Text = "System Configurations";
            this.tilePrinterSettings.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.tilePrinterSettings.UseSelectable = true;
            this.tilePrinterSettings.Click += new System.EventHandler(this.tilePrinterSettings_Click);
            // 
            // tileDeviceRegister
            // 
            this.tileDeviceRegister.ActiveControl = null;
            this.tileDeviceRegister.Location = new System.Drawing.Point(192, 150);
            this.tileDeviceRegister.Name = "tileDeviceRegister";
            this.tileDeviceRegister.Size = new System.Drawing.Size(160, 114);
            this.tileDeviceRegister.TabIndex = 2;
            this.tileDeviceRegister.Text = "Device Register";
            this.tileDeviceRegister.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.tileDeviceRegister.UseSelectable = true;
            this.tileDeviceRegister.Click += new System.EventHandler(this.tileDeviceRegister_Click);
            // 
            // ucConfigurations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tileDeviceRegister);
            this.Controls.Add(this.tilePrinterSettings);
            this.Controls.Add(this.tileManageUsers);
            this.Controls.Add(this.tileBusinessSetup);
            this.Name = "ucConfigurations";
            this.Size = new System.Drawing.Size(996, 462);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTile tileBusinessSetup;
        private MetroFramework.Controls.MetroTile tileManageUsers;
        private MetroFramework.Controls.MetroTile tilePrinterSettings;
        private MetroFramework.Controls.MetroTile tileDeviceRegister;

    }
}
