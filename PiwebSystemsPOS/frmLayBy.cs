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
    public partial class frmLayBy : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();

        private static string connString = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private static SqlConnection sqlConn = new SqlConnection(connString);
        private static SqlCommand cmd;
        private static SqlDataAdapter sda;
        private static DataTable dt;

        private decimal dueAmount;
        private string itemName;

        private string itemCode;

        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }
        private decimal itemPrice;

        public decimal ItemPrice
        {
            get { return itemPrice; }
            set { itemPrice = value; }
        }
        private decimal itemTax;

        public decimal ItemTax
        {
            get { return itemTax; }
            set { itemTax = value; }
        }
        private decimal itemDiscount;

        public decimal ItemDiscount
        {
            get { return itemDiscount; }
            set { itemDiscount = value; }
        }
        public decimal DueAmount
        {
            get { return dueAmount; }
            set { dueAmount = value; }
        }

        public string LoadSerials()
        {
            var serialNo = "";

            cmd = new SqlCommand("(SELECT isnull(MAX([CustomerCode]),10000) + 1 AS 'Serial' FROM [dbo].[CRM_Customers])", sqlConn);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            serialNo = dt.Rows[0]["Serial"].ToString();

            return serialNo;
        }
        public frmLayBy()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLayBy_Load(object sender, EventArgs e)
        {
            txtCustomerNo.Text = LoadSerials();
            txtCustomerNo.ReadOnly = true;
            txtCustomerNo.Enabled = false;
            txtCustomerName.Focus();
            lblDueAmount.Text = String.Format("{0:N}", dueAmount);
            txtBalance.Text = String.Format("{0:N}", dueAmount);
            txtProductCode.Text = itemCode;
            txtProductCode.Enabled = false;
            txtProductName.Text = itemName;
            txtProductName.Enabled = false;
            txtItemPrice.Text = String.Format("{0:N}", itemPrice);
            txtItemPrice.Enabled = false;
            txtTax.Text = String.Format("{0:N}", itemTax);
            txtTax.Enabled = false;
            txtDiscount.Text = String.Format("{0:N}", itemDiscount);
            txtDiscount.Enabled = false;

        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            frmCustomerList_Sales openCustomerList = new frmCustomerList_Sales();
            openCustomerList.ShowDialog();

            #region Get Customer Details
            string customerCode = TransactionsHelper.CustomerCode;
            if (customerCode == null)
            {
                return;
            }
            DataTable customerDetails = piwebDataOps.GetCustomers(customerCode);
            txtCustomerNo.Text = customerDetails.Rows[0]["CustomerCode"].ToString();
            txtCustomerName.Text = customerDetails.Rows[0]["Name"].ToString();
            txtPhoneNo.Text = customerDetails.Rows[0]["PhoneNo"].ToString();
            txtAltPhone.Text = customerDetails.Rows[0]["AltPhoneNo"].ToString();
            txtEmail.Text = customerDetails.Rows[0]["Email"].ToString();





            #endregion
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerName.Text))
            {
                MessageBox.Show("Customer Name is required", "Layaway", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtPhoneNo.Text))
            {
                MessageBox.Show("Phone No. is required", "Layaway", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
