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
using FoxLearn.License;
using System.Security.Cryptography;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;

namespace PiwebSystemsPOS
{
    public partial class frmStartPage : Form
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        //UserSession sessionManager = new UserSession();

        private static string connString = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private static SqlConnection sqlConn = new SqlConnection(connString);
        private static SqlCommand cmd;
        private static SqlDataAdapter sda;
        private DataTable dt = new DataTable();
        public frmStartPage()
        {
            InitializeComponent();
        }
        #region License Validation
        private void LicenseValidation()
        {
            //Check license
            lblProductID.Text = ComputerInfo.GetComputerId();
            KeyManager km = new KeyManager(lblProductID.Text);
            LicenseInfo lic = new LicenseInfo();
            int value = km.LoadSuretyFile(string.Format(@"{0}\key.lic", Application.StartupPath), ref lic);
            string productKey = lic.ProductKey;
            if (km.ValidKey(ref productKey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                if (km.DisassembleKey(productKey, ref kv))
                {

                    lblProductName.Text = lic.FullName;
                    //lblProductKey.Text = productKey;
                    int Expiry = Convert.ToInt32(string.Format(@"{0}", (kv.Expiration - DateTime.Now).Days));
                    int _counter = 30;


                    if (kv.Type == LicenseType.TRIAL)
                    {
                        //Check Expiry from 60 Days
                        if (Expiry <= _counter)
                        {
                            var result = MessageBox.Show("You have " + Expiry + " Days left to use PiwebPOS. Do you want to register your copy of PiwebPOS now?", "Register", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            if (result == DialogResult.Yes)
                            {
                                using (frmLicRegister openLicRegister = new frmLicRegister())
                                {
                                    openLicRegister.ShowDialog();
                                }
                            }
                            lblExpiration.Text = "License - Expires in " + Expiry + " Days";
                            linkActivate.Visible = true;
                        }
                        else
                            lblExpiration.Text = "License - Expires in " + Expiry + " Days";

                    }
                    else
                        lblExpiration.Text = "Full";
                }
            }
        }
        #endregion
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                MessageBox.Show("Username is Required", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (string.IsNullOrEmpty(txtPsw.Text))
            {
                MessageBox.Show("Password is required", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            #region Check license
            //Check if License is Active
            lblProductID.Text = ComputerInfo.GetComputerId();
            KeyManager km = new KeyManager(lblProductID.Text);
            LicenseInfo lic = new LicenseInfo();
            int value = km.LoadSuretyFile(string.Format(@"{0}\key.lic", Application.StartupPath), ref lic);
            string productKey = lic.ProductKey;
            if (km.ValidKey(ref productKey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                if (km.DisassembleKey(productKey, ref kv))
                {

                    lblProductName.Text = lic.FullName;
                    //lblProductKey.Text = productKey;
                    int Expiry = Convert.ToInt32(string.Format(@"{0}", (kv.Expiration - DateTime.Now).Days));
                    int _counter = 0;


                    if (kv.Type == LicenseType.TRIAL)
                    {
                        //Check Expiry from 60 Days
                        if (Expiry == _counter)
                        {
                            MessageBox.Show("Licese is Expired!! Activation is required to proceed", "License Activation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            using (frmLicRegister openLicRegister = new frmLicRegister())
                            {
                                openLicRegister.ShowDialog();

                            }
                            lblExpiration.Text = "License - Expires in " + Expiry + " Days";
                            linkActivate.Visible = true;
                            return;
                        }
                        else
                        {
                            lblExpiration.Text = "License - Expires in " + Expiry + " Days";

                            string _query = @"SELECT [UserID],[UserName],[FullName],[Role],[Password],[ClerkPassword] FROM [dbo].[SYS_User] WHERE [UserName] = '" + txtUserName.Text + "' AND [Password] = '" + PiwebSystemsPOS.Classes.HASH.Encrypt(txtPsw.Text) + "'";
                            cmd = new SqlCommand(_query, sqlConn);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                UserSession.userId = Convert.ToInt32(dt.Rows[0]["Role"].ToString());
                                UserSession.userName = dt.Rows[0]["UserName"].ToString();
                                UserSession.fullUser = dt.Rows[0]["FullName"].ToString();
                                int userRole = Convert.ToInt32(dt.Rows[0]["Role"].ToString());

                                if (userRole == 0) //Manager
                                {

                                }

                                switch (userRole)
                                {
                                    case 0: //Manager
                                        UserSession.roleID = 0;
                                        frmControlPanel openControlPanel = new frmControlPanel();
                                        openControlPanel.Show();
                                        this.Hide();
                                        break;
                                    case 1: //Supervisor
                                        UserSession.roleID = 1;
                                        MessageBox.Show("Not implemented", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    case 3: //Administrator
                                        UserSession.roleID = 3;
                                        frmControlPanel openControlPanelAdmin = new frmControlPanel();
                                        openControlPanelAdmin.Show();
                                        this.Hide();
                                        break;
                                    default:
                                        UserSession.roleID = 2;
                                        frmCashRegisterWithItems openCashRegister = new frmCashRegisterWithItems();
                                        openCashRegister.Show();
                                        this.Hide();
                                        break;
                                }

                            }
                            else
                            {
                                MessageBox.Show("Invalid Credentials", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }

                    }
                    else
                    {
                        MessageBox.Show("Full License not applicable", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            #endregion

        }

        const int productCode = 1;
        private void frmStartPage_Load(object sender, EventArgs e)
        {
            try
            {
                //txtUserName.Focus();

                //string saveDirectory = @"C:\SavedImages\";
                DataTable getCompanyInfo = piwebDataOps.GetCompanyInfo();
                lblCompanyName.Text = getCompanyInfo.Rows[0]["Name"].ToString();
                string logo = getCompanyInfo.Rows[0]["logoPath"].ToString(),
                    abbrevNames = getCompanyInfo.Rows[0]["abbreviatedNames"].ToString();
                char showAbbrev = Convert.ToChar(getCompanyInfo.Rows[0]["showAbbreviatedNames"].ToString());

                if (showAbbrev == 'Y')
                    lblCompanyName.Text = getCompanyInfo.Rows[0]["Name"].ToString() + " (" + abbrevNames + ")";

                if (!string.IsNullOrEmpty(logo))
                    pictureBox1.Image = Image.FromFile(@"C:\SavedImages\" + logo);

                lblClockIn.Visible = false;

                LicenseValidation();


                //Show OnScreenKeyboard

                #region Show or Hide OnScreenKeyboard
                //if (txtUserName.Focus())
                //{
                //    var keyboard = Process.GetProcessesByName("TabTip");
                //    if (keyboard.Length == 0)
                //    {
                //        csOnScreenKeyboard.ShowOnScreenKeyboard();
                //    }
                //    else
                //    {
                //        csOnScreenKeyboard.HideOnScreenKeyboard();
                //    }
                //}
                #endregion
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Login Screen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            lblClockIn.Visible = true;
            lblClockIn.Text = "Cashier Clock In";
            using (frmLicRegister openLicRegister = new frmLicRegister())
            {
                openLicRegister.ShowDialog();
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkActivate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (frmLicRegister openLicRegister = new frmLicRegister())
            {
                openLicRegister.ShowDialog();
                LicenseValidation();
            }
        }

        private void linkGenerate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (frmLicGenerate openLicGenerate = new frmLicGenerate())
            {
                openLicGenerate.ShowDialog();
            }
        }

        private void linkLicInformation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (frmAbout openAbout = new frmAbout())
            {
                openAbout.ShowDialog();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnUserKeyboard_Click(object sender, EventArgs e)
        {
            string value = "usernameField";
            frmOnScreenKeyboard openKeyboard = new frmOnScreenKeyboard(value);
            openKeyboard.ShowDialog();
            txtUserName.Text = UserSession.onComingValue;
        }

        private void txtPsw_Click(object sender, EventArgs e)
        {
            string value = "psw";
            frmOnScreenKeyboard openKeyboard = new frmOnScreenKeyboard(value);
            openKeyboard.ShowDialog();
            txtPsw.Text = UserSession.onComingValue;
            UserSession.onComingValue = "";
        }

        private void txtUserName_Click(object sender, EventArgs e)
        {
            string value = "usernameField";
            frmOnScreenKeyboard openKeyboard = new frmOnScreenKeyboard(value);
            openKeyboard.ShowDialog();
            txtUserName.Text = UserSession.onComingValue;
            UserSession.onComingValue = "";
        }

        private void txtPsw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogIn_Click(sender, e);
            }
        }
    }
}
