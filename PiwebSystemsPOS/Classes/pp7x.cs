using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PiwebSystemsPOS.Classes
{
    public delegate void TOnReceiveData(IntPtr ReceiveData, int ReceiveLen); //
    public delegate void TProcess(int PackNo); //
    public class pp7x
    {
        public pp7x()
        {

        }

        [DllImport("pp7device.dll")]
        public extern static void __Config(int TimeOut, int RetryCount, int BaudRate, string CommName);

        [DllImport("pp7device.dll")]
        public extern static void __Open();

        [DllImport("pp7device.dll")]
        public extern static void __Close();

        [DllImport("pp7device.dll")]
        public extern static Boolean __Active();

        [DllImport("pp7device.dll")]
        public extern static void __IniOnShowReceiveData(TOnReceiveData value);

        [DllImport("pp7device.dll")]
        public extern static int __CashierLogin(int clerkId, string ClerkPsw);

        [DllImport("pp7device.dll")]
        public extern static int __OpenFiscalReceipt();


        [DllImport("pp7device.dll")]
        public extern static int __Registeringsale(string PluName, int TaxIndex, double unitPrice, double quantity, Boolean dotflag, Boolean Isvoid);

        [DllImport("pp7device.dll")]
        public extern static int __SetCustomInfo(string PIN, string customname);

        [DllImport("pp7device.dll")]
        public extern static int __PrintMessage(string msg);

        [DllImport("pp7device.dll")]
        public extern static int __SetClerkPsw(int ID, string Name, string Psw);

        [DllImport("pp7device.dll")]
        public extern static int __Dealsum(int payid);

        [DllImport("pp7device.dll")]
        public extern static int __Totalcalculating(int PayMode, double Amount, Boolean dotflag);

        [DllImport("pp7device.dll")]
        public extern static int __EC();

        [DllImport("pp7device.dll")]
        public extern static int __AllVoid();

        [DllImport("pp7device.dll")]
        public extern static int __addpere(int iType, double value, string description);

        [DllImport("pp7device.dll")]
        public extern static int __subpere(int iType, double value, string description);

        [DllImport("pp7device.dll")]
        public extern static int __Discount(double value, Boolean dotflag);

        [DllImport("pp7device.dll")]
        public extern static int __CashIntoDrawer(double value, Boolean dotflag);

        [DllImport("pp7device.dll")]
        public extern static int __Paidoutfromdrawer(double value, Boolean dotflag);

        [DllImport("pp7device.dll")]
        public extern static int __PrinterStatus();

        [DllImport("pp7device.dll")]
        public extern static int __Paperfeed();

        [DllImport("pp7device.dll")]
        public extern static int __GetSoftwareStatus();

        [DllImport("pp7device.dll")]
        public extern static int __GetHardwareStatus();

        [DllImport("pp7device.dll")]
        public extern static int __PrintSubtotal();

        [DllImport("pp7device.dll")]
        public extern static int __OpenDrawer();


        [DllImport("pp7device.dll")]
        public extern static int __PrintDrawerReport();

        [DllImport("pp7device.dll")]
        public extern static int __PrintVatReport();

        [DllImport("pp7device.dll")]
        public extern static int __PrintFimVersionReport();


        [DllImport("pp7device.dll")]
        public extern static int __PrintClerkReport();

        [DllImport("pp7device.dll")]
        public extern static int __CloseFiscalReceipt();

        [DllImport("pp7device.dll")]
        public extern static int __PrintXReport();

        [DllImport("pp7device.dll")]
        public extern static int __PrintZReport();

        [DllImport("pp7device.dll")]
        public extern static int __PrintFisBydate(string datefrom, string dateto, int fisType);

        [DllImport("pp7device.dll")]
        public extern static int __PrintFisByZNO(int znofrom, int znoto, int fisType);


        [DllImport("pp7device.dll")]
        public extern static int __PrintEJRptByNO(int iType, int RptType, int nofrom, int noto);

        [DllImport("pp7device.dll")]
        public extern static int __PrintEJRptByDate(int iType, int RptType, string datefrom, string dateto);

        [DllImport("pp7device.dll")]
        public extern static int __CopyLastReceipt();

        [DllImport("pp7device.dll")]
        public extern static int __ReadRcptbyZDateEx(string Datefrom, string DateTo, string Hexfile, TProcess Ejevent, int Maintype, int SubType);
    }
}
