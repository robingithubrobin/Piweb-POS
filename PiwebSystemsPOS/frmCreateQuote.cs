using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PiwebSystemsPOS.Classes;
using System.Configuration;
using System.Data.SqlClient;

namespace PiwebSystemsPOS
{
    public partial class frmCreateQuote : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        private DataTable dt = new DataTable();

        private static string connString = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private static SqlConnection sqlConn = new SqlConnection(connString);
        private static SqlCommand cmd;
        private static SqlDataAdapter sda;
        public frmCreateQuote()
        {
            InitializeComponent();
        }
        //
        //Sales Quote No
        //
        #region Generate Serial No.
        public string LoadQuoteNo()
        {
            var serialNo = "";
            int _result = 0;
            string rec, _prefix = "QOT";

            cmd = new SqlCommand("SELECT MAX([SalesQuoteNo]) AS 'QuoteNo' FROM [dbo].[SAL_SalesQuotes]", sqlConn);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            rec = dt.Rows[0]["QuoteNo"].ToString();
            if (!string.IsNullOrEmpty(rec))
            {
                _result = Convert.ToInt32(rec.Substring(3, rec.Length - 3)) + 1;
                serialNo = _prefix + _result.ToString();

            }
            else
            {
                _result = 100000 + 1;
                serialNo = _prefix + _result.ToString();
            }

            return serialNo;
        }
        #endregion
        private void btnGetCustomer_Click(object sender, EventArgs e)
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
                txtCustomer.Text = customerDetails.Rows[0]["Name"].ToString();
                txtPhone.Text = customerDetails.Rows[0]["PhoneNo"].ToString();

            #endregion
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            #region Save Quote Header
            //
            //
            //
            string createdBy = UserSession.userName,
                deviceID = System.Environment.MachineName;

            string _quoteNo = txtQuoteNo.Text.Trim(),
                _customerCode = TransactionsHelper.CustomerCode,
                customerName = txtCustomer.Text.Trim(),
                phoneNo = txtPhone.Text.Trim(),
                _startDate = startDate.Value.ToString(),
                _endDate = endDate.Value.ToString(),
                soType = cmbInvoiceType.Text;

            if (!string.IsNullOrEmpty(_customerCode))
                piwebDataOps.CreateSalesQuoteHeader(_quoteNo,"NEW", Convert.ToDateTime(_startDate), Convert.ToDateTime(_endDate), soType, _customerCode, customerName, phoneNo, createdBy, deviceID);
            else
                piwebDataOps.CreateSalesQuoteHeader(_quoteNo,"NEW", Convert.ToDateTime(_startDate), Convert.ToDateTime(_endDate), soType, "", customerName, phoneNo, createdBy, deviceID);
            //MessageBox.Show("Quote Header Created Successfully");
            #endregion
            frmOrder openOrder = new frmOrder();
            TransactionsHelper.formType = "Quote";
            TransactionsHelper.DocumentNo = txtQuoteNo.Text;
            TransactionsHelper.CustomerName = txtCustomer.Text;
            TransactionsHelper.CustomerPhone = txtPhone.Text;
            openOrder.ShowDialog();
            this.Close();
        }

        private void frmCreateQuote_Load(object sender, EventArgs e)
        {
            txtQuoteNo.Text = LoadQuoteNo();
            txtQuoteNo.ReadOnly = true;

            //Calculate Expiry Date (30 Days)
            DateTime sDate = DateTime.Parse(startDate.Text);
            DateTime eDate = DateTime.Parse(startDate.Text).AddDays(31);
            endDate.Text = eDate.ToString();
        }

        private void startDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime sDate = DateTime.Parse(startDate.Text);
            DateTime eDate = DateTime.Parse(startDate.Text).AddDays(31);
            endDate.Text = eDate.ToString();

        }
    }
}
