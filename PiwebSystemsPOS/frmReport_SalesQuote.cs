using Microsoft.Reporting.WinForms;
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
using PiwebSystemsPOS.Classes;
using System.IO;

namespace PiwebSystemsPOS
{
    public partial class frmReport_SalesQuote : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        private static string cnString = ConfigurationManager.ConnectionStrings["sqlConn"].ConnectionString;
        //declare Connection, command and other related objects 
        SqlConnection conReport = new SqlConnection(cnString);
        SqlCommand cmdReport = new SqlCommand();
        SqlDataReader drReport;
        DataSet dsReport = new dsPiwebSystems();

        private string salesQuoteNo;

        public string SalesQuoteNo
        {
            get { return salesQuoteNo; }
            set { salesQuoteNo = value; }
        }
        public frmReport_SalesQuote()
        {
            InitializeComponent();
        }

        private void frmReport_SalesQuote_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            string _salesQuoteNo = salesQuoteNo;
            LoadSalesQuotes(_salesQuoteNo);
            LoadCompanyInfo();
            LoadLogo();
        }
        private void LoadSalesQuotes(string _salesQuoteNo)
        {
            try
            {
                //open connection 
                conReport.Open();

                //prepare connection object to get the data 
                //through reader and populate into dataset 
                cmdReport.CommandType = CommandType.Text;
                cmdReport.Connection = conReport;
                cmdReport.CommandText = @"SELECT * FROM [dbo].[SAL_SalesQuotes] WHERE SalesQuoteNo = '" + _salesQuoteNo + "'";

                //read data from command object 
                drReport = cmdReport.ExecuteReader();

                //load data directly from reader to dataset 
                dsReport.Tables[2].Load(drReport);

                //close reader and connection 
                drReport.Close();
                conReport.Close();

                //provide local report information to viewer 
                reportViewer1.LocalReport.ReportEmbeddedResource = "PiwebSystemsPOS.rptSalesQuote.rdlc";
                reportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SalesQuoteLines);

                //prepare report data source 
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "dsPiwebSystems_dtSalesQuotes";
                rds.Value = dsReport.Tables[2];
                reportViewer1.LocalReport.DataSources.Add(rds);

                //load report viewer 
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        void SalesQuoteLines(object sender, SubreportProcessingEventArgs e)
        {
            string salesQuoteNo = e.Parameters["SalesQuoteNo"].Values[0].ToString();

            DataTable getSalesQuoteLines = piwebDataOps.GetSalesQuoteLines(salesQuoteNo);
            ReportDataSource ds = new ReportDataSource("dsPiwebSystems_dtSalesQuotesLines", getSalesQuoteLines);
            e.DataSources.Add(ds);

        }
        private void LoadCompanyInfo()
        {
            try
            {
                //open connection 
                conReport.Open();

                //prepare connection object to get the data 
                //through reader and populate into dataset 
                cmdReport.CommandType = CommandType.Text;
                cmdReport.Connection = conReport;
                cmdReport.CommandText = @"SELECT * FROM [dbo].[SYS_CompanyInfo]";

                //read data from command object 
                drReport = cmdReport.ExecuteReader();

                //load data directly from reader to dataset 
                dsReport.Tables[1].Load(drReport);

                //close reader and connection 
                drReport.Close();
                conReport.Close();

                //provide local report information to viewer 
                reportViewer1.LocalReport.ReportEmbeddedResource = "PiwebSystemsPOS.rptSalesQuote.rdlc";

                //prepare report data source 
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "dsPiwebSystems_dtCompanyInfo";
                rds.Value = dsReport.Tables[1];
                reportViewer1.LocalReport.DataSources.Add(rds);

                //load report viewer 
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void LoadLogo()
        {
            try
            {
                string saveDirectory = @"file:///C:\SavedImages\";
                string imageName = string.Empty;
                //open connection 
                conReport.Open();

                //prepare connection object to get the data 
                //through reader and populate into dataset 
                cmdReport.CommandType = CommandType.Text;
                cmdReport.Connection = conReport;
                cmdReport.CommandText = @"SELECT * FROM [dbo].[SYS_CompanyInfo]";

                //read data from command object 
                drReport = cmdReport.ExecuteReader();

                //get Image
                while (drReport.Read())
                {
                    imageName = drReport["logoPath"].ToString();
                }

                //load data directly from reader to dataset 
                dsReport.Tables[1].Load(drReport);

                //close reader and connection 
                drReport.Close();
                conReport.Close();

                //provide local report information to viewer 
                reportViewer1.LocalReport.ReportEmbeddedResource = "PiwebSystemsPOS.rptSalesQuote.rdlc";

                //Image Path for logo
                string File = saveDirectory + imageName;

                ReportParameter paramLogo = new ReportParameter();
                paramLogo.Name = "pImage";
                paramLogo.Values.Add(File);
                this.reportViewer1.LocalReport.EnableExternalImages = true;
                reportViewer1.LocalReport.SetParameters(paramLogo);

                //load report viewer 
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
