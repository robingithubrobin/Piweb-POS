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

namespace PiwebSystemsPOS
{
    public partial class ucDashboard : MetroFramework.Controls.MetroUserControl
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        private static ucDashboard _instance;
        public static ucDashboard instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucDashboard();
                return _instance;
            }
        }
        public ucDashboard()
        {
            InitializeComponent();
            LoadStockChart();
        }

        private void LoadStockChart()
        {
            try
            {
                DataTable getStockProducts = piwebDataOps.GetProducts();

                if (getStockProducts.Rows.Count > 0)
                {
                    string productName = getStockProducts.Rows[0]["ProductName"].ToString();
                    string currentStock = getStockProducts.Rows[0]["CurrentStock"].ToString();

                    chart1.Series["Series1"].XValueMember = "ProductName";
                    chart1.Series["Series1"].YValueMembers = "CurrentStock";
                    chart1.DataSource = getStockProducts;
                    chart1.DataBind();
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Product Load failed \n\n"+ex.Message, "Load Products", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void ucDashboard_Load(object sender, EventArgs e)
        {
            string today = DateTime.Now.ToString("MM/dd/yyyy");
            LoadStockChart();

            //
            // Display Total Sales
            //
            decimal totalSales = 0;
            try
            {
                DataTable sales = piwebDataOps.GetSalesInvoicesLinesTotals(today);
                var value =  sales.Rows[0]["SalesTotals"].ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    totalSales = Convert.ToDecimal(value);
                    lblSalesAmounts.Text = "MWK" + String.Format("{0:N}", totalSales);
                }
                lblSalesAmounts.Text = "MWK"+String.Format("{0:N}",totalSales);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load Today Sales \n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

        }

        private void metroTileSales_Click(object sender, EventArgs e)
        {
            frmProductSales sales = new frmProductSales();
            sales.Show();
        }
    }
}
