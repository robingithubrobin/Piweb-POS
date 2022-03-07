using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiwebSystemsPOS
{
    public partial class ucTransactions : MetroFramework.Controls.MetroUserControl
    {
        private static ucTransactions _instance;
        public static ucTransactions instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucTransactions();
                return _instance;
            }
        }
        public ucTransactions()
        {
            InitializeComponent();
        }

        private void TileBill_Click(object sender, EventArgs e)
        {
            frmCashRegisterWithItems openCashRegister = new frmCashRegisterWithItems();
            openCashRegister.ShowDialog();
        }

        private void TileInventory_Click(object sender, EventArgs e)
        {
            frmStockAdjustment openStockAjustment = new frmStockAdjustment();
            openStockAjustment.ShowDialog();
        }

        private void TileOrder_Click(object sender, EventArgs e)
        {
            frmSalesOrders openSalesOrder = new frmSalesOrders();
            openSalesOrder.ShowDialog();
            //frmOrder openOrder = new frmOrder();
            //openOrder.ShowDialog();
            //metroPanel3.Controls.Clear();
            //pictureBoxIcon.Image = global::PiwebSystemsPOS.Properties.Resources.POS_Terminal_50px;
            //pictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;

            //if (!metroPanel3.Controls.Contains(ucTransactions.instance))
            //{
            //    metroPanel3.Controls.Add(ucTransactions.instance);
            //    ucTransactions.instance.Dock = DockStyle.Fill;
            //    ucTransactions.instance.BringToFront();
            //}
        }

        private void TilePurchase_Click(object sender, EventArgs e)
        {
            frmPurchaseInvoices openPurchaseInvoices = new frmPurchaseInvoices();
            openPurchaseInvoices.ShowDialog();
        }

        private void metroTile4_Click(object sender, EventArgs e)
        {
            frmSplashScreen openSplash = new frmSplashScreen();
            openSplash.ShowDialog();
        }
    }
}
