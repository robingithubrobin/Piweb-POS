using PiwebSystemsPOS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiwebSystemsPOS
{
    public partial class btnRestoreReceiptPrinterSettings : Form
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        public btnRestoreReceiptPrinterSettings()
        {
            InitializeComponent();
        }

        string serverName = System.Environment.MachineName,
            database = "",
            loginName = "",
            password = "";

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnFiscalPrinter_Click(object sender, EventArgs e)
        {
            frmPrinterSettings openFiscalPrinterSettings = new frmPrinterSettings();
            openFiscalPrinterSettings.ShowDialog();
        }

        private void frmSystemConfigurations_Load(object sender, EventArgs e)
        {
            loadCompanyInforamtion();
            loadDefaultConnectionSettings();
            loadDefaultPrinterSettings();

        }

        private void btnSaveConnection_Click(object sender, EventArgs e)
        {
            loginName = txtLogin.Text;
            password = txtPassword.Text;
            database = txtDatabase.Text;
            Properties.Settings.Default.serverName = txtServerPath.Text;
            Properties.Settings.Default.authenticationType = txtAuthType.Text;
            Properties.Settings.Default.databaseName = txtDatabase.Text;
            Properties.Settings.Default.login = txtLogin.Text;
            Properties.Settings.Default.password = txtPassword.Text;
            //Set connection string
            string connectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};", serverName, database, loginName, password);
            try
            {
                csSqlConnHelper helper = new csSqlConnHelper(connectionString);
                if (helper.IsConnection)
                {
                    csSqlConnection settings = new csSqlConnection();
                    settings.SaveConnectionString("sqlconn", connectionString);
                    Properties.Settings.Default.Save();
                    MessageBox.Show("Connection Saved Successfully!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            loginName = txtLogin.Text;
            password = txtPassword.Text;
            database = txtDatabase.Text;
            //Set connection string
            string connectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};", serverName, database, loginName, password);
            try
            {
                csSqlConnHelper helper = new csSqlConnHelper(connectionString);
                if (helper.IsConnection)
                {
                    MessageBox.Show("Successfully Connected.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadCompanyInforamtion()
        {
            string logoDirectory = @"C:\SavedImages\";
            string logoPath, companyName, address1, address2, phone1, altPhone, vatRegNo;
            //  Fetch company info
            DataTable getCompanyInfo = piwebDataOps.GetCompanyInfo();
            logoPath = getCompanyInfo.Rows[0]["logoPath"].ToString();
            companyName = getCompanyInfo.Rows[0]["Name"].ToString();
            address1 = getCompanyInfo.Rows[0]["AddressLine1"].ToString();
            address2 = getCompanyInfo.Rows[0]["AddressLine2"].ToString();
            phone1 = getCompanyInfo.Rows[0]["Mobile1"].ToString();
            altPhone = getCompanyInfo.Rows[0]["Mobile2"].ToString();
            vatRegNo = getCompanyInfo.Rows[0]["VATRegNo"].ToString();
            txtLogo.Text = logoDirectory + logoPath;
            txtHeader1.Text = companyName;
            txtHeader2.Text = address1 + ", " + address2; ;
            txtHeader3.Text = phone1 + ", " + altPhone;
            txtHeader4.Text = vatRegNo;

            txtLogo.Enabled = false;
            txtHeader1.Enabled = false;
            txtHeader2.Enabled = false;
            txtHeader3.Enabled = false;
            txtHeader4.Enabled = false;

            txtFooterLine1.Text = Properties.Settings.Default.footerLine1;
            txtFooterLine2.Text = Properties.Settings.Default.footerLine2;

        }

        private void loadDefaultConnectionSettings()
        {
            txtServerPath.Text = Properties.Settings.Default.serverName;
            txtAuthType.Text = Properties.Settings.Default.authenticationType;
            txtDatabase.Text = Properties.Settings.Default.databaseName;
            txtLogin.Text = Properties.Settings.Default.login;
            txtPassword.Text = Properties.Settings.Default.password;
            txtAuthType.Enabled = false;
        }

        private void loadDefaultPrinterSettings()
        {
            cboPrinterName.Items.Clear();
            cboKitchenPrinter.Items.Clear();
            PrintDocument prtdoc = new PrintDocument();
            string strDefaultPrinter = prtdoc.PrinterSettings.PrinterName;

            foreach (String strPrinter in PrinterSettings.InstalledPrinters)
            {
                cboPrinterName.Items.Add(strPrinter);
                string printerName = Properties.Settings.Default.receiptPrinterName;
                if (!string.IsNullOrEmpty(printerName))
                {
                    cboPrinterName.SelectedIndex = cboPrinterName.Items.IndexOf(printerName);
                }
                else if (strPrinter == strDefaultPrinter)
                {
                    cboPrinterName.SelectedIndex = cboPrinterName.Items.IndexOf(strPrinter);
                }
            }

            foreach (String strPrinter in PrinterSettings.InstalledPrinters)
            {
                cboKitchenPrinter.Items.Add(strPrinter);
                string printerName = Properties.Settings.Default.kitchenPrinterName;
                if (!string.IsNullOrEmpty(printerName))
                {
                    cboKitchenPrinter.SelectedIndex = cboKitchenPrinter.Items.IndexOf(printerName);
                }
                else if (strPrinter == strDefaultPrinter)
                {
                    cboKitchenPrinter.SelectedIndex = cboKitchenPrinter.Items.IndexOf(strPrinter);
                }
            }

            // Receipt Printer Values
            txtFontName.Text = Properties.Settings.Default.receiptPrinterFontFamily;
            txtWidth.Text = Properties.Settings.Default.receiptWidth.ToString();
            txtStartX.Text = Properties.Settings.Default.startX.ToString();
            txtStartY.Text = Properties.Settings.Default.startY.ToString();
            txtOffset.Text = Properties.Settings.Default.offset.ToString();
            txtAddOffset.Text = Properties.Settings.Default.addOffset.ToString();

        }

        private void btnSavePrinterSettings_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.receiptPrinterName = cboPrinterName.SelectedItem.ToString();
                Properties.Settings.Default.receiptPrinterFontFamily = txtFontName.Text;
                Properties.Settings.Default.receiptWidth = Convert.ToInt32(txtWidth.Text);
                Properties.Settings.Default.startX = Convert.ToInt32(txtStartX.Text);
                Properties.Settings.Default.startY = Convert.ToInt32(txtStartY.Text);
                Properties.Settings.Default.offset = Convert.ToInt32(txtOffset.Text);
                Properties.Settings.Default.addOffset = Convert.ToInt32(txtAddOffset.Text);
                Properties.Settings.Default.Save();

                MessageBox.Show("POS Receipt Printer Settings Saved Successfully", "System Configurations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Configurations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnRestorePrinterSettings_Click(object sender, EventArgs e)
        {
            PrintDocument prtdoc = new PrintDocument();
            string strDefaultPrinter = prtdoc.PrinterSettings.PrinterName;
            cboPrinterName.Items.Clear();
            foreach (String strPrinter in PrinterSettings.InstalledPrinters)
            {
                cboPrinterName.Items.Add(strPrinter);
                if (strPrinter == strDefaultPrinter)
                {
                    cboPrinterName.SelectedIndex = cboPrinterName.Items.IndexOf(strPrinter);
                }
            }
            // Receipt Printer Values
            txtFontName.Text = "Courier New";
            txtWidth.Text = "280";
            txtStartX.Text = "10";
            txtStartY.Text = "10";
            txtOffset.Text = "10";
            txtAddOffset.Text = "15";
        }

        private void btnSaveReceiptParameters_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.footerLine1 = txtFooterLine1.Text;
            Properties.Settings.Default.footerLine2 = txtFooterLine2.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("POS Receipt Parameters Saved Successfully", "System Configurations", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lblNoSeries_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmNoSeries openNoSeries = new frmNoSeries();
            openNoSeries.ShowDialog();
        }
    }
}
