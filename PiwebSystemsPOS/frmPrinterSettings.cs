using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PiwebSystemsPOS.Classes;
using System.Configuration;
using System.Data.SqlClient;

namespace PiwebSystemsPOS
{
    public partial class frmPrinterSettings : MetroFramework.Forms.MetroForm
    {

        PiwebSystems piwebDataOps = new PiwebSystems();
        private static string connString = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private static SqlConnection sqlConn = new SqlConnection(connString);
        private static SqlCommand cmd;
        private static SqlDataAdapter sda;

        private DataTable dt = new DataTable();
        public frmPrinterSettings()
        {
            InitializeComponent();
        }

        private void LoadWorkStations()
        {
            //SqlConnection con = Helper.getconnection();
            sqlConn.Open();
            string strCmd = "SELECT [DeviceID],[WorkStation] FROM [dbo].[SYS_Device]";
            SqlCommand cmd = new SqlCommand(strCmd, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter(strCmd, sqlConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmbWorkStation.DataSource = ds.Tables[0];
            cmbWorkStation.DisplayMember = "WorkStation";
            cmbWorkStation.ValueMember = "DeviceID";
            cmbWorkStation.Enabled = true;
            cmd.ExecuteNonQuery();
            sqlConn.Close();
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int deviceID = Convert.ToInt32(cmbWorkStation.SelectedValue.ToString());

            string s = null;

            string commName = cbb_CommName.Text;

            int retryCount = Convert.ToInt32(txt_RetryCount.Text.Trim()),
                timeOut = Convert.ToInt32(txt_TimeOut.Text.Trim()),
                boudRate = Convert.ToInt32(txt_BaudRate.Text.Trim());

            if (checkError.Checked)
            {
                s = "1";
            }
            else
                s = "0";

            piwebDataOps.CreatePrinterSettings(deviceID, commName, timeOut, retryCount, boudRate, s);
            MessageBox.Show("Connection Settings Saved Successfully", "Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmPrinterSettings_Load(object sender, EventArgs e)
        {
            LoadWorkStations();
        }
    }
}
