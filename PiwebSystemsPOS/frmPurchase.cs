using MetroFramework.Controls;
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
    public partial class frmPurchase : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        TransactionsHelper transHelper = new TransactionsHelper();

        private static string connString = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private static SqlConnection sqlConn = new SqlConnection(connString);
        private static SqlCommand cmd;
        private static SqlDataAdapter sda;
        private static DataTable dt;

        public string LoadSerials()
        {
            var serialNo = "";
            string rec;

            cmd = new SqlCommand("SELECT isnull(MAX([PurchaseInvoiceLineID]),100000) AS 'InvoiceLineNo' FROM [dbo].[PUR_PurchaseInvoiceLines]", sqlConn);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            rec = dt.Rows[0]["InvoiceLineNo"].ToString();

            serialNo = rec;

            return serialNo;
        }
        public frmPurchase()
        {
            InitializeComponent();
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
                char active = Convert.ToChar(dr["Active"].ToString()),
                    purchaseItem = Convert.ToChar(dr["PurchaseProduct"].ToString());

                if (purchaseItem != 'N')
                {
                    if (active == 'Y')
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
                            string path = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
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
                        MetroLabel productName = new MetroLabel();
                        productName.FontWeight = MetroFramework.MetroLabelWeight.Bold;
                        productName.UseCustomBackColor = true;
                        productName.BackColor = Color.Transparent;
                        productName.AutoSize = true;
                        productName.Location = new System.Drawing.Point(3, 90);
                        productName.Name = "btnProductName";
                        productName.Size = new System.Drawing.Size(82, 13);
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
                        PanelProductsView.Controls.Add(productsPanel);
                    }
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
                MetroLabel categoryName = new MetroLabel();
                categoryName.FontWeight = MetroFramework.MetroLabelWeight.Bold;
                categoryName.UseCustomBackColor = true;
                categoryName.BackColor = Color.Transparent;
                categoryName.AutoSize = true;
                categoryName.Location = new System.Drawing.Point(3, 90);
                categoryName.Name = "btnProductName";
                categoryName.Size = new System.Drawing.Size(82, 13);
                categoryName.TabIndex = 2;
                categoryName.Text = _categoryName;
                categoryName.AutoEllipsis = true;
                categoryName.SendToBack();

                //
                // Category Panel
                //
                MetroPanel categoryPanel = new MetroPanel();
                categoryPanel.UseCustomBackColor = true;
                categoryPanel.BackColor = System.Drawing.Color.White;
                categoryPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                categoryPanel.Controls.Add(categoryName);
                categoryPanel.Controls.Add(CategoryImage);
                categoryPanel.HorizontalScrollbarBarColor = true;
                categoryPanel.HorizontalScrollbarHighlightOnWheel = false;
                categoryPanel.HorizontalScrollbarSize = 10;
                categoryPanel.Location = new System.Drawing.Point(3, 3);
                categoryPanel.Name = _categoryCode; //
                categoryPanel.Size = new System.Drawing.Size(140, 130);
                categoryPanel.TabIndex = 2;
                categoryPanel.VerticalScrollbarBarColor = true;
                categoryPanel.VerticalScrollbarHighlightOnWheel = false;
                categoryPanel.VerticalScrollbarSize = 10;
                PanelCategoryView.Controls.Add(categoryPanel);
            }
        }
        private void Category_Click(object sender, EventArgs e)
        {
            //Clear PanelProductsView Controls
            PanelProductsView.Controls.Clear();

            //Initialize object Sender Instance
            PictureBox categoryImage = (PictureBox)sender;
            string categoryName = categoryImage.Name;

            DataTable getProducts = piwebDataOps.GetProducts(categoryName);
            foreach (DataRow dr in getProducts.Rows)
            {
                string _photo = dr["Photo"].ToString(),
                    _no = dr["No."].ToString(),
                    _product = dr["Product Name"].ToString(),
                    _stock = dr["Current Stock"].ToString();
                decimal _price = Convert.ToDecimal(dr["Unit Price"].ToString());
                char active = Convert.ToChar(dr["Active"].ToString()),
                    purchaseItem = Convert.ToChar(dr["Purchase Product"].ToString());

                if (purchaseItem != 'N')
                {
                    if (active == 'Y')
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
                            string path = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
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
                        MetroLabel productName = new MetroLabel();
                        productName.FontWeight = MetroFramework.MetroLabelWeight.Bold;
                        productName.UseCustomBackColor = true;
                        productName.BackColor = Color.Transparent;
                        productName.AutoSize = true;
                        productName.Location = new System.Drawing.Point(3, 90);
                        productName.Name = "btnProductName";
                        productName.Size = new System.Drawing.Size(82, 13);
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
                        PanelProductsView.Controls.Add(productsPanel);
                    }
                }
            }
        }
        private void Product_Click(object sender, EventArgs e)
        {
            PictureBox productImage = (PictureBox)sender;
            string productNo = productImage.Name;

            AddToSalesGridView(productNo);

        }
        private void AddToSalesGridView(string prodNo)
        {
            string x, y;
            decimal _price, _lineAmount;
            int qty;
            decimal ItemTaxRate = 0, taxAmount = 0;

            bool found = false;
            DataTable getProducts = piwebDataOps.GetProducts(prodNo);
            string taxGroupCode = getProducts.Rows[0]["Tax Group Code"].ToString();

            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (getProducts.Rows[0]["Product Name"].ToString() == row.Cells[0].Value.ToString())
                    {
                        row.Cells[1].Value = Convert.ToString(1 + Convert.ToInt32(row.Cells[1].Value.ToString()));

                        //Calculate Line Amount
                        _lineAmount = Convert.ToDecimal(row.Cells[1].Value) * Convert.ToDecimal(getProducts.Rows[0]["Unit Price"].ToString());
                        row.Cells[2].Value = String.Format("{0:N}", _lineAmount);

                        //Get Product Tax
                        if (!string.IsNullOrEmpty(taxGroupCode))
                        {
                            DataTable getTax = piwebDataOps.GetTax(taxGroupCode);
                            ItemTaxRate = Convert.ToDecimal(getTax.Rows[0]["Tax"].ToString());
                            taxAmount = (Convert.ToDecimal(getProducts.Rows[0]["Unit Price"].ToString()) * ItemTaxRate) / 100;
                        }
                        found = true;
                        //qty = );
                    }
                }
                if (!found)
                {
                    _price = Convert.ToDecimal(getProducts.Rows[0]["Unit Price"].ToString());

                    dataGridView1.Rows.Add(getProducts.Rows[0]["Product Name"].ToString(), "1", String.Format("{0:N}", _price));

                    if (!string.IsNullOrEmpty(taxGroupCode))
                    {
                        DataTable getTax = piwebDataOps.GetTax(taxGroupCode);
                        ItemTaxRate = Convert.ToDecimal(getTax.Rows[0]["Tax"].ToString());
                        taxAmount = (Convert.ToDecimal(getProducts.Rows[0]["Unit Price"].ToString()) * ItemTaxRate) / 100;
                    }

                    this.dataGridView1.ClearSelection();
                    this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Selected = true;
                }
            }
            else
            {
                _price = Convert.ToDecimal(getProducts.Rows[0]["Unit Price"].ToString());

                dataGridView1.Rows.Add(getProducts.Rows[0]["Product Name"].ToString(), "1", String.Format("{0:N}", _price));

                if (!string.IsNullOrEmpty(taxGroupCode))
                {
                    DataTable getTax = piwebDataOps.GetTax(taxGroupCode);
                    ItemTaxRate = Convert.ToDecimal(getTax.Rows[0]["Tax"].ToString());
                    taxAmount = (Convert.ToDecimal(getProducts.Rows[0]["Unit Price"].ToString()) * ItemTaxRate) / 100;
                }

                this.dataGridView1.ClearSelection();
                this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Selected = true;
            }

            CalculateDisplayAmounts(taxAmount);


        }
        private void CalculateDisplayAmounts(decimal _itemTax)
        {
            decimal itemTax = 0, tax = 0, discount = 0;
            //int quantity = 0;

            if (_itemTax != 0)
            {
                itemTax = _itemTax;
            }
            //Adding Line Total column figures
            var result = dataGridView1.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells[2].Value));

            decimal subtotal = result; //- Convert.ToDecimal(discount);        //Calculating Sub Total After Discount


            lblSubTotal.Text = String.Format("{0:N}", subtotal);
            lblDiscount.Text = String.Format("{0:N}", Convert.ToDecimal(lblDiscount.Text) + discount);            //SubTotal
            lblTax.Text = String.Format("{0:N}", Convert.ToDecimal(lblTax.Text) + itemTax);                      //Displaying Tax Amount
            lblTotalAmount.Text = String.Format("{0:N}", subtotal + Convert.ToDecimal(lblTax.Text));     //Grand Total
        }

        private void btnCategoriesAll_Click(object sender, EventArgs e)
        {
            PanelProductsView.Controls.Clear();
            ProductButton();
        }

        private void frmPurchase_Load(object sender, EventArgs e)
        {
            CategoryButton();
            ProductButton();
            lblTotalAmount.Text = "0.00";
            lblTax.Text = "0.00";
            lblDiscount.Text = "0.00";

            MessageBox.Show("Purchase Order No: " + LoadSerials() + "\nPurchase Date: " + TransactionsHelper.PurchaseDate.ToString() + "\n" + "Invoice Type: " + TransactionsHelper.PurchaseType + "\n" + "Supplier Name: " + TransactionsHelper.Supplier + "\nReference No: " + TransactionsHelper.ReferenceNo + "\n Received Date: " + TransactionsHelper.ReceivingDate);
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                MessageBox.Show("Please Add Items to Make Payment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string purchaseInvoiceNo = TransactionsHelper.DocumentNo,
                invoiceType = TransactionsHelper.PurchaseType,
                supplierCode = TransactionsHelper.Supplier,
                supplierReferenceNo = TransactionsHelper.ReferenceNo,
                tax1ID = "", tax2ID = "", tax3ID = "", remarks = "",
                createdByDeviceId = System.Environment.MachineName,
                createdBy = "SYSTEM";

                DateTime purchaseDate = TransactionsHelper.PurchaseDate,
                    receivedDate = TransactionsHelper.ReceivingDate;

                decimal salesTotal = Convert.ToDecimal(lblTotalAmount.Text),
                    netPayable = salesTotal,
                    itemTax = 0,
                    tax1 = 0, tax2 = 0, tax3 = 0,
                    taxRate1 = 0, taxRate2 = 0, taxRate3 = 0,
                    itemDiscount = 0, discountOnTotal = 0, adjustment = 0, rounding = 0,
                    totalTaxable = salesTotal,
                    discountRate = 0, cashDiscount = 0;

                char billingAddressSameAsShipping = 'Y';


                #region Create Purchase Invoice Lines
                int lineNo = Convert.ToInt32(LoadSerials());
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    lineNo++;
                    string description = dataGridView1.Rows[i].Cells["description"].Value.ToString(),
                        productCode = "", taxGroupCode = "", discountGroupCode = "", status = "", unitOfMeasure = "";
                    int quantity = Convert.ToInt32(dataGridView1.Rows[i].Cells["qty"].Value);
                    decimal lineTotal = Convert.ToDecimal(dataGridView1.Rows[i].Cells["amount"].Value);

                    char active, includesVAT;

                    //Get Product Code
                    DataTable getProductDetails = piwebDataOps.GetProducts(description, "");
                    productCode = getProductDetails.Rows[0]["No."].ToString();
                    taxGroupCode = getProductDetails.Rows[0]["Tax Group Code"].ToString();
                    discountGroupCode = getProductDetails.Rows[0]["Disc. Group Code"].ToString();
                    unitOfMeasure = getProductDetails.Rows[0]["Unit of Measure Code"].ToString();
                    active = Convert.ToChar(getProductDetails.Rows[0]["Active"].ToString());
                    includesVAT = Convert.ToChar(getProductDetails.Rows[0]["Price Inc. VAT"].ToString());

                    decimal unitPrice = Convert.ToDecimal(getProductDetails.Rows[0]["Unit Price"].ToString());
                    if (active == 'Y')
                        status = "Yes";
                    else
                        status = "No";

                    //Get Product Tax
                    decimal ItemTaxRate = 0, lineTax = 0;
                    if (!string.IsNullOrEmpty(taxGroupCode))
                    {
                        DataTable getTax = piwebDataOps.GetTax(taxGroupCode);
                        ItemTaxRate = Convert.ToDecimal(getTax.Rows[0]["Tax"].ToString());
                        if (ItemTaxRate != 0)
                            lineTax = (unitPrice * ItemTaxRate) / 100;
                    }

                    piwebDataOps.CreatePurchaseInvoiceLines(lineNo.ToString(), purchaseInvoiceNo, productCode, description, unitPrice, quantity, unitOfMeasure, unitPrice, 0, 0, "COMPLETED", taxGroupCode, "", "", lineTax, 0, 0, ItemTaxRate, 0, 0, includesVAT, taxGroupCode, 0, 0, 0, "", "SYSTEM");
                    piwebDataOps.UpdatePurchaseInvoice(purchaseInvoiceNo, "COMPLETED", Convert.ToDecimal(lblSubTotal.Text), itemTax, tax1, 0, 0, 0, Convert.ToDecimal(lblDiscount.Text), Convert.ToDecimal(lblTotalAmount.Text), taxGroupCode, "", "", ItemTaxRate, 0, 0, Convert.ToDecimal(lblTotalAmount.Text), createdByDeviceId, "SYSTEM");
                    //piwebDataOps.CreatePurchaseInvoiceLines(lineNo.ToString(), purchaseInvoiceNo, productCode, description, unitPrice, quantity, unitOfMeasure, unitPrice, 0, 0, "NEW", taxGroupCode, "", "", lineTax, 0, 0,ItemTaxRate, 0, 0, includesVAT, taxGroupCode, 0, 0, 0, "", "SYSTEM");
                    MessageBox.Show("Purchase Invoice Line No: " + lineNo + "\nProduct Code: " + productCode + "\nUnit Price: " + unitPrice + "\nTax Group Code: " + taxGroupCode + "\nTax Rate: " + ItemTaxRate + "\nLine Tax: " + lineTax + "\nDiscount Group Code: " + discountGroupCode + "\nDescription: " + description + "\nActive: " + status + "\nQuantity: " + quantity.ToString() + "\nLine Amount: " + (Convert.ToDecimal(lineTotal + lineTax)).ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
                #endregion
            }
        }

        private void btnAdjustQty_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                MessageBox.Show("Please Add Items to Change Quantity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                decimal ItemTaxRate = 0, taxAmount = 0;

                TransactionsHelper.productName = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                frmChangeQuantity openChangeQty = new frmChangeQuantity();
                openChangeQty.Quantity = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                openChangeQty.ShowDialog();

                //Calculate lineAmount
                DataTable product = piwebDataOps.GetProducts(TransactionsHelper.productName, "");
                decimal unitPrice = Convert.ToDecimal(product.Rows[0]["Unit Price"].ToString());
                string taxGroupCode = product.Rows[0]["Tax Group Code"].ToString();
                MessageBox.Show(TransactionsHelper.productName + "\n Price: " + unitPrice);

                //Update Quantity cell
                dataGridView1.SelectedRows[0].Cells[1].Value = TransactionsHelper.qty;
                dataGridView1.SelectedRows[0].Cells[2].Value = String.Format("{0:N}", Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[1].Value) * unitPrice);

                ////Calculate Tax
                //if (!string.IsNullOrEmpty(taxGroupCode))
                //{
                //    DataTable getTax = piwebDataOps.GetTax(taxGroupCode);
                //    ItemTaxRate = Convert.ToDecimal(getTax.Rows[0]["Tax"].ToString());
                //    taxAmount = (Convert.ToDecimal(product.Rows[0]["Unit Price"].ToString()) * ItemTaxRate) / 100;
                //}

                //CalculateDisplayAmounts(taxAmount);

                TransactionsHelper.productName = "";
                TransactionsHelper.qty = "";
            }
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count <= 0)
            {
                MessageBox.Show("Please add Items to Add Discount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                frmDiscount openDiscount = new frmDiscount();
                openDiscount.SubTotal = lblSubTotal.Text;
                openDiscount.ShowDialog();

                lblDiscount.Text = String.Format("{0:N}", Convert.ToDecimal(TransactionsHelper.discount));
                lblTotalAmount.Text = String.Format("{0:N}", Convert.ToDecimal(lblTotalAmount.Text) - Convert.ToDecimal(TransactionsHelper.discount));
                TransactionsHelper.discount = "";
            }
        }

        private void btnOnHold_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                MessageBox.Show("Please add Items to Put On Hold", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                string purchaseInvoiceNo = TransactionsHelper.DocumentNo,
                        invoiceType = TransactionsHelper.PurchaseType,
                        supplierCode = TransactionsHelper.Supplier,
                        supplierReferenceNo = TransactionsHelper.ReferenceNo,
                        tax1ID = "", tax2ID = "", tax3ID = "", remarks = "",
                        createdByDeviceId = System.Environment.MachineName,
                        createdBy = "SYSTEM";

                DateTime purchaseDate = TransactionsHelper.PurchaseDate,
                    receivedDate = TransactionsHelper.ReceivingDate;

                decimal salesTotal = Convert.ToDecimal(lblTotalAmount.Text),
                    netPayable = salesTotal,
                    itemTax = 0,
                    tax1 = 0, tax2 = 0, tax3 = 0,
                    taxRate1 = 0, taxRate2 = 0, taxRate3 = 0,
                    itemDiscount = 0, discountOnTotal = 0, adjustment = 0, rounding = 0,
                    totalTaxable = salesTotal,
                    discountRate = 0, cashDiscount = 0;

                char billingAddressSameAsShipping = 'Y';

                #region Create Purchase Invoice Lines
                int lineNo = Convert.ToInt32(LoadSerials());
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    lineNo++;
                    string description = dataGridView1.Rows[i].Cells["description"].Value.ToString(),
                        productCode = "", taxGroupCode = "", discountGroupCode = "", status = "", unitOfMeasure = "";
                    int quantity = Convert.ToInt32(dataGridView1.Rows[i].Cells["qty"].Value);
                    decimal lineTotal = Convert.ToDecimal(dataGridView1.Rows[i].Cells["amount"].Value);

                    char active, includesVAT;

                    //Get Product Code
                    DataTable getProductDetails = piwebDataOps.GetProducts(description, "");
                    productCode = getProductDetails.Rows[0]["No."].ToString();
                    taxGroupCode = getProductDetails.Rows[0]["Tax Group Code"].ToString();
                    discountGroupCode = getProductDetails.Rows[0]["Disc. Group Code"].ToString();
                    unitOfMeasure = getProductDetails.Rows[0]["Unit of Measure Code"].ToString();
                    active = Convert.ToChar(getProductDetails.Rows[0]["Active"].ToString());
                    includesVAT = Convert.ToChar(getProductDetails.Rows[0]["Price Inc. VAT"].ToString());

                    decimal unitPrice = Convert.ToDecimal(getProductDetails.Rows[0]["Unit Price"].ToString());
                    if (active == 'Y')
                        status = "Yes";
                    else
                        status = "No";

                    //Get Product Tax
                    decimal ItemTaxRate = 0, lineTax = 0;
                    if (!string.IsNullOrEmpty(taxGroupCode))
                    {
                        DataTable getTax = piwebDataOps.GetTax(taxGroupCode);
                        ItemTaxRate = Convert.ToDecimal(getTax.Rows[0]["Tax"].ToString());
                        if (ItemTaxRate != 0)
                            lineTax = (unitPrice * ItemTaxRate) / 100;
                    }

                    piwebDataOps.CreatePurchaseInvoiceLines(lineNo.ToString(), purchaseInvoiceNo, productCode, description, unitPrice, quantity, unitOfMeasure, unitPrice, 0, 0, "ON HOLD", taxGroupCode, "", "", lineTax, 0, 0, ItemTaxRate, 0, 0, includesVAT, taxGroupCode, 0, 0, 0, "", "SYSTEM");
                    piwebDataOps.UpdatePurchaseInvoice(purchaseInvoiceNo, "ON HOLD", Convert.ToDecimal(lblSubTotal.Text), itemTax, tax1, 0, 0, 0, Convert.ToDecimal(lblDiscount.Text), Convert.ToDecimal(lblTotalAmount.Text), taxGroupCode, "", "", ItemTaxRate, 0, 0, Convert.ToDecimal(lblTotalAmount.Text), createdByDeviceId, "SYSTEM");
                    //MessageBox.Show("Purchase Invoice Line No: " + lineNo + "\nProduct Code: " + productCode + "\nUnit Price: " + unitPrice + "\nTax Group Code: " + taxGroupCode + "\nTax Rate: " + ItemTaxRate + "\nLine Tax: " + lineTax + "\nDiscount Group Code: " + discountGroupCode + "\nDescription: " + description + "\nActive: " + status + "\nQuantity: " + quantity.ToString() + "\nLine Amount: " + (Convert.ToDecimal(lineTotal + lineTax)).ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.Rows.Clear();

                }
                #endregion
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                MessageBox.Show("Can't void without Items", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                string purchaseInvoiceNo = TransactionsHelper.DocumentNo,
                        invoiceType = TransactionsHelper.PurchaseType,
                        supplierCode = TransactionsHelper.Supplier,
                        supplierReferenceNo = TransactionsHelper.ReferenceNo,
                        tax1ID = "", tax2ID = "", tax3ID = "", remarks = "",
                        createdByDeviceId = System.Environment.MachineName,
                        createdBy = "SYSTEM";

                DateTime purchaseDate = TransactionsHelper.PurchaseDate,
                    receivedDate = TransactionsHelper.ReceivingDate;

                decimal salesTotal = Convert.ToDecimal(lblTotalAmount.Text),
                    netPayable = salesTotal,
                    itemTax = 0,
                    tax1 = 0, tax2 = 0, tax3 = 0,
                    taxRate1 = 0, taxRate2 = 0, taxRate3 = 0,
                    itemDiscount = 0, discountOnTotal = 0, adjustment = 0, rounding = 0,
                    totalTaxable = salesTotal,
                    discountRate = 0, cashDiscount = 0;

                char billingAddressSameAsShipping = 'Y';


                #region Create Purchase Invoice Lines
                int lineNo = Convert.ToInt32(LoadSerials());
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    lineNo++;
                    string description = dataGridView1.Rows[i].Cells["description"].Value.ToString(),
                        productCode = "", taxGroupCode = "", discountGroupCode = "", status = "", unitOfMeasure = "";
                    int quantity = Convert.ToInt32(dataGridView1.Rows[i].Cells["qty"].Value);
                    decimal lineTotal = Convert.ToDecimal(dataGridView1.Rows[i].Cells["amount"].Value);

                    char active, includesVAT;

                    //Get Product Code
                    DataTable getProductDetails = piwebDataOps.GetProducts(description, "");
                    productCode = getProductDetails.Rows[0]["No."].ToString();
                    taxGroupCode = getProductDetails.Rows[0]["Tax Group Code"].ToString();
                    discountGroupCode = getProductDetails.Rows[0]["Disc. Group Code"].ToString();
                    unitOfMeasure = getProductDetails.Rows[0]["Unit of Measure Code"].ToString();
                    active = Convert.ToChar(getProductDetails.Rows[0]["Active"].ToString());
                    includesVAT = Convert.ToChar(getProductDetails.Rows[0]["Price Inc. VAT"].ToString());

                    decimal unitPrice = Convert.ToDecimal(getProductDetails.Rows[0]["Unit Price"].ToString());
                    if (active == 'Y')
                        status = "Yes";
                    else
                        status = "No";

                    //Get Product Tax
                    decimal ItemTaxRate = 0, lineTax = 0;
                    if (!string.IsNullOrEmpty(taxGroupCode))
                    {
                        DataTable getTax = piwebDataOps.GetTax(taxGroupCode);
                        ItemTaxRate = Convert.ToDecimal(getTax.Rows[0]["Tax"].ToString());
                        if (ItemTaxRate != 0)
                            lineTax = (unitPrice * ItemTaxRate) / 100;
                    }

                    piwebDataOps.CreatePurchaseInvoiceLines(lineNo.ToString(), purchaseInvoiceNo, productCode, description, unitPrice, quantity, unitOfMeasure, unitPrice, 0, 0, "CANCELLED", taxGroupCode, "", "", lineTax, 0, 0,ItemTaxRate, 0, 0, includesVAT, taxGroupCode,0,0, 0, "", "SYSTEM");
                    piwebDataOps.UpdatePurchaseInvoice(purchaseInvoiceNo, "CANCELLED", Convert.ToDecimal(lblSubTotal.Text), itemTax, tax1, 0, 0, 0, Convert.ToDecimal(lblDiscount.Text), Convert.ToDecimal(lblTotalAmount.Text), taxGroupCode, "", "", ItemTaxRate, 0, 0, Convert.ToDecimal(lblTotalAmount.Text), createdByDeviceId, "SYSTEM");
                    //MessageBox.Show("Purchase Invoice Line No: " + lineNo + "\nProduct Code: " + productCode + "\nUnit Price: " + unitPrice + "\nTax Group Code: " + taxGroupCode + "\nTax Rate: " + ItemTaxRate + "\nLine Tax: " + lineTax + "\nDiscount Group Code: " + discountGroupCode + "\nDescription: " + description + "\nActive: " + status + "\nQuantity: " + quantity.ToString() + "\nLine Amount: " + (Convert.ToDecimal(lineTotal + lineTax)).ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.Rows.Clear();

                }
                #endregion
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
