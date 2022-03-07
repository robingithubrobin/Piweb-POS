using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiwebSystemsPOS.Classes
{
    public class TransactionsHelper
    {
        public string _totalAmount { get; set; }

        public static string qty;
        public static string discount;
        public static string productName { get; set; }
        public static string DocumentNo { get; set; }
        public static DateTime PurchaseDate { get; set; }
        public static string PurchaseType { get; set; }
        public static string Supplier { get; set; }
        public static string ReferenceNo { get; set; }
        public static DateTime ReceivingDate { get; set; }
        public static List<String> voidItems { get; set; }
        //Tax Amount Value;
        public static decimal TaxValue;
        public TransactionsHelper()
        {
        }
        //
        // Discount Values
        //
        public static bool isDiscount { get; set; }
        public static decimal _productPrice { get; set; }
        public static decimal _discountRate { get; set; }
        public static bool isPercentage { get; set; }
        public static decimal _lastSoldItemAmount { get; set; }
        public static int _iType { get; set; }
        public static double _payAmount { get; set; }
        public static string _description { get; set; }

        //
        //Customer Details
        //
        #region Customer Details

        public static string CustomerCode
        {
            get;
            set;
        }
        public static string CustomerName
        {
            get;
            set;
        }
        public static string CustomerPhone
        {
            get;
            set;
        }
        #endregion

        //
        // Form Type
        //
        public static string formType { get; set; }

        //Printer connection static Variables
        public static string commName { get; set; }
        public static int timeOut { get; set; }
        public static int retryCount { get; set; }
        public static int baudRate { get; set; }

        //
        //  No Series Code
        //
        public static string noSeriesCode { get; set; }

    }
}
