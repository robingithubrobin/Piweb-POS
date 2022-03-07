using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiwebSystemsPOS.Classes
{
    public class ProductHelper
    {
        #region Product properties
        public string _no { get; set; }
        public string _GTIN { get; set; }
        public string _productName { get; set; }
        public string _description { get; set; }
        public string _costPrice { get; set; }
        public string _unitPrice { get; set; }
        public string _unitPriceExclVAT { get; set; }
        public string _createdBy { get; set; }
        public string _createdDate { get; set; }
        public string _categoryCode { get; set; }
        public string _productType { get; set; }
        public string _unitOfMeasure { get; set; }
        public string _parentProduct { get; set; }
        public string _productBrand { get; set; }
        public string _taxGroup { get; set; }
        public string _discountGroup { get; set; }
        public string _fileName { get; set; }
        #endregion
    }
}
