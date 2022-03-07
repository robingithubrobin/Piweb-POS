using MetroFramework.Controls;
using PiwebSystemsPOS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiwebSystemsPOS
{
    public partial class frmCreateProduct : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();

        OpenFileDialog openFile = new OpenFileDialog();

        private static string connString = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private static SqlConnection sqlConn = new SqlConnection(connString);
        private static SqlCommand cmd;
        private static SqlDataAdapter sda;
        private static DataTable dt;

        private static string fileName;

        public frmCreateProduct()
        {
            InitializeComponent();
            PanelWorkSpace.Controls.Clear();
            PanelWorkSpace.Controls.Add(lblSelectToView);
        }

        void Wait()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(20);
                //Process Data
            }
        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            #region Show or Hide OnScreenKeyboard
            var keyboard = Process.GetProcessesByName("TabTip");
            if (keyboard.Length == 0)
            {
                csOnScreenKeyboard.ShowOnScreenKeyboard();
            }
            else
            {
                csOnScreenKeyboard.HideOnScreenKeyboard();
            }
            #endregion
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            using (var processForm = new frmProcessing(Wait))
            {
                processForm.ShowDialog(this);
                if (btnSave.Text.Trim() == "Save")
                {
                    if (PanelWorkSpace.Controls.Contains(TabControlCreateProduct))
                    {
                        string no = txtNo.Text.Trim(),
                            GTIN = txtGTIN.Text.Trim(),
                            productName = txtProductName.Text.Trim(),
                            description = txtDescription.Text.Trim(),
                            costPrice = txtUnitCost.Text.Trim(),
                            unitPrice = txtUnitPrice.Text.Trim(),
                            unitPriceExclVAT = txtUnitPriceExclVAT.Text.Trim(),
                            createdBy = txtCreatedBy.Text.Trim(),
                            createdDate = txtCreatedDate.Text.Trim(),
                            categoryCode = "",
                            productType = "",
                            unitOfMeasure = "",
                            parentProduct = "",
                            productBrand = "",
                            taxGroup = "",
                            discountGroup = "",
                            fileName = "";

                        if (string.IsNullOrEmpty(txtProductName.Text))
                        {
                            txtProductName.Focus();
                            return;
                        }
                        if (string.IsNullOrEmpty(cmbCategory.Text))
                        {
                            cmbCategory.Focus();
                            return;
                        }

                        if (!string.IsNullOrEmpty(cmbCategory.Text))
                            categoryCode = cmbCategory.SelectedValue.ToString();
                        if (!string.IsNullOrEmpty(cmbProductType.Text))
                            productType = cmbProductType.SelectedValue.ToString();
                        if (!string.IsNullOrEmpty(cmbUnitOfMeasure.Text))
                            unitOfMeasure = cmbUnitOfMeasure.SelectedValue.ToString();
                        if (!string.IsNullOrEmpty(cmbParentProduct.Text))
                            parentProduct = cmbParentProduct.SelectedValue.ToString();
                        if (!string.IsNullOrEmpty(cmbProductBrand.Text))
                            productBrand = cmbProductBrand.SelectedValue.ToString();
                        if (!string.IsNullOrEmpty(cmbTaxGroup.Text))
                            taxGroup = cmbTaxGroup.Text;
                        if (!string.IsNullOrEmpty(cmbDiscGroup.Text))
                            discountGroup = cmbDiscGroup.SelectedValue.ToString();


                        char returnAllowed = 'N',
                            active = 'N',
                            salesProduct = 'N',
                            purchaseProduct = 'N',
                            AllowNegativeStock = 'N',
                            vatIncluded = 'N';

                        if (chkReturnAllowed.Checked == true)
                            returnAllowed = 'Y';
                        if (chkActive.Checked == true)
                            active = 'Y';
                        if (chkSalesProduct.Checked == true)
                            salesProduct = 'Y';
                        if (chkPurchaseProduct.Checked == true)
                            purchaseProduct = 'Y';
                        if (chkAllowNegStock.Checked == true)
                            AllowNegativeStock = 'Y';
                        if (chkPriceInclVAT.Checked == true)
                            vatIncluded = 'Y';

                        if (openFile.FileName != "")
                        {
                            if (openFile.CheckFileExists)
                            {
                                string _directoryName = "Images";
                                if (!Directory.Exists(_directoryName))
                                {
                                    Directory.CreateDirectory(_directoryName);
                                }
                                string path = Application.StartupPath; //.Substring(0, Application.StartupPath.Length - 10);
                                fileName = Path.GetFileName(openFile.FileName);
                                bool fileExists = File.Exists(path + "\\Images\\" + fileName);

                                if (fileExists == true)
                                {
                                    MessageBox.Show("File Already Exists");
                                    return;
                                }
                                else
                                    File.Copy(openFile.FileName, path + "\\Images\\" + fileName);
                            }
                        }

                        if (fileName == "")
                        {
                            piwebDataOps.CreateProduct(no, GTIN, productName, description, categoryCode, unitOfMeasure, parentProduct, productBrand, Convert.ToDecimal(costPrice), Convert.ToDecimal(unitPrice), Convert.ToDecimal(unitPriceExclVAT), vatIncluded, taxGroup, discountGroup, salesProduct, active, purchaseProduct, returnAllowed, AllowNegativeStock, createdBy);
                        }
                        else
                        {
                            piwebDataOps.CreateProduct(no, GTIN, productName, description, categoryCode, unitOfMeasure, parentProduct, productBrand, Convert.ToDecimal(costPrice), Convert.ToDecimal(unitPrice), Convert.ToDecimal(unitPriceExclVAT), vatIncluded, taxGroup, discountGroup, salesProduct, active, purchaseProduct, returnAllowed, AllowNegativeStock, fileName, createdBy);
                        }

                        //piwebDataOps.CreateProduct(no, GTIN, productName, description, categoryCode, unitOfMeasure, parentProduct, productBrand, Convert.ToDecimal(costPrice), Convert.ToDecimal(unitPrice), Convert.ToDecimal(unitPriceExclVAT), vatIncluded, taxGroup, discountGroup, salesProduct, active, purchaseProduct, returnAllowed, AllowNegativeStock, fileName, createdBy);
                        MessageBox.Show("Product Created Successfully","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);

                        //Load Serials
                        LoadSerials();

                        //Clear Controls on FlowLayoutPanel
                        flowLayoutPanel1.Controls.Clear();

                        //Enable FlowLayoutPanel
                        flowLayoutPanel1.Enabled = true;

                        //Load Product to FlowLayoutPanel
                        ProductButton();

                        fileName = "";
                    }
                }
                else if (btnSave.Text.Trim() == "Save Changes")
                {
                    if (PanelWorkSpace.Controls.Contains(TabControlCreateProduct))
                    {
                        string no = txtNo.Text.Trim(),
                            GTIN = txtGTIN.Text.Trim(),
                            productName = txtProductName.Text.Trim(),
                            description = txtDescription.Text.Trim(),
                            costPrice = txtUnitCost.Text.Trim(),
                            unitPrice = txtUnitPrice.Text.Trim(),
                            unitPriceExclVAT = txtUnitPriceExclVAT.Text.Trim(),
                            createdBy = txtCreatedBy.Text.Trim(),
                            createdDate = txtCreatedDate.Text.Trim(),
                            categoryCode = "",
                            productType = "",
                            unitOfMeasure = "",
                            parentProduct = "",
                            productBrand = "",
                            taxGroup = "",
                            discountGroup = "",
                            fileName = "";

                        if (string.IsNullOrEmpty(txtProductName.Text))
                        {
                            txtProductName.Focus();
                            return;
                        }
                        if (string.IsNullOrEmpty(cmbCategory.Text))
                        {
                            cmbCategory.Focus();
                            return;
                        }

                        if (!string.IsNullOrEmpty(cmbCategory.Text))
                            categoryCode = cmbCategory.SelectedValue.ToString();
                        if (!string.IsNullOrEmpty(cmbProductType.Text))
                            productType = cmbProductType.SelectedValue.ToString();
                        if (!string.IsNullOrEmpty(cmbUnitOfMeasure.Text))
                            unitOfMeasure = cmbUnitOfMeasure.SelectedValue.ToString();
                        if (!string.IsNullOrEmpty(cmbParentProduct.Text))
                            parentProduct = cmbParentProduct.SelectedValue.ToString();
                        if (!string.IsNullOrEmpty(cmbProductBrand.Text))
                            productBrand = cmbProductBrand.SelectedValue.ToString();
                        if (!string.IsNullOrEmpty(cmbTaxGroup.Text))
                            taxGroup = cmbTaxGroup.SelectedValue.ToString();
                        if (!string.IsNullOrEmpty(cmbDiscGroup.Text))
                            discountGroup = cmbDiscGroup.SelectedValue.ToString();


                        char returnAllowed = 'N',
                            active = 'N',
                            salesProduct = 'N',
                            purchaseProduct = 'N',
                            AllowNegativeStock = 'N',
                            vatIncluded = 'N';

                        if (chkReturnAllowed.Checked == true)
                            returnAllowed = 'Y';
                        if (chkActive.Checked == true)
                            active = 'Y';
                        if (chkSalesProduct.Checked == true)
                            salesProduct = 'Y';
                        if (chkPurchaseProduct.Checked == true)
                            purchaseProduct = 'Y';
                        if (chkAllowNegStock.Checked == true)
                            AllowNegativeStock = 'Y';
                        if (chkPriceInclVAT.Checked == true)
                            vatIncluded = 'Y';

                        if (openFile.FileName != "")
                        {
                            if (openFile.CheckFileExists)
                            {
                                string path = Application.StartupPath; //.Substring(0, Application.StartupPath.Length - 10);
                                fileName = Path.GetFileName(openFile.FileName);
                                bool fileExists = File.Exists(path + "\\Images\\" + fileName);

                                if (fileExists == true)
                                {
                                    MessageBox.Show("File Already Exists");
                                    return;
                                }
                                else
                                    File.Copy(openFile.FileName, path + "\\Images\\" + fileName);
                            }
                        }

                        if (fileName == "")
                        {
                            piwebDataOps.UpdateProduct(no, GTIN, productName, description, categoryCode, unitOfMeasure, parentProduct, productBrand, Convert.ToDecimal(costPrice), Convert.ToDecimal(unitPrice), Convert.ToDecimal(unitPriceExclVAT), vatIncluded, taxGroup, discountGroup, salesProduct, active, purchaseProduct, returnAllowed, AllowNegativeStock, createdBy);
                        }
                        else
                            piwebDataOps.UpdateProduct(no, GTIN, productName, description, categoryCode, unitOfMeasure, parentProduct, productBrand, Convert.ToDecimal(costPrice), Convert.ToDecimal(unitPrice), Convert.ToDecimal(unitPriceExclVAT), vatIncluded, taxGroup, discountGroup, salesProduct, active, purchaseProduct, returnAllowed, AllowNegativeStock, fileName, createdBy);

                        MessageBox.Show("Prooduct Updated Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Load Serials
                        LoadSerials();

                        //Clear Controls on FlowLayoutPanel
                        flowLayoutPanel1.Controls.Clear();

                        //Enable FlowLayoutPanel
                        flowLayoutPanel1.Enabled = true;

                        //Load Product to FlowLayoutPanel
                        ProductButton();

                        fileName = "";
                    }
                }
                else //if(btnSave.Text == "Edit")
                {
                    fileName = "";
                    if (PanelWorkSpace.Controls.Contains(ucProductCard.instance))
                    {
                        string productName = ucProductCard.instance._prodName;
                        DataTable getProducts = piwebDataOps.GetProducts(productName, "");
                        foreach (DataRow dr in getProducts.Rows)
                        {
                            decimal costPrice = Convert.ToDecimal(dr["UnitCost"].ToString()),
                                unitPrice = Convert.ToDecimal(dr["UnitPrice"].ToString()),
                                unitPriceExclVAT = Convert.ToDecimal(dr["UnitPriceExclVAT"].ToString());

                            txtNo.Text = dr["No"].ToString();
                            txtGTIN.Text = dr["GTIN"].ToString();
                            txtProductName.Text = dr["ProductName"].ToString();
                            txtDescription.Text = "";
                            txtUnitCost.Text = String.Format("{0:N}", costPrice);
                            txtUnitPrice.Text = String.Format("{0:N}", unitPrice);
                            txtUnitPriceExclVAT.Text = String.Format("{0:N}", unitPriceExclVAT);
                            txtCreatedBy.Text = "";
                            txtCreatedDate.Text = "";
                            cmbCategory.Text = "";
                            cmbProductType.Text = "";
                            cmbUnitOfMeasure.Text = "";
                            cmbParentProduct.Text = "";
                            cmbProductBrand.Text = "";
                            cmbTaxGroup.Text = "";
                            cmbDiscGroup.Text = "";
                            fileName = dr["Photo"].ToString();

                            if (fileName != "")
                            {
                                string path = Application.StartupPath; //.Substring(0, Application.StartupPath.Length - 10);
                                pictureBox1.Image = Image.FromFile(path + "\\Images\\" + fileName);
                            }
                            else
                            {
                                pictureBox1.Image = global::PiwebSystemsPOS.Properties.Resources.icon;
                            }

                        }
                        PanelWorkSpace.Controls.Add(TabControlCreateProduct);
                        TabControlCreateProduct.Dock = DockStyle.Fill;
                        TabControlCreateProduct.BringToFront();

                        btnSave.Text = "  Save Changes";
                        btnSave.Image = global::PiwebSystemsPOS.Properties.Resources.save24x24;

                        ////Clear Contents

                        ////Load Serials
                        //LoadSerials();

                        //Clear Controls on FlowLayoutPanel
                        flowLayoutPanel1.Controls.Clear();

                        //Disable FlowLayoutPanel
                        flowLayoutPanel1.Enabled = false;

                        //Load Product to FlowLayoutPanel
                        ProductButton();
                    }
                }
            }
        }
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
        private void frmCreateProduct_Load(object sender, EventArgs e)
        {
            //metroTabControl1.Visible = false;
            txtNo.Text = LoadSerials();
            //txtCreatedBy.Text = "SYSTEM";
            //txtCreatedDate.Text = DateTime.Now.ToString();
            FillCategory();
            FillUnitOfMeasure();
            FillParentProduct();
            FillProductType();
            FillProductBrand();
            FillTaxGroupCode();

            ProductButton();
        }

        private void lnkClear_Click(object sender, EventArgs e)
        {
            openFile.Title = "";
            openFile.FileName = "";
            this.pictureBox1.Image = global::PiwebSystemsPOS.Properties.Resources.icon;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openFile.InitialDirectory = "C:\\";
            openFile.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png|All Files(*.*)|*.*";
            openFile.FilterIndex = 1;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFile.FileName);
            }
        }
        private void ProductButton()
        {
            DataTable getProducts = piwebDataOps.GetProducts();
            foreach (DataRow dr in getProducts.Rows)
            {
                string _photo = dr["Photo"].ToString(),
                    _no = dr["No"].ToString(),
                    _product = dr["ProductName"].ToString(),
                    _stock = dr["CurrentStock"].ToString();
                decimal _price = Convert.ToDecimal(dr["UnitPrice"].ToString());

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
                    string path = Application.StartupPath;//.Substring(0, Application.StartupPath.Length - 10);
                    productImage.Image = Image.FromFile(path + "\\Images\\" + _photo);
                }
                productImage.Location = new System.Drawing.Point(3, 0);
                productImage.Name = "productImage";
                productImage.Size = new System.Drawing.Size(60, 80);
                productImage.TabIndex = 2;
                productImage.TabStop = false;

                //
                // Product Name Label
                //
                MetroLabel productName = new MetroLabel();
                productName.FontWeight = MetroFramework.MetroLabelWeight.Bold;
                productName.UseCustomBackColor = true;
                productName.BackColor = Color.Transparent;
                productName.AutoSize = true;
                productName.Location = new System.Drawing.Point(76, 12);
                productName.Name = "btnProductName";
                productName.Size = new System.Drawing.Size(90, 13);
                productName.TabIndex = 2;
                productName.Text = _product;
                productName.SendToBack();

                //
                // Unit Price Label
                //
                MetroLabel unitPrice = new MetroLabel();
                unitPrice.FontWeight = MetroFramework.MetroLabelWeight.Regular;
                unitPrice.UseCustomBackColor = true;
                unitPrice.BackColor = Color.Transparent;
                unitPrice.AutoSize = true;
                unitPrice.Location = new System.Drawing.Point(76, 45);
                unitPrice.Name = "btnUnitPrice";
                unitPrice.Size = new System.Drawing.Size(90, 19);
                unitPrice.TabIndex = 2;
                unitPrice.Text = "Sales Price: " + String.Format("{0:N}", _price);
                unitPrice.SendToBack();

                //
                // Quantity Label
                //
                MetroLabel lblQuantity = new MetroLabel();
                lblQuantity.FontWeight = MetroFramework.MetroLabelWeight.Regular;
                lblQuantity.UseCustomBackColor = true;
                lblQuantity.BackColor = Color.Transparent;
                lblQuantity.AutoSize = true;
                lblQuantity.Location = new System.Drawing.Point(76, 74);
                lblQuantity.Name = "lblQty";
                lblQuantity.Size = new System.Drawing.Size(90, 19);
                lblQuantity.TabIndex = 2;
                lblQuantity.Text = "Quantity: " + _stock;
                lblQuantity.SendToBack();

                //
                // Product Panel
                //
                MetroPanel productsPanel = new MetroPanel();
                productsPanel.UseCustomBackColor = true;
                productsPanel.BackColor = System.Drawing.Color.Transparent;
                productsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                productsPanel.Controls.Add(productName);
                productsPanel.Controls.Add(unitPrice);
                productsPanel.Controls.Add(lblQuantity);
                productsPanel.Controls.Add(productImage);
                productsPanel.HorizontalScrollbarBarColor = true;
                productsPanel.HorizontalScrollbarHighlightOnWheel = false;
                productsPanel.HorizontalScrollbarSize = 10;
                productsPanel.Location = new System.Drawing.Point(3, 3);
                productsPanel.Name = _no; //
                productsPanel.Size = new System.Drawing.Size(228, 104);
                productsPanel.TabIndex = 2;
                productsPanel.VerticalScrollbarBarColor = true;
                productsPanel.VerticalScrollbarHighlightOnWheel = false;
                productsPanel.VerticalScrollbarSize = 10;
                productsPanel.MouseLeave += new System.EventHandler(this.metroPanel3_MouseLeave);
                productsPanel.MouseHover += new System.EventHandler(this.metroPanel3_MouseHover);
                productsPanel.Click += new System.EventHandler(Product_Click);
                flowLayoutPanel1.Controls.Add(productsPanel);
            }

        }
        private void Product_Click(object sender, EventArgs e)
        {
            MetroPanel panel = (MetroPanel)sender;
            panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            panel.UseCustomBackColor = true;
            panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));

            PanelWorkSpace.Controls.Clear(); //Clear PanelWorkSpace 

            if (!PanelWorkSpace.Controls.Contains(ucProductCard.instance))
            {
                PanelWorkSpace.Controls.Add(ucProductCard.instance);
                ucProductCard.instance.Dock = DockStyle.Fill;
                ucProductCard.instance.BringToFront();

                DataTable getproducts = piwebDataOps.GetProducts(panel.Name);
                foreach (DataRow dr in getproducts.Rows)
                {
                    decimal unitPrice = Convert.ToDecimal(dr["UnitPrice"].ToString());
                    ucProductCard.instance._prodName = dr["ProductName"].ToString();
                    ucProductCard.instance._prodDescription = dr["Description"].ToString();
                    ucProductCard.instance._unitPrice = String.Format("{0:N}", unitPrice);
                    ucProductCard.instance._photo = dr["Photo"].ToString();
                    ucProductCard.instance._quantity = dr["CurrentStock"].ToString();
                    ucProductCard.instance._category = dr["Category"].ToString();
                    ucProductCard.instance._uom = dr["UnitOfMeasure"].ToString();
                }
            }
            //else if (PanelWorkSpace.Controls.Contains(ucProductCard.instance))
            //{
            //    PanelWorkSpace.Controls.Add(ucProductCard.instance);
            //    ucProductCard.instance.Dock = DockStyle.Fill;
            //    ucProductCard.instance.BringToFront();

            //    DataTable getproducts = piwebDataOps.GetProducts(panel.Name);
            //    foreach (DataRow dr in getproducts.Rows)
            //    {
            //        decimal unitPrice = Convert.ToDecimal(dr["Unit Price"].ToString());
            //        ucProductCard.instance._prodName = dr["Product Name"].ToString();
            //        ucProductCard.instance._prodDescription = dr["Description"].ToString();
            //        ucProductCard.instance._unitPrice = String.Format("{0:N}", unitPrice);
            //        ucProductCard.instance._photo = dr["Photo"].ToString();
            //        ucProductCard.instance._quantity = dr["Current Stock"].ToString();
            //        ucProductCard.instance._category = dr["Category"].ToString();
            //        ucProductCard.instance._uom = dr["Unit of Measure"].ToString();

            //    }
            //}

            btnSave.Enabled = true;
            btnSave.Text = "  Edit";
            btnSave.Image = global::PiwebSystemsPOS.Properties.Resources.files_Edit_File_icon;
        }
        private void metroPanel3_MouseHover(object sender, EventArgs e)
        {
            MetroPanel panel = new MetroPanel();
            panel.UseCustomBackColor = true;
            panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
        }

        private void metroPanel3_MouseLeave(object sender, EventArgs e)
        {
            MetroPanel panel = (MetroPanel)sender;
            panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel.BackColor = System.Drawing.Color.Transparent;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtNo.Text = LoadSerials();
            fileName = "";
            pictureBox1.Image = global::PiwebSystemsPOS.Properties.Resources.icon;
            btnSave.Text = "  Save";
            btnSave.Image = global::PiwebSystemsPOS.Properties.Resources.save24x24;
            using (var processForm = new frmProcessing(Wait))
            {
                processForm.ShowDialog(this);

                PanelWorkSpace.Controls.Clear();
                if (!PanelWorkSpace.Controls.Contains(TabControlCreateProduct))
                {
                    PanelWorkSpace.Controls.Add(TabControlCreateProduct);
                    TabControlCreateProduct.Dock = DockStyle.Fill;
                    TabControlCreateProduct.BringToFront();

                    flowLayoutPanel1.Enabled = false;

                    btnSave.Enabled = true;
                    btnSave.Image = global::PiwebSystemsPOS.Properties.Resources.save24x24;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            PanelWorkSpace.Controls.Clear();
            flowLayoutPanel1.Enabled = true;
            btnSave.Enabled = false;
            PanelWorkSpace.Controls.Add(lblSelectToView);
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            frmProductsList openProdList = new frmProductsList();
            openProdList.ShowDialog();
        }

        #region Fill Category
        private void FillCategory()
        {
            DataRow dr;
            string _query = "SELECT [CategoryCode],[Description] FROM [dbo].[INV_ProductCategory]";
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
                cmbCategory.ValueMember = "CategoryCode";
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
        #endregion

        #region Fill Tax Group
        private void FillTaxGroupCode()
        {
            DataRow dr;
            string _query = "SELECT [TaxGroupCode],[Description] FROM [dbo].[COM_TaxGroup] ORDER BY [TaxGroupCode] ASC";
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
                cmbTaxGroup.DisplayMember = "TaxGroupCode";
                cmbTaxGroup.ValueMember = "TaxGroupCode";
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
        #endregion

        #region Fill Product Brand
        private void FillProductBrand()
        {
            DataRow dr;
            string _query = "SELECT [BrandCode],[Brand] FROM [dbo].[INV_ProductBrand] ORDER BY [Brand] ASC";
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
                cmbProductBrand.ValueMember = "BrandCode";
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
        #endregion

        #region Fill Product Type
        private void FillProductType()
        {
            string _query = "SELECT [ProductTypeCode],[ProductType] FROM [dbo].[INV_ProductType] ORDER BY [ProductType] ASC";
            try
            {
                cmd = sqlConn.CreateCommand();
                cmd.CommandText = _query;

                sqlConn.Open();

                dt = new DataTable();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                cmbProductType.DisplayMember = "ProductType";
                cmbProductType.ValueMember = "ProductTypeCode";
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
        #endregion

        #region Fill Parent Product
        private void FillParentProduct()
        {
            DataRow dr;
            string _query = "SELECT [No],[ProductName] FROM [dbo].[INV_Product]";
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
                cmbParentProduct.DisplayMember = "ProductName";
                cmbParentProduct.ValueMember = "No";
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
        #endregion

        #region Fill Unit Of Measure
        private void FillUnitOfMeasure()
        {
            DataRow dr;
            string _query = "SELECT [UnitOfMeasureCode],[Description] FROM [dbo].[INV_UnitOfMeasure]";
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
                cmbUnitOfMeasure.ValueMember = "UnitOfMeasureCode";
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
        #endregion
    }
}
