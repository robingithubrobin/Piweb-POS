using ExcelDataReader;
using PiwebSystemsPOS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z.Dapper.Plus;
using System.Data.OleDb;

namespace PiwebSystemsPOS
{
    public partial class frmDataImport : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();

        private static string connString = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private static SqlConnection sqlConn = new SqlConnection(connString);
        private static SqlCommand cmd;
        private static SqlDataAdapter sda;
        private static DataTable dt;
        string formName = "Data Import";
        public frmDataImport()
        {
            InitializeComponent();
        }

        DataTableCollection tableCollection;

        public string LoadSerials()
        {
            var serialNo = "";

            cmd = new SqlCommand("(SELECT isnull(MAX([No]),10000) + 1 AS Serial FROM [dbo].[INV_Product])", sqlConn);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            serialNo = dt.Rows[0]["Serial"].ToString();

            return serialNo;
        }
        private void LoadGridView()
        {

            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

            DataTable dataTable = tableCollection[cboSheet.SelectedItem.ToString()];

            dataTable.Columns.Add("No", typeof(string));
            dataTable.Columns.Add("Product Name", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            dataTable.Columns.Add("Category Code", typeof(string));
            dataTable.Columns.Add("Unit Of Measure", typeof(string));
            dataTable.Columns.Add("Unit Price", typeof(string));
            dataTable.Columns.Add("Selling Price", typeof(string));

            foreach (DataRow dr in piwebDataOps.GetProducts().Rows)
            {

                decimal unitCost = Convert.ToDecimal(dr["UnitCost"].ToString());
                decimal UnitPrice = Convert.ToDecimal(dr["UnitPrice"].ToString());
                decimal unitCostRound = Math.Round(unitCost, 2);
                decimal unitPriceRound = Math.Round(UnitPrice, 2);
                string formatUnitCostRound = String.Format("{0:N}", unitCostRound);
                string formatUnitPriceRound = String.Format("0:N", unitPriceRound);
                dataTable.Rows.Add(dr["No"].ToString(), dr["ProductName"].ToString(), dr["Description"].ToString(), dr["CategoryCode"].ToString(), dr["UnitOfMeasureCode"].ToString(), formatUnitCostRound, formatUnitPriceRound);
            }

            //dataGridView1.DataSource = dt;

            if (dataTable != null)
            {
                List<csProduct> customers = new List<csProduct>();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    csProduct product = new csProduct();
                    //product.No = dt.Rows[i]["No"].ToString();
                    product.ProductName = dataTable.Rows[i]["ProductName"].ToString();
                    product.Description = dataTable.Rows[i]["Description"].ToString();
                    product.CategoryCode = dataTable.Rows[i]["CategoryCode"].ToString();
                    product.UnitOfMeasureCode = dataTable.Rows[i]["UnitOfMeasureCode"].ToString();
                    product.UnitCost = Convert.ToDecimal(dataTable.Rows[i]["UnitCost"].ToString());
                    product.UnitPrice = Convert.ToDecimal(dataTable.Rows[i]["UnitPrice"].ToString());
                }

                dataGridView1.DataSource = customers;
            }
            //dataGridView1.Columns["Model"].ReadOnly = true;
            //dataGridView1.Columns["Serial No"].ReadOnly = true;
            //dataGridView1.Columns["WorkStation"].ReadOnly = true;

            //dataGridView1.Columns["No"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Product Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Category Code"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Unit Of Measure"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Unit Price"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Selling Price"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dataGridView1.Columns["Credit Limit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.AllowUserToAddRows = false;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel 97-2003 Workbook|*.xls|Excel Workbook|*.xlsx" })
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        txtFileName.Text = openFileDialog.FileName;
                        using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                        {
                            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                    {
                                        UseHeaderRow = true
                                    }
                                });
                                tableCollection = result.Tables;
                                cboSheet.Items.Clear();
                                foreach (DataTable table in tableCollection)
                                {
                                    cboSheet.Items.Add(table.TableName);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("File Could be open with another Program\n\nSelect file again to continue", formName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void cboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt = tableCollection[cboSheet.SelectedItem.ToString()];
            //dataGridView1.DataSource = dt;

            try
            {
                if (dt != null)
                {
                    List<csProduct> products = new List<csProduct>();
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        csProduct product = new csProduct();
                        product.No = dt.Rows[i]["No"].ToString();
                        product.gtin = dt.Rows[i]["GTIN"].ToString();
                        product.ProductName = dt.Rows[i]["ProductName"].ToString();
                        product.Description = dt.Rows[i]["Description"].ToString();
                        product.CategoryCode = dt.Rows[i]["CategoryCode"].ToString();
                        product.UnitOfMeasureCode = dt.Rows[i]["UnitOfMeasureCode"].ToString();
                        product.ParentProductCode = dt.Rows[i]["ParentProductCode"].ToString();
                        product.BrandID = dt.Rows[i]["BrandID"].ToString();
                        if (!string.IsNullOrEmpty(dt.Rows[i]["UnitCost"].ToString()))
                        {
                            product.UnitCost = Convert.ToDecimal(dt.Rows[i]["UnitCost"].ToString());
                        }
                        else
                        {
                            MessageBox.Show("Column [UnitCost] requires decimal values\n\nCheck Row No. " + i, formName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        
                        if (!string.IsNullOrEmpty(dt.Rows[i]["UnitPrice"].ToString()))
                        {
                            product.UnitPrice = Convert.ToDecimal(dt.Rows[i]["UnitPrice"].ToString());
                        }
                        else
                        {
                            MessageBox.Show("Column [UnitPrice] requires decimal values\n\nCheck Row No. " + i, formName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (!string.IsNullOrEmpty(dt.Rows[i]["UnitPriceExclVAT"].ToString()))
                        {
                            product.UnitPriceExclVAT = Convert.ToDecimal(dt.Rows[i]["UnitPriceExclVAT"].ToString());
                        }
                        else
                        {
                            MessageBox.Show("Column [UnitPriceExclVAT] requires decimal values\n\nCheck Row No. " + i, formName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        product.PriceIncVAT = Convert.ToChar(dt.Rows[i]["PriceIncVAT"].ToString());
                        product.TaxGroupCode = dt.Rows[i]["TaxGroupCode"].ToString();
                        product.DiscountGroupCode = dt.Rows[i]["DiscountGroupCode"].ToString();
                        product.SalesProduct = Convert.ToChar(dt.Rows[i]["SalesProduct"].ToString());
                        product.Active = Convert.ToChar(dt.Rows[i]["Active"].ToString());
                        product.PurchaseProduct = Convert.ToChar(dt.Rows[i]["PurchaseProduct"].ToString());
                        product.ReturnAllowed = Convert.ToChar(dt.Rows[i]["ReturnAllowed"].ToString());
                        product.AllowNegStock = Convert.ToChar(dt.Rows[i]["AllowNegStock"].ToString());
                        product.Photo = string.Empty;
                        product.CreatedBy = UserSession.userName;
                        product.ModifiedBy = UserSession.userName;
                        product.CreatedDate = DateTime.Now;
                        product.ModifiedDate = DateTime.Now;
                        products.Add(product);
                    }
                    dataGridView1.DataSource = products;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Data Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to import Product(s)?", "Import Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                       //check if data comboBox is compatible with file selected

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {

                            string productNo = LoadSerials(); //dataGridView1.Rows[i].Cells["No"].Value.ToString();
                            string productGtin = dataGridView1.Rows[i].Cells["GTIN"].Value.ToString();
                            string productName = dataGridView1.Rows[i].Cells["ProductName"].Value.ToString();
                            string productDescription = dataGridView1.Rows[i].Cells["Description"].Value.ToString();
                            string productCategoryCode = dataGridView1.Rows[i].Cells["CategoryCode"].Value.ToString();
                            string productUnitOfMeasureCode = dataGridView1.Rows[i].Cells["UnitOfMeasureCode"].Value.ToString();
                            string productParentProductCode = dataGridView1.Rows[i].Cells["ParentProductCode"].Value.ToString();
                            string productBrandID = dataGridView1.Rows[i].Cells["BrandID"].Value.ToString();
                            decimal productUnitCost = Convert.ToDecimal(dataGridView1.Rows[i].Cells["UnitCost"].Value.ToString());
                            decimal productUnitPrice = Convert.ToDecimal(dataGridView1.Rows[i].Cells["UnitPrice"].Value.ToString());
                            decimal productUnitPriceExclVAT = Convert.ToDecimal(dataGridView1.Rows[i].Cells["UnitPriceExclVAT"].Value.ToString());
                            char productPriceIncVAT = Convert.ToChar(dataGridView1.Rows[i].Cells["PriceIncVAT"].Value.ToString());
                            string productTaxGroupCode = dataGridView1.Rows[i].Cells["TaxGroupCode"].Value.ToString();
                            string productDiscountGroupCode = dataGridView1.Rows[i].Cells["DiscountGroupCode"].Value.ToString();
                            char productSalesProduct = Convert.ToChar(dataGridView1.Rows[i].Cells["SalesProduct"].Value.ToString());
                            char productActive = Convert.ToChar(dataGridView1.Rows[i].Cells["Active"].Value.ToString());
                            char productPurchaseProduct = Convert.ToChar(dataGridView1.Rows[i].Cells["PurchaseProduct"].Value.ToString());
                            char productReturnAllowed = Convert.ToChar(dataGridView1.Rows[i].Cells["ReturnAllowed"].Value.ToString());
                            char productAllowNegStock = Convert.ToChar(dataGridView1.Rows[i].Cells["AllowNegStock"].Value.ToString());
                            string createdBy = UserSession.userName;

                            piwebDataOps.CreateProduct(productNo, productGtin, productName, productDescription, productCategoryCode, productUnitOfMeasureCode, productParentProductCode,
                                productBrandID, productUnitCost, productUnitPrice, productUnitPriceExclVAT, productPriceIncVAT, productTaxGroupCode, productDiscountGroupCode, productSalesProduct,
                                productActive, productPurchaseProduct, productReturnAllowed, productAllowNegStock, createdBy);
                        }

                    }
                    else
                    {
                        return;
                    }

                    dataGridView1.DataSource = null;
                    cboSheet.Text = "";
                    txtFileName.Clear();
                    txtFileName.Focus();
                }
                else
                {
                    MessageBox.Show("Please Select File to Import", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Import Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDataImport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dsPiwebSystems.dtProducts' table. You can move, or remove it, as needed.
            //this.dtProductsTableAdapter.Fill(this.dsPiwebSystems.dtProducts);

            //LoadGridView();

        }
    }
}
