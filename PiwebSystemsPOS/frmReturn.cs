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
    public partial class frmReturn : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        TransactionsHelper TransactionsHelpertransHelper = new TransactionsHelper();
        string username = UserSession.userName, deviceName = System.Environment.MachineName;

        public frmReturn()
        {
            InitializeComponent();
        }
        private string invoiceNo;

        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }
        private string itemCode;

        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }
        private string itemName;

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }
        private decimal itemPrice;

        public decimal ItemPrice
        {
            get { return itemPrice; }
            set { itemPrice = value; }
        }
        private decimal qty;

        public decimal Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        private decimal subTotal;

        public decimal SubTotal
        {
            get { return subTotal; }
            set { subTotal = value; }
        }

        private decimal _totalDiscount;

        public decimal TotalDiscount
        {
            get { return _totalDiscount; }
            set { _totalDiscount = value; }
        }

        private decimal _totalTax;

        public decimal TotalTax
        {
            get { return _totalTax; }
            set { _totalTax = value; }
        }

        private decimal _totalAmount;

        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; }
        }

        private List<csReturnItems> returnItems;

        public List<csReturnItems> ReturnItems
        {
            get { return returnItems; }
            set { returnItems = value; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReturn_Load(object sender, EventArgs e)
        {
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            string tenderType = "";
            decimal tenderAmount = 0;
            // Open TenderType
            frmPayment openPayment = new frmPayment();
            openPayment.Sender = "return";
            openPayment.TotalAmount = _totalAmount.ToString();
            openPayment.ShowDialog();

            //  Update invoice Header
            piwebDataOps.UpdateSalesInvoice(InvoiceNo, "RETURN", subTotal, _totalTax, _totalDiscount, deviceName, username);

            //
            //Register Sale in DB
            //
            string productCode = ""
                    , PluName = itemName
                    , unitOfMeasure = ""
                    , _statusCode = "RETURN"
                    , _priceListID = ""
                    , _tax1ID = ""
                    , taxGroupCode = ""
                    , discountGroupCode = ""
                    , deviceID = deviceName;
            decimal quantity = 0, unitPrice = 0, _lineTax1 = 0, _tax1Rate = 0, _lineDiscount = 0, _extendedPrice = 0, _fixedPrice = 0, discountRate = 0, discountAmount = 0;
            char includesVAT = 'Y';

            for (int i = 0; i < returnItems.Count; i++)
            {
                DataTable getProducts = piwebDataOps.GetProductsByItemName(returnItems[i].itemName);
                PluName = returnItems[i].itemName;
                productCode = getProducts.Rows[0]["No"].ToString();
                unitOfMeasure = getProducts.Rows[0]["UnitOfMeasure"].ToString();
                taxGroupCode = getProducts.Rows[0]["TaxGroupCode"].ToString();
                discountGroupCode = getProducts.Rows[0]["DiscountGroupCode"].ToString();
                quantity = returnItems[i].quantity;
                unitPrice = Convert.ToDecimal("-"+getProducts.Rows[0]["UnitPrice"].ToString());

                DataTable getTax = piwebDataOps.GetTax(taxGroupCode);
                _lineTax1 = returnItems[i].amount * Convert.ToDecimal(getTax.Rows[0]["Tax"].ToString());
                _tax1ID = getTax.Rows[0]["ID"].ToString();
                _tax1Rate = Convert.ToDecimal(getTax.Rows[0]["Tax"].ToString());

                includesVAT = getProducts.Rows[0]["PriceIncVAT"].ToString() == "Y" ? 'Y' : 'N';

                piwebDataOps.CreateSalesInvoiceLine(InvoiceNo, productCode, PluName, Convert.ToDecimal(quantity), unitOfMeasure, _statusCode, _priceListID, Convert.ToDecimal(unitPrice), Convert.ToDecimal(unitPrice), _tax1ID, Convert.ToDecimal(_lineTax1), Convert.ToDecimal(_tax1Rate), taxGroupCode, discountGroupCode, _lineDiscount, _extendedPrice, includesVAT, _fixedPrice, discountRate, discountAmount, UserSession.userName, deviceID);

                //Insert Data int INV_InventoryStock
                piwebDataOps.CreateInventoryStock("RETURN", InvoiceNo, productCode, quantity, 0, username);
            }


            //
            //Register Payment In Database
            //
            string payMode = openPayment.PaymentMode, bankName = openPayment.BankName;
            tenderAmount = Convert.ToDecimal(openPayment.TenderedAmount);
            decimal totalAmount = Convert.ToDecimal(openPayment.TotalAmount);
            int paymentTypeMode = -1;

            switch (payMode)
            {
                case "CASH":
                    paymentTypeMode = 0;
                    piwebDataOps.CreatePaymentLine(_statusCode, invoiceNo, totalAmount, Convert.ToDecimal(tenderAmount), payMode, username);
                    break;
                case "CARD":
                    paymentTypeMode = 2;
                    piwebDataOps.CreatePaymentLineCard(_statusCode, invoiceNo, totalAmount, payMode, paymentTypeMode, username);
                    break;
                case "CHEQUE":
                    paymentTypeMode = 1;
                    piwebDataOps.CreatePaymentLineCheque(_statusCode, invoiceNo, totalAmount, payMode, 1, username);
                    break;
                case "":
                    MessageBox.Show("Data Not Saved", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            returnItems.Clear();
            this.Close();
        }
    }
}
