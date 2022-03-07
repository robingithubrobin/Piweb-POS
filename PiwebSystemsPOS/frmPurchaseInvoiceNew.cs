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
    public partial class frmPurchaseInvoiceNew : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();

        private static string connString = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private static SqlConnection sqlConn = new SqlConnection(connString);
        private static SqlCommand cmd;
        private static SqlDataAdapter sda;
        private static DataTable dt;

        public string LoadSerials()
        {
            var serialNo = "";
            int _result = 0;
            string rec, _prefix = "PUR";

            cmd = new SqlCommand("SELECT MAX([PurchaseInvoiceNo]) AS 'InvoiceNo' FROM [dbo].[PUR_PurchaseInvoices]", sqlConn);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            rec = dt.Rows[0]["InvoiceNo"].ToString();
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
        public frmPurchaseInvoiceNew()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DateTime invoiceDate = InvoiceDate.Value,
                     receivingDate = ReceivingDate.Value;
            string purchaseInvoiceNo = LoadSerials(),
                invoiceType = "",
                supplierCode = "",
                supplierReferenceNo = txtRefNo.Text.Trim(),
                deviceID = System.Environment.MachineName,
                createdBy = "SYSTEM";

            if (!string.IsNullOrEmpty(cmbInvoiceType.Text))
                invoiceType = cmbInvoiceType.SelectedValue.ToString();
            else
                return;
            if (!string.IsNullOrEmpty(cmbSupplier.Text))
                supplierCode = cmbSupplier.SelectedValue.ToString();
            else
                return;

            TransactionsHelper.DocumentNo = purchaseInvoiceNo;
            TransactionsHelper.PurchaseDate = invoiceDate;
            TransactionsHelper.PurchaseType = invoiceType;
            TransactionsHelper.Supplier = supplierCode;
            TransactionsHelper.ReferenceNo = supplierReferenceNo;
            TransactionsHelper.ReceivingDate = receivingDate;

            //MessageBox.Show("Purchase Invoice No: "+purchaseInvoiceNo+"\nPurchase Date: " + TransactionsHelper.PurchaseDate.ToString() + "\n" + "Invoice Type: " + TransactionsHelper.PurchaseType + "\n" + "Supplier Name: " + TransactionsHelper.Supplier + "\nReference No: " + TransactionsHelper.ReferenceNo + "\n Received Date: " + TransactionsHelper.ReceivingDate,"Purchase Order Details",MessageBoxButtons.OK,MessageBoxIcon.Information);

            piwebDataOps.CreatePurchaseInvoice(purchaseInvoiceNo, invoiceDate, receivingDate, "NEW", invoiceType, receivingDate,supplierCode,supplierReferenceNo, deviceID, createdBy);
            frmPurchase openPurchase = new frmPurchase();
            openPurchase.ShowDialog();
            this.Close();
        }
        private void FillSupplier()
        {
            DataRow dr;
            string _query = "SELECT [Supplier Code],[Name] FROM [dbo].[CRM_Suppliers] ORDER BY Name ASC";
            try
            {
                cmd = sqlConn.CreateCommand();
                cmd.CommandText = _query;

                sqlConn.Open();

                dt = new DataTable();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "" };
                dt.Rows.InsertAt(dr, 0);
                cmbSupplier.DisplayMember = "Name";
                cmbSupplier.ValueMember = "Supplier Code";
                cmbSupplier.DataSource = dt;

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }
        }
        private void FillInvoiceOrderType()
        {
            //DataRow dr;
            string _query = "SELECT [POTypeCode], [PurchaseOrderType] FROM [dbo].[PUR_PurchaseOrderTypes]";
            try
            {
                cmd = sqlConn.CreateCommand();
                cmd.CommandText = _query;

                sqlConn.Open();

                dt = new DataTable();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                cmbInvoiceType.DisplayMember = "PurchaseOrderType";
                cmbInvoiceType.ValueMember = "POTypeCode";
                cmbInvoiceType.DataSource = dt;

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }
        }

        private void frmPurchaseInvoiceNew_Load(object sender, EventArgs e)
        {
            FillInvoiceOrderType();
            FillSupplier();
            InvoiceDate.Value = DateTime.Now;
            ReceivingDate.Value = DateTime.Now;
        }
    }
}
