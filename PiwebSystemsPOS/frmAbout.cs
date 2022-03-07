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
    public partial class frmAbout : MetroFramework.Forms.MetroForm
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

        }
        const int productCode = 1;
        private void frmAbout_Load(object sender, EventArgs e)
        {
            lblProductID.Text = ComputerInfo.GetComputerId();
            KeyManager km = new KeyManager(lblProductID.Text);
            LicenseInfo lic = new LicenseInfo();
            int value = km.LoadSuretyFile(string.Format(@"{0}\key.lic", Application.StartupPath), ref lic);
            string productKey = lic.ProductKey;
            if (km.ValidKey(ref productKey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                if (km.DisassembleKey(productKey, ref kv))
                {
                    lblProductName.Text = lic.FullName;
                    lblProductKey.Text = productKey;
                    if (kv.Type == LicenseType.TRIAL)
                        lblLicenseType.Text = string.Format(@"{0} Days", (kv.Expiration - DateTime.Now).Days);
                    else
                        lblLicenseType.Text = "Full";
                }
            }
        }
    }
}
