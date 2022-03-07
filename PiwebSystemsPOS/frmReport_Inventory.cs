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
    public partial class frmReport_Inventory : MetroFramework.Forms.MetroForm
    {
        public frmReport_Inventory()
        {
            InitializeComponent();
        }

        private void frmReport_Inventory_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
