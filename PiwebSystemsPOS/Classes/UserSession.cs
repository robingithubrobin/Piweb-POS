using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiwebSystemsPOS.Classes
{
    class UserSession
    {
        //public static string username = "SYSTEM"; // { get; set; }
        public static int userId{get;set;}
        public static string userName { get; set; }
        public static string fullUser { get; set; }
        public static int roleID { get; set; }

        //Keyboard Session
        public static string onComingValue;
        public static string keyField;
        public UserSession(){}

    }
}
