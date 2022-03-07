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
    public partial class frmSalesQuoteList : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        TransactionsHelper transHelper = new TransactionsHelper();
        private DataTable dt = new DataTable();
        public frmSalesQuoteList()
        {
            InitializeComponent();
        }
        private void loadGridView()
        {

            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

            dt = new DataTable();

            dt.Columns.Add("Quote No.", typeof(string));
            dt.Columns.Add("Quote Date", typeof(string));
            dt.Columns.Add("End Date", typeof(string));//
            dt.Columns.Add("Customer", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("Created By", typeof(string));
            dt.Columns.Add("Created Date", typeof(string));

            foreach (DataRow dr in piwebDataOps.GetSalesQuotes("COMPLETED").Rows)
            {
                string quoteNo = dr["SalesQuoteNo"].ToString(),
                    quoteDate = dr["SalesQuoteDate"].ToString(),
                    endDate = dr["SalesQuoteEndDate"].ToString(),
                    customer = dr["CustomerName"].ToString(),
                    phoneNo = dr["CustomerPhoneNo"].ToString(),
                    createdBy = dr["CreatedBy"].ToString(),
                    createdDate = dr["CreatedDate"].ToString();

                dt.Rows.Add(quoteNo, quoteDate, endDate, customer, phoneNo, createdBy, createdDate);
            }

            dataGridView1.DataSource = dt;
            //dataGridView1.Columns["Model"].ReadOnly = true;
            //dataGridView1.Columns["Serial No"].ReadOnly = true;
            //dataGridView1.Columns["WorkStation"].ReadOnly = true;

            dataGridView1.Columns["Quote No."].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Quote Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["End Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Customer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Phone"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Created By"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Created Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.AllowUserToAddRows = false;
        }
        private void loadGridView(string text)
        {

            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

            dt = new DataTable();

            dt.Columns.Add("Quote No.", typeof(string));
            dt.Columns.Add("Quote Date", typeof(string));
            dt.Columns.Add("End Date", typeof(string));//
            dt.Columns.Add("Customer", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("Created By", typeof(string));
            dt.Columns.Add("Created Date", typeof(string));

            foreach (DataRow dr in piwebDataOps.GetSalesQuotes_search(text).Rows)
            {
                string quoteNo = dr["SalesQuoteNo"].ToString(),
                    quoteDate = dr["SalesQuoteDate"].ToString(),
                    endDate = dr["SalesQuoteEndDate"].ToString(),
                    customer = dr["CustomerName"].ToString(),
                    phoneNo = dr["CustomerPhoneNo"].ToString(),
                    createdBy = dr["CreatedBy"].ToString(),
                    createdDate = dr["CreatedDate"].ToString();

                dt.Rows.Add(quoteNo, quoteDate, endDate, customer, phoneNo, createdBy, createdDate);
            }

            dataGridView1.DataSource = dt;
            //dataGridView1.Columns["Model"].ReadOnly = true;
            //dataGridView1.Columns["Serial No"].ReadOnly = true;
            //dataGridView1.Columns["WorkStation"].ReadOnly = true;

            dataGridView1.Columns["Quote No."].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Quote Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["End Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Customer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Phone"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Created By"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Created Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.AllowUserToAddRows = false;
        }
        private void loadGridView(string text, DateTime from, DateTime to)
        {

            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

            dt = new DataTable();

            dt.Columns.Add("Quote No.", typeof(string));
            dt.Columns.Add("Quote Date", typeof(string));
            dt.Columns.Add("End Date", typeof(string));//
            dt.Columns.Add("Customer", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("Created By", typeof(string));
            dt.Columns.Add("Created Date", typeof(string));

            foreach (DataRow dr in piwebDataOps.GetSalesQuotes_search_dateFilter(text, from,to).Rows)
            {
                string quoteNo = dr["SalesQuoteNo"].ToString(),
                    quoteDate = dr["SalesQuoteDate"].ToString(),
                    endDate = dr["SalesQuoteEndDate"].ToString(),
                    customer = dr["CustomerName"].ToString(),
                    phoneNo = dr["CustomerPhoneNo"].ToString(),
                    createdBy = dr["CreatedBy"].ToString(),
                    createdDate = dr["CreatedDate"].ToString();

                dt.Rows.Add(quoteNo, quoteDate, endDate, customer, phoneNo, createdBy, createdDate);
            }

            dataGridView1.DataSource = dt;
            //dataGridView1.Columns["Model"].ReadOnly = true;
            //dataGridView1.Columns["Serial No"].ReadOnly = true;
            //dataGridView1.Columns["WorkStation"].ReadOnly = true;

            dataGridView1.Columns["Quote No."].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Quote Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["End Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Customer"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Phone"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Created By"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Created Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.AllowUserToAddRows = false;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmCreateQuote openCreateQuote = new frmCreateQuote();
            openCreateQuote.ShowDialog();
        }

        private void frmSalesQuoteList_Load(object sender, EventArgs e)
        {
            chkDateFilter.Checked = false;
            panelDateFilter.Visible = false;
            panel1.Height = 63;
            loadGridView();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gr in dataGridView1.SelectedRows)
            {
                string salesQuoteNo = dataGridView1.SelectedRows[0].Cells["Quote No."].Value.ToString();
                frmReport_SalesQuote openSalesQuoteReport = new frmReport_SalesQuote();
                openSalesQuoteReport.SalesQuoteNo = salesQuoteNo;
                openSalesQuoteReport.ShowDialog();
            }
            
            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
            loadGridView();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void chkDateFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDateFilter.Checked == true)
            {
                panelDateFilter.Visible = true;
                panel1.Height = 103;
            }
            else
            {
                panelDateFilter.Visible = false;
                panel1.Height = 69;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            DateTime fromDate = dtpFrom.Value, toDate = dtpTo.Value;

            if (chkDateFilter.Checked == true)
            {
                loadGridView(searchTerm, fromDate, toDate);
            }
            else
            {
                loadGridView(searchTerm);
            }
        }
    }
}
