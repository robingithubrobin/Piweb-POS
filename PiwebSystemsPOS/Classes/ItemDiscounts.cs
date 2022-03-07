using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiwebSystemsPOS.Classes
{
    public class ItemDiscounts
    {
        private bool isPercentageDiscount;
        private string itemName;
        private decimal discountValue;

        public bool IsPercentageDiscount
        {
            get { return isPercentageDiscount; }
            set { isPercentageDiscount = value; }
        }
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        public decimal DiscountValue
        {
            get { return discountValue; }
            set { discountValue = value; }
        }

        public ItemDiscounts(string _itemName, decimal _discountValue, bool _isPercentageDiscount)
        {
            this.itemName = _itemName;
            this.discountValue = _discountValue;
            this.isPercentageDiscount = _isPercentageDiscount;
        }
    }
}
