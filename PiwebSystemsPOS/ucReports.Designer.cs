namespace PiwebSystemsPOS
{
    partial class ucReports
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
            this.TileInventory = new MetroFramework.Controls.MetroTile();
            this.tileDailyReport = new MetroFramework.Controls.MetroTile();
            this.metroTile2 = new MetroFramework.Controls.MetroTile();
            this.metroTile3 = new MetroFramework.Controls.MetroTile();
            this.SuspendLayout();
            // 
            // TileInventory
            // 
            this.TileInventory.ActiveControl = null;
            this.TileInventory.Location = new System.Drawing.Point(26, 30);
            this.TileInventory.Name = "TileInventory";
            this.TileInventory.Size = new System.Drawing.Size(160, 114);
            this.TileInventory.TabIndex = 1;
            this.TileInventory.Text = "Inventory Report";
            this.TileInventory.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.TileInventory.UseSelectable = true;
            this.TileInventory.Click += new System.EventHandler(this.TileInventory_Click);
            // 
            // tileDailyReport
            // 
            this.tileDailyReport.ActiveControl = null;
            this.tileDailyReport.Location = new System.Drawing.Point(26, 150);
            this.tileDailyReport.Name = "tileDailyReport";
            this.tileDailyReport.Size = new System.Drawing.Size(160, 114);
            this.tileDailyReport.TabIndex = 1;
            this.tileDailyReport.Text = "Daily Report";
            this.tileDailyReport.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.tileDailyReport.UseSelectable = true;
            this.tileDailyReport.Click += new System.EventHandler(this.tileDailyReport_Click);
            // 
            // metroTile2
            // 
            this.metroTile2.ActiveControl = null;
            this.metroTile2.Location = new System.Drawing.Point(191, 150);
            this.metroTile2.Name = "metroTile2";
            this.metroTile2.Size = new System.Drawing.Size(160, 114);
            this.metroTile2.TabIndex = 1;
            this.metroTile2.Text = "Report X";
            this.metroTile2.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.metroTile2.UseSelectable = true;
            // 
            // metroTile3
            // 
            this.metroTile3.ActiveControl = null;
            this.metroTile3.Location = new System.Drawing.Point(357, 150);
            this.metroTile3.Name = "metroTile3";
            this.metroTile3.Size = new System.Drawing.Size(160, 114);
            this.metroTile3.TabIndex = 1;
            this.metroTile3.Text = "VAT Report";
            this.metroTile3.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.metroTile3.UseSelectable = true;
            // 
            // ucReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.metroTile3);
            this.Controls.Add(this.metroTile2);
            this.Controls.Add(this.tileDailyReport);
            this.Controls.Add(this.TileInventory);
            this.Name = "ucReports";
            this.Size = new System.Drawing.Size(1024, 462);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTile TileInventory;
        private MetroFramework.Controls.MetroTile tileDailyReport;
        private MetroFramework.Controls.MetroTile metroTile2;
        private MetroFramework.Controls.MetroTile metroTile3;

    }
}
