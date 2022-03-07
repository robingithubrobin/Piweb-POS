using PiwebSystemsPOS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiwebSystemsPOS
{
    public partial class frmPriceList : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        DataTable dt;
        public frmPriceList()
        {
            InitializeComponent();
        }
        private void LoadGridView()
        {

            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

            dt = new DataTable();

            dt.Columns.Add("Product Code", typeof(string));
            dt.Columns.Add("Product", typeof(string));
            dt.Columns.Add("Price", typeof(decimal));

            foreach (DataRow dr in piwebDataOps.GetProducts().Rows)
            {
                    dt.Rows.Add(dr["No"].ToString(), dr["ProductName"].ToString(), Convert.ToDecimal(String.Format("{0:N}",dr["UnitPrice"])));
            }

            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Product Code"].ReadOnly = true;
            dataGridView1.Columns["Product"].ReadOnly = true;
            dataGridView1.Columns["Product"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Price"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void frmPriceList_Load(object sender, EventArgs e)
        {
            LoadGridView();
        }
    }
}
