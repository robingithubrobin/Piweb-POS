using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiwebSystemsPOS.Classes
{
    class Sales
    {
        private string productCode;
        private string taxGroupCode;
        private string discountGroupCode;
        private string status;
        private string unitOfMeasure;
        public string ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }


        public string TaxGroupCode
        {
            get { return taxGroupCode; }
            set { taxGroupCode = value; }
        }

        public string DiscountGroupCode
        {
            get { return discountGroupCode; }
            set { discountGroupCode = value; }
        }


        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string UnitOfMeasure
        {
            get { return unitOfMeasure; }
            set { unitOfMeasure = value; }
        }

    }
}
