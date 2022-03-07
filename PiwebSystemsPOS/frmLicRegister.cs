using FoxLearn.License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiwebSystemsPOS
{
    public partial class frmLicRegister : MetroFramework.Forms.MetroForm
    {
        public frmLicRegister()
        {
            InitializeComponent();
        }

        const int productCode = 1;
        private void frmLicRegister_Load(object sender, EventArgs e)
        {
            txtProductID.Text = ComputerInfo.GetComputerId();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KeyManager km = new KeyManager(txtProductID.Text);
            string productKey = txtProductKey.Text;
            if (km.ValidKey(ref productKey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                if (km.DisassembleKey(productKey, ref kv))
                {
                    LicenseInfo lic = new LicenseInfo();
                    lic.ProductKey = productKey;
                    lic.FullName = "Piweb Systems POS";
                    if (kv.Type == LicenseType.TRIAL)
                    {
                        lic.Day = kv.Expiration.Day;
                        lic.Month = kv.Expiration.Month;
                        lic.Year = kv.Expiration.Year;
                    }
                    km.SaveSuretyFile(string.Format(@"{0}\key.lic", Application.StartupPath), lic);
                    MessageBox.Show("License Activation Complete","License Activation",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("License Key is Invalid", "License Activation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUserKeyboard_Click(object sender, EventArgs e)
        {

        }
    }
}
