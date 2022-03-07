using MetroFramework.Controls;
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
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using System.Configuration;
namespace PiwebSystemsPOS
{
    public partial class frmPurchaseInvoices : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        public frmPurchaseInvoices()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmPurchaseInvoiceNew openInvoice = new frmPurchaseInvoiceNew();
            openInvoice.ShowDialog();
        }

        private void frmPurchaseInvoices_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }
        private void LoadGrid()
        {
            DataTable getPurchaseInvoices = piwebDataOps.GetPendingInvoices("", "ON HOLD");
            dataGridView1.DataSource = getPurchaseInvoices;

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gr in dataGridView1.SelectedRows)
            {
                string purchaseInvoiceNo = dataGridView1.SelectedRows[0].Cells["PurchaseInvoiceNo"].Value.ToString();
                frmPurchaseInvoiceView openPurchaseInvoiceView = new frmPurchaseInvoiceView();
                openPurchaseInvoiceView.PurchaseInvoiceNo = purchaseInvoiceNo;
                openPurchaseInvoiceView.ShowDialog();
            }
        }
        //private void PurchaseInvoice_Click(object sender, EventArgs e)
        //{
        //    PanelWorkSpace.Controls.Clear();
        //    if (!PanelWorkSpace.Controls.Contains(reportViewer1))
        //    {
        //        PanelWorkSpace.Controls.Add(reportViewer1);
        //        reportViewer1.BringToFront();
        //        MetroPanel panel = (MetroPanel)sender;
        //        string invoiceNo = panel.Name;
        //        LoadPurchaseInvoiceReport(invoiceNo);
        //        reportViewer1.Refresh();
        //    }

        //}
        //private void LoadPurchaseInvoiceReport(string _no)
        //{
        ////declare connection string, please substitute 
        ////DataSource with your Server name 
        //string cnString = ConfigurationManager.ConnectionStrings["sqlConn"].ConnectionString;
        ////declare Connection, command and other related objects 
        //SqlConnection conReport = new SqlConnection(cnString);
        //SqlCommand cmdReport = new SqlCommand();
        //SqlDataReader drReport;
        //DataSet dsReport = new dsPiwebSystems();
        //try
        //{
        //    //open connection 
        //    conReport.Open();

        //    //prepare connection object to get the data 
        //    //through reader and populate into dataset 
        //    cmdReport.CommandType = CommandType.Text;
        //    cmdReport.Connection = conReport;
        //    cmdReport.CommandText = //@"SELECT PUR_PurchaseInvoices.PurchaseInvoiceNo,FORMAT(PUR_PurchaseInvoices.InvoiceDate,'d','en-gb') AS 'InvoiceDate', FORMAT(PUR_PurchaseInvoices.ReceivingDate,'d','en-gb') AS 'ReceivingDate',PUR_PurchaseInvoices.SupplierCode,PUR_PurchaseInvoiceLines.Description,FORMAT(PUR_PurchaseInvoiceLines.Quantity,'N1') AS 'Quantity',FORMAT(PUR_PurchaseInvoiceLines.UnitPrice,'N') AS 'UnitPrice', FORMAT(PUR_PurchaseInvoiceLines.LineDiscount,'N') AS 'LineDiscount',FORMAT(PUR_PurchaseInvoiceLines.LineTax1,'N') AS 'LineTax1',FORMAT(PUR_PurchaseInvoiceLines.LinePrice,'N') AS 'LinePrice' FROM [dbo].[PUR_PurchaseInvoices] INNER JOIN PUR_PurchaseInvoiceLines ON PUR_PurchaseInvoices.[PurchaseInvoiceNo] = PUR_PurchaseInvoiceLines.PurchaseInvoiceNo WHERE PUR_PurchaseInvoices.PurchaseInvoiceNo = '" + _no + "'";
        //                            @"SELECT PUR_PurchaseInvoices.PurchaseInvoiceNo,PUR_PurchaseInvoices.InvoiceDate,PUR_PurchaseInvoices.ReceivingDate,PUR_PurchaseInvoices.SupplierCode,PUR_PurchaseInvoiceLines.Description,PUR_PurchaseInvoiceLines.Quantity,PUR_PurchaseInvoiceLines.UnitPrice,PUR_PurchaseInvoiceLines.LineDiscount,PUR_PurchaseInvoiceLines.LineTax1,PUR_PurchaseInvoiceLines.LinePrice FROM [dbo].[PUR_PurchaseInvoices] INNER JOIN PUR_PurchaseInvoiceLines ON PUR_PurchaseInvoices.[PurchaseInvoiceNo] = PUR_PurchaseInvoiceLines.PurchaseInvoiceNo WHERE PUR_PurchaseInvoices.PurchaseInvoiceNo = '" + _no + "'";

        //    //read data from command object 
        //    drReport = cmdReport.ExecuteReader();

        //    //load data directly from reader to dataset 
        //    dsReport.Tables[0].Load(drReport);

        //    //close reader and connection 
        //    drReport.Close();
        //    conReport.Close();

        //    //provide local report information to viewer 
        //    reportViewer1.LocalReport.ReportEmbeddedResource = "PiwebSystemsPOS.rptPurchaseOrderInvoice.rdlc";

        //    //prepare report data source 
        //    ReportDataSource rds = new ReportDataSource();
        //    rds.Name = "dsPiwebSystems_dtPurchaseInvoice";
        //    rds.Value = dsReport.Tables[0];
        //    reportViewer1.LocalReport.DataSources.Add(rds);

        //    //load report viewer 
        //    reportViewer1.RefreshReport();
        //}
        //catch (Exception ex)
        //{
        //    //display generic error message back to user 
        //    MessageBox.Show(ex.Message);
        //}
        //finally
        //{
        //    //check if connection is still open then attempt to close it 
        //    if (conReport.State == ConnectionState.Open) { conReport.Close(); }
        //}

        //}

        //}
    }
}
