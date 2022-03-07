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
    public partial class ucAdministrative : MetroFramework.Controls.MetroUserControl
    {
        private static ucAdministrative _instance;
        public static ucAdministrative instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucAdministrative();
                return _instance;
            }
        }
        public ucAdministrative()
        {
            InitializeComponent();
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            frmCreateProduct openCreateProduct = new frmCreateProduct();
            openCreateProduct.ShowDialog();
        }

        private void TileUnitOfMeasure_Click(object sender, EventArgs e)
        {
            frmUnitOfMeasure openUnitofMeasure = new frmUnitOfMeasure();
            openUnitofMeasure.ShowDialog();
        }

        private void TileProductCategory_Click(object sender, EventArgs e)
        {
            frmProductCategory openCategory = new frmProductCategory();
            openCategory.ShowDialog();
        }

        private void TileProductBrand_Click(object sender, EventArgs e)
        {
            frmProductBrand openBrand = new frmProductBrand();
            openBrand.ShowDialog();
        }

        private void TileTaxGroup_Click(object sender, EventArgs e)
        {
            frmTaxGroup openTaxGroup = new frmTaxGroup();
            openTaxGroup.ShowDialog();
        }

        private void TileTaxTate_Click(object sender, EventArgs e)
        {
            frmTaxRate openTaxRate = new frmTaxRate();
            openTaxRate.ShowDialog();
        }

        private void TileStockAdjustment_Click(object sender, EventArgs e)
        {
            frmStockAdjustment openStockAdjustment = new frmStockAdjustment();
            openStockAdjustment.ShowDialog();
        }

        private void TileCustomer_Click(object sender, EventArgs e)
        {
            frmCustomers openCustomer = new frmCustomers();
            openCustomer.ShowDialog();
        }

        private void TileSuppliers_Click(object sender, EventArgs e)
        {
            frmSuppliers openSuppliers = new frmSuppliers();
            openSuppliers.ShowDialog();
        }

        private void metroTileImportData_Click(object sender, EventArgs e)
        {
            frmDataImport openDataImport = new frmDataImport();
            openDataImport.ShowDialog();

        }
    }
}
