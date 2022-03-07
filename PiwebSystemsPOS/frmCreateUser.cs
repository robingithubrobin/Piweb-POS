using PiwebSystemsPOS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiwebSystemsPOS
{
    public partial class frmCreateUser : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        private static string connString = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private static SqlConnection sqlConn = new SqlConnection(connString);
        private static SqlCommand cmd;
        private static SqlDataAdapter sda;

        string createdBy = "SYSTEM";

        public frmCreateUser()
        {
            InitializeComponent();
            metroTabControl1.SelectedTab = metroTabPage1;
        }
        private void LoadWorkStations()
        {
            //SqlConnection con = Helper.getconnection();
            sqlConn.Open();
            string strCmd = "SELECT [DeviceID],[WorkStation] FROM [dbo].[SYS_Device]";
            cmd = new SqlCommand(strCmd, sqlConn);
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
        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void chkUseLoginCred_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseLoginCred.Checked == true)
            {
                edtClerkName.Text = txtName.Text;
                edtClerkPsw.Text = txtPassword.Text;
                edtClerkName.Enabled = false;
                edtClerkPsw.Enabled = false;
            }
            else
            {
                edtClerkName.Clear();
                edtClerkPsw.Clear();
                edtClerkName.Enabled = true;
                edtClerkPsw.Enabled = true;
                edtClerkName.Focus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int deviceID = Convert.ToInt32(cmbWorkStation.SelectedValue.ToString());
            //
            // Tab 1
            //
            string username = txtName.Text.Trim(),
                fullname = txtFullName.Text.Trim(),
                psw = PiwebSystemsPOS.Classes.HASH.Encrypt(txtPassword.Text.Trim());
            int role = cmbRole.SelectedIndex;
            DateTime expiry = calExpiryDate.Value;

            char isActive = 'N';
            if (chkIsActive.Checked == true)
                isActive = 'Y';
            //
            // Tab 2
            //
            string mobile = txtMobile.Text.Trim(),
                mobile2 = txtMobile2.Text.Trim(),
                email = txtEmail.Text.Trim();
            //
            // Tab 3
            //
            string printerName = "",
                clerkName = edtClerkName.Text.Trim(),
                clerkPass = PiwebSystemsPOS.Classes.HASH.Encrypt(edtClerkPsw.Text.Trim());

            int clerkId = Convert.ToInt32(EditID.Value.ToString());

            char useLoginCredentials = 'N';
            if (chkUseLoginCred.Checked == true)
                useLoginCredentials = 'Y';


                //Insert user
                piwebDataOps.CreateUser(username, fullname, isActive, expiry, role, psw, email, mobile, mobile2, clerkId, clerkName, clerkPass, useLoginCredentials, deviceID, createdBy);
                MessageBox.Show("User Created Successfully", "User", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnSetClerkPsw_Click(object sender, EventArgs e)
        {
            int nResult = pp7x.__SetClerkPsw(Convert.ToInt32(EditID.Value), edtClerkName.Text, edtClerkPsw.Text);

            switch (nResult)
            {
                case -1: MessageBox.Show("Timeout", "Set Clerk", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); break;
                case -2: MessageBox.Show("Fail", "Set Clerk", MessageBoxButtons.OK, MessageBoxIcon.Error); break;
                case 1: MessageBox.Show("Clerk has been set on printer [printer Name]", "Set Clerk", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
            }
        }

        private void frmCreateUser_Load(object sender, EventArgs e)
        {
            LoadWorkStations();
        }
    }
}
