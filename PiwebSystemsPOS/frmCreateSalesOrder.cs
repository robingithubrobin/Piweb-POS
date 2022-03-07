using PiwebSystemsPOS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiwebSystemsPOS
{
    public partial class frmCreateSalesOrder : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        private DataTable dt = new DataTable();

        private static string connString = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private static SqlConnection sqlConn = new SqlConnection(connString);
        private static SqlCommand cmd;
        private static SqlDataAdapter sda;
        public frmCreateSalesOrder()
        {
            InitializeComponent();
        }

        #region Generate Serial No.
        public string LoadOrderNo()
        {
            var serialNo = "";
            int _result = 0;
            string rec, _prefix = "ORD";

            cmd = new SqlCommand("SELECT MAX([SalesOrderNo]) AS 'OrderNo' FROM [dbo].[SAL_SalesOrders]", sqlConn);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            rec = dt.Rows[0]["OrderNo"].ToString();
            if (!string.IsNullOrEmpty(rec))
            {
                _result = Convert.ToInt32(rec.Substring(3, rec.Length - 3)) + 1;
                serialNo = _prefix + _result.ToString();

            }
            else
            {
                _result = 100000 + 1;
                serialNo = _prefix + _result.ToString();
            }

            return serialNo;
        }
        #endregion
        private void btnOk_Click(object sender, EventArgs e)
        {
            //Form Data
            string orderNo = txtOrderNo.Text.Trim(),
                customer = txtCustomer.Text.Trim(),
                address = txtAddress.Text.Trim(),
                address2 = txtAddress2.Text.Trim(),
                phone = txtPhone.Text.Trim(),
                altPhone = txtAltPhone.Text.Trim(),
                location = txtLocation.Text.Trim(),
                refNo = txtRefNo.Text.Trim(),
                orderDate = dtOrderDate.Text.Trim(),
                deliveryDate = dtDeliveryDate.Text.Trim();

            //piwebDataOps.CreateSalesOrder(orderNo, DateTime _orderDate, string _SOTypeCode, string _customerID, string _statusCode, string _deliveryAddress, string _deliveryArea , string _deliveryZipCode, string _referenceNo ,DateTime _deliveryTime,decimal _packagingCharges,decimal _deliveryCharges,DateTime _dueDate,string _createdBy)

            frmOrder openOrder = new frmOrder();
            TransactionsHelper.formType = "Order";
            TransactionsHelper.DocumentNo = txtOrderNo.Text;
            TransactionsHelper.CustomerName = txtCustomer.Text;
            openOrder.ShowDialog();
        }

        private void btnLookUp_Click(object sender, EventArgs e)
        {
            frmCustomerList_Sales openCustomerList = new frmCustomerList_Sales();
            openCustomerList.ShowDialog();

            #region Get Customer Details

            string customerCode = TransactionsHelper.CustomerCode;

            DataTable customerDetails = piwebDataOps.GetCustomers(customerCode);
            txtCustomer.Text = customerDetails.Rows[0]["Name"].ToString();
            txtAddress.Text = customerDetails.Rows[0]["Address"].ToString();
            txtAddress2.Text = customerDetails.Rows[0]["Address2"].ToString();
            txtPhone.Text = customerDetails.Rows[0]["PhoneNo"].ToString();
            txtAltPhone.Text = customerDetails.Rows[0]["AltPhoneNo"].ToString();
            txtLocation.Text = customerDetails.Rows[0]["City"].ToString();
            #endregion
        }

        private void frmCreateSalesOrder_Load(object sender, EventArgs e)
        {
            txtOrderNo.Text = LoadOrderNo();
        }
    }
}
