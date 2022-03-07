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
    public partial class frmReports_InventoryStockCost : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        private static string cnString = ConfigurationManager.ConnectionStrings["sqlConn"].ConnectionString;

        //  declare Connection, command and other related objects 
        SqlConnection conReport = new SqlConnection(cnString);
        SqlCommand cmdReport = new SqlCommand();
        SqlDataReader drReport;
        DataSet dsReport = new dsPiwebSystems();

        public frmReports_InventoryStockCost()
        {
            InitializeComponent();
        }

        private void frmReports_InventoryStockCost_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            LoadStockCostReport();
        }

        private void LoadStockCostReport()
        {
            try
            {
                //open connection 
                conReport.Open();

                //prepare connection object to get the data 
                //through reader and populate into dataset 
                cmdReport.CommandType = CommandType.Text;
                cmdReport.Connection = conReport;
                cmdReport.CommandText = @"SELECT [ProductCode], 
		                                    (SELECT [UnitCost] FROM [dbo].[INV_Product] WHERE No = s.ProductCode) AS ItemCost,
		                                    (SELECT [UnitPrice] FROM [dbo].[INV_Product] WHERE No = s.ProductCode) AS ItemPrice,
		                                    (SUM([QuantityIn]) - SUM([QuantityOut])) AS CurrentStock,
		                                    (SELECT [UnitCost] FROM [dbo].[INV_Product] WHERE No = s.ProductCode) * (SUM([QuantityIn]) - SUM([QuantityOut]))  AS ItemCostValue,
		                                    (SELECT [UnitPrice] FROM [dbo].[INV_Product] WHERE No = s.ProductCode) * (SUM([QuantityIn]) - SUM([QuantityOut])) AS ItemSellingValue
                                        FROM [dbo].[INV_InventoryStock] s GROUP BY ProductCode";

                //read data from command object 
                drReport = cmdReport.ExecuteReader();

                //load data directly from reader to dataset 
                dsReport.Tables[6].Load(drReport);

                //close reader and connection 
                drReport.Close();
                conReport.Close();

                //provide local report information to viewer 
                reportViewer1.LocalReport.ReportEmbeddedResource = "PiwebSystemsPOS.rptInventoryReport.rdlc";

                //prepare report data source 
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "dsPiwebSystems_dtInventoryStockCost";
                rds.Value = dsReport.Tables[6];
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
