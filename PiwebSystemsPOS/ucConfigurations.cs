using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiwebSystemsPOS
{
    public partial class ucConfigurations : MetroFramework.Controls.MetroUserControl
    {
        private static ucConfigurations _instance;
        public static ucConfigurations instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucConfigurations();
                return _instance;
            }
        }
        public ucConfigurations()
        {
            InitializeComponent();
        }

        private void tilePrinterSettings_Click(object sender, EventArgs e)
        {
            btnRestoreReceiptPrinterSettings openSystemConfig = new btnRestoreReceiptPrinterSettings();
            openSystemConfig.Show();
        }

        private void tileBusinessSetup_Click(object sender, EventArgs e)
        {
            frmBusinessInfo openBusinessInfo = new frmBusinessInfo();
            openBusinessInfo.ShowDialog();
        }

        private void tileManageUsers_Click(object sender, EventArgs e)
        {
            Users openUsers = new Users();
            openUsers.ShowDialog();
        }

        private void tileDeviceRegister_Click(object sender, EventArgs e)
        {
            frmDeviceList openDeviceList = new frmDeviceList();
            openDeviceList.ShowDialog();
        }
    }
}
