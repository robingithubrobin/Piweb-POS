using PiwebSystemsPOS.Classes;
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
    public partial class frmUnitOfMeasure : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        public frmUnitOfMeasure()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string unitOfMeasureCode = txtUnit.Text.Trim(),
                unit = txtUnit.Text.Trim();

            if (string.IsNullOrEmpty(txtUnitCode.Text))
            {
                txtUnitCode.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtUnit.Text))
            {
                txtUnit.Focus();
                return;
            }
            piwebDataOps.CreateUnitOfMeasure(unitOfMeasureCode, unit);

            MessageBox.Show("Success");


        }
    }
}
