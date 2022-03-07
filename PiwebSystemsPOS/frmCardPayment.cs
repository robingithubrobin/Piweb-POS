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
    public partial class frmCardPayment : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        TransactionsHelper transHelper = new TransactionsHelper();

        private static string connString = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private static SqlConnection sqlConn = new SqlConnection(connString);
        private static SqlCommand cmd;
        private static SqlDataAdapter sda;
        private DataTable dt = new DataTable();

        int close = 0;
        int paymentId = 0;
        string invoiceNo = "";
        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }
        public string TotalAmount
        {
            get { return txtAmount.Text; }
            set { txtAmount.Text = value; }
        }
        public int isClose {
            get { return close; }
            set { close = value;  }
        }
        public int PaymentId { 
            get { return paymentId; } 
            set { paymentId=value;} 
        }
        public frmCardPayment()
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
            string bankName = txtBank.Text.Trim(), transactionReference = invoiceNo;
                int cardNo = Convert.ToInt32(txtCardNo.Text.Trim());
            DateTime expiry = dtExpiry.Value;
            decimal amount = Convert.ToDecimal(txtAmount.Text.Trim());

            piwebDataOps.CreateCardDetails(transactionReference, bankName, cardNo, expiry, "", amount, "SYSTEM");

            DataTable cardDetails = piwebDataOps.GetCardDetails(cardNo);
            paymentId = Convert.ToInt32(cardDetails.Rows[0]["CardID"].ToString());

            this.Close();
        }

        private void frmCardPayment_Load(object sender, EventArgs e)
        {
            txtBank.Focus();
        }
    }
}
