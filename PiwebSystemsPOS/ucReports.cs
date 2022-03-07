using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PiwebSystemsPOS.Classes;
using System.Diagnostics;
using System.IO;

namespace PiwebSystemsPOS
{
    public partial class ucReports : MetroFramework.Controls.MetroUserControl
    {
        private static ucReports _instance;
        public static ucReports instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucReports();
                return _instance;
            }
        }
        public ucReports()
        {
            InitializeComponent();
        }

        private void tileDailyReport_Click(object sender, EventArgs e)
        {
            frmReports_DailySales dailySales = new frmReports_DailySales();
            dailySales.Show();
        }

        private void TileInventory_Click(object sender, EventArgs e)
        {
            frmReports_InventoryStockCost inventoryStockCost = new frmReports_InventoryStockCost();
            inventoryStockCost.Show();
        }
    }
}
