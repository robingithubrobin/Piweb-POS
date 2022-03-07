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
    public partial class frmChangeQuantity : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        TransactionsHelper transHelper = new TransactionsHelper();
        private string quantity;
        public string Quantity
        {
            get { return txtQty.Text; }
            set 
            { 
                txtQty.Text = value; 
                quantity = value; 
            }
        }
        public frmChangeQuantity()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (txtQty.Text == "0")
                txtQty.Clear();

            Button b = (Button)sender;
            txtQty.Text = txtQty.Text + b.Text;

        }
        private void btn_Backspace(object sender, EventArgs e)
        {
            int length = txtQty.TextLength - 1;
            string text = txtQty.Text;
            txtQty.Clear();
            for (int i = 0; i < length; i++)
            {
                txtQty.Text = txtQty.Text + text[i];
            }
        }
        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (txtQty.Text == string.Empty)
            {
                MessageBox.Show("Quantity cannot be empty", "Application", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtQty.Focus();
            }
            else
            {
                //quantity = Quantity;
                int qty = int.Parse(txtQty.Text);
                if (qty <= 0)
                {
                    MessageBox.Show("Quantity should be greater than 0", "Application", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtQty.Focus();
                }
                else
                {
                    //Initial Cashregister Product quantity
                    TransactionsHelper.qty = Convert.ToInt32(txtQty.Text).ToString();

                    string itemName = TransactionsHelper.productName,
                        taxGroupCode = "";
                    decimal ItemTaxRate = 0, taxAmount = 0, currentProductTax = 0, currentTaxValue = 0;
                    DataTable getProduct = piwebDataOps.GetProducts(itemName, "");
                    taxGroupCode = getProduct.Rows[0]["TaxGroupCode"].ToString();

                    //Calculate Tax
                    if (!string.IsNullOrEmpty(taxGroupCode))
                    {
                        DataTable getTax = piwebDataOps.GetTax(taxGroupCode);
                        ItemTaxRate = Convert.ToDecimal(getTax.Rows[0]["Tax"].ToString());
                        taxAmount = (Convert.ToDecimal(getProduct.Rows[0]["UnitPrice"].ToString()) * ItemTaxRate) / 100;

                        
                        currentProductTax = taxAmount * Convert.ToInt32(quantity); //CashRegister Tax

                        TransactionsHelper.TaxValue = currentProductTax; //Passing currentProductTax to TransactionsHelper Class

                        currentTaxValue = taxAmount * Convert.ToDecimal(txtQty.Text);

                        
                        MessageBox.Show("Current Tax Amount: "+currentProductTax.ToString()+"\nCurrent Tax Value: "+currentTaxValue);

                    }
                    this.Close();
                }
            }
            //this.Close();
        }
        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void frmChangeQuantity_Load(object sender, EventArgs e)
        {

        }
    }
}
