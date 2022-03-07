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
    public partial class frmLicGenerate : MetroFramework.Forms.MetroForm
    {
        public frmLicGenerate()
        {
            InitializeComponent();
        }
        const int productCode = 1;
        private void btnGenerateKey_Click(object sender, EventArgs e)
        {
            KeyManager km = new KeyManager(txtProductID.Text.Trim());
            KeyValuesClass kv;
            string productKey = string.Empty;
            if (cmbLicType.SelectedIndex == 0)
            {
                kv = new KeyValuesClass()
                {
                    Type = LicenseType.FULL,
                    Header = Convert.ToByte(9),
                    Footer = Convert.ToByte(6),
                    ProductCode = (byte)productCode,
                    Edition = Edition.ENTERPRISE,
                    Version = 1

                };
                if (!km.GenerateKey(kv, ref productKey))
                    txtProductKey.Text = "Error";

            }
            else
            {
                kv = new KeyValuesClass()
                {
                    Type = LicenseType.TRIAL,
                    Header = Convert.ToByte(9),
                    Footer = Convert.ToByte(6),
                    ProductCode = (byte)productCode,
                    Edition = Edition.ENTERPRISE,
                    Version = 1,
                    Expiration = DateTime.Now.AddDays(Convert.ToInt32(txtExperienceDays.Text))

                };
                if (!km.GenerateKey(kv, ref productKey))
                    txtProductKey.Text = "Error";
            }

            txtProductKey.Text = productKey;
        }

        private void frmLicGenerate_Load(object sender, EventArgs e)
        {
            cmbLicType.SelectedIndex = 0;
            txtProductID.Text = ComputerInfo.GetComputerId();
        }
    }
}
