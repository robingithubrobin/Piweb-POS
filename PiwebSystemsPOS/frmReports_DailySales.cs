using Microsoft.Reporting.WinForms;
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
    public partial class frmReports_DailySales : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        private static string cnString = ConfigurationManager.ConnectionStrings["sqlConn"].ConnectionString;

        //  declare Connection, command and other related objects 
        SqlConnection conReport = new SqlConnection(cnString);
        SqlCommand cmdReport = new SqlCommand();
        SqlDataReader drReport;
        DataSet dsReport = new dsPiwebSystems();

        public frmReports_DailySales()
        {
            InitializeComponent();
        }

        private void frmReports_DailySales_Load(object sender, EventArgs e)
        {
            //  Load dateTimepickers with Current Date
            DateTime date = DateTime.Now;
            LoadDailySalesReport(Convert.ToDateTime(date));
        }
        private void LoadDailySalesReport(DateTime _date)
        {
            try
            {
                //open connection 
                conReport.Open();

                //prepare connection object to get the data 
                //through reader and populate into dataset 
                cmdReport.CommandType = CommandType.Text;
                cmdReport.Connection = conReport;
                cmdReport.CommandText = @"SELECT [ProductCode], [Description], SUM([Quantity]) AS totalQty, [UnitPrice], SUM([LineTax1]) AS totalTax, SUM([LineDiscount]) AS totalDiscount  FROM [dbo].[SAL_SalesInvoiceLines] WHERE datediff(day, CreatedDate , '" + _date + "') = 0 GROUP BY [ProductCode],[Description],[UnitPrice]";

                //read data from command object 
                drReport = cmdReport.ExecuteReader();

                //load data directly from reader to dataset 
                dsReport.Tables[5].Load(drReport);

                //close reader and connection 
                drReport.Close();
                conReport.Close();

                //provide local report information to viewer 
                reportViewer1.LocalReport.ReportEmbeddedResource = "PiwebSystemsPOS.rptDailySalesReport.rdlc";

                //prepare report data source 
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "dsPiwebSystems_dtDailySales";
                rds.Value = dsReport.Tables[5];
                reportViewer1.LocalReport.DataSources.Add(rds);

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
