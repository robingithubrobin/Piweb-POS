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
    public partial class frmChequePayment : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        TransactionsHelper transHelper = new TransactionsHelper();

        private static string connString = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private static SqlConnection sqlConn = new SqlConnection(connString);
        private static SqlCommand cmd;
        private static SqlDataAdapter sda;
        private DataTable dt = new DataTable();

        string username = "SYSTEM";

        int close = 0;

        int paymentId = 0;
        public string InvoiceNo { get; set; }
        public string TotalAmount
        {
            get { return txtAmount.Text; }
            set { txtAmount.Text = value; }
        }

        public int isClose { 
            get { return close; } 
            set { close = value; } 
        }

        public int PaymentId
        {
            get { return paymentId; }
            set { paymentId = value; }
        }
        public frmChequePayment()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.close = 1;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string _bank = txtBank.Text.Trim(),
                _branch = txtBranchName.Text.Trim(),
            _accountNo = txtAccountNo.Text.Trim(),
            _chequeNo = txtChequeNo.Text.Trim();

            DateTime chequeDate = dtChequeDate.Value;
            decimal _amount = Convert.ToDecimal(txtAmount.Text.Trim());

            piwebDataOps.CreateChequeDetails(InvoiceNo, _accountNo, _bank, _branch, _chequeNo, chequeDate, _amount, username);

            DataTable chequeDetails = piwebDataOps.GetChequeDetails(_chequeNo);
            paymentId = Convert.ToInt32(chequeDetails.Rows[0]["ChequeID"].ToString());

            TotalAmount = txtAmount.Text;
            this.Close();
        }
    }
}
