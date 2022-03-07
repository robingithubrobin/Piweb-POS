using MetroFramework.Controls;
using PiwebSystemsPOS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiwebSystemsPOS
{
    //pp7xDevice.dll
    public delegate void TOnReceiveData(IntPtr ReceiveData, int ReceiveLen); //
    public delegate void TProcess(int PackNo); //
    public partial class frmCashRegisterWithItems : MetroFramework.Forms.MetroForm
    {

        #region pp7xDevice

        [DllImport("pp7device.dll")]
        public extern static void __IniOnShowReceiveData(TOnReceiveData value);
        public struct TRecvInfo
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4096)]
            public byte[] RecvData;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] StateData;
        }

        private TOnReceiveData fOnReceiveData = null;
        private TProcess fOnProcess = null;
        private string RefStr = null;//
        private string FPErrorCode = "";
        private ErrorMsg errmsg = new ErrorMsg();

        private void OnShowReceiveData(IntPtr ReceiveData, int ReceiveLen)//ErrorMessage from dll 
        {
            try
            {
                int j = 0;
                int i = 0;
                Encoding cnEncoding = Encoding.GetEncoding(28599);
                byte[] bt = new Byte[ReceiveLen];
                string s = "";
                RefStr = "";
                FPErrorCode = "";
                TRecvInfo RecvInfo = (TRecvInfo)Marshal.PtrToStructure(ReceiveData, typeof(TRecvInfo));

                for (i = 0; i < ReceiveLen; i++)
                {
                    bt[i] = RecvInfo.RecvData[i];
                }
                RefStr = cnEncoding.GetString(bt, 0, ReceiveLen).ToString();

                if ((ReceiveLen > 4) || (ReceiveLen == 4))
                {
                    if (RefStr[0] == 'E')
                    {
                        s = RefStr.Substring(0, 4);
                        FPErrorCode = errmsg.GetError(s);

                        if (FPErrorCode.Length > 0)
                        {
                            MessageBox.Show(FPErrorCode);
                        }

                    }
                }

            }
            catch
            {
            }
        }
        //private void ejstatus(int PackNo)
        //{
        //    try
        //    {
        //        statusStrip1.Items[1].Text = "Pack No:" + PackNo.ToString();
        //    }
        //    catch
        //    {
        //    }
        //}

        #endregion

        PiwebSystems piwebDataOps = new PiwebSystems();
        TransactionsHelper TransactionsHelpertransHelper = new TransactionsHelper();
        PrintDocument pdoc = null;

        //Initiate DiscountTaxSales
        csDiscountTaxSales discountTaxSale = new csDiscountTaxSales();

        //UserSession sessionManager = new UserSession();

        private static string connString = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private static SqlConnection sqlConn = new SqlConnection(connString);
        private static SqlCommand cmd;
        private static SqlDataAdapter sda;
        private DataTable dt = new DataTable();

        private string InvoiceNo, //Holds Invoice Number
            username = UserSession.userName, //Holds username
            workStation = string.Empty; //Holds Work Station

        private bool hasDiscount = false; //Hold True if Discount button is clicked and False if item is clicked

        List<ItemDiscounts> ItemDiscounts = new List<ItemDiscounts>();

        //  Paymode
        string paymentMode = "";

        // Tendered
        string cashTendered = "0.0";

        // Change
        string change = "0.0";

        int noSale = 0;

        public frmCashRegisterWithItems()
        {
            InitializeComponent();

            //Initialize pp7xDevice Methods
            fOnReceiveData = new TOnReceiveData(OnShowReceiveData);
            //fOnProcess = new TProcess(ejstatus);
            __IniOnShowReceiveData(fOnReceiveData);

            InvoiceNo = LoadSerials();
            //PP7X_Status();

            //Set WorkStation Name
            //lblUserName.Text = UserSession.fullUser;
        }

        #region Generate Serial NOs
        public string LoadSerials()
        {
            var serialNo = "";
            int _result = 0;
            string rec, _prefix = "INV";

            cmd = new SqlCommand("SELECT MAX([SalesInvoiceNo]) AS 'InvoiceNo' FROM [dbo].[SAL_SalesInvoices]", sqlConn);
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
        #endregion

        #region Create Invoice
        private void CreateInvoice()
        {
            DateTime invoiceDate = DateTime.Now;
            String invoiceStatus = "NEW",
                deviceID = System.Environment.MachineName,
                createdBy = UserSession.userName;
            decimal subTotal = Convert.ToDecimal(lblSubTotal.Text);

            piwebDataOps.CreateSalesInvoice(InvoiceNo, invoiceDate, invoiceStatus, subTotal, deviceID, createdBy);

        }
        #endregion

        #region Printer Status
        private void PP7X_Status()
        {
            int nResult = pp7x.__PrinterStatus();

            switch (nResult)
            {

                case -1:
                    MessageBox.Show("Timeout"); break;
                //End Timeout case

                case -2:
                    //MessageBox.Show("Please Check Printer Connection", "Printer Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Try Create Printer Connection
                    try
                    {
                        string _query = @"SELECT [DeviceID],[WorkStation] ,[ComputerName], (SELECT [CommName] FROM [dbo].[SYS_PrinterSettings] WHERE DeviceID = d.DeviceID) AS 'CommName',(SELECT [timeOut] FROM [dbo].[SYS_PrinterSettings] WHERE DeviceID = d.DeviceID) AS 'timeOut',(SELECT [RetryCount] FROM [dbo].[SYS_PrinterSettings] WHERE DeviceID = d.DeviceID) AS 'RetryCount',(SELECT [BaundRate] FROM [dbo].[SYS_PrinterSettings] WHERE DeviceID = d.DeviceID) AS 'BaundRate' FROM [dbo].[SYS_Device] d WHERE  [ComputerName] = '" + Environment.MachineName + "'";
                        cmd = new SqlCommand(_query, sqlConn);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        //Get WorkStation
                        //lblComputerName.Text = dt.Rows[0]["WorkStation"].ToString();
                        //Printer Settings
                        string CommName = dt.Rows[0]["CommName"].ToString(); // Get Value from DB
                        int timeOut = Convert.ToInt32(dt.Rows[0]["timeOut"].ToString()); // Get Value from DB
                        int RetryCount = Convert.ToInt32(dt.Rows[0]["RetryCount"].ToString()); // Get Value from DB
                        int BaudRate = Convert.ToInt32(dt.Rows[0]["BaundRate"].ToString()); // Get Value from DB

                        //Store Values in static values
                        TransactionsHelper.commName = CommName;
                        TransactionsHelper.timeOut = timeOut;
                        TransactionsHelper.retryCount = RetryCount;
                        TransactionsHelper.baudRate = BaudRate;

                        pp7x.__Config(timeOut, RetryCount, BaudRate, CommName);
                        pp7x.__Open();
                        if (pp7x.__Active())
                        {
                            nResult = pp7x.__GetSoftwareStatus();
                            if ((nResult == -1) || (nResult == -2))
                            {
                                //MessageBox.Show("Connection to Printer failed", "Printer Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //return;
                                //lblStatus.Text = "Connection Failed";
                            }
                            else
                            {
                                //MessageBox.Show("Connect ok", "Printer Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //lblStatus.Text = "Connected";
                            }
                        }
                        else
                        {
                            //MessageBox.Show("Printer is not Active\n Check USB cable or Printer is Off", "Printer Connection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //return;
                            //lblStatus.Text = "Printer is not Active. Check USB cable or Printer is Off";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Printer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    break; //End Fail case

                case 1: MessageBox.Show("Printer Check Complete"); break;
        #endregion
            }
        }
        private void ProductButton()
        {
            DataTable getProducts = piwebDataOps.GetProducts();
            //  To load custom No. of items from Settings
            int nGridItems = 60;

            //for (int i = 0; i < nGridItems; i++)
            //{
            //    DataRow dr = getProducts.Rows[i];

            //}
            DataTable prod = getProducts.Rows.Cast<DataRow>().Take(nGridItems).CopyToDataTable();
            foreach (DataRow dr in prod.Rows)
            {
                string _photo = dr["Photo"].ToString(),
                    _no = dr["No"].ToString(),
                    _product = dr["ProductName"].ToString(),
                    _stock = dr["CurrentStock"].ToString();
                decimal _price = Convert.ToDecimal(dr["UnitPrice"].ToString());

                char _active = Convert.ToChar(dr["Active"].ToString());

                //
                // Load Products if Active
                //
                if (_active == 'Y')
                {
                    //
                    // Product Image
                    //
                    PictureBox productImage = new PictureBox();
                    productImage.BackColor = System.Drawing.Color.Transparent;
                    productImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;


                    //check if product has image
                    if (string.IsNullOrEmpty(_photo) || _photo == "0")
                    {
                        productImage.Image = global::PiwebSystemsPOS.Properties.Resources.icon;
                    }
                    else
                    {
                        //productImage.Image = global::PiwebSystemsPOS.Properties.Resources.icon;
                        string path = Application.StartupPath; //.Substring(0, Application.StartupPath.Length - 10);
                        productImage.Image = Image.FromFile(path + "\\Images\\" + _photo);
                    }
                    productImage.Location = new System.Drawing.Point(16, 3);
                    productImage.Name = _no;
                    productImage.Size = new System.Drawing.Size(87, 84);
                    productImage.TabStop = false;
                    productImage.Click += new System.EventHandler(Product_Click);

                    //
                    // Product Name Label
                    //
                    Label productName = new Label();
                    //productName.FontWeight = MetroFramework.MetroLabelWeight.Bold;
                    //productName.UseCustomBackColor = true;
                    productName.BackColor = Color.Transparent;
                    productName.AutoSize = true;
                    productName.Location = new System.Drawing.Point(3, 90);
                    productName.Name = "btnProductName";
                    productName.Size = new System.Drawing.Size(82, 13);
                    productName.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    productName.TabIndex = 2;
                    productName.Text = _product;
                    productName.AutoEllipsis = true;
                    productName.SendToBack();

                    //
                    // Unit Price Label
                    //
                    Label unitPrice = new Label();
                    //unitPrice.FontWeight = MetroFramework.MetroLabelWeight.Regular;
                    //unitPrice.UseCustomBackColor = true;
                    unitPrice.BackColor = Color.Transparent;
                    unitPrice.TextAlign = ContentAlignment.MiddleRight;
                    unitPrice.AutoSize = true;
                    unitPrice.Location = new System.Drawing.Point(3, 113);
                    unitPrice.Name = "btnUnitPrice";
                    unitPrice.Size = new System.Drawing.Size(93, 13);
                    unitPrice.TabIndex = 2;
                    unitPrice.Text = "MWK" + String.Format("{0:N}", _price);
                    unitPrice.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                    unitPrice.SendToBack();

                    //
                    // Product Panel
                    //
                    MetroPanel productsPanel = new MetroPanel();
                    productsPanel.UseCustomBackColor = true;
                    productsPanel.BackColor = System.Drawing.Color.White;
                    productsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    productsPanel.Controls.Add(productName);
                    productsPanel.Controls.Add(unitPrice);
                    productsPanel.Controls.Add(productImage);
                    productsPanel.HorizontalScrollbarBarColor = true;
                    productsPanel.HorizontalScrollbarHighlightOnWheel = false;
                    productsPanel.HorizontalScrollbarSize = 10;
                    productsPanel.Location = new System.Drawing.Point(2, 2);
                    productsPanel.Name = "Product"; //
                    productsPanel.Size = new System.Drawing.Size(128, 130);
                    productsPanel.TabIndex = 2;
                    productsPanel.VerticalScrollbarBarColor = true;
                    productsPanel.VerticalScrollbarHighlightOnWheel = false;
                    productsPanel.VerticalScrollbarSize = 10;
                    btnDiscount.Controls.Add(productsPanel);

                }
            }

        }
        private void ProductButton(string item)
        {
            DataTable getProducts = piwebDataOps.GetProductsByItemName(item);
            foreach (DataRow dr in getProducts.Rows)
            {
                string _photo = dr["Photo"].ToString(),
                    _no = dr["No"].ToString(),
                    _product = dr["ProductName"].ToString(),
                    _stock = dr["CurrentStock"].ToString();
                decimal _price = Convert.ToDecimal(dr["UnitPrice"].ToString());

                char _active = Convert.ToChar(dr["Active"].ToString());

                //
                // Load Products if Active
                //
                if (_active == 'Y')
                {
                    //
                    // Product Image
                    //
                    PictureBox productImage = new PictureBox();
                    productImage.BackColor = System.Drawing.Color.Transparent;
                    productImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;


                    //check if product has image
                    if (string.IsNullOrEmpty(_photo) || _photo == "0")
                    {
                        productImage.Image = global::PiwebSystemsPOS.Properties.Resources.icon;
                    }
                    else
                    {
                        //productImage.Image = global::PiwebSystemsPOS.Properties.Resources.icon;
                        string path = Application.StartupPath; //.Substring(0, Application.StartupPath.Length - 10);
                        productImage.Image = Image.FromFile(path + "\\Images\\" + _photo);
                    }
                    productImage.Location = new System.Drawing.Point(16, 3);
                    productImage.Name = _no;
                    productImage.Size = new System.Drawing.Size(87, 84);
                    productImage.TabStop = false;
                    productImage.Click += new System.EventHandler(Product_Click);

                    //
                    // Product Name Label
                    //
                    Label productName = new Label();
                    //productName.FontWeight = MetroFramework.MetroLabelWeight.Bold;
                    //productName.UseCustomBackColor = true;
                    productName.BackColor = Color.Transparent;
                    productName.AutoSize = true;
                    productName.Location = new System.Drawing.Point(3, 90);
                    productName.Name = "btnProductName";
                    productName.Size = new System.Drawing.Size(82, 13);
                    productName.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    productName.TabIndex = 2;
                    productName.Text = _product;
                    productName.AutoEllipsis = true;
                    productName.SendToBack();

                    //
                    // Unit Price Label
                    //
                    Label unitPrice = new Label();
                    //unitPrice.FontWeight = MetroFramework.MetroLabelWeight.Regular;
                    //unitPrice.UseCustomBackColor = true;
                    unitPrice.BackColor = Color.Transparent;
                    unitPrice.TextAlign = ContentAlignment.MiddleRight;
                    unitPrice.AutoSize = true;
                    unitPrice.Location = new System.Drawing.Point(3, 113);
                    unitPrice.Name = "btnUnitPrice";
                    unitPrice.Size = new System.Drawing.Size(93, 13);
                    unitPrice.TabIndex = 2;
                    unitPrice.Text = "MWK" + String.Format("{0:N}", _price);
                    unitPrice.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                    unitPrice.SendToBack();

                    //
                    // Product Panel
                    //
                    MetroPanel productsPanel = new MetroPanel();
                    productsPanel.UseCustomBackColor = true;
                    productsPanel.BackColor = System.Drawing.Color.White;
                    productsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    productsPanel.Controls.Add(productName);
                    productsPanel.Controls.Add(unitPrice);
                    productsPanel.Controls.Add(productImage);
                    productsPanel.HorizontalScrollbarBarColor = true;
                    productsPanel.HorizontalScrollbarHighlightOnWheel = false;
                    productsPanel.HorizontalScrollbarSize = 10;
                    productsPanel.Location = new System.Drawing.Point(2, 2);
                    productsPanel.Name = "Product"; //
                    productsPanel.Size = new System.Drawing.Size(128, 130);
                    productsPanel.TabIndex = 2;
                    productsPanel.VerticalScrollbarBarColor = true;
                    productsPanel.VerticalScrollbarHighlightOnWheel = false;
                    productsPanel.VerticalScrollbarSize = 10;
                    btnDiscount.Controls.Add(productsPanel);

                }
            }

        }
        private void CategoryButton()
        {
            DataTable getCategories = piwebDataOps.GetCategories();
            foreach (DataRow dr in getCategories.Rows)
            {
                string _photo = dr["Photo"].ToString(),
                    _categoryCode = dr["CategoryCode"].ToString(),
                    _categoryName = dr["Description"].ToString();

                //
                // Product Image
                //
                PictureBox CategoryImage = new PictureBox();
                CategoryImage.BackColor = System.Drawing.Color.Transparent;
                CategoryImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;


                //check if product has image
                if (string.IsNullOrEmpty(_photo))
                {
                    CategoryImage.Image = global::PiwebSystemsPOS.Properties.Resources.icon;
                }
                else
                {
                    //productImage.Image = global::PiwebSystemsPOS.Properties.Resources.icon;
                    string path = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                    CategoryImage.Image = Image.FromFile(path + "\\Images\\" + _photo);
                }
                CategoryImage.Location = new System.Drawing.Point(26, 3);
                CategoryImage.Name = _categoryCode;
                CategoryImage.Size = new System.Drawing.Size(87, 84);
                CategoryImage.Click += new System.EventHandler(Category_Click);

                //
                // Product Name Label
                //
                //MetroLabel categoryName = new MetroLabel();
                //categoryName.FontWeight = MetroFramework.MetroLabelWeight.Bold;
                //categoryName.UseCustomBackColor = true;
                //categoryName.BackColor = Color.Transparent;
                //categoryName.AutoSize = true;
                //categoryName.Location = new System.Drawing.Point(3, 90);
                //categoryName.Name = "btnProductName";
                //categoryName.Size = new System.Drawing.Size(82, 13);
                //categoryName.TabIndex = 2;
                //categoryName.Text = _categoryName;
                //categoryName.AutoEllipsis = true;
                //categoryName.SendToBack();

                //
                // Category Panel
                //
                //MetroPanel categoryPanel = new MetroPanel();
                //categoryPanel.UseCustomBackColor = true;
                //categoryPanel.BackColor = System.Drawing.Color.White;
                //categoryPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                //categoryPanel.Controls.Add(categoryName);
                //categoryPanel.Controls.Add(CategoryImage);
                //categoryPanel.HorizontalScrollbarBarColor = true;
                //categoryPanel.HorizontalScrollbarHighlightOnWheel = false;
                //categoryPanel.HorizontalScrollbarSize = 10;
                //categoryPanel.Location = new System.Drawing.Point(3, 3);
                //categoryPanel.Name = _categoryCode; //
                //categoryPanel.Size = new System.Drawing.Size(120, 100);
                //categoryPanel.TabIndex = 2;
                //categoryPanel.VerticalScrollbarBarColor = true;
                //categoryPanel.VerticalScrollbarHighlightOnWheel = false;
                //categoryPanel.VerticalScrollbarSize = 10;
                //PanelCategoryView.Controls.Add(categoryPanel);

                // 
                // btnCategory
                // 
                Button btnCategory = new Button();
                btnCategory.BackColor = System.Drawing.Color.Transparent;
                btnCategory.FlatAppearance.BorderSize = 0;
                btnCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btnCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btnCategory.Image = global::PiwebSystemsPOS.Properties.Resources.btnTransparent;
                btnCategory.Location = new System.Drawing.Point(2, 2);
                btnCategory.Margin = new System.Windows.Forms.Padding(2);
                btnCategory.Name = _categoryCode;
                btnCategory.Size = new System.Drawing.Size(129, 61);
                btnCategory.TabIndex = 0;
                btnCategory.Text = _categoryName;
                btnCategory.UseVisualStyleBackColor = false;
                btnCategory.Click += new System.EventHandler(Category_Click);
                PanelCategoryView.Controls.Add(btnCategory);
            }
        }
        private void Category_Click(object sender, EventArgs e)
        {
            //Clear PanelProductsView Controls
            btnDiscount.Controls.Clear();

            //Initialize object Sender Instance
            Button categoryButton = (Button)sender;
            string categoryName = categoryButton.Name;

            DataTable getProducts = piwebDataOps.GetProducts(categoryName);
            foreach (DataRow dr in getProducts.Rows)
            {
                string _photo = dr["Photo"].ToString(),
                    _no = dr["No"].ToString(),
                    _product = dr["ProductName"].ToString(),
                    _stock = dr["CurrentStock"].ToString();
                decimal _price = Convert.ToDecimal(dr["UnitPrice"].ToString());
                char _active = Convert.ToChar(dr["Active"].ToString());

                if (_active == 'Y')
                {

                    //
                    // Product Image
                    //
                    PictureBox productImage = new PictureBox();
                    productImage.BackColor = System.Drawing.Color.Transparent;
                    productImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;


                    //check if product has image
                    if (string.IsNullOrEmpty(_photo))
                    {
                        productImage.Image = global::PiwebSystemsPOS.Properties.Resources.icon;
                    }
                    else
                    {
                        //productImage.Image = global::PiwebSystemsPOS.Properties.Resources.icon;
                        string path = Application.StartupPath; //.Substring(0, Application.StartupPath.Length - 10);
                        productImage.Image = Image.FromFile(path + "\\Images\\" + _photo);
                    }
                    productImage.Location = new System.Drawing.Point(16, 3);
                    productImage.Name = _no;
                    productImage.Size = new System.Drawing.Size(87, 84);
                    productImage.TabStop = false;
                    productImage.Click += new System.EventHandler(Product_Click);

                    //
                    // Product Name Label
                    //
                    Label productName = new Label();
                    //productName.FontWeight = MetroFramework.MetroLabelWeight.Bold;
                    //productName.UseCustomBackColor = true;
                    productName.BackColor = Color.Transparent;
                    productName.AutoSize = true;
                    productName.Location = new System.Drawing.Point(3, 90);
                    productName.Name = "btnProductName";
                    productName.Size = new System.Drawing.Size(82, 13);
                    productName.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    productName.TabIndex = 2;
                    productName.Text = _product;
                    productName.AutoEllipsis = true;
                    productName.SendToBack();

                    //
                    // Unit Price Label
                    //
                    Label unitPrice = new Label();
                    //unitPrice.FontWeight = MetroFramework.MetroLabelWeight.Regular;
                    //unitPrice.UseCustomBackColor = true;
                    unitPrice.BackColor = Color.Transparent;
                    unitPrice.TextAlign = ContentAlignment.MiddleRight;
                    unitPrice.AutoSize = true;
                    unitPrice.Location = new System.Drawing.Point(3, 113);
                    unitPrice.Name = "btnUnitPrice";
                    unitPrice.Size = new System.Drawing.Size(93, 13);
                    unitPrice.TabIndex = 2;
                    unitPrice.Text = "MWK" + String.Format("{0:N}", _price);
                    unitPrice.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                    unitPrice.SendToBack();

                    //
                    // Product Panel
                    //
                    MetroPanel productsPanel = new MetroPanel();
                    productsPanel.UseCustomBackColor = true;
                    productsPanel.BackColor = System.Drawing.Color.White;
                    productsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    productsPanel.Controls.Add(productName);
                    productsPanel.Controls.Add(unitPrice);
                    productsPanel.Controls.Add(productImage);
                    productsPanel.HorizontalScrollbarBarColor = true;
                    productsPanel.HorizontalScrollbarHighlightOnWheel = false;
                    productsPanel.HorizontalScrollbarSize = 10;
                    productsPanel.Location = new System.Drawing.Point(3, 3);
                    productsPanel.Name = "Product"; //
                    productsPanel.Size = new System.Drawing.Size(120, 130);
                    productsPanel.TabIndex = 2;
                    productsPanel.VerticalScrollbarBarColor = true;
                    productsPanel.VerticalScrollbarHighlightOnWheel = false;
                    productsPanel.VerticalScrollbarSize = 10;
                    btnDiscount.Controls.Add(productsPanel);
                }
            }
        }
        private void Product_Click(object sender, EventArgs e)
        {
            PictureBox productImage = (PictureBox)sender;
            string productNo = productImage.Name;
            hasDiscount = false; //Set False (Item Clicked)
            AddToSalesGridView(productNo);

        }
        private void frmCashRegisterWithItems_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable getCompanyInfo = piwebDataOps.GetCompanyInfo();
                //Set company Name on form
                this.Text = getCompanyInfo.Rows[0]["Name"].ToString();

                //PPX7 Printer Status
                //PP7X_Status();

                CategoryButton();
                ProductButton();
                lblTotalAmount.Text = "0.00";
                lblTax.Text = "0.00";
                lblDiscount.Text = "0.00";
                lblInvNo.Text = LoadSerials();
                CreateInvoice();
                //MessageBox.Show("Invoice No: " + InvoiceNo + " created Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnCategoriesAll_Click(object sender, EventArgs e)
        {
            btnDiscount.Controls.Clear();
            ProductButton();
        }

        private void AddToSalesGridView(string prodNo)
        {
            DataTable getProducts = piwebDataOps.GetProducts(prodNo);

            string _productName = getProducts.Rows[0]["ProductName"].ToString();
            string _currentStock = getProducts.Rows[0]["CurrentStock"].ToString();
            if (_currentStock == string.Empty)
                _currentStock = "0";
            int _stockLevel = 5;
            char _allowNegativeStock = Convert.ToChar(getProducts.Rows[0]["AllowNegStock"].ToString());
            char _returnAllowed = Convert.ToChar(getProducts.Rows[0]["ReturnAllowed"].ToString());

            //
            //Check if product is available in stock
            //
            if (Convert.ToInt32(_currentStock) == 0)
            {
                //
                //Allow Negative Stock
                //
                switch (_allowNegativeStock)
                {
                    case 'Y':
                        CheckIfGridHasRows(prodNo);
                        break;
                    default:
                        if (_returnAllowed == 'Y')
                        {
                            CheckIfGridHasRows(prodNo);
                        }
                        else
                        {
                            MessageBox.Show("\"" + _productName + "\"" + " is out of Stock", "Product", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        return;
                }
            }
            else if (Convert.ToInt32(_currentStock) < _stockLevel)
            {
                MessageBox.Show("\"" + _productName + "\"" + " below Stock level", "Product", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                CheckIfGridHasRows(prodNo);
            }
            else
            {
                CheckIfGridHasRows(prodNo);
            }

        }
        private void AddToSalesGridViewFilter(string filter)
        {
            try
            {
                DataTable getProducts = piwebDataOps.GetProductsByFilter(filter);

                string prodNo = getProducts.Rows[0]["No"].ToString();
                string _productName = getProducts.Rows[0]["ProductName"].ToString();
                string _currentStock = getProducts.Rows[0]["CurrentStock"].ToString();
                if (_currentStock == string.Empty)
                    _currentStock = "0";
                int _stockLevel = 5;
                char _allowNegativeStock = Convert.ToChar(getProducts.Rows[0]["AllowNegStock"].ToString());
                char _returnAllowed = Convert.ToChar(getProducts.Rows[0]["ReturnAllowed"].ToString());

                //
                //Check if product is available in stock
                //
                if (Convert.ToInt32(_currentStock) == 0)
                {
                    //
                    //Allow Negative Stock
                    //
                    switch (_allowNegativeStock)
                    {
                        case 'Y':
                            CheckIfGridHasRows(prodNo);
                            break;
                        default:
                            if (_returnAllowed == 'Y')
                            {
                                CheckIfGridHasRows(prodNo);
                            }
                            else
                            {
                                MessageBox.Show("\"" + _productName + "\"" + " is out of Stock", "Product", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }

                            return;
                    }
                }
                else if (Convert.ToInt32(_currentStock) < _stockLevel)
                {
                    MessageBox.Show("\"" + _productName + "\"" + " below Stock level", "Product", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    CheckIfGridHasRows(prodNo);
                }
                else
                {
                    CheckIfGridHasRows(prodNo);
                }
            }
            catch (Exception e)
            {

                MessageBox.Show("Item not found, Please scan or paste item code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        /// <summary>
        /// Get product and add to dataGridView
        /// </summary>
        /// <param name="_no"></param>
        private void CheckIfGridHasRows(string _no)
        {
            try
            {
                hasDiscount = TransactionsHelper.isDiscount; // Set true if discount button is clicked

                decimal _price, _lineAmount = 0;

                decimal ItemTaxRate = 0, taxAmount = 0, VAT = 0,
                    _subTotal = Convert.ToDecimal(lblSubTotal.Text),
                    _tax = discountTaxSale.TaxAmount,
                    _discount = discountTaxSale.DiscountAmount,
                    _total = Convert.ToDecimal(lblTotalAmount.Text);
                DataTable getTax = null, getDiscount = null;

                bool found = false;
                DataTable getProducts = piwebDataOps.GetProducts(_no);
                string taxGroupCode = getProducts.Rows[0]["TaxGroupCode"].ToString();
                string discountGroupCode = getProducts.Rows[0]["DiscountGroupCode"].ToString();
                decimal originalPrice = Convert.ToDecimal(getProducts.Rows[0]["UnitPrice"].ToString());
                decimal unitPriceExclVAT = Convert.ToDecimal(getProducts.Rows[0]["UnitPriceExclVAT"].ToString());
                char priceIncVAT = Convert.ToChar(getProducts.Rows[0]["PriceIncVAT"].ToString());
                if (dataGridView1.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {

                        if (getProducts.Rows[0]["ProductName"].ToString() == row.Cells[0].Value.ToString())
                        {

                            switch (priceIncVAT)
                            {
                                case 'Y':
                                    //Increase Quantity
                                    row.Cells[1].Value = Convert.ToString(1 + Convert.ToInt32(row.Cells[1].Value.ToString()));

                                    //Calculate Line Amount
                                    if (hasDiscount == true)
                                    {
                                        _lineAmount = Convert.ToDecimal(row.Cells[2].Value) + discountTaxSale.SalesPrice;
                                        TransactionsHelper.isDiscount = false;
                                    }
                                    else
                                        _lineAmount = Convert.ToDecimal(row.Cells[2].Value) + originalPrice;

                                    row.Cells[2].Value = String.Format("{0:N}", _lineAmount);


                                    //Get Product Tax
                                    if (!string.IsNullOrEmpty(taxGroupCode))
                                    {
                                        getTax = piwebDataOps.GetTax(taxGroupCode);
                                        ItemTaxRate = Convert.ToDecimal(getTax.Rows[0]["Tax"].ToString());
                                    }

                                    //Get Discount
                                    if (!string.IsNullOrEmpty(discountGroupCode))
                                    {
                                        getDiscount = piwebDataOps.GetDiscount(discountGroupCode);
                                        _discount = Convert.ToDecimal(getDiscount.Rows[0]["DiscountRate"].ToString());
                                    }
                                    else
                                    {
                                        discountTaxSale.ProductName = getProducts.Rows[0]["ProductName"].ToString();
                                        discountTaxSale.OriginalPrice = originalPrice;
                                        discountTaxSale.TaxRate = ItemTaxRate;
                                    }

                                    //Calculate Price After Tax
                                    discountTaxSale.SalesPriceOffTax(originalPrice, ItemTaxRate);

                                    break;
                                case 'N':
                                    //Increase Quantity
                                    row.Cells[1].Value = Convert.ToString(1 + Convert.ToInt32(row.Cells[1].Value.ToString()));

                                    //Calculate Line Amount
                                    if (hasDiscount == true)
                                        _lineAmount = Convert.ToDecimal(row.Cells[2].Value) + discountTaxSale.SalesPrice;
                                    else
                                        _lineAmount = (Convert.ToDecimal(row.Cells[2].Value) + unitPriceExclVAT);

                                    row.Cells[2].Value = String.Format("{0:N}", _lineAmount);

                                    //Get Product Tax
                                    if (!string.IsNullOrEmpty(taxGroupCode))
                                    {
                                        getTax = piwebDataOps.GetTax(taxGroupCode);
                                        ItemTaxRate = Convert.ToDecimal(getTax.Rows[0]["Tax"].ToString());
                                    }

                                    //Get Discount
                                    if (!string.IsNullOrEmpty(discountGroupCode))
                                    {
                                        getDiscount = piwebDataOps.GetDiscount(discountGroupCode);
                                        _discount = Convert.ToDecimal(getDiscount.Rows[0]["DiscountRate"].ToString());
                                    }
                                    else
                                    {
                                        discountTaxSale.ProductName = getProducts.Rows[0]["ProductName"].ToString();
                                        discountTaxSale.OriginalPrice = unitPriceExclVAT;
                                        discountTaxSale.TaxRate = ItemTaxRate;
                                    }

                                    //Calculate new Price + Tax
                                    discountTaxSale.SalesPriceWithTax(unitPriceExclVAT, ItemTaxRate);
                                    break;
                            }


                            found = true; //Item has been found

                            this.dataGridView1.ClearSelection();
                            this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Selected = true;
                        }
                    }
                    if (!found) // item not found
                    {
                        AddNewProductToGridView(_no);
                    }
                }
                else
                {
                    AddNewProductToGridView(_no);
                }

                CalculateDisplayAmounts(discountTaxSale.VAT, discountTaxSale.Discount);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Products", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void AddNewProductToGridView(string _no)
        {
            try
            {
                decimal _price, _lineAmount = 0;

                decimal ItemTaxRate = 0, taxAmount = 0, VAT = 0,
                    _subTotal = Convert.ToDecimal(lblSubTotal.Text),
                    _tax = discountTaxSale.TaxAmount,
                    _discount = discountTaxSale.DiscountAmount,
                    _total = Convert.ToDecimal(lblTotalAmount.Text);

                DataTable getTax = null, getDiscount = null;

                DataTable getProducts = piwebDataOps.GetProducts(_no);
                string taxGroupCode = getProducts.Rows[0]["TaxGroupCode"].ToString();
                string discountGroupCode = getProducts.Rows[0]["DiscountGroupCode"].ToString();
                decimal originalPrice = Convert.ToDecimal(getProducts.Rows[0]["UnitPrice"].ToString());
                decimal unitPriceExclVAT = Convert.ToDecimal(getProducts.Rows[0]["UnitPriceExclVAT"].ToString());
                char priceIncVAT = Convert.ToChar(getProducts.Rows[0]["PriceIncVAT"].ToString());

                switch (priceIncVAT)
                {
                    case 'Y':
                        //Add new product to gridview if empty
                        dataGridView1.Rows.Add(getProducts.Rows[0]["ProductName"].ToString(), "1", String.Format("{0:N}", originalPrice));

                        //Get Product Tax
                        if (!string.IsNullOrEmpty(taxGroupCode))
                        {
                            getTax = piwebDataOps.GetTax(taxGroupCode);
                            ItemTaxRate = Convert.ToDecimal(getTax.Rows[0]["Tax"].ToString());

                        }
                        //Get Discount
                        if (!string.IsNullOrEmpty(discountGroupCode))
                        {
                            getDiscount = piwebDataOps.GetDiscount(discountGroupCode);
                            _discount = Convert.ToDecimal(getDiscount.Rows[0]["DiscountRate"].ToString());
                        }
                        else
                        {
                            discountTaxSale.ProductName = getProducts.Rows[0]["ProductName"].ToString();
                            discountTaxSale.OriginalPrice = originalPrice;
                            discountTaxSale.TaxRate = ItemTaxRate;
                        }

                        //Calculate Sales Price - VAT
                        discountTaxSale.SalesPriceOffTax(originalPrice, ItemTaxRate);
                        break;

                    case 'N':
                        //Add new product to gridview if empty
                        dataGridView1.Rows.Add(getProducts.Rows[0]["ProductName"].ToString(), "1", String.Format("{0:N}", unitPriceExclVAT));

                        //Get Product Tax
                        if (!string.IsNullOrEmpty(taxGroupCode))
                        {
                            getTax = piwebDataOps.GetTax(taxGroupCode);
                            ItemTaxRate = Convert.ToDecimal(getTax.Rows[0]["Tax"].ToString());

                        }
                        //Get Discount
                        if (!string.IsNullOrEmpty(discountGroupCode))
                        {
                            getDiscount = piwebDataOps.GetDiscount(discountGroupCode);
                            _discount = Convert.ToDecimal(getDiscount.Rows[0]["DiscountRate"].ToString());
                        }
                        else
                        {
                            discountTaxSale.ProductName = getProducts.Rows[0]["ProductName"].ToString();
                            discountTaxSale.OriginalPrice = unitPriceExclVAT;
                            discountTaxSale.TaxRate = ItemTaxRate;
                        }

                        //Calculate Sales Price + VAT
                        discountTaxSale.SalesPriceWithTax(unitPriceExclVAT, ItemTaxRate);
                        break;
                }

                this.dataGridView1.ClearSelection();
                this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Selected = true;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        private void CalculateDisplayAmounts(decimal _itemTax, decimal _discount)
        {
            decimal itemTax = 0, tax = 0, discount = 0;
            //int quantity = 0;

            if (_itemTax != 0)
            {
                itemTax = _itemTax;
            }

            if (_discount != 0)
            {
                discount = _discount;
            }

            //Adding Line Total column figures
            var result = dataGridView1.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells[2].Value));

            switch (TransactionsHelper.isDiscount)
            {
                case true:
                    decimal TaxBeforeDiscount = discountTaxSale.ItemTaxBeforeDiscount;
                    discountTaxSale.DiscountAmount += discount;
                    lblDiscount.Text = String.Format("{0:N}", discountTaxSale.DiscountAmount);
                    var taxDiff = discountTaxSale.TaxAmount - _itemTax;
                    lblTax.Text = String.Format("{0:N}", discountTaxSale.TaxAmount - taxDiff);
                    //lblSubTotal.Text = String.Format("{0:N}", result - discountTaxSale.TaxAmount - discountTaxSale.DiscountAmount);
                    lblSubTotal.Text = String.Format("{0:N}", result - _itemTax - discountTaxSale.DiscountAmount);
                    lblTotalAmount.Text = String.Format("{0:N}", Convert.ToDecimal(lblSubTotal.Text) + _itemTax + discountTaxSale.DiscountAmount); //Grand Total

                    //Set to Defaults
                    TransactionsHelper.isDiscount = false;
                    discountTaxSale.SalesPrice = 0;
                    discountTaxSale.OriginalPrice = 0;
                    discountTaxSale.TaxRate = 0;
                    discountTaxSale.VAT = 0;
                    discountTaxSale.Discount = 0;
                    discountTaxSale.ItemTaxBeforeDiscount = 0;
                    break;
                default:
                    if (discount != 0)
                        lblDiscount.Text = String.Format("{0:N}", discountTaxSale.DiscountAmount);

                    discountTaxSale.TaxAmount += _itemTax;
                    lblTax.Text = String.Format("{0:N}", discountTaxSale.TaxAmount);                 //Displaying Tax Amount
                    lblSubTotal.Text = String.Format("{0:N}", result - discountTaxSale.TaxAmount - discountTaxSale.DiscountAmount);// - Convert.ToDecimal(lblDiscount.Text));
                    lblTotalAmount.Text = String.Format("{0:N}", Convert.ToDecimal(lblSubTotal.Text) + discountTaxSale.TaxAmount + discountTaxSale.DiscountAmount); //Grand Total
                    break;
            }


            //decimal subtotal = result - discount; // - itemTax; //- Convert.ToDecimal(discount);        //Calculating Sub Total After Discount
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            //print_Receipt();

            string invoiceNo = LoadSerials(),
                transactionType = "",
                customerCode = "",
                deviceID = System.Environment.MachineName,
                createdBy = UserSession.userName;

            //Product Details
            string PluName = ""; // textBox35.Text;
            int taxindex = 0; //Convert.ToInt16(numericUpDown3.Value);
            double unitPrice = 0; //Convert.ToDouble(textBox36.Text);
            double quantity = 0;//Convert.ToDouble(textBox37.Text);

            string _statusCode = "Pending";
            string _priceListID = "";
            string _tax1ID = "";
            double _lineTax1 = 0;
            double _tax1Rate = 0;
            decimal _lineDiscount = 0;
            decimal _extendedPrice = 0;
            decimal _fixedPrice = 0;
            decimal discountRate = 0;
            decimal _cashDiscount = 0;

            decimal subTotal = Convert.ToDecimal(lblSubTotal.Text),
                tax = discountTaxSale.TaxAmount,
                discount = discountTaxSale.DiscountAmount,
                totalAmount = Convert.ToDecimal(lblTotalAmount.Text);
            decimal discountAmount = 0;
            bool isPercentageDiscount = false;

            if (dataGridView1.Rows.Count < 1)
            {
                MessageBox.Show("Please Add Items to Pay", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(cmbTransactionType.Text))
                transactionType = cmbTransactionType.SelectedValue.ToString();

            registerSales();
            test_print();
            //pp7x_receipt();

            dataGridView1.Rows.Clear();
            btnDiscount.Controls.Clear();
            PanelCategoryView.Controls.Clear();
            CategoryButton();
            ProductButton();
            lblTotalAmount.Text = "0.00";
            lblTax.Text = "0.00";
            lblDiscount.Text = "0.00";
            lblSubTotal.Text = "0.00";
            lblInvNo.Text = LoadSerials();
        }
        private void pp7x_receipt()
        {
            string invoiceNo = LoadSerials(),
                transactionType = "",
                customerCode = "",
                deviceID = System.Environment.MachineName,
                createdBy = "SYSTEM";
            string PluName = ""; // textBox35.Text;
            int taxindex = 0; //Convert.ToInt16(numericUpDown3.Value);
            double unitPrice = 0; //Convert.ToDouble(textBox36.Text);
            double quantity = 0;//Convert.ToDouble(textBox37.Text);

            string _statusCode = "Pending";
            string _priceListID = "";
            string _tax1ID = "";
            double _lineTax1 = 0;
            double _tax1Rate = 0;
            decimal _lineDiscount = 0;
            decimal _extendedPrice = 0;
            decimal _fixedPrice = 0;
            decimal discountRate = 0;
            decimal _cashDiscount = 0;


            decimal subTotal = Convert.ToDecimal(lblSubTotal.Text),
                tax = Convert.ToDecimal(lblTax.Text),
                discount = Convert.ToDecimal(lblDiscount.Text),
                totalAmount = Convert.ToDecimal(lblTotalAmount.Text);
            decimal discountAmount = 0;
            bool isPercentageDiscount = false;


            int nResult = 0;
            string pass = "000000";
            int clerkId = 1;

            try
            {
                //Check Printer Status
                #region Printer Status
                nResult = pp7x.__PrinterStatus();

                switch (nResult)
                {

                    case -1:
                        MessageBox.Show("Timeout", "Printer Status", MessageBoxButtons.OK, MessageBoxIcon.Hand); break;
                    //End Timeout case

                    case -2:
                        MessageBox.Show("Please Check Printer Connection", "Printer Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Try Create Printer Connection
                        try
                        {
                            string CommName = TransactionsHelper.commName; // Get Value from TransactionHelper
                            int timeOut = TransactionsHelper.timeOut; // Get Value from TransactionHelper
                            int RetryCount = TransactionsHelper.retryCount; // Get Value from TransactionHelper
                            int BaudRate = TransactionsHelper.baudRate; // Get Value from TransactionHelper

                            pp7x.__Config(timeOut, RetryCount, BaudRate, CommName);
                            pp7x.__Open();
                            if (pp7x.__Active())
                            {
                                nResult = pp7x.__GetSoftwareStatus();
                                if ((nResult == -1) || (nResult == -2))
                                {
                                    MessageBox.Show("Connection to Printer failed", "Printer Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                else
                                {
                                    MessageBox.Show("Connect Ok", "Printer Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Printer is not Active\n Check connection or Printer is Off", "Printer Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Printer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        break; //End Fail case

                    case 1: break;
                }
                #endregion

                //Check Clerk LogIn
                #region Check Clerk Login

                //1. Clerk LogIn
                nResult = pp7x.__CashierLogin(clerkId, pass);

                switch (nResult)
                {
                    case -1: MessageBox.Show("Timeout"); break;
                    case -2: MessageBox.Show("Fail"); break;
                    case 1: ; break;
                }

                #endregion

                //Try Open Fiscal Receipt
                #region OpenFiscalReceipt

                //2. Open Fiscal Receipt
                nResult = pp7x.__OpenFiscalReceipt();
                switch (nResult)
                {
                    case -1: MessageBox.Show("Timeout"); break;
                    case -2: MessageBox.Show("Fail"); break;
                    case 1: break;
                }
                #endregion

                //Try Register sales
                #region Register Sales

                //3. Register Sale
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {

                    #region Product Details

                    char active = 'Y', includesVAT = 'Y';
                    string productCode = "", taxGroupCode = "", discountGroupCode = "", status = "", unitOfMeasure = "";
                    string description = dataGridView1.Rows[i].Cells["description"].Value.ToString();

                    //Get Item and Discount from List<ItemDiscounts>
                    var discountValue = from d in ItemDiscounts where d.ItemName == description select d;

                    //_cashDiscount = Convert.ToDecimal(discountValue);
                    foreach (var item in discountValue)
                    {
                        _cashDiscount = Convert.ToDecimal(item.DiscountValue);
                        isPercentageDiscount = Convert.ToBoolean(item.IsPercentageDiscount);
                    }

                    //Get product Details from DB
                    DataTable getProductDetails = piwebDataOps.GetProducts(description, "");

                    active = Convert.ToChar(getProductDetails.Rows[0]["Active"].ToString());
                    includesVAT = Convert.ToChar(getProductDetails.Rows[0]["PriceIncVAT"].ToString());

                    switch (includesVAT)
                    {
                        case 'N':
                            unitPrice = Convert.ToDouble(getProductDetails.Rows[0]["UnitPriceExclVAT"].ToString());
                            break;
                        default:
                            unitPrice = Convert.ToDouble(getProductDetails.Rows[0]["UnitPrice"].ToString());
                            break;
                    }

                    PluName = Convert.ToString(dataGridView1.Rows[i].Cells["description"].Value);
                    //taxindex = 1;
                    quantity = Convert.ToInt32(dataGridView1.Rows[i].Cells["qty"].Value);

                    decimal lineTotal = Convert.ToDecimal(dataGridView1.Rows[i].Cells["amount"].Value);
                    //Get Product Code
                    productCode = getProductDetails.Rows[0]["No"].ToString();
                    taxGroupCode = getProductDetails.Rows[0]["TaxGroupCode"].ToString();
                    discountGroupCode = getProductDetails.Rows[0]["DiscountGroupCode"].ToString();
                    unitOfMeasure = getProductDetails.Rows[0]["UnitOfMeasureCode"].ToString();

                    if (active == 'Y')
                        status = "Yes";
                    else
                        status = "No";

                    //Get Product Tax
                    //double ItemTaxRate = 0, lineTax = 0;
                    if (!string.IsNullOrEmpty(taxGroupCode))
                    {
                        DataTable getTax = piwebDataOps.GetTax(taxGroupCode);
                        _tax1Rate = Convert.ToDouble(getTax.Rows[0]["Tax"].ToString());
                        _tax1ID = getTax.Rows[0]["TaxGroupCode"].ToString();

                        switch (_tax1ID)
                        {
                            case "VAT A": taxindex = 1; break;
                            case "VAT B": taxindex = 2; break;
                            case "VAT E": taxindex = 3; break;
                            default:
                                break;
                        }

                        if (_tax1Rate != 0)
                            _lineTax1 = (unitPrice * _tax1Rate) / 100;
                    }
                    #endregion

                    //Registering Sale in PPX Device
                    nResult = pp7x.__Registeringsale(PluName, taxindex, unitPrice, quantity, true, false);
                    switch (nResult)
                    {
                        case -1: MessageBox.Show("Timeout"); break;
                        case -2: MessageBox.Show("Fail"); break;
                        case 1: ; break;
                    }

                    #region Register Discount

                    try
                    {
                        int iType = TransactionsHelper._iType;
                        double _payAmount = Convert.ToDouble(_cashDiscount);
                        string mdescription = TransactionsHelper._description;

                        if (isPercentageDiscount == true)
                        {
                            //Grab Discount Rate and discountAmount
                            discountAmount = Convert.ToDecimal((unitPrice * _payAmount) / 100);
                            _lineDiscount = Convert.ToDecimal((unitPrice * _payAmount) / 100);
                            discountRate = Convert.ToDecimal(_payAmount);

                            // % Amount
                            nResult = pp7x.__subpere(iType, _payAmount, mdescription);

                            switch (nResult)
                            {

                                case -1: MessageBox.Show("Timeout", "Discount", MessageBoxButtons.OK, MessageBoxIcon.Hand); break;
                                case -2: MessageBox.Show("Failed to register Discount with percentage", "Discount", MessageBoxButtons.OK, MessageBoxIcon.Error); break;
                                case 1: break;
                            }
                        }
                        else
                        {
                            //Grab Discount Rate and discountAmount
                            discountAmount = Convert.ToDecimal(_payAmount);
                            _lineDiscount = Convert.ToDecimal(_payAmount);

                            //Fixed Amount
                            nResult = pp7x.__Discount(_payAmount, true);

                            switch (nResult)
                            {

                                case -1: MessageBox.Show("Timeout", "Discount", MessageBoxButtons.OK, MessageBoxIcon.Hand); break;
                                case -2: MessageBox.Show("Failed to register Discount with fixed amount", "Discount", MessageBoxButtons.OK, MessageBoxIcon.Error); break;
                                case 1: break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Discount process failed\n" + "\n" + ex.StackTrace, "Discount", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //Register Sale in DB
                    piwebDataOps.CreateSalesInvoiceLine(InvoiceNo, productCode, PluName, Convert.ToDecimal(quantity), unitOfMeasure, _statusCode, _priceListID, Convert.ToDecimal(unitPrice), Convert.ToDecimal(unitPrice), _tax1ID, Convert.ToDecimal(_lineTax1), Convert.ToDecimal(_tax1Rate), taxGroupCode, discountGroupCode, _lineDiscount, _extendedPrice, includesVAT, _fixedPrice, discountRate, discountAmount, UserSession.userName, deviceID);

                    //Insert Data int INV_InventoryStock
                    piwebDataOps.CreateInventoryStock("SALESINV", InvoiceNo, productCode, 0, Convert.ToDecimal(quantity), UserSession.userName);

                    //Remove item from List<ItemDiscounts>
                    var itemToRemove = ItemDiscounts.Find(r => r.ItemName == description);
                    if (itemToRemove != null)
                        ItemDiscounts.Remove(itemToRemove);
                    _cashDiscount = 0;
                    isPercentageDiscount = false;

                    #endregion

                }
                #endregion

                //Try to Register Payment
                #region Register Payment

                //Open Payment Form
                frmPayment openPaymentType = new frmPayment();
                openPaymentType.TotalAmount = lblTotalAmount.Text;
                openPaymentType.ShowDialog();

                string TenderedAmount = openPaymentType.TenderedAmount;

                double PayAmount = Convert.ToDouble(TenderedAmount);

                while (PayAmount < Convert.ToDouble(lblTotalAmount.Text))
                {
                    MessageBox.Show("Pay Amount is not sufficient.\n Due Amount: " + lblTotalAmount.Text, "Payment", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //Re-Open Payment Form
                    openPaymentType = new frmPayment();
                    openPaymentType.TotalAmount = lblTotalAmount.Text;
                    openPaymentType.ShowDialog();

                    TenderedAmount = openPaymentType.TenderedAmount;
                    PayAmount = Convert.ToDouble(TenderedAmount);
                }
                //
                //Register Payment In Database
                //
                string payMode = openPaymentType.PaymentMode, bankName = openPaymentType.BankName;
                int paymentTypeMode = -1;

                switch (payMode)
                {
                    case "CASH":
                        paymentTypeMode = 0;
                        piwebDataOps.CreatePaymentLine(payMode, invoiceNo, totalAmount, Convert.ToDecimal(PayAmount), payMode, username);
                        break;
                    case "CARD":
                        paymentTypeMode = 2;
                        piwebDataOps.CreatePaymentLineCard(payMode, invoiceNo, totalAmount, payMode, paymentTypeMode, username);
                        break;
                    case "CHEQUE":
                        paymentTypeMode = 1;
                        piwebDataOps.CreatePaymentLineCheque(payMode, invoiceNo, totalAmount, payMode, 1, username);
                        break;
                    case "":
                        MessageBox.Show("Data Not Saved", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                //Register Payment in Device
                nResult = pp7x.__Totalcalculating(paymentTypeMode, PayAmount, true);

                switch (nResult)
                {

                    case -1: MessageBox.Show("Timeout"); break;
                    case -2: MessageBox.Show("Failed to Make payment"); break;
                    case 1: break;
                }

                #endregion

                //Try Close Fiscal Receipt
                #region CloseFiscalReceipt
                nResult = pp7x.__CloseFiscalReceipt();
                switch (nResult)
                {
                    case -1: MessageBox.Show("Timeout"); break;
                    case -2: MessageBox.Show("Fail"); break;
                    case 1: ; break;
                }
                #endregion

                //
                //End Sale
                //
            }
            catch (Exception ex)
            {
                MessageBox.Show("Check Help to Contact Developer\n" + ex.StackTrace, "Payment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        void pdoc_PrintReceipt(object sender, PrintPageEventArgs e)
        {

            //int startX = 30;
            //int startY = 35;
            //int Offset = 40;

            int startX = 10;
            int startY = 20;
            int Offset = 10;
            Offset += 0;

            float x = 0;
            float y = 0;
            int descWidth = 185;
            int amountWidth = 180;//365; // max width I found through trial and error
            float height = 0F;
            int pageWidth = 365;

            Font drawFontArial12Bold = new Font("Courier New", 12, FontStyle.Bold);
            Font drawFontCourierNew10Bold = new Font("Courier New", 10, FontStyle.Bold);
            Font drawFontArial10Regular = new Font("Courier New", 10, FontStyle.Regular);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Set format of string.
            StringFormat drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            StringFormat drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            StringFormat drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;

            // Draw string to screen.

            string underLine = "-------------------------------------------";
            //Print Company Name
            string text = "Piweb Technology";
            e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(startX, startY + Offset, pageWidth, height), drawFormatCenter);
            Offset = Offset + 15;

            //Print Address
            text = "P.O.Box 1878, Lilongwe";
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(startX, startY + Offset, pageWidth, height), drawFormatCenter);
            Offset = Offset + 15;

            //Print Tel
            text = "+265 999 664 862";
            e.Graphics.DrawString("Phone: " + text, drawFontArial10Regular, drawBrush, new RectangleF(startX, startY + Offset, pageWidth, height), drawFormatCenter);
            Offset = Offset + 15;
            Offset = Offset + 15;

            //Print VAR Reg No.
            text = "VAT NO: 000022220000";
            e.Graphics.DrawString("VAT RegNo.: " + text, drawFontArial10Regular, drawBrush, new RectangleF(startX, startY + Offset, descWidth, height), drawFormatLeft);

            //Print Invoice No
            text = lblInvNo.ToString();
            e.Graphics.DrawString("Invoice No.: " + text, drawFontArial10Regular, drawBrush, new RectangleF(startX, startY + Offset, amountWidth, height), drawFormatRight);
            Offset = Offset + 15;

            //Print Cashier
            text = "Paul Chifukwa";
            e.Graphics.DrawString("Cashier: " + text, drawFontArial10Regular, drawBrush, new RectangleF(startX, startY + Offset, amountWidth, height), drawFormatLeft);

            //Print Counter
            text = "1";
            e.Graphics.DrawString("Till #: " + text, drawFontArial10Regular, drawBrush, new RectangleF(startX, startY + Offset, amountWidth, height), drawFormatRight);
            Offset = Offset + 15;

            //Print Start Line
            e.Graphics.DrawString(underLine, new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 15;

            //Print Item Description Header
            e.Graphics.DrawString("Description", drawFontCourierNew10Bold, drawBrush, new RectangleF(startX, startY + Offset, descWidth, height), drawFormatLeft);
            e.Graphics.DrawString("Amount".PadLeft(18), drawFontCourierNew10Bold, drawBrush, new RectangleF(startX, startY + Offset, amountWidth, height), drawFormatRight);
            Offset = Offset + 15;

            //Print Item Description
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int _quantity = 1;//Convert.ToInt32(row.Cells[2].Value.ToString());
                string _description = row.Cells[1].Value.ToString();
                string _amount = "800";//row.Cells[4].Value.ToString();

                e.Graphics.DrawString(_description + " X " + _quantity, drawFontArial10Regular, drawBrush, new RectangleF(startX, startY + Offset, descWidth, height), drawFormatLeft);
                e.Graphics.DrawString(_amount.PadLeft(18), drawFontArial10Regular, drawBrush, new RectangleF(startX, startY + Offset, amountWidth, height), drawFormatRight);

                Offset = Offset + 20;
                Offset = Offset + 10;
            }

            //Print End Line
            e.Graphics.DrawString(underLine, new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            e.Graphics.DrawString("Total".ToUpper(), drawFontCourierNew10Bold, drawBrush, new RectangleF(startX, startY + Offset, descWidth, height), drawFormatLeft);
            e.Graphics.DrawString(lblTotalAmount.ToString(), drawFontCourierNew10Bold, drawBrush, new RectangleF(startX, startY + Offset, amountWidth, height), drawFormatRight);
            Offset = Offset + 20;
            e.Graphics.DrawString("Tendered".ToUpper(), drawFontCourierNew10Bold, drawBrush, new RectangleF(startX, startY + Offset, descWidth, height), drawFormatLeft);
            e.Graphics.DrawString(lblTotalAmount.ToString(), drawFontCourierNew10Bold, drawBrush, new RectangleF(startX, startY + Offset, amountWidth, height), drawFormatRight);
            Offset = Offset + 20;
            e.Graphics.DrawString("Change".ToUpper(), drawFontCourierNew10Bold, drawBrush, new RectangleF(startX, startY + Offset, descWidth, height), drawFormatLeft);
            e.Graphics.DrawString(lblTotalAmount.ToString(), drawFontCourierNew10Bold, drawBrush, new RectangleF(startX, startY + Offset, amountWidth, height), drawFormatRight);
            Offset = Offset + 20;
            Offset = Offset + 20;
            //Print Address
            text = "Designed by: Piweb Technology";
            e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(startX, startY + Offset, pageWidth, height), drawFormatCenter);
            Offset = Offset + 20;

        }
        private void print_Receipt()
        {
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 12);


            PaperSize psize = new PaperSize("Custom", 100, 30000);


            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            pdoc.DefaultPageSettings.PaperSize.Height = 30000;

            pdoc.DefaultPageSettings.PaperSize.Width = 520;
            pd.PrinterSettings.PrinterName = "80mm Series Printer(1)";
            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintReceipt);
            pdoc.Print();
            pdoc.PrintPage -= new PrintPageEventHandler(pdoc_PrintReceipt);

            //DialogResult result = pd.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    PrintPreviewDialog pp = new PrintPreviewDialog();
            //    pp.Document = pdoc;
            //    result = pp.ShowDialog();
            //    if (result == DialogResult.OK)
            //    {
            //        pdoc.Print();
            //    }
            //}

        }

        private void test_print()
        {
            //getDaily(DateTime.Now);
            //wStock = this.getDaily(DateTime.Now);

            try
            {
                PrintDialog pd = new PrintDialog();
                pdoc = new PrintDocument();
                PrinterSettings ps = new PrinterSettings();
                Font font = new Font("Courier New", 15);
                PaperSize psize = new PaperSize("Custom", 100, 30000);
                pd.Document = pdoc;
                pd.Document.DefaultPageSettings.PaperSize = psize;
                pdoc.DefaultPageSettings.PaperSize.Height = 30000;
                pdoc.DefaultPageSettings.PaperSize.Width = 520;
                pdoc.PrinterSettings.PrinterName = "80mm Series Printer(1)";
                pdoc.PrintPage += new PrintPageEventHandler(dailyDep);
                pdoc.Print();
                pdoc.PrintPage -= new PrintPageEventHandler(dailyDep);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void dailyDep(object sender, PrintPageEventArgs e)
        {
            string logoDirectory = @"C:\SavedImages\";
            string logoPath, companyName, address1, address2, phone1, altPhone, vatRegNo;
            Image logo = null;

            try
            {
                //  Fetch company info
                DataTable getCompanyInfo = piwebDataOps.GetCompanyInfo();
                logoPath = getCompanyInfo.Rows[0]["logoPath"].ToString();
                companyName = getCompanyInfo.Rows[0]["Name"].ToString();
                address1 = getCompanyInfo.Rows[0]["AddressLine1"].ToString();
                address2 = getCompanyInfo.Rows[0]["AddressLine2"].ToString();
                phone1 = getCompanyInfo.Rows[0]["Mobile1"].ToString();
                altPhone = getCompanyInfo.Rows[0]["Mobile2"].ToString();
                vatRegNo = getCompanyInfo.Rows[0]["VATRegNo"].ToString();

                //Set Logo
                logo = Image.FromFile(logoDirectory + logoPath);
                //-----------
                Graphics graphics = e.Graphics;
                Font font = new Font("Courier New", 10);
                float fontHeight = font.GetHeight();
                String underLine = "------------------------------------------";
                int startX = 10;
                int startY = 20;
                int Offset = 10;
                Offset += 0;
                Offset = Offset + 15;

                int descWidth = 185;
                int amountWidth = 180;//365; // max width I found through trial and error
                float height = 0F;
                int pageWidth = 280;


                Font drawFontArial14Bold = new Font("Courier New", 12, FontStyle.Bold);
                Font drawFontArial12Bold = new Font("Courier New", 12, FontStyle.Bold);
                Font drawFontArial10Regular = new Font("Courier New", 10, FontStyle.Regular);
                Font drawFontArial8Regular = new Font("Courier New", 8, FontStyle.Regular);
                SolidBrush drawBrush = new SolidBrush(Color.Black);

                // Set format of string.
                StringFormat drawFormatCenter = new StringFormat();
                drawFormatCenter.Alignment = StringAlignment.Center;
                StringFormat drawFormatLeft = new StringFormat();
                drawFormatLeft.Alignment = StringAlignment.Near;
                StringFormat drawFormatRight = new StringFormat();
                drawFormatRight.Alignment = StringAlignment.Far;


                //  Draw Logo
                e.Graphics.DrawImage(logo, startX + (Offset * 4), startY);
                //e.Graphics.DrawImage(logo, startX, startY);
                Offset = Offset + 16;
                //Print Company Name
                string text = companyName.ToUpper();
                e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(startX, startY + Offset, pageWidth, height), drawFormatCenter);
                //e.Graphics.DrawString(text.ToUpper(), drawFontArial12Bold, drawBrush, startX, startY + Offset);
                Offset = Offset + 16;

                //  Print Slogan
                text = "Company slogan";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(startX, startY + Offset, pageWidth, height), drawFormatCenter);
                Offset = Offset + 16;

                //Print Address
                text = address1 + "," + address2;
                e.Graphics.DrawString(text, drawFontArial8Regular, drawBrush, new RectangleF(startX, startY + Offset, pageWidth, height), drawFormatCenter);
                Offset = Offset + 16;

                //Print Tel
                text = phone1 + Convert.ToString(!string.IsNullOrEmpty(altPhone) ? ", " + altPhone : "");
                e.Graphics.DrawString(text, drawFontArial8Regular, drawBrush, new RectangleF(startX, startY + Offset, pageWidth, height), drawFormatCenter);
                Offset = Offset + 15;
                if (!string.IsNullOrEmpty(altPhone))
                {
                    text = altPhone;
                    e.Graphics.DrawString(text, drawFontArial8Regular, drawBrush, startX, startY + Offset);
                    Offset = Offset + 15;
                }
                //Print VAR Reg No.
                text = vatRegNo;
                e.Graphics.DrawString("TPIN: " + text, drawFontArial8Regular, drawBrush, new RectangleF(startX, startY + Offset, pageWidth, height), drawFormatCenter);
                Offset = Offset + 15;
                Offset = Offset + 15;
                if (noSale == 1)
                {
                    //  No Sale
                    graphics.DrawString(underLine, new Font("Courier New", 8), new SolidBrush(Color.Black), 0, startY + Offset);
                    Offset = Offset + 15;
                    text = "No Sale";
                    e.Graphics.DrawString(text, drawFontArial14Bold, drawBrush, new RectangleF(startX, startY + Offset, pageWidth, height), drawFormatCenter);
                    Offset = Offset + 20;
                    graphics.DrawString(underLine, new Font("Courier New", 8), new SolidBrush(Color.Black), 0, startY + Offset);
                    Offset = Offset + 15;
                }
                else
                {
                    //
                    //Print Invoice No
                    text = lblInvNo.Text.ToString();
                    e.Graphics.DrawString("Receipt No.: ".ToUpper(), drawFontArial8Regular, drawBrush, startX, startY + Offset);
                    graphics.DrawString(text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                    Offset = Offset + 15;
                    //Receipt Date
                    graphics.DrawString("Receipt Date:  ".ToUpper(), drawFontArial8Regular, new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString(DateTime.Now.ToString("dd/MM/yyyy"), drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                    Offset = Offset + 15;

                    //Print Customer
                    text = "N/A";
                    e.Graphics.DrawString("Customer: ".ToUpper(), drawFontArial8Regular, drawBrush, startX, startY + Offset);
                    graphics.DrawString(text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                    Offset = Offset + 15;
                    //Print Counter
                    text = "1";
                    e.Graphics.DrawString("Till No: ".ToUpper(), drawFontArial8Regular, drawBrush, startX, startY + Offset);
                    graphics.DrawString(text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                    Offset = Offset + 15;
                    graphics.DrawString(underLine, new Font("Courier New", 10), new SolidBrush(Color.Black), 0, startY + Offset);
                    Offset = Offset + 10;
                    graphics.DrawString("Description".ToUpper(), new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString("Amount".ToUpper(), new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 200, startY + Offset);
                    Offset = Offset + 10;
                    graphics.DrawString(underLine, new Font("Courier New", 10), new SolidBrush(Color.Black), 0, startY + Offset);
                    Offset = Offset + 15;

                    //Print Item Description
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        int _quantity = Convert.ToInt32(row.Cells[1].Value.ToString());
                        string _description = row.Cells[0].Value.ToString();
                        string _amount = row.Cells[2].Value.ToString();

                        //graphics.DrawString(_description + " x " + _quantity, new Font("Calibri", 8), new SolidBrush(Color.Black), new RectangleF(startX, startY + Offset);
                        graphics.DrawString(_description.ToUpper() + " x " + _quantity, new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
                        //graphics.DrawString(_amount.PadLeft(18), new Font("Calibri", 8), new SolidBrush(Color.Black), new RectangleF(startX, startY + Offset, amountWidth, height), drawFormatRight);
                        graphics.DrawString(_amount, new Font("Courier New", 8), new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                        Offset = Offset + 15;
                    }

                    graphics.DrawString(underLine, new Font("Courier New", 8), new SolidBrush(Color.Black), 0, startY + Offset);
                    Offset = Offset + 15;

                    //  Totals
                    //Sub Total
                    text = lblSubTotal.Text;
                    e.Graphics.DrawString("Sub Total: ".ToUpper(), drawFontArial8Regular, drawBrush, startX, startY + Offset);
                    graphics.DrawString(text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                    Offset = Offset + 15;

                    //Discount
                    text = "-" + String.Format("{0:N}", Convert.ToDecimal(discountTaxSale.DiscountAmount.ToString()));
                    e.Graphics.DrawString("Discount: ".ToUpper(), drawFontArial8Regular, drawBrush, startX, startY + Offset);
                    graphics.DrawString(text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                    Offset = Offset + 15;

                    //VAT
                    text = String.Format("{0:N}", Convert.ToDecimal(discountTaxSale.TaxAmount.ToString()));
                    e.Graphics.DrawString("VAT 16.5%: ".ToUpper(), drawFontArial8Regular, drawBrush, startX, startY + Offset);
                    graphics.DrawString(text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                    Offset = Offset + 15;
                    graphics.DrawString(underLine, new Font("Courier New", 12), new SolidBrush(Color.Black), 0, startY + Offset);
                    Offset = Offset + 15;

                    //  Total
                    text = lblTotalAmount.Text;
                    e.Graphics.DrawString("TOTAL: ", new Font("Courier New", 15), drawBrush, startX, startY + Offset);
                    graphics.DrawString(text, new Font("Courier New", 15), new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                    Offset = Offset + 15;
                    graphics.DrawString(underLine, new Font("Courier New", 12), new SolidBrush(Color.Black), 0, startY + Offset);
                    Offset = Offset + 15;
                    //  Payment Mode
                    text = paymentMode;
                    e.Graphics.DrawString("Pay Mode: ".ToUpper(), drawFontArial8Regular, drawBrush, startX, startY + Offset);
                    graphics.DrawString(text.ToUpper(), drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                    Offset = Offset + 15;
                    //  Cash Tendered
                    text = String.Format("{0:N}", Convert.ToDecimal(cashTendered));
                    e.Graphics.DrawString("Tendered: ".ToUpper(), drawFontArial8Regular, drawBrush, startX, startY + Offset);
                    graphics.DrawString(text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                    Offset = Offset + 15;
                    //  Change
                    text = String.Format("{0:N}", Convert.ToDecimal(change));
                    e.Graphics.DrawString("Change: ".ToUpper(), drawFontArial8Regular, drawBrush, startX, startY + Offset);
                    graphics.DrawString(text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                    Offset = Offset + 15;

                    //  Served By
                    text = UserSession.fullUser;
                    e.Graphics.DrawString("Served By: ".ToUpper(), drawFontArial8Regular, drawBrush, startX, startY + Offset);
                    graphics.DrawString(text.ToUpper(), drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                    Offset = Offset + 15;
                    graphics.DrawString(underLine, new Font("Courier New", 8), new SolidBrush(Color.Black), 0, startY + Offset);
                    Offset = Offset + 15;
                    graphics.DrawString("Once goods are bought,", new Font("Courier New", 8), new SolidBrush(Color.Black), startX + 65, startY + Offset);
                    Offset = Offset + 15;
                    graphics.DrawString("Cash is not refundable.", new Font("Courier New", 8), new SolidBrush(Color.Black), startX + 62, startY + Offset);
                    Offset = Offset + 15;
                    graphics.DrawString("Thank you for doing business with us!!", new Font("Courier New", 8), new SolidBrush(Color.Black), startX + 8, startY + Offset);

                    Offset = Offset + 15;
                    Offset = Offset + 15;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void registerSales()
        {
            string invoiceNo = LoadSerials(),
                transactionType = "",
                customerCode = "",
                deviceID = System.Environment.MachineName,
                createdBy = UserSession.userName;
            string PluName = ""; // textBox35.Text;
            int taxindex = 0; //Convert.ToInt16(numericUpDown3.Value);
            double unitPrice = 0; //Convert.ToDouble(textBox36.Text);
            double quantity = 0;//Convert.ToDouble(textBox37.Text);

            string _statusCode = "Pending";
            string _priceListID = "";
            string _tax1ID = "";
            double _lineTax1 = 0;
            double _tax1Rate = 0;
            decimal _lineDiscount = 0;
            decimal _extendedPrice = 0;
            decimal _fixedPrice = 0;
            decimal discountRate = 0;
            decimal _cashDiscount = 0;

            decimal subTotal = Convert.ToDecimal(lblSubTotal.Text),
                tax = discountTaxSale.TaxAmount,
                discount = discountTaxSale.DiscountAmount,
                totalAmount = Convert.ToDecimal(lblTotalAmount.Text);
            decimal discountAmount = 0;
            bool isPercentageDiscount = false;
            //Try Register sales
            #region Register Sales

            //3. Register Sale
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                #region Product Details

                char active = 'Y', includesVAT = 'Y';
                string productCode = "", taxGroupCode = "", discountGroupCode = "", status = "", unitOfMeasure = "";
                string description = dataGridView1.Rows[i].Cells["description"].Value.ToString();
                string linePrice = dataGridView1.Rows[i].Cells["amount"].Value.ToString();

                //Get Item and Discount from List<ItemDiscounts>
                var discountValue = from d in ItemDiscounts where d.ItemName == description select d;

                //_cashDiscount = Convert.ToDecimal(discountValue);
                foreach (var item in discountValue)
                {
                    _cashDiscount = Convert.ToDecimal(item.DiscountValue);
                    isPercentageDiscount = Convert.ToBoolean(item.IsPercentageDiscount);
                }

                //Get product Details from DB
                DataTable getProductDetails = piwebDataOps.GetProducts(description, "");

                string prodName = getProductDetails.Rows[0]["ProductName"].ToString();
                active = Convert.ToChar(getProductDetails.Rows[0]["Active"].ToString());
                includesVAT = Convert.ToChar(getProductDetails.Rows[0]["PriceIncVAT"].ToString());

                switch (includesVAT)
                {
                    case 'N':
                        unitPrice = Convert.ToDouble(getProductDetails.Rows[0]["UnitPriceExclVAT"].ToString());
                        break;
                    default:
                        unitPrice = Convert.ToDouble(getProductDetails.Rows[0]["UnitPrice"].ToString());
                        break;
                }

                PluName = Convert.ToString(dataGridView1.Rows[i].Cells["description"].Value);
                //taxindex = 1;
                quantity = Convert.ToInt32(dataGridView1.Rows[i].Cells["qty"].Value);

                decimal lineTotal = Convert.ToDecimal(dataGridView1.Rows[i].Cells["amount"].Value);
                //Get Product Code
                productCode = getProductDetails.Rows[0]["No"].ToString();
                taxGroupCode = getProductDetails.Rows[0]["TaxGroupCode"].ToString();
                discountGroupCode = getProductDetails.Rows[0]["DiscountGroupCode"].ToString();
                unitOfMeasure = getProductDetails.Rows[0]["UnitOfMeasureCode"].ToString();

                if (active == 'Y')
                    status = "Yes";
                else
                    status = "No";

                //Get Product Tax
                //double ItemTaxRate = 0, lineTax = 0;
                if (!string.IsNullOrEmpty(taxGroupCode))
                {
                    DataTable getTax = piwebDataOps.GetTax(taxGroupCode);
                    _tax1Rate = Convert.ToDouble(getTax.Rows[0]["Tax"].ToString());
                    _tax1ID = getTax.Rows[0]["TaxGroupCode"].ToString();

                    switch (_tax1ID)
                    {
                        case "VAT A": taxindex = 1; break;
                        case "VAT B": taxindex = 2; break;
                        case "VAT E": taxindex = 3; break;
                        default:
                            break;
                    }

                    if (_tax1Rate != 0)
                        _lineTax1 = (unitPrice * _tax1Rate) / 100;
                }
                #endregion

                #region Register Discount

                try
                {
                    int iType = TransactionsHelper._iType;
                    double _payAmount = Convert.ToDouble(_cashDiscount);
                    string mdescription = TransactionsHelper._description;

                    if (isPercentageDiscount == true)
                    {
                        //Grab Discount Rate and discountAmount
                        discountAmount = Convert.ToDecimal((unitPrice * _payAmount) / 100);
                        _lineDiscount = Convert.ToDecimal((unitPrice * _payAmount) / 100);
                        discountRate = Convert.ToDecimal(_payAmount);
                    }
                    else
                    {
                        //Grab Discount Rate and discountAmount
                        discountAmount = Convert.ToDecimal(_payAmount);
                        _lineDiscount = Convert.ToDecimal(_payAmount);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Discount process failed\n" + "\n" + ex.StackTrace, "Discount", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Register Sale in DB
                piwebDataOps.CreateSalesInvoiceLine(InvoiceNo, productCode, PluName, Convert.ToDecimal(quantity), unitOfMeasure, _statusCode, _priceListID, Convert.ToDecimal(unitPrice), Convert.ToDecimal(linePrice), _tax1ID, Convert.ToDecimal(_lineTax1), Convert.ToDecimal(_tax1Rate), taxGroupCode, discountGroupCode, _lineDiscount, _extendedPrice, includesVAT, _fixedPrice, discountRate, discountAmount, UserSession.userName, deviceID);

                //Insert Data int INV_InventoryStock
                piwebDataOps.CreateInventoryStock("SALESINV", InvoiceNo, productCode, 0, Convert.ToDecimal(quantity), UserSession.userName);

                //Remove item from List<ItemDiscounts>
                var itemToRemove = ItemDiscounts.Find(r => r.ItemName == description);
                if (itemToRemove != null)
                    ItemDiscounts.Remove(itemToRemove);
                _cashDiscount = 0;
                isPercentageDiscount = false;

                #endregion

            }
            #endregion

            //Try to Register Payment
            #region Register Payment

            //Open Payment Form
            frmPayment openPaymentType = new frmPayment();
            openPaymentType.TotalAmount = lblTotalAmount.Text;
            openPaymentType.InvoiceNo = lblInvNo.Text;
            openPaymentType.ShowDialog();

            string TenderedAmount = openPaymentType.TenderedAmount;
            cashTendered = TenderedAmount;
            change = openPaymentType.Change;
            int paymentId = openPaymentType.paymentId;
            double PayAmount = Convert.ToDouble(TenderedAmount);

            while (PayAmount < Convert.ToDouble(lblTotalAmount.Text))
            {
                MessageBox.Show("Pay Amount is not sufficient.\n Due Amount: " + lblTotalAmount.Text, "Payment", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //Re-Open Payment Form
                openPaymentType = new frmPayment();
                openPaymentType.TotalAmount = lblTotalAmount.Text;
                openPaymentType.InvoiceNo = lblInvNo.Text;
                openPaymentType.ShowDialog();

                TenderedAmount = openPaymentType.TenderedAmount;
                PayAmount = Convert.ToDouble(TenderedAmount);
            }

            //
            //Register Payment In Database
            //
            string payMode = openPaymentType.PaymentMode, bankName = openPaymentType.BankName;
            int paymentTypeMode = -1;

            switch (payMode)
            {
                case "CASH":
                    paymentTypeMode = 0;
                    paymentMode = payMode;
                    piwebDataOps.CreatePaymentLine(payMode, invoiceNo, totalAmount, Convert.ToDecimal(PayAmount), payMode, username);
                    break;
                case "CARD":
                    paymentTypeMode = 2;
                    paymentMode = payMode;
                    piwebDataOps.CreatePaymentLineCard(payMode, invoiceNo, totalAmount, payMode, paymentId, username);
                    break;
                case "CHEQUE":
                    paymentTypeMode = 1;
                    paymentMode = payMode;
                    piwebDataOps.CreatePaymentLineCheque(payMode, invoiceNo, totalAmount, payMode, paymentId, username);
                    break;
                case "":
                    MessageBox.Show("Data Not Saved", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            #endregion
        }
        private void btnAdjustQty_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                MessageBox.Show("Please Add Items to Change Quantity", "Cash Register", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                decimal ItemTaxRate = 0, taxAmount = 0;

                TransactionsHelper.productName = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                frmChangeQuantity openChangeQty = new frmChangeQuantity();
                openChangeQty.Quantity = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                //openChangeQty.Quantity = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                openChangeQty.ShowDialog();

                //Calculate lineAmount
                DataTable product = piwebDataOps.GetProducts(TransactionsHelper.productName, "");
                decimal unitPrice = Convert.ToDecimal(product.Rows[0]["UnitPrice"].ToString());
                string taxGroupCode = product.Rows[0]["TaxGroupCode"].ToString();
                int newQty = Convert.ToInt32(openChangeQty.Quantity);

                if (Convert.ToDecimal(lblTax.Text) != 0)
                {
                    if (Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value.ToString()) < Convert.ToInt32(TransactionsHelper.qty))
                    {
                        decimal newTax = Convert.ToDecimal(lblTax.Text) - TransactionsHelper.TaxValue;
                        lblTax.Text = String.Format("{0:N}", newTax);

                        if (!string.IsNullOrEmpty(taxGroupCode))
                        {
                            DataTable getTax = piwebDataOps.GetTax(taxGroupCode);
                            ItemTaxRate = Convert.ToDecimal(getTax.Rows[0]["Tax"].ToString());
                            taxAmount = newQty * ((Convert.ToDecimal(product.Rows[0]["UnitPrice"].ToString()) * ItemTaxRate) / 100);
                        }
                    }
                    else
                    {
                        decimal newTax = Convert.ToDecimal(lblTax.Text) - TransactionsHelper.TaxValue;
                        lblTax.Text = String.Format("{0:N}", newTax);

                        //Recalculate Tax
                        if (!string.IsNullOrEmpty(taxGroupCode))
                        {
                            DataTable getTax = piwebDataOps.GetTax(taxGroupCode);
                            ItemTaxRate = Convert.ToDecimal(getTax.Rows[0]["Tax"].ToString());
                            taxAmount = newQty * ((Convert.ToDecimal(product.Rows[0]["UnitPrice"].ToString()) * ItemTaxRate) / 100);
                        }
                    }
                }

                //Update Quantity cell
                dataGridView1.SelectedRows[0].Cells[1].Value = TransactionsHelper.qty;
                dataGridView1.SelectedRows[0].Cells[2].Value = String.Format("{0:N}", Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[1].Value) * unitPrice);

                CalculateDisplayAmounts(taxAmount, 0);

                TransactionsHelper.productName = "";
                TransactionsHelper.qty = "";
            }
        }

        private void btnPriceList_Click(object sender, EventArgs e)
        {
            frmPriceList openPriceList = new frmPriceList();
            openPriceList.ShowDialog();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {

        }

        private void btn_Void(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to Void this Invoice?", "Cash Register", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string productCode = "",
                        itemName = "", uom = "", taxGroupCode = "", discountGroupCode = "";
                    decimal quantity = 0,
                        itemPrice = 0,
                        lineTotal = 0,
                        voidVAT = 0,
                        tax = 0,
                        _discount = 0;
                    char priceIncVAT = 'Y';
                    DataTable getTax = null;
                    decimal ItemTaxRate = 0;
                    decimal subTotal = Convert.ToDecimal(lblSubTotal.Text),
                        _totalTax = Convert.ToDecimal(lblTax.Text),
                        _totalDiscount = Convert.ToDecimal(lblDiscount.Text);

                    //  Update invoice
                    piwebDataOps.UpdateSalesInvoice(InvoiceNo, "VOID", subTotal, _totalTax, _totalDiscount, System.Environment.MachineName, username);

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        itemName = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                        quantity = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                        lineTotal = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());

                        DataTable getProducts = piwebDataOps.GetProductsByItemName(itemName);
                        itemPrice = Convert.ToDecimal(getProducts.Rows[0]["UnitPrice"].ToString());
                        productCode = getProducts.Rows[0]["No"].ToString();
                        uom = getProducts.Rows[0]["UnitOfMeasureCode"].ToString();
                        taxGroupCode = getProducts.Rows[0]["TaxGroupCode"].ToString();
                        discountGroupCode = getProducts.Rows[0]["DiscountGroupCode"].ToString();
                        priceIncVAT = Convert.ToChar(getProducts.Rows[0]["priceIncVAT"].ToString());

                        //Get Product Tax
                        if (!string.IsNullOrEmpty(taxGroupCode))
                        {
                            getTax = piwebDataOps.GetTax(taxGroupCode);
                            ItemTaxRate = Convert.ToDecimal(getTax.Rows[0]["Tax"].ToString());
                        }

                        if (!string.IsNullOrEmpty(lblSubTotal.Text))
                        {
                            tax = Convert.ToDecimal(getTax.Rows[0]["Tax"].ToString());
                            voidVAT = Convert.ToDecimal(itemPrice - (itemPrice / (1 + (tax / 100)))) * quantity;
                            decimal salesPriceBeforeTax = Convert.ToDecimal(discountTaxSale.SalesPriceOffTax(itemPrice, ItemTaxRate) * quantity);
                            //  Add Back Item Discounted Value to SubTotal
                            if (ItemDiscounts.Count > 0)
                            {
                                for (int j = 0; j < ItemDiscounts.Count; j++)
                                {
                                    if (itemName == ItemDiscounts[j].ItemName)
                                    {
                                        _discount = ItemDiscounts[j].DiscountValue;
                                        itemPrice = itemPrice - _discount;
                                        voidVAT = Convert.ToDecimal(itemPrice - (itemPrice / (1 + (tax / 100)))) * quantity;
                                        salesPriceBeforeTax = Convert.ToDecimal(discountTaxSale.SalesPriceOffTax(itemPrice, ItemTaxRate) * quantity);
                                    }
                                    ItemDiscounts.RemoveAt(i);
                                }
                            }

                            // New Display amounts
                            var voidTax = Convert.ToDecimal(lblTax.Text) - (voidVAT);
                            var voidDiscount = Convert.ToDecimal(lblDiscount.Text) - _discount;
                            var voidSubTotal = Convert.ToDecimal(lblSubTotal.Text) - (salesPriceBeforeTax - _discount);
                            var voidTotal = Convert.ToDecimal(lblTotalAmount.Text) - ((salesPriceBeforeTax + (voidVAT * quantity)));

                            //  Check if Current Invoice exists
                            DataTable currentInvoice = piwebDataOps.GetSalesInvoices(InvoiceNo);
                            if (currentInvoice.Rows.Count > 0)
                            {
                                //  Create VOID Lines
                                piwebDataOps.CreateSalesInvoiceLine(InvoiceNo, productCode, itemName, quantity, uom, "VOID", "", itemPrice, lineTotal, "", voidVAT, tax, taxGroupCode, "", _discount, 0, priceIncVAT, 0, 0, 0, username, System.Environment.MachineName);
                            }
                            discountTaxSale.TaxAmount = 0;
                            discountTaxSale.DiscountAmount = 0;
                        }
                        dataGridView1.Rows.Clear();
                        btnDiscount.Controls.Clear();
                        PanelCategoryView.Controls.Clear();
                        CategoryButton();
                        ProductButton();
                        lblTotalAmount.Text = "0.00";
                        lblTax.Text = "0.00";
                        lblDiscount.Text = "0.00";
                        lblSubTotal.Text = "0.00";


                        //  TODO: Enable pp7x from Settings
                        //try
                        //{
                        //    int nResult = pp7x.__AllVoid();

                        //    switch (nResult)
                        //    {
                        //        case -1: MessageBox.Show("Timeout"); break;
                        //        case -2: MessageBox.Show("Fail"); break;
                        //        case 1: MessageBox.Show("Complete"); break;
                        //    }
                        //}
                        //catch
                        //{
                        //}
                    }
                }
            }
            else
            {
                MessageBox.Show("There is nothing to Void", "Cash Register", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRecall_Click(object sender, EventArgs e)
        {
            frmRecall openRecall = new frmRecall();
            openRecall.ShowDialog();

            MessageBox.Show("GridView will Show after this operation");
        }

        private void btnDrawer_Click(object sender, EventArgs e)
        {
            noSale = 1;
            test_print();
            noSale = 0;
            //  TODO: Print No Sale Receipt

            //try
            //{
            //    int nResult = pp7x.__OpenDrawer();

            //    switch (nResult)
            //    {

            //        case -1: MessageBox.Show("Timeout"); break;
            //        case -2: MessageBox.Show("Fail"); break;
            //        case 1: MessageBox.Show("Complete"); break;
            //    }
            //}
            //catch
            //{
            //}
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                MessageBox.Show("Finish the Transaction before closing", "Cash Register", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Cash Register", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    if (UserSession.userId == 2)
                    {
                        frmStartPage openStartPage = new frmStartPage();
                        openStartPage.Show();
                        this.Close();
                    }
                    else
                    {
                        this.Close();
                    }

                    UserSession.userId = -1;
                    UserSession.roleID = -1;
                    UserSession.userName = string.Empty;
                }
                else
                {
                    return;
                }
            }

        }

        private void product_search(object sender, EventArgs e)
        {
            string filter = txtFilter.Text;
            if (!string.IsNullOrEmpty(filter))
            {
                DataTable getProducts = piwebDataOps.GetProductsByFilter(filter);
                //string searchText = "";                
                AddToSalesGridViewFilter(filter);
                txtFilter.Clear();
                txtFilter.Focus();
            }
        }

        private void txtFilter_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = metroTxtSearch.Text.Trim();
            btnDiscount.Controls.Clear();
            ProductButton(searchTerm);
        }

        private void frmCashRegisterWithItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnRemove_Click(sender, e);
            }
            if (e.KeyCode == Keys.F2)
            {
                btnAdjustQty_Click(sender, e);
            }
            if (e.KeyCode == Keys.F4)
            {
                btnDiscounts_Click(sender, e);
            }
            if (e.KeyCode == Keys.F5)
            {
                btnPriceList_Click(sender, e);
            }
            if (e.KeyCode == Keys.F6)
            {
                btnOnHold_Click(sender, e);
            }
            if (e.KeyCode == Keys.F3)
            {
                btnRecall_Click(sender, e);
            }
            if (e.KeyCode == Keys.F7)
            {
                btnLayBy_Click(sender, e);
            }
            if (e.KeyCode == Keys.F8)
            {
                btnDrawer_Click(sender, e);
            }
            if (e.KeyCode == Keys.F11)
            {
                btnEOD_Click(sender, e);
            }
            if (e.KeyCode == Keys.F12)
            {
                btn_Void(sender, e);
            }
        }

        private void btnDiscounts_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                frmDiscount openDiscount = new frmDiscount();
                openDiscount.SubTotal = lblSubTotal.Text;
                openDiscount.ShowDialog();

                //Check if SalesPrice Has a Value (Discounted Value)
                bool salesPriceEmpty = false;
                decimal discountPrice = discountTaxSale.SalesPrice;
                if (discountPrice == 0)
                    salesPriceEmpty = true;

                try
                {

                    string itemName = discountTaxSale.ProductName;
                    int iType = TransactionsHelper._iType;
                    decimal originalPrice = discountTaxSale.OriginalPrice;
                    decimal discountRate = TransactionsHelper._discountRate;
                    double discountAmount = Convert.ToDouble(TransactionsHelper._payAmount);
                    decimal discountValue = 0; //For In memory Discount
                    decimal subtotal = Convert.ToDecimal(openDiscount.SubTotal);
                    string description = TransactionsHelper._description;
                    decimal _lineAmount = 0, taxRate = 0, discount = 0, _VAT = 0;
                    bool isPercentage = false;

                    switch (iType)
                    {
                        case 0:
                            if (TransactionsHelper.isPercentage == true)
                            {
                                #region Item discount %
                                decimal salesPrice = 0;

                                discountTaxSale.ItemTaxBeforeDiscount = discountTaxSale.VAT;

                                //Hold discount rate for in-memory data
                                discountValue = discountRate;

                                //Calculate discount based on discount rate
                                discountTaxSale.SalesPriceAfterDiscount(originalPrice, discountRate);

                                salesPrice = discountTaxSale.SalesPrice;
                                taxRate = discountTaxSale.TaxRate;
                                discount = discountTaxSale.Discount;

                                //Iterate through datagridview
                                foreach (DataGridViewRow row in dataGridView1.Rows)
                                {
                                    //Check if Item matches
                                    if (itemName == row.Cells[0].Value.ToString())
                                    {
                                        switch (salesPriceEmpty)
                                        {
                                            case false:
                                                //if (Convert.ToDecimal(row.Cells[1].Value) > 1)
                                                _lineAmount = (Convert.ToDecimal(row.Cells[2].Value) - originalPrice) + salesPrice;
                                                //else
                                                //    _lineAmount = (Convert.ToDecimal(row.Cells[2].Value) - originalPrice) + salesPrice;
                                                //_lineAmount = Convert.ToDecimal(row.Cells[1].Value) * salesPrice;
                                                break;
                                            default:
                                                _lineAmount = Convert.ToDecimal(row.Cells[1].Value) * salesPrice;
                                                break;
                                        }

                                        row.Cells[2].Value = String.Format("{0:N}", _lineAmount);
                                    }
                                }

                                discountTaxSale.SalesPriceOffTax(salesPrice, taxRate);

                                //New VAT
                                _VAT = discountTaxSale.VAT;

                                isPercentage = TransactionsHelper.isPercentage;
                                TransactionsHelper.isDiscount = true; // Set true since Discount Button was clicked
                                CalculateDisplayAmounts(_VAT, discount);
                                #endregion

                            }
                            else
                            {
                                #region Item discount Fixed Amount

                                decimal salesPrice = 0;

                                discountTaxSale.ItemTaxBeforeDiscount = discountTaxSale.VAT;

                                //Hold discount Amount for in-memory data
                                discountValue = Convert.ToDecimal(discountAmount);

                                //Calculate discount based on discountAmount
                                discountTaxSale.SalesPriceFixedAmountDiscount(originalPrice, Convert.ToDecimal(discountAmount));

                                salesPrice = discountTaxSale.SalesPrice;
                                taxRate = discountTaxSale.TaxRate;
                                discount = discountTaxSale.Discount;


                                //Iterate through datagridview
                                foreach (DataGridViewRow row in dataGridView1.Rows)
                                {
                                    //Check if Item matches
                                    if (itemName == row.Cells[0].Value.ToString())
                                    {
                                        switch (salesPriceEmpty)
                                        {
                                            case false:
                                                //if (Convert.ToDecimal(row.Cells[1].Value) > 1)
                                                _lineAmount = (Convert.ToDecimal(row.Cells[2].Value) - originalPrice) + salesPrice;
                                                //else
                                                //    _lineAmount = (Convert.ToDecimal(row.Cells[2].Value) - originalPrice) + salesPrice;
                                                //_lineAmount = Convert.ToDecimal(row.Cells[1].Value) * salesPrice;
                                                break;
                                            default:
                                                _lineAmount = Convert.ToDecimal(row.Cells[1].Value) * salesPrice;
                                                break;
                                        }

                                        row.Cells[2].Value = String.Format("{0:N}", _lineAmount);
                                    }
                                }

                                discountTaxSale.SalesPriceOffTax(salesPrice, taxRate);

                                //New VAT
                                _VAT = discountTaxSale.VAT;

                                isPercentage = TransactionsHelper.isPercentage;
                                TransactionsHelper.isDiscount = true; // Set true since Discount Button was clicked
                                CalculateDisplayAmounts(_VAT, discount);
                                #endregion
                            }
                            break;
                        case 1:
                            if (TransactionsHelper.isPercentage == true)
                            {
                                discountAmount = Convert.ToDouble(TransactionsHelper._lastSoldItemAmount) * TransactionsHelper._payAmount / 100;
                                CalculateDisplayAmounts(0, Convert.ToDecimal(discountAmount));
                            }
                            else
                            {
                                CalculateDisplayAmounts(0, Convert.ToDecimal(discountAmount));
                            }
                            break;
                    }

                    //Add Item and discount to list
                    ItemDiscounts.Add(new ItemDiscounts(itemName, discountValue, isPercentage));

                }
                catch
                {
                }
            }
            else
            {
                MessageBox.Show("No item for discount", "Cash Register", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    string itemName = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    DialogResult result = MessageBox.Show("Are you sure you want to Void \"" + itemName + "\"?", "Cash Register", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
                        {

                            decimal ItemTaxRate = 0;

                            DataTable getTax = null, getDiscount = null;

                            decimal quantity = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                            decimal lineTotal = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
                            dataGridView1.Rows.RemoveAt(item.Index);

                            //  Adding Line Total column figures
                            DataTable getProducts = piwebDataOps.GetProductsByItemName(itemName);
                            decimal itemPrice = Convert.ToDecimal(getProducts.Rows[0]["UnitPrice"].ToString());
                            string productCode = getProducts.Rows[0]["No"].ToString();
                            string uom = getProducts.Rows[0]["UnitOfMeasureCode"].ToString();
                            string taxGroupCode = getProducts.Rows[0]["TaxGroupCode"].ToString();
                            string discountGroupCode = getProducts.Rows[0]["DiscountGroupCode"].ToString();
                            char priceIncVAT = Convert.ToChar(getProducts.Rows[0]["priceIncVAT"].ToString());

                            //Get Product Tax
                            if (!string.IsNullOrEmpty(taxGroupCode))
                            {
                                getTax = piwebDataOps.GetTax(taxGroupCode);
                                ItemTaxRate = Convert.ToDecimal(getTax.Rows[0]["Tax"].ToString());
                            }

                            if (!string.IsNullOrEmpty(lblSubTotal.Text))
                            {
                                decimal tax = Convert.ToDecimal(getTax.Rows[0]["Tax"].ToString());
                                var voidVAT = Convert.ToDecimal(itemPrice - (itemPrice / (1 + (tax / 100)))) * quantity;
                                var salesPriceBeforeTax = Convert.ToDecimal(discountTaxSale.SalesPriceOffTax(itemPrice, ItemTaxRate) * quantity);
                                decimal _discount = 0, voidPrice = 0;
                                //  Add Back Item Discounted Value to SubTotal
                                if (ItemDiscounts.Count > 0)
                                {
                                    for (int i = 0; i < ItemDiscounts.Count; i++)
                                    {
                                        if (itemName == ItemDiscounts[i].ItemName)
                                        {
                                            _discount = ItemDiscounts[i].DiscountValue;
                                            itemPrice = itemPrice - _discount;
                                            voidVAT = Convert.ToDecimal(itemPrice - (itemPrice / (1 + (tax / 100)))) * quantity;
                                            salesPriceBeforeTax = Convert.ToDecimal(discountTaxSale.SalesPriceOffTax(itemPrice, ItemTaxRate) * quantity);
                                        }
                                        ItemDiscounts.RemoveAt(i);
                                    }
                                }

                                // New Display amounts
                                var voidTax = Convert.ToDecimal(lblTax.Text) - (voidVAT);
                                var voidDiscount = Convert.ToDecimal(lblDiscount.Text) - _discount;
                                var voidSubTotal = Convert.ToDecimal(lblSubTotal.Text) - (salesPriceBeforeTax - _discount);
                                var voidTotal = Convert.ToDecimal(lblTotalAmount.Text) - ((salesPriceBeforeTax + (voidVAT * quantity)));

                                lblSubTotal.Text = String.Format("{0:N}", voidSubTotal);
                                lblTax.Text = String.Format("{0:N}", voidTax);
                                lblDiscount.Text = String.Format("{0:N}", voidDiscount);
                                lblTotalAmount.Text = String.Format("{0:N}", voidTotal);

                                //  TODO: Save Void Data (Header)
                                //InvoiceNo = lblInvNo.Text;  
                                string status = "New";
                                DateTime invoiceDate = DateTime.Now;

                                //  Check if Current Invoice exists
                                DataTable currentInvoice = piwebDataOps.GetSalesInvoices(InvoiceNo);
                                if (currentInvoice.Rows.Count > 0)
                                {
                                    //  Create VOID Lines
                                    piwebDataOps.CreateSalesInvoiceLine(InvoiceNo, productCode, itemName, quantity, uom, "VOID", "", itemPrice, lineTotal, "", voidVAT, tax, taxGroupCode, "", _discount, 0, priceIncVAT, 0, 0, 0, username, System.Environment.MachineName);
                                }

                                discountTaxSale.TaxAmount = 0;
                                discountTaxSale.DiscountAmount = 0;

                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("There is no item to void", "Cash Register", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnOnHold_Click(object sender, EventArgs e)
        {
            MessageBox.Show("On Hold");
        }

        private void btnLayBy_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (dataGridView1.Rows.Count > 1)
                {
                    MessageBox.Show("Layaway requires single item at a time", "Cash Register", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //  TODO: Handle Lay-by functions
                    frmLayBy openLayBy = new frmLayBy();
                    string itemName = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    openLayBy.ItemName = itemName;
                    openLayBy.ItemPrice = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
                    openLayBy.ItemTax = Convert.ToDecimal(lblTax.Text);
                    openLayBy.ItemDiscount = Convert.ToDecimal(lblDiscount.Text);
                    openLayBy.DueAmount = Convert.ToDecimal(lblTotalAmount.Text);
                    DataTable getProducts = piwebDataOps.GetProductsByItemName(itemName);
                    openLayBy.ItemCode = getProducts.Rows[0]["No"].ToString();
                    openLayBy.Show();
                }
            }
            else
            {
                MessageBox.Show("There is no Item to Layaway", "Cash Register", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private string startingInvoiceNo = "", endingInvoiceNo = "";
        private DateTime reportDate = DateTime.Now;
        private decimal cashIn = 0, cashOut = 0;
        private int nUserSales = 0;
        private void btnEOD_Click(object sender, EventArgs e)
        {
            startingInvoiceNo = GetStartingInvoiceNo();
            endingInvoiceNo = GetEndingInvoiceNo();
            DataTable getCashIn = piwebDataOps.GetCashIn(reportDate.ToString("yyyy-MM-dd"));
            decimal _cashIn = Convert.ToDecimal(!string.IsNullOrEmpty(getCashIn.Rows[0]["CashIn"].ToString()) ? Convert.ToDecimal(getCashIn.Rows[0]["CashIn"].ToString()) : 0);
            cashIn = Convert.ToDecimal(_cashIn);
            DataTable getCashOut = piwebDataOps.GetCashOut(reportDate.ToString("yyyy-MM-dd"));
            decimal _cashOut = Convert.ToDecimal(!string.IsNullOrEmpty(getCashOut.Rows[0]["CashOut"].ToString()) ? Convert.ToDecimal(getCashOut.Rows[0]["CashOut"].ToString()) : 0);
            cashOut = Convert.ToDecimal(_cashOut);
            DataTable getNUserSales = piwebDataOps.GetDistinctInvoiceUser(reportDate.ToString("yyyy-MM-dd"));
            nUserSales = getNUserSales.Rows.Count;
            EOD_print();
        }
        #region Invoice Starting No. and End No.
        public string GetStartingInvoiceNo()
        {
            string rec;
            DateTime today = DateTime.Now;

            cmd = new SqlCommand("SELECT MIN([SalesInvoiceNo]) AS 'InvoiceNo' FROM [dbo].[SAL_SalesInvoices] WHERE InvoiceDate >= '" + today.ToString("MM/dd/yyyy") + "'", sqlConn);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            rec = dt.Rows[0]["InvoiceNo"].ToString();

            return rec;
        }
        public string GetEndingInvoiceNo()
        {
            string rec;
            DateTime today = DateTime.Now;

            cmd = new SqlCommand("SELECT MAX([SalesInvoiceNo]) AS 'InvoiceNo' FROM [dbo].[SAL_SalesInvoices] WHERE InvoiceDate >= '" + today.ToString("MM/dd/yyyy") + "'", sqlConn);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            rec = dt.Rows[0]["InvoiceNo"].ToString();

            return rec;
        }
        #endregion

        private void EOD_print()
        {
            try
            {
                PrintDialog pd = new PrintDialog();
                pdoc = new PrintDocument();
                PrinterSettings ps = new PrinterSettings();
                Font font = new Font("Courier New", 15);
                PaperSize psize = new PaperSize("Custom", 100, 30000);
                pd.Document = pdoc;
                pd.Document.DefaultPageSettings.PaperSize = psize;
                pdoc.DefaultPageSettings.PaperSize.Height = 30000;
                pdoc.DefaultPageSettings.PaperSize.Width = 520;
                pdoc.PrinterSettings.PrinterName = Properties.Settings.Default.receiptPrinterName;
                pdoc.PrintPage += new PrintPageEventHandler(EOD_ReceiptDetails);
                pdoc.Print();
                pdoc.PrintPage -= new PrintPageEventHandler(EOD_ReceiptDetails);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void EOD_ReceiptDetails(object sender, PrintPageEventArgs e)
        {
            string logoDirectory = @"C:\SavedImages\";
            string logoPath, companyName, address1, address2, phone1, altPhone, vatRegNo;
            Image logo = null;

            try
            {
                //  Fetch company info
                DataTable getCompanyInfo = piwebDataOps.GetCompanyInfo();
                logoPath = getCompanyInfo.Rows[0]["logoPath"].ToString();
                companyName = getCompanyInfo.Rows[0]["Name"].ToString();
                address1 = getCompanyInfo.Rows[0]["AddressLine1"].ToString();
                address2 = getCompanyInfo.Rows[0]["AddressLine2"].ToString();
                phone1 = getCompanyInfo.Rows[0]["Mobile1"].ToString();
                altPhone = getCompanyInfo.Rows[0]["Mobile2"].ToString();
                vatRegNo = getCompanyInfo.Rows[0]["VATRegNo"].ToString();

                //Set Logo
                if (!string.IsNullOrEmpty(logoPath))
                {
                    logo = Image.FromFile(logoDirectory + logoPath);
                }

                //-----------
                Graphics graphics = e.Graphics;
                string fontFamily = Properties.Settings.Default.receiptPrinterFontFamily;
                Font font = new Font(fontFamily, 10);
                float fontHeight = font.GetHeight();
                String underLine = "------------------------------------------";
                int startX = Properties.Settings.Default.startX;
                int startY = Properties.Settings.Default.startY;
                int Offset = Properties.Settings.Default.offset;
                int extraOffset = Properties.Settings.Default.addOffset;
                Offset += 0;
                Offset = Offset + extraOffset;

                int descWidth = 185;
                int amountWidth = 180;//365; // max width I found through trial and error
                float height = 0F;
                int pageWidth = Properties.Settings.Default.receiptWidth;


                Font drawFontArial14Bold = new Font(fontFamily, 14, FontStyle.Bold);
                Font drawFontArial12Bold = new Font(fontFamily, 12, FontStyle.Bold);
                Font drawFontArial10Regular = new Font(fontFamily, 10, FontStyle.Regular);
                Font drawFontArial8Bold = new Font(fontFamily, 8, FontStyle.Bold);
                Font drawFontArial8Regular = new Font(fontFamily, 8, FontStyle.Regular);
                SolidBrush drawBrush = new SolidBrush(Color.Black);

                // Set format of string.
                StringFormat drawFormatCenter = new StringFormat();
                drawFormatCenter.Alignment = StringAlignment.Center;
                StringFormat drawFormatLeft = new StringFormat();
                drawFormatLeft.Alignment = StringAlignment.Near;
                StringFormat drawFormatRight = new StringFormat();
                drawFormatRight.Alignment = StringAlignment.Far;


                //  Draw Logo
                if (!string.IsNullOrEmpty(logoPath))
                {
                    e.Graphics.DrawImage(logo, startX + (Offset * 4), startY);
                }
                //e.Graphics.DrawImage(logo, startX, startY);
                Offset = Offset + 16;
                //Print Company Name
                string text = companyName.ToUpper();
                e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(startX, startY + Offset, pageWidth, height), drawFormatCenter);
                Offset = Offset + 20;
                //e.Graphics.DrawString(text.ToUpper(), drawFontArial12Bold, drawBrush, startX, startY + Offset);
                //Offset = Offset + 16;

                //  Print Slogan
                //text = "Company slogan";
                //e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(startX, startY + Offset, pageWidth, height), drawFormatCenter);
                //Offset = Offset + 16;

                //Print Address
                text = address1 + "," + address2;
                e.Graphics.DrawString(text, drawFontArial8Regular, drawBrush, new RectangleF(startX, startY + Offset, pageWidth, height), drawFormatCenter);
                Offset = Offset + 16;

                //Print Tel
                text = "TEL: " + phone1 + Convert.ToString(!string.IsNullOrEmpty(altPhone) ? ", " + altPhone : "");
                e.Graphics.DrawString(text, drawFontArial8Regular, drawBrush, new RectangleF(startX, startY + Offset, pageWidth, height), drawFormatCenter);
                Offset = Offset + extraOffset;

                //Print VAR Reg No.
                text = vatRegNo;
                e.Graphics.DrawString("TPIN: " + text, drawFontArial8Regular, drawBrush, new RectangleF(startX, startY + Offset, pageWidth, height), drawFormatCenter);
                Offset = Offset + extraOffset;
                graphics.DrawString(underLine, new Font("Courier New", 8), new SolidBrush(Color.Black), 0, startY + Offset);
                Offset = Offset + extraOffset;
                //
                //Report Name
                text = "Z Report".ToUpper();
                e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(startX, startY + Offset, pageWidth, height), drawFormatCenter);
                Offset = Offset + extraOffset;
                Offset = Offset + extraOffset;

                //Print Report Date
                DateTime printDate = DateTime.Now;
                text = printDate.ToString("dd/MM/yyyy");
                e.Graphics.DrawString("Date: ", drawFontArial8Regular, drawBrush, startX, startY + Offset);
                graphics.DrawString(text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                Offset = Offset + extraOffset;
                //Print Report Time
                graphics.DrawString("Time: ", drawFontArial8Regular, new SolidBrush(Color.Black), startX, startY + Offset);
                graphics.DrawString(printDate.ToString("HH:mm:ss"), drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                Offset = Offset + extraOffset;

                //Report No
                text = "320";
                e.Graphics.DrawString("Report #: ", drawFontArial8Regular, drawBrush, startX, startY + Offset);
                graphics.DrawString(text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                Offset = Offset + extraOffset;
                Offset = Offset + extraOffset;

                //From Document No
                text = startingInvoiceNo;
                e.Graphics.DrawString("From Document: ", drawFontArial8Regular, drawBrush, startX, startY + Offset);
                graphics.DrawString(text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                Offset = Offset + extraOffset;

                //To Document No
                text = endingInvoiceNo;
                e.Graphics.DrawString("To Document: ", drawFontArial8Regular, drawBrush, startX, startY + Offset);
                graphics.DrawString(text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                Offset = Offset + extraOffset;

                //Cash In
                text = String.Format("{0:N}", cashIn);
                e.Graphics.DrawString("Cash In: ", drawFontArial8Regular, drawBrush, startX, startY + Offset);
                graphics.DrawString(text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                Offset = Offset + extraOffset;

                //Cash Out
                text = String.Format("{0:N}", cashOut);
                e.Graphics.DrawString("Cash Out: ", drawFontArial8Regular, drawBrush, startX, startY + Offset);
                graphics.DrawString(text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                Offset = Offset + extraOffset;
                Offset = Offset + extraOffset;

                text = nUserSales.ToString();
                e.Graphics.DrawString("User Sales: ", drawFontArial8Regular, drawBrush, startX, startY + Offset);
                graphics.DrawString(text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                Offset = Offset + extraOffset;
                // DrawLine
                graphics.DrawString(underLine, drawFontArial8Regular, new SolidBrush(Color.Black), 0, startY + Offset);
                Offset = Offset + extraOffset;

                //Get User Sales
                string todaysDate = reportDate.ToString("yyyy-MM-dd");
                DataTable userSales = piwebDataOps.GetDistinctInvoiceUser(todaysDate);
                foreach (DataRow user in userSales.Rows)
                {
                    string userName = user["CreatedBy"].ToString();
                    graphics.DrawString(userName, drawFontArial8Regular, new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + extraOffset;
                    graphics.DrawString(underLine, drawFontArial8Regular, new SolidBrush(Color.Black), 0, startY + Offset);
                    Offset = Offset + extraOffset;

                    //  Loop through Tender Types by User
                    decimal totalTenderedByUser = 0;
                    DataTable tenderTypes = piwebDataOps.GetPaymentsModes();
                    foreach (DataRow tenderType in tenderTypes.Rows)
                    {
                        decimal amount = 0; // Convert.ToDecimal(tenderType["Amount"].ToString());
                        e.Graphics.DrawString(tenderType["PaymentModeTypeCode"].ToString(), drawFontArial8Regular, drawBrush, startX, startY + Offset);

                        //Tender Amount By Tender Type
                        DataTable totalTenderAmountByUserDate = piwebDataOps.GetPaymentsModesByUserDate(userName, todaysDate, tenderType["PaymentModeTypeCode"].ToString());
                        foreach (DataRow total in totalTenderAmountByUserDate.Rows)
                        {
                            string test = total["Amount"].ToString();
                            amount = Convert.ToDecimal(!string.IsNullOrEmpty(test) ? Convert.ToDecimal(test) : 0);
                            text = String.Format("{0:N}", amount);
                            graphics.DrawString(text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                            totalTenderedByUser += amount;
                        }
                        Offset = Offset + extraOffset;
                        //  End Loop Tender Types by User
                    }

                    //Total Tendered for User
                    text = String.Format("{0:N}", totalTenderedByUser);
                    graphics.DrawString("=" + text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                    Offset = Offset + extraOffset;

                    //DrawLine
                    graphics.DrawString(underLine, drawFontArial8Regular, drawBrush, 0, startY + Offset);
                    Offset = Offset + extraOffset;

                }

                //Summary of all Tender Types(for all users)
                e.Graphics.DrawString("Tender Types: ", drawFontArial8Regular, drawBrush, startX, startY + Offset);
                Offset = Offset + extraOffset;

                decimal totalTendered = 0;
                DataTable summaryTenderTypes = piwebDataOps.GetPaymentsModes();
                foreach (DataRow tenderType in summaryTenderTypes.Rows)
                {
                    string _tenderType = tenderType["PaymentModeTypeCode"].ToString();
                    e.Graphics.DrawString(_tenderType, drawFontArial8Regular, drawBrush, startX, startY + Offset);

                    DataTable getTotalTenderedByCard = piwebDataOps.GetPaymentsModesByDate(todaysDate, _tenderType);
                    foreach (DataRow amount in getTotalTenderedByCard.Rows)
                    {
                        string _amount = amount["Amount"].ToString();
                        decimal _totalTenderedAmount = Convert.ToDecimal(!string.IsNullOrEmpty(_amount) ? Convert.ToDecimal(_amount) : 0);
                        text = String.Format("{0:N}", _totalTenderedAmount);
                        graphics.DrawString(text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                        totalTendered += _totalTenderedAmount;
                        Offset = Offset + extraOffset;
                    }
                }

                //DrawLine
                graphics.DrawString(underLine, drawFontArial8Regular, drawBrush, 0, startY + Offset);
                Offset = Offset + extraOffset;
                //Tender Types Totals
                text = String.Format("{0:N}", totalTendered);
                e.Graphics.DrawString("Total Tendered: ", drawFontArial8Regular, drawBrush, startX, startY + Offset);
                graphics.DrawString(text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                Offset = Offset + extraOffset;

                //DrawLine
                graphics.DrawString(underLine, new Font("Courier New", 8), new SolidBrush(Color.Black), 0, startY + Offset);
                Offset = Offset + extraOffset;

                //Returns by tenderType
                text = "0";
                e.Graphics.DrawString("Returns: ", drawFontArial8Regular, drawBrush, startX, startY + Offset);
                graphics.DrawString(text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                Offset = Offset + extraOffset;
                DataTable getReturnsByTenderType = piwebDataOps.GetReturnsByTenderType(todaysDate);
                foreach (DataRow _return in getReturnsByTenderType.Rows)
                {
                    string paymentModeCode = _return["PaymentModeCode"].ToString();
                    cashOut = Convert.ToDecimal(_return["CashOut"].ToString());
                    text = String.Format("{0:N}", cashOut);
                    e.Graphics.DrawString(paymentModeCode+": ", drawFontArial8Regular, drawBrush, startX, startY + Offset);
                    graphics.DrawString(text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                }
                Offset = Offset + extraOffset;
                //DrawLine
                graphics.DrawString(underLine, drawFontArial8Regular, drawBrush, 0, startY + Offset);
                Offset = Offset + extraOffset;
                //Total Returns
                DataTable getTotalReturns = piwebDataOps.GetTotalReturns(todaysDate);
                cashOut = Convert.ToDecimal(!string.IsNullOrEmpty(getTotalReturns.Rows[0]["TotalCashOut"].ToString()) ? Convert.ToDecimal(getTotalReturns.Rows[0]["TotalCashOut"].ToString()) : 0);
                text = String.Format("{0:N}", cashOut);
                e.Graphics.DrawString("Total Returns: ", drawFontArial8Regular, drawBrush, startX, startY + Offset);
                graphics.DrawString(text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                Offset = Offset + extraOffset;
                //DrawLine
                graphics.DrawString(underLine, new Font("Courier New", 8), new SolidBrush(Color.Black), 0, startY + Offset);
                Offset = Offset + extraOffset;
                Offset = Offset + extraOffset;

                //Tax Collected
                e.Graphics.DrawString("Tax Collected: ", drawFontArial8Regular, drawBrush, startX, startY + Offset);
                Offset = Offset + extraOffset;
                //List Tax groups
                double taxRate = 0;
                DataTable getTax = piwebDataOps.GetTaxGroup();
                foreach (DataRow taxGroup in getTax.Rows)
                {
                    string _taxGroup = taxGroup["TaxGroupCode"].ToString();
                    taxRate = Convert.ToDouble(taxGroup["Tax"].ToString());
                    e.Graphics.DrawString(_taxGroup + "(" + taxRate + "%): ".ToUpper(), drawFontArial8Regular, drawBrush, startX, startY + Offset);
                    DataTable getTaxCollected = piwebDataOps.GetTaxCollectedByTaxGroupDate(_taxGroup, todaysDate);
                    foreach (DataRow totalTax in getTaxCollected.Rows)
                    {
                        decimal _totalTax = Convert.ToDecimal(!string.IsNullOrEmpty(totalTax["taxCollected"].ToString()) ? Convert.ToDecimal(totalTax["taxCollected"].ToString()) : 0);
                        //Total Tax Collected
                        text = String.Format("{0:N}", _totalTax);
                        graphics.DrawString(text, drawFontArial8Regular, drawBrush, startX + 275, startY + Offset, drawFormatRight);
                    }
                }
                Offset = Offset + extraOffset;
                Offset = Offset + extraOffset;
                graphics.DrawString(underLine, drawFontArial8Regular, drawBrush, 0, startY + Offset);
                Offset = Offset + 3;
                graphics.DrawString(underLine, drawFontArial8Regular, drawBrush, 0, startY + Offset);
                Offset = Offset + extraOffset;
                // Totals Taxable
                decimal totalTaxable = 0;
                foreach (DataRow taxGroup in getTax.Rows)
                {
                    string _taxGroup = taxGroup["TaxGroupCode"].ToString();
                    DataTable getTotalTaxable = piwebDataOps.GetTotalTaxable(_taxGroup, todaysDate);
                    foreach (DataRow _totalTaxable in getTotalTaxable.Rows)
                    {
                        decimal total = Convert.ToDecimal(!string.IsNullOrEmpty(_totalTaxable["totalTaxable"].ToString()) ? Convert.ToDecimal(_totalTaxable["totalTaxable"].ToString()) : 0);
                        totalTaxable += Convert.ToDecimal(total);
                    }
                }
                text = String.Format("{0:N}", totalTaxable);
                e.Graphics.DrawString("Total Taxable: ", drawFontArial8Regular, drawBrush, startX, startY + Offset);
                graphics.DrawString(text.ToUpper(), drawFontArial8Regular, drawBrush, startX + 275, startY + Offset, drawFormatRight);
                Offset = Offset + extraOffset;
                // tax
                decimal cummulativeTax = 0;
                foreach (DataRow taxGroup in getTax.Rows)
                {
                    string _taxGroup = taxGroup["TaxGroupCode"].ToString();
                    DataTable getCummulativeTax = piwebDataOps.GetTotalTax(_taxGroup, todaysDate);
                    foreach (DataRow totalTax in getCummulativeTax.Rows)
                    {
                        decimal cummuTax = Convert.ToDecimal(!string.IsNullOrEmpty(totalTax["cummulativeTax"].ToString()) ? Convert.ToDecimal(totalTax["cummulativeTax"].ToString()) : 0);
                        cummulativeTax += Convert.ToDecimal(cummuTax);
                    }
                }
                text = String.Format("{0:N}", cummulativeTax);
                e.Graphics.DrawString("Tax: ", drawFontArial8Regular, drawBrush, startX, startY + Offset);
                graphics.DrawString(text, drawFontArial8Regular, drawBrush, startX + 275, startY + Offset, drawFormatRight);
                Offset = Offset + extraOffset;
                // Tax Totals
                decimal taxTotal = totalTaxable + cummulativeTax;
                text = String.Format("{0:N}", taxTotal);
                e.Graphics.DrawString("Total: ", drawFontArial8Regular, drawBrush, startX, startY + Offset);
                graphics.DrawString(text, drawFontArial8Regular, new SolidBrush(Color.Black), startX + 275, startY + Offset, drawFormatRight);
                Offset = Offset + extraOffset;
                //DrawLine
                graphics.DrawString(underLine, drawFontArial8Regular, drawBrush, 0, startY + Offset);
                Offset = Offset + extraOffset;
                //  Total Balance
                decimal TotalBalance = cashIn + cashOut;
                text = String.Format("{0:N}", TotalBalance);
                e.Graphics.DrawString("Total Balance: ", drawFontArial8Bold, drawBrush, startX, startY + Offset);
                graphics.DrawString(text, drawFontArial8Bold, drawBrush, startX + 275, startY + Offset, drawFormatRight);
                Offset = Offset + extraOffset;
                graphics.DrawString(underLine, drawFontArial8Regular, drawBrush, 0, startY + Offset);
                Offset = Offset + extraOffset;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {

                //  TODO: Handle Return
                csReturnItems returns = new csReturnItems();
                List<csReturnItems> returnList = new List<csReturnItems>();
                foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
                {
                    //  Get Product Name from dataGrid
                    string itemName = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    DataTable getProducts = piwebDataOps.GetProductsByItemName(itemName);
                    char _returnAllowed = Convert.ToChar(getProducts.Rows[0]["ReturnAllowed"].ToString());
                    if (_returnAllowed == 'N')
                    {
                        MessageBox.Show("Return is not allowed for \"" + itemName + "\"", "Cash Register", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    returns.productCode = getProducts.Rows[0]["No"].ToString();
                    returns.itemName = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    returns.quantity = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                    returns.amount = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
                    returnList.Add(returns);
                }

                frmReturn openReturn = new frmReturn();
                openReturn.InvoiceNo = lblInvNo.Text;
                openReturn.ReturnItems = returnList;
                openReturn.SubTotal = Convert.ToDecimal(lblSubTotal.Text);
                openReturn.TotalTax = Convert.ToDecimal(lblTax.Text);
                openReturn.TotalAmount = Convert.ToDecimal(lblTotalAmount.Text);
                openReturn.TotalDiscount = Convert.ToDecimal(lblDiscount.Text);
                openReturn.ShowDialog();

                //Clear dataGridView
                openReturn.ReturnItems.Clear();
                dataGridView1.Rows.Clear();
                btnDiscount.Controls.Clear();
                PanelCategoryView.Controls.Clear();
                CategoryButton();
                ProductButton();
                lblTotalAmount.Text = "0.00";
                lblTax.Text = "0.00";
                lblDiscount.Text = "0.00";
                lblSubTotal.Text = "0.00";
                lblInvNo.Text = LoadSerials();                
            }
            else
            {
                MessageBox.Show("Please Selecte Item to return", "Cash Register", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
