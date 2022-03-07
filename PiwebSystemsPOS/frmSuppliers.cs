using MetroFramework.Controls;
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

namespace PiwebSystemsPOS
{
    public partial class frmSuppliers : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        private OpenFileDialog openFile = new OpenFileDialog();

        private static string connString = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private static SqlConnection sqlConn = new SqlConnection(connString);
        private static SqlCommand cmd;
        private static SqlDataAdapter sda;
        private static DataTable dt;

        public frmSuppliers()
        {
            InitializeComponent();
        }
        public string LoadSerials()
        {
            var serialNo = "";

            cmd = new SqlCommand("(SELECT isnull(MAX([SupplierCode]),10000) + 1 AS 'Serial' FROM [dbo].[CRM_Suppliers])", sqlConn);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            serialNo = dt.Rows[0]["Serial"].ToString();

            return serialNo;
        }

        private void frmSuppliers_Load(object sender, EventArgs e)
        {
            PanelView.Controls.Clear();
            SupplierButton();
            chkActive.Checked = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string supplierCode = txtSupplierCode.Text.Trim(),
                supplierName = txtSupplierName.Text.Trim(),
                address = txtAddress.Text.Trim(),
                email = txtEmail.Text.Trim(),
                url = txtWebsite.Text.Trim(),
                phone = txtMobile.Text.Trim(),
                altPhone = txtAltMobile.Text.Trim(),
                city = txtCity.Text.Trim(),
                vatNo = txtVATNo.Text.Trim(),
                photo = "";

            char active = 'N';

            if (chkActive.Checked == true)
                active = 'Y';


            if (string.IsNullOrEmpty(txtSupplierName.Text))
            {
                MessageBox.Show("Customer Name is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSupplierName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Email is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtMobile.Text))
            {
                MessageBox.Show("Mobile No. is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMobile.Focus();
                return;
            }
            if (openFile.FileName != "")
            {
                if (openFile.CheckFileExists)
                {
                    string path = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                    photo = Path.GetFileName(openFile.FileName);
                    bool fileExists = File.Exists(path + "\\Images\\" + photo);

                    if (fileExists == true)
                    {
                        MessageBox.Show("File Already Exists");
                        return;
                    }
                    else
                        File.Copy(openFile.FileName, path + "\\Images\\" + photo);
                }
            }

            if (photo == "")
                //Create Supplier Without Photo
                piwebDataOps.CreateSuppier(supplierCode,supplierName,address,email,city,phone,altPhone,vatNo,url,active,"SYSTEM");
            else
                //Create Supplier With Photo
                piwebDataOps.CreateSuppier(supplierCode, supplierName, address, email, city, phone, altPhone, vatNo,photo, url, active, "SYSTEM");

            MessageBox.Show("Supplier Created Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Clear Customers Panel
            flowLayoutPanel1.Controls.Clear();

            //Re-load Customers in the panel
            SupplierButton();

            //Re-load Serial
            txtSupplierCode.Text = LoadSerials();
        }

        private void SupplierButton()
        {
            DataTable getSuppliers = piwebDataOps.GetSuppliers();
            foreach (DataRow dr in getSuppliers.Rows)
            {
                string _photo = dr["Photo"].ToString(),
                    _no = dr["SupplierCode"].ToString(),
                    _supplier = dr["Name"].ToString(),
                _phone = dr["PhoneNo"].ToString();

                //
                // Product Image
                //
                PictureBox customerImage = new PictureBox();
                customerImage.BackColor = System.Drawing.Color.Transparent;
                customerImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;


                //check if product has image
                if (string.IsNullOrEmpty(_photo))
                {
                    customerImage.Image = global::PiwebSystemsPOS.Properties.Resources.icon;
                }
                else
                {
                    //productImage.Image = global::PiwebSystemsPOS.Properties.Resources.icon;
                    string path = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                    customerImage.Image = Image.FromFile(path + "\\Images\\" + _photo);
                }
                customerImage.Location = new System.Drawing.Point(3, 0);
                customerImage.Name = "productImage";
                customerImage.Size = new System.Drawing.Size(60, 80);
                customerImage.TabIndex = 2;
                customerImage.TabStop = false;

                //
                // Product Name Label
                //
                MetroLabel customerName = new MetroLabel();
                customerName.FontWeight = MetroFramework.MetroLabelWeight.Bold;
                customerName.UseCustomBackColor = true;
                customerName.BackColor = Color.Transparent;
                customerName.AutoSize = true;
                customerName.Location = new System.Drawing.Point(76, 12);
                customerName.Name = "btnProductName";
                customerName.Size = new System.Drawing.Size(90, 13);
                customerName.TabIndex = 2;
                customerName.Text = _supplier;
                customerName.SendToBack();

                //
                // Unit Price Label
                //
                MetroLabel unitPrice = new MetroLabel();
                unitPrice.FontSize = MetroFramework.MetroLabelSize.Small;
                unitPrice.FontWeight = MetroFramework.MetroLabelWeight.Regular;
                unitPrice.UseCustomBackColor = true;
                unitPrice.BackColor = Color.Transparent;
                unitPrice.AutoSize = true;
                unitPrice.Location = new System.Drawing.Point(76, 45);
                unitPrice.Name = "btnUnitPrice";
                unitPrice.Size = new System.Drawing.Size(90, 19);
                unitPrice.TabIndex = 2;
                unitPrice.Text = "Phone : " + _phone;
                unitPrice.SendToBack();

                //
                // Product Panel
                //
                MetroPanel productsPanel = new MetroPanel();
                productsPanel.UseCustomBackColor = true;
                productsPanel.BackColor = System.Drawing.Color.Transparent;
                productsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                productsPanel.Controls.Add(customerName);
                productsPanel.Controls.Add(unitPrice);
                productsPanel.Controls.Add(customerImage);
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
                //productsPanel.MouseLeave += new System.EventHandler(this.metroPanel3_MouseLeave);
                //productsPanel.MouseHover += new System.EventHandler(this.metroPanel3_MouseHover);
                //productsPanel.Click += new System.EventHandler(Product_Click);
                flowLayoutPanel1.Controls.Add(productsPanel);
            }

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!PanelView.Controls.Contains(PanelCreateSupplier))
            {
                PanelView.Controls.Clear();
                PanelView.Controls.Add(PanelCreateSupplier);
                PanelCreateSupplier.BringToFront();
                txtSupplierCode.Text = LoadSerials();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            PanelView.Controls.Clear();
        }

    }
}