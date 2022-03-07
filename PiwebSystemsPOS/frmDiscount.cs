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
    public partial class frmDiscount : MetroFramework.Forms.MetroForm
    {
        TransactionsHelper transHelper = new TransactionsHelper();
        private string _subTotal;

        public string SubTotal
        {
            get { return _subTotal; }
            set { _subTotal = value; }
        }
        public frmDiscount()
        {
            InitializeComponent();
        }

        private void frmDiscount_Load(object sender, EventArgs e)
        {
            rdFixedAmount.Checked = true;
            txtPercentage.Enabled = false;

            txtFixedAmount.Text = "0.00";
            txtPercentage.Text = "0.00";
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cmbiType.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select discount type", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if(cmbiType.SelectedIndex == 1)
            {
                MessageBox.Show("Discount on SubTotal not active", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                if (rdFixedAmount.Checked == true)
                {
                    if (txtFixedAmount.Enabled == true && string.IsNullOrEmpty(txtFixedAmount.Text) || txtFixedAmount.Text == "0.00")
                    {
                        MessageBox.Show("Please Enter Discount Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtFixedAmount.Focus();
                        return;
                    }
                    else
                    {
                        int itemType = cmbiType.SelectedIndex;
                        switch (itemType)
                        {
                            case 0:
                                TransactionsHelper._iType = itemType;
                                TransactionsHelper._payAmount = (Convert.ToDouble(txtFixedAmount.Text));
                                TransactionsHelper.isPercentage = false;
                                break;
                            case 1:
                                TransactionsHelper._iType = itemType;
                                TransactionsHelper._payAmount = (Convert.ToDouble(txtFixedAmount.Text));
                                TransactionsHelper.isPercentage = false;
                                break;
                        }
                    }
                }
                if (rdPercentage.Checked == true)
                {
                    if (txtPercentage.Enabled == true && string.IsNullOrEmpty(txtPercentage.Text) || txtPercentage.Text == "0.00")
                    {
                        MessageBox.Show("Please Enter Discount Percentage", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPercentage.Focus();
                        return;
                    }
                    else
                    {
                        int itemType = cmbiType.SelectedIndex;
                        switch (itemType)
                        {
                            case 0:
                                TransactionsHelper._iType = itemType;
                                TransactionsHelper._discountRate = (Convert.ToDecimal(txtPercentage.Text));
                                TransactionsHelper.isPercentage = true;
                                break;
                            case 1:
                                TransactionsHelper._iType = itemType;
                                TransactionsHelper._discountRate = (Convert.ToDecimal(txtPercentage.Text));
                                TransactionsHelper.isPercentage = true;
                                break;
                        }
                    }
                }
            }

            //TransactionsHelper.discount = discount.ToString();

            this.Close();
        }

        private void rdPercentage_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPercentage.Checked == true)
            {
                txtPercentage.Enabled = true;
                txtPercentage.Focus();

                txtFixedAmount.Enabled = false;
            }
        }

        private void rdFixedAmount_CheckedChanged(object sender, EventArgs e)
        {
            if (rdFixedAmount.Checked == true)
            {
                txtFixedAmount.Enabled = true;
                txtFixedAmount.Focus();

                txtPercentage.Enabled = false;
            }
        }
    }
}
