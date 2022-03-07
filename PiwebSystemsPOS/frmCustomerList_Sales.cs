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
    public partial class frmCustomerList_Sales : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        DataTable dt;
        public frmCustomerList_Sales()
        {
            InitializeComponent();
        }

        private void LoadGridView()
        {

            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

            dt = new DataTable();

            dt.Columns.Add("Customer Code", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Address", typeof(string));//
            dt.Columns.Add("City", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("Alt Phone", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Website", typeof(string));
            dt.Columns.Add("Credit Limit", typeof(string));

            foreach (DataRow dr in piwebDataOps.GetCustomers().Rows)
            {
                decimal creditLimit = Convert.ToDecimal(dr["CreditLimit"].ToString());
                decimal credLimRound = Math.Round(creditLimit, 2);
                string formatCreditLimit = String.Format("{0:N}", credLimRound);
                dt.Rows.Add(dr["CustomerCode"].ToString(), dr["Name"].ToString(), dr["Address"].ToString(), dr["City"].ToString(), dr["PhoneNo"].ToString(), dr["AltPhoneNo"].ToString(), dr["Email"].ToString(), dr["Url"].ToString(), formatCreditLimit);
            }

            dataGridView1.DataSource = dt;
            //dataGridView1.Columns["Model"].ReadOnly = true;
            //dataGridView1.Columns["Serial No"].ReadOnly = true;
            //dataGridView1.Columns["WorkStation"].ReadOnly = true;

            dataGridView1.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Address"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["City"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Phone"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Alt Phone"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Website"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Credit Limit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.AllowUserToAddRows = false;
        }

        private void btnNewCustomer_Click(object sender, EventArgs e)
        {
            frmCustomer_Sales openCustomerSales = new frmCustomer_Sales();
            openCustomerSales.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCustomerList_Sales_Load(object sender, EventArgs e)
        {
            LoadGridView();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
            LoadGridView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TransactionsHelper.CustomerCode = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            //transHelper.CustomerName = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            this.Close();
        }
    }
}
