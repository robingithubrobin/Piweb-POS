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
    public partial class frmPayment : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        TransactionsHelper transHelper = new TransactionsHelper();

        private static string connString = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private static SqlConnection sqlConn = new SqlConnection(connString);
        private static SqlCommand cmd;
        private static SqlDataAdapter sda;
        private DataTable dt = new DataTable();

        string _paymentMode = "";
        string change = "0";
        string bankName = "";
        private string sender;

        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }
        public string InvoiceNo { get; set; }
        public string TotalAmount
        {
            get { return txtTotalAmount.Text; }
            set { txtTotalAmount.Text = value; }
        }
        public string TenderedAmount
        {
            get { return txtTendered.Text; }
            set { txtTendered.Text = value; }
        }

        public string Change
        {
            get { return change; }
            set { change = value; }
        }

        public string PaymentMode
        {
            get { return _paymentMode; }
            set { _paymentMode = value; }
        }
        public string BankName
        {
            get { return bankName; }
            set { bankName = value; }
        }
        public int paymentId { get; set; }

        public frmPayment()
        {
            InitializeComponent();


        }
        private void PaymentModeButton()
        {
            DataTable getProducts = piwebDataOps.GetPaymentMode();
            foreach (DataRow dr in getProducts.Rows)
            {
                string _paymentModeCode = dr["PaymentModeCode"].ToString(),
                    _paymentModeTypeCode = dr["PaymentModeTypeCode"].ToString();

                //
                // Payment Mode Dynamic Button
                //
                Button btnPaymentMode = new Button();
                btnPaymentMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                btnPaymentMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
                btnPaymentMode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
                btnPaymentMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btnPaymentMode.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btnPaymentMode.ForeColor = System.Drawing.Color.White;
                btnPaymentMode.Location = new System.Drawing.Point(144, 26);
                btnPaymentMode.Name = _paymentModeCode;
                btnPaymentMode.Size = new System.Drawing.Size(130, 60);
                btnPaymentMode.TabIndex = 10;
                btnPaymentMode.Text = _paymentModeTypeCode;
                btnPaymentMode.UseVisualStyleBackColor = false;
                btnPaymentMode.Click += new System.EventHandler(doPaymentAction);
                PaymentflowLayoutPanel.Controls.Add(btnPaymentMode);
            }

        }

        private void doPaymentAction(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Name == "CASH")
            {
                txtTendered.ReadOnly = false;
                txtTendered.Focus();
                _paymentMode = b.Name;
            }
            else if (b.Name == "CARD")
            {
                frmCardPayment openCardPayment = new frmCardPayment();
                openCardPayment.TotalAmount = txtTotalAmount.Text;
                openCardPayment.InvoiceNo = InvoiceNo;
                openCardPayment.ShowDialog();
                TenderedAmount = txtTotalAmount.Text;
                _paymentMode = b.Name;
                paymentId = openCardPayment.PaymentId;
                if (openCardPayment.isClose == 0)
                    this.Close();
            }
            else if (b.Name == "CHEQUE")
            {
                frmChequePayment openChequePayment = new frmChequePayment();
                openChequePayment.TotalAmount = txtTotalAmount.Text;
                openChequePayment.InvoiceNo = InvoiceNo;
                openChequePayment.ShowDialog();
                TenderedAmount = txtTotalAmount.Text;
                _paymentMode = b.Name;
                paymentId = openChequePayment.PaymentId;
                if (openChequePayment.isClose == 0)
                    this.Close();
            }
        }

        private void frmPayment_Load(object sender, EventArgs e)
        {
            PaymentModeButton();
            txtChange.Text = "0.00";
            txtTendered.Text = "0.00";
            btnNearest.Text = txtTotalAmount.Text;
            txtTendered.ReadOnly = true;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (!txtTendered.Focus())
                txtTendered.Focus();
            if (txtTendered.Text == "0.00" || txtTendered.Text == "0")
                txtTendered.Clear();

            Button b = (Button)sender;
            txtTendered.Text = txtTendered.Text + b.Text;

        }
        private void btn_FastCash(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            decimal num = Convert.ToDecimal(txtTendered.Text.Trim());
            decimal result = Convert.ToDecimal(num) + Convert.ToDecimal(b.Text);
            txtTendered.Text = result.ToString();
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            txtTendered.Clear();
            txtTendered.Text = "0.00";
        }
        private void btn_Backspace(object sender, EventArgs e)
        {
            int length = txtTendered.TextLength - 1;
            string text = txtTendered.Text;
            txtTendered.Clear();
            for (int i = 0; i < length; i++)
            {
                txtTendered.Text = txtTendered.Text + text[i];
            }
        }

        private void txtTendered_Leave(object sender, EventArgs e)
        {

        }

        private void txtTendered_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTendered.Text.Trim()))
            {
                if (sender != "return")
                {
                    _paymentMode = "CASH";

                    decimal _change = Convert.ToDecimal(txtTendered.Text) - Convert.ToDecimal(txtTotalAmount.Text);
                    change = _change.ToString();

                    this.Close();
                }
            }
            else
                return;
        }
    }
}
