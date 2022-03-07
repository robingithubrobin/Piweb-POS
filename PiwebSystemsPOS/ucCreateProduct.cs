using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PiwebSystemsPOS.Classes;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace PiwebSystemsPOS
{
    public partial class ucCreateProduct : UserControl
    {
        private static string connString = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private static SqlConnection sqlConn = new SqlConnection(connString);
        private static SqlCommand cmd;
        private static SqlDataAdapter sda;
        private static DataTable dt;

        private static ucCreateProduct _instance;
        private OpenFileDialog openFile = new OpenFileDialog();
        private static string fileName;
        //Checkbox Variables
        private char returnAllowed = 'N',
            active = 'N',
            salesProduct = 'N',
            purchaseProduct = 'N',
            AllowNegativeStock = 'N',
            vatIncluded = 'N';

        public static ucCreateProduct instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucCreateProduct();
                return _instance;
            }
        }

        #region Product properties
        public String _no
        {
            get { return txtNo.Text; }
            set { txtNo.Text = value; }
        }
        public String _GTIN
        {
            get { return txtGTIN.Text; }
            set { txtGTIN.Text = value; }
        }
        public String _productName
        {
            get { return txtProductName.Text; }
            set { txtProductName.Text = value; }
        }
        public String _description
        {
            get { return txtDescription.Text; }
            set { txtDescription.Text = value; }
        }
        public String _costPrice
        {
            get { return txtUnitCost.Text; }
            set { txtUnitCost.Text = value; }
        }
        public String _unitPrice
        {
            get { return txtUnitPrice.Text; }
            set { txtUnitPrice.Text = value; }
        }
        public String _unitPriceExclVAT
        {
            get { return txtUnitPriceExclVAT.Text; }
            set { txtUnitPriceExclVAT.Text = value; }
        }
        public String _createdBy
        {
            get { return txtCreatedBy.Text; }
            set { txtCreatedBy.Text = value; }
        }
        public String _createdDate
        {
            get { return txtCreatedDate.Text; }
            set { txtCreatedDate.Text = value; }
        }
        //
        // ComboBox Values
        //
        public String _categoryCode
        {
            get
            {
                string categoryCode = "";
                if (!string.IsNullOrEmpty(cmbCategory.Text))
                    categoryCode = cmbCategory.SelectedValue.ToString();
                return categoryCode;
            }
            set { cmbCategory.SelectedItem = value; }
        }
        public String _productType
        {
            get
            {
                string productType = "";
                if (!string.IsNullOrEmpty(cmbProductType.Text))
                    productType = cmbProductType.SelectedValue.ToString();
                return productType;
            }
            set { cmbProductType.SelectedItem = value; }
        }
        public String _unitOfMeasure
        {
            get
            {
                string uom = "";
                if (!string.IsNullOrEmpty(cmbUnitOfMeasure.Text))
                    uom = cmbUnitOfMeasure.SelectedValue.ToString();
                return uom;
            }
            set { cmbUnitOfMeasure.SelectedItem = value; }
        }
        public String _parentProduct
        {
            get
            {
                string parentProduct = "";
                if (!string.IsNullOrEmpty(cmbParentProduct.Text))
                    parentProduct = cmbParentProduct.SelectedValue.ToString();
                return parentProduct;
            }
            set { cmbParentProduct.SelectedItem = value; }
        }
        public String _productBrand
        {
            get
            {
                string productBrand = "";
                if (!string.IsNullOrEmpty(cmbProductBrand.Text))
                    productBrand = cmbProductBrand.SelectedValue.ToString();
                return productBrand;
            }
            set { cmbProductBrand.SelectedItem = value; }
        }
        public String _taxGroup
        {
            get
            {
                string taxGroup = "";
                if (!string.IsNullOrEmpty(cmbTaxGroup.Text))
                    taxGroup = cmbTaxGroup.SelectedValue.ToString();
                return taxGroup;
            }
            set { cmbTaxGroup.SelectedItem = value; }
        }
        public String _discountGroup
        {
            get
            {
                string discountGroup = "";
                if (!string.IsNullOrEmpty(cmbDiscGroup.Text))
                    discountGroup = cmbDiscGroup.SelectedValue.ToString();
                return discountGroup;
            }
            set { cmbDiscGroup.SelectedItem = value; }
        }
        public string _fileName
        {
            get
            {
                string fileName = "Test";
                if (openFile.Title != "" && openFile.FileName != "")
                {
                    if (openFile.CheckFileExists)
                    {

                        string path = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                        fileName = Path.GetFileName(openFile.FileName);
                        File.Copy(openFile.FileName, path + "\\Images\\" + fileName);
                    }
                }

                return fileName;
            }
            set { string fileName = value; }
        }
        public String _readImage
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
                if (!string.IsNullOrEmpty(fileName))
                {
                    string path = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                    pictureBox1.Image = Image.FromFile(path + "\\Images\\" + fileName);
                }
                else
                {
                    pictureBox1.Image = global::PiwebSystemsPOS.Properties.Resources.icon;
                }
            }
        }

        //
        // CheckBox Values
        //
        public char _active
        {
            get
            {
                if (chkActive.Checked == true)
                    active = 'Y';

                return active;
            }
            set { active = value; }
        }
        public char _returnAllowed
        {
            get
            {
                if (chkReturnAllowed.Checked == true)
                    returnAllowed = 'Y';
                return returnAllowed;
            }
            set { returnAllowed = value; }
        }

        public char _salesProduct
        {
            get
            {
                if (chkSalesProduct.Checked == true)
                    salesProduct = 'Y';
                return salesProduct;
            }
            set { salesProduct = value; }
        }
        public char _purchaseProduct
        {
            get
            {
                if (chkPurchaseProduct.Checked == true)
                    purchaseProduct = 'Y';
                return purchaseProduct;
            }
        }
        public char _allowNegativeStock
        {
            get
            {
                if (chkAllowNegStock.Checked == true)
                    AllowNegativeStock = 'Y';
                return AllowNegativeStock;
            }
            set { AllowNegativeStock = value; }

        }
        public char _vatIncluded
        {
            get
            {
                if (chkPriceInclVAT.Checked == true)
                    vatIncluded = 'Y';
                return vatIncluded;
            }
        }
        #endregion

        public ucCreateProduct()
        {
            InitializeComponent();
            txtNo.Text = LoadSerials();
        }
        private void FillCategory()
        {
            DataRow dr;
            string _query = "SELECT [CategoryCode],[Description] FROM [dbo].[INV_Product Category]";
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
                cmbCategory.DisplayMember = "Description";
                cmbCategory.ValueMember = "Category Code";
                cmbCategory.DataSource = dt;

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
        private void FillTaxGroupCode()
        {
            DataRow dr;
            string _query = "SELECT [Tax Group Code],[Description] FROM [dbo].[COM_Tax Group] ORDER BY [Tax Group Code] ASC";
            try
            {
                cmd = sqlConn.CreateCommand();
                cmd.CommandText = _query;

                sqlConn.Open();

                dt = new DataTable();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                dr = dt.NewRow();
                dr.ItemArray = new object[] { "" };
                dt.Rows.InsertAt(dr, 0);
                cmbTaxGroup.DisplayMember = "Tax Group Code";
                cmbTaxGroup.ValueMember = "Tax Group Code";
                cmbTaxGroup.DataSource = dt;

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
        private void FillProductBrand()
        {
            DataRow dr;
            string _query = "SELECT [Brand Code],[Brand] FROM [dbo].[INV_ProductBrand] ORDER BY [Brand] ASC";
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
                cmbProductBrand.DisplayMember = "Brand";
                cmbProductBrand.ValueMember = "Brand Code";
                cmbProductBrand.DataSource = dt;

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
        private void FillProductType()
        {
            string _query = "SELECT [Product Type Code],[Product Type] FROM [dbo].[INV_ProductType] ORDER BY [Product Type] ASC";
            try
            {
                cmd = sqlConn.CreateCommand();
                cmd.CommandText = _query;

                sqlConn.Open();

                dt = new DataTable();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                cmbProductType.DisplayMember = "Product Type";
                cmbProductType.ValueMember = "Product Type Code";
                cmbProductType.DataSource = dt;

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
        private void FillParentProduct()
        {
            DataRow dr;
            string _query = "SELECT [No.],[Product Name] FROM [dbo].[INV_Product]";
            try
            {
                cmd = sqlConn.CreateCommand();
                cmd.CommandText = _query;

                sqlConn.Open();

                dt = new DataTable();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                dr = dt.NewRow();
                dr.ItemArray = new object[] { "" };
                dt.Rows.InsertAt(dr, 0);
                cmbParentProduct.DisplayMember = "Product Name";
                cmbParentProduct.ValueMember = "No.";
                cmbParentProduct.DataSource = dt;

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
        private void FillUnitOfMeasure()
        {
            DataRow dr;
            string _query = "SELECT [Unit of Measure Code],[Description] FROM [dbo].[INV_Unit Of Measure]";
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
                cmbUnitOfMeasure.DisplayMember = "Description";
                cmbUnitOfMeasure.ValueMember = "Unit of Measure Code";
                cmbUnitOfMeasure.DataSource = dt;

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
        private string LoadSerials()
        {
            var serialNo = "";

            cmd = new SqlCommand("(SELECT isnull(MAX([No.]),10000) + 1 AS Serial FROM [dbo].[INV_Product])", sqlConn);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            serialNo = dt.Rows[0]["Serial"].ToString();

            return serialNo;
        }
        private void ucCreateProduct_Load(object sender, EventArgs e)
        {
            txtNo.Text = LoadSerials();
            FillCategory();
            FillUnitOfMeasure();
            FillTaxGroupCode();
            FillProductBrand();
            FillProductType();
            FillParentProduct();
            chkActive.Checked = true;
            chkSalesProduct.Checked = true;
        }

        private void lnkImportImage_Click(object sender, EventArgs e)
        {
            openFile.InitialDirectory = "C:\\";
            openFile.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png|All Files(*.*)|*.*";
            openFile.FilterIndex = 1;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFile.FileName);
            }
        }
    }
}
