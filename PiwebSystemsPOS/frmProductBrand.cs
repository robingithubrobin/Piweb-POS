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
    public partial class frmProductBrand : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        public frmProductBrand()
        {
            InitializeComponent();
        }

        private void frmProductBrand_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string brand = txtBrand.Text.Trim(),
                manufacturer = txtManufacturer.Text.Trim(),
                description = txtDescription.Text.Trim(),
                discountGroup = "";
            if (!string.IsNullOrEmpty(cmbDiscountGroup.Text))
                discountGroup = cmbDiscountGroup.SelectedValue.ToString();

            piwebDataOps.CreateProductBrand(brand, description, discountGroup);

            MessageBox.Show("Success");
        }
    }
}
