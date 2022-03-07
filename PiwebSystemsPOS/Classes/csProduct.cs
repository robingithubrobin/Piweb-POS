using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiwebSystemsPOS.Classes
{
    class csProduct
    {
        public string No { get; set; }
        public string gtin { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string CategoryCode { get; set; }
        public string UnitOfMeasureCode { get; set; }
        public string ParentProductCode { get; set; }
        public string BrandID { get; set; }
        public decimal UnitCost { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitPriceExclVAT { get; set; }
        public char PriceIncVAT { get; set; }
        public string TaxGroupCode { get; set; }
        public string DiscountGroupCode { get; set; }
        public char SalesProduct { get; set; }
        public char Active { get; set; }
        public char PurchaseProduct { get; set; }
        public char ReturnAllowed { get; set; }
        public char AllowNegStock { get; set; }
        public string Photo { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
