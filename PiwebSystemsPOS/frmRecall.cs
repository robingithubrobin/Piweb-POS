using PiwebSystemsPOS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiwebSystemsPOS
{
    public partial class frmRecall : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        TransactionsHelper transHelper = new TransactionsHelper();

        private static string connString = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private static SqlConnection sqlConn = new SqlConnection(connString);
        private static SqlCommand cmd;
        private static SqlDataAdapter sda;
        private DataTable dt = new DataTable();

        public frmRecall()
        {
            InitializeComponent();

            LoadGridView();
        }

        private void LoadGridView()
        {
            string userName = "SYSTEM", deviceID = System.Environment.MachineName;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

            dt = new DataTable();

            dt.Columns.Add("Invoice Date", typeof(DateTime));
            dt.Columns.Add("Invoice No.", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Sub Total", typeof(decimal));

            foreach (DataRow dr in piwebDataOps.GetSalesInvoices(userName, deviceID).Rows)
            {
                dt.Rows.Add(dr["InvoiceDate"].ToString(), dr["SalesInvoiceNo"].ToString(), dr["StatusCode"].ToString(), Convert.ToDecimal(String.Format("{0:N}", dr["SubTotal"])));
            }

            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Invoice Date"].ReadOnly = true;
            dataGridView1.Columns["Invoice No."].ReadOnly = true;
            dataGridView1.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Sub Total"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
