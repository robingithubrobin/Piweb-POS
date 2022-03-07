using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiwebSystemsPOS.Classes
{
    public class csDiscountTaxSales
    {
        private decimal itemTaxBeforeDiscount; // Accessible from Display amounts

        private string productName;
        private decimal originalPrice;
        private decimal salesPrice;
        private decimal taxRate;
        private decimal discountRate;
        private decimal discount;
        private decimal taxAmount;
        private decimal discountAmount;
        private decimal _VAT;
        private bool isDiscount;
        private bool isPercentageDiscount;
        private decimal grandTotal;


        public bool IsPercentageDiscount
        {
            get { return isPercentageDiscount; }
            set { isPercentageDiscount = value; }
        }
        public bool IsDiscount
        {
            get { return isDiscount; }
            set { isDiscount = value; }
        }


        public decimal ItemTaxBeforeDiscount
        {
            get { return itemTaxBeforeDiscount; }
            set { itemTaxBeforeDiscount = value; }
        }
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        public decimal VAT
        {
            get { return _VAT; }
            set { _VAT = value; }
        }

        public decimal OriginalPrice
        {
            get { return originalPrice; }
            set { originalPrice = value; }
        }

        public decimal SalesPrice
        {
            get { return salesPrice; }
            set { salesPrice = value; }
        }

        public decimal TaxRate
        {
            get { return taxRate; }
            set { taxRate = value; }
        }

        public decimal DiscountRate
        {
            get { return discountRate; }
            set { discountRate = value; }
        }

        public decimal Discount
        {
            get { return discount; }
            set { discount = value; }
        }


        /// <summary>
        /// Holds Cummulative Discount Amount
        /// </summary>
        public decimal DiscountAmount
        {
            get { return discountAmount; }
            set { discountAmount = value; }
        }

        /// <summary>
        /// Holds Cummulative Tax
        /// </summary>
        public decimal TaxAmount
        {
            get { return taxAmount; }
            set { taxAmount = value; }
        }

        /// <summary>
        /// Holds Cummulative Grand Totals
        /// </summary>
        public decimal GrandTotal
        {
            get { return grandTotal; }
            set { grandTotal = value; }
        }
        public csDiscountTaxSales()
        {
            
        }
        public csDiscountTaxSales(bool _isPercentageDiscount)
        {
            this.isPercentageDiscount = _isPercentageDiscount;
        }
        /// <summary>
        /// Calculate Sales Price after discount
        /// </summary>
        /// <param name="_originalPrice"></param>
        /// <param name="_discountRate"></param>
        /// <returns></returns>
        public decimal SalesPriceAfterDiscount(decimal _originalPrice, decimal _discountRate)
        {
            discount = _originalPrice * (_discountRate / 100);
            salesPrice = _originalPrice - discount;
            return salesPrice;
        }

        /// <summary>
        /// Calculate Sales Price discount with fixed amount
        /// </summary>
        /// <param name="_originalPrice"></param>
        /// <param name="_discountAmount"></param>
        /// <returns></returns>
        public decimal SalesPriceFixedAmountDiscount(decimal _originalPrice, decimal _discountAmount)
        {
            discount = _discountAmount;

            salesPrice = _originalPrice - _discountAmount;

            return salesPrice;
        }

        /// <summary>
        /// Price excludes Tax (Calculate Sales Price Plus Tax)
        /// </summary>
        /// <param name="_originalPrice"></param>
        /// <param name="_taxRate"></param>
        /// <returns></returns>
        public decimal SalesPriceWithTax(decimal _originalPrice, decimal _taxRate)
        {
            VAT = _originalPrice * (_taxRate / 100);
            salesPrice = _originalPrice + VAT;

            return salesPrice;
        }

        /// <summary>
        /// Price Includes Tax (Calculate Sales minus Tax)
        /// </summary>
        /// <param name="_originalPrice"></param>
        /// <param name="_taxRate"></param>
        /// <returns></returns>
        public decimal SalesPriceOffTax(decimal _originalPrice, decimal _taxRate)
        {
            VAT = _originalPrice - (_originalPrice / (1 + (_taxRate / 100)));

            salesPrice = _originalPrice - VAT;

            return salesPrice;
        }
    }
}
