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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiwebSystemsPOS
{
    public partial class frmBusinessInfo : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        TransactionsHelper transHelper = new TransactionsHelper();

        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        string saveDirectory = @"C:\SavedImages\";

        private static string connString = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private static SqlConnection sqlConn = new SqlConnection(connString);
        private static SqlCommand cmd;
        private static SqlDataAdapter sda;
        private DataTable dt = new DataTable();

        string createdBy = UserSession.userName;
        int _companyID = -1;
        public frmBusinessInfo()
        {
            InitializeComponent();
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
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //
            // Tab 1
            //
            string name = txtName.Text.Trim(),
                address = txtAddress.Text.Trim(),
                address2 = txtAddress2.Text.Trim(),
                location = txtLocation.Text.Trim(),
                country = txtCountry.Text.Trim(),
                phoneNo = txtPhoneNo.Text.Trim(),
                VATNo = txtVATNo.Text.Trim(),
                indClass = txtIndClass.Text.Trim(),
                abbrevNames = txtAbbrevNames.Text.Trim(),
                fileName = "";

            if (openFileDialog1.FileName != "")
            {
                if (openFileDialog1.CheckFileExists)
                {
                    string path = @"C:\SavedImages\"; //Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                    fileName = Path.GetFileName(openFileDialog1.FileName);
                    bool fileExists = File.Exists(path + fileName);

                    if (fileExists == true)
                    {
                        MessageBox.Show("File Already Exists");
                        return;
                    }
                    else
                        File.Copy(openFileDialog1.FileName, path + fileName);
                }
            }


            char showAbbrevNames = 'N';
            if (chkShowAbbrevNames.Checked)
                showAbbrevNames = 'Y';

            //
            // Tab 2
            //
            string mobile = txtMobile.Text.Trim(),
                mobile2 = txtMobile2.Text.Trim(),
                fax = txtFax.Text.Trim(),
                email = txtEmail.Text.Trim(),
                url = txtWebsite.Text.Trim();

            char whatsAppMobile1 = 'N',
                whatsAppMobile2 = 'N';

            if (chkWhatsApp.Checked)
                whatsAppMobile1 = 'Y';
            if (chkMob2WhatsApp.Checked)
                whatsAppMobile2 = 'Y';

            //
            // Tab 3
            //
            string bankName = txtBankName.Text.Trim(),
                branch = txtBranch.Text.Trim(),
                accountName = txtAccountName.Text.Trim(),
                accountNo = txtAccountNo.Text.Trim();

            if (btnSave.Text == "Update")
            {
                //
                //Update Company Info
                //
                piwebDataOps.UpdateCompanyInfo(_companyID, name, address, address2, mobile, mobile2, fax, email, url, location, country, phoneNo, VATNo, indClass, abbrevNames, showAbbrevNames, whatsAppMobile1, whatsAppMobile2, fileName, createdBy);
                piwebDataOps.UpdateBank(_companyID, bankName, branch, accountName, accountNo); //Update Bank Info
                MessageBox.Show("Business Information updated successfully", "Business Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            else
            {
                //
                // Check if Table has Data
                //

                cmd = new SqlCommand("SELECT * FROM [dbo].[SYS_CompanyInfo]", sqlConn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Only One Company is Allowed", "Business Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    //
                    // Save Company Info
                    //
                    piwebDataOps.CreateCompany(name, address, address2, mobile, mobile2, fax, email, url, location, country, phoneNo, VATNo, indClass, abbrevNames, showAbbrevNames, whatsAppMobile1, whatsAppMobile2, fileName, createdBy);
                    piwebDataOps.CreateBank(bankName, branch, accountName, accountNo, _companyID); //Create Bank Info
                    MessageBox.Show("Business Information Saved Successfully", "Business Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            // Reset Form
            //
            // Tab 1
            //
            txtName.Clear();
            txtAddress.Clear();
            txtAddress2.Clear();
            txtLocation.Clear();
            txtCountry.Clear();
            txtPhoneNo.Clear();
            txtVATNo.Clear();
            txtIndClass.Clear();
            txtAbbrevNames.Clear();
            fileName = "";

            showAbbrevNames = 'N';

            //
            // Tab 2
            //
            txtMobile.Clear();
            txtMobile2.Clear();
            txtFax.Clear();
            txtEmail.Clear();
            txtWebsite.Clear();

            //
            // Tab 3
            //
            txtBankName.Clear();
            txtBranch.Clear();
            txtAccountName.Clear();
            txtAccountNo.Clear();


        }

        private void frmBusinessInfo_Load(object sender, EventArgs e)
        {
            // Load Company Info
            //
            DataTable dt = piwebDataOps.GetCompanyInfo();
            //
            // Tab 1
            //
            if (dt.Rows.Count <= 0)
            {
                btnSave.Text = "Save";
                return;
            }
            else
            {

                btnSave.Text = "Update";

                _companyID = Convert.ToInt32(dt.Rows[0]["COMPID"].ToString());

                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtAddress.Text = dt.Rows[0]["AddressLine1"].ToString();
                txtAddress2.Text = dt.Rows[0]["AddressLine2"].ToString();
                txtLocation.Text = dt.Rows[0]["Location"].ToString();
                txtCountry.Text = dt.Rows[0]["Country"].ToString();
                txtPhoneNo.Text = dt.Rows[0]["PhoneNo"].ToString();
                txtVATNo.Text = dt.Rows[0]["VATRegNo"].ToString();
                txtIndClass.Text = dt.Rows[0]["IndustrialClass"].ToString();
                txtAbbrevNames.Text = dt.Rows[0]["AbbreviatedNames"].ToString();
                string fileName = dt.Rows[0]["logoPath"].ToString();

                if (!string.IsNullOrEmpty(fileName))
                    pictureBox1.Image = Image.FromFile(saveDirectory + fileName);

                char showAbbrevNames = Convert.ToChar(dt.Rows[0]["ShowAbbreviatedNames"].ToString());
                if (showAbbrevNames == 'Y')
                    chkShowAbbrevNames.Checked = true;

                //
                // Tab 2
                //
                txtMobile.Text = dt.Rows[0]["Mobile1"].ToString();
                txtMobile2.Text = dt.Rows[0]["Mobile2"].ToString();
                txtFax.Text = dt.Rows[0]["FaxNo"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtWebsite.Text = dt.Rows[0]["Url"].ToString();

                char whatsAppMobile1 = Convert.ToChar(dt.Rows[0]["IsWhatsAppMob1"].ToString()),
                     whatsAppMobile2 = Convert.ToChar(dt.Rows[0]["IsWhatsAppMob2"].ToString());

                if (whatsAppMobile1 == 'Y')
                    chkWhatsApp.Checked = true;
                if (whatsAppMobile2 == 'Y')
                    chkMob2WhatsApp.Checked = true;

                //
                // Tab 3
                //
                txtBankName.Text = dt.Rows[0]["BankName"].ToString();
                txtBranch.Text = dt.Rows[0]["Branch"].ToString();
                txtAccountName.Text = dt.Rows[0]["AccountName"].ToString();
                txtAccountNo.Text = dt.Rows[0]["AccountNo"].ToString();
            }
        }

        private void btnImportLogo_Click(object sender, EventArgs e)
        {
            saveDirectory = @"C:\SavedImages\";

            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png|All Files(*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!Directory.Exists(saveDirectory))
                {
                    Directory.CreateDirectory(saveDirectory);
                }
                //Load Image on PictureBox
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }

        }
        private void btnImportCoverImage_Click(object sender, EventArgs e)
        {
            saveDirectory = @"C:\SavedImages\";

            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png|All Files(*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!Directory.Exists(saveDirectory))
                {
                    Directory.CreateDirectory(saveDirectory);
                }
                //Load Image on PictureBox
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
