using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiwebSystemsPOS.Classes
{
    public class csReturnItems
    {
        public string productCode { get; set; }
        public string itemName { get; set; }
        public decimal quantity { get; set; }
        public decimal amount { get; set; }
    }
}
