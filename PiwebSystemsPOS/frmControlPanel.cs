using PiwebSystemsPOS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiwebSystemsPOS
{
    public partial class frmControlPanel : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        public frmControlPanel()
        {
            InitializeComponent();
            selector.Top = btnHome.Top;
            selector.Height = btnHome.Height;
            lblTabLabel.Text = btnHome.Text;
            pictureBoxIcon.Image = global::PiwebSystemsPOS.Properties.Resources.Speedometer_24px;
            pictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;

            //Username
            lblUsername.Text = UserSession.fullUser;

            //Initialize Dashboard
            if (!metroPanel3.Controls.Contains(ucDashboard.instance))
            {
                metroPanel3.Controls.Add(ucDashboard.instance);
                ucDashboard.instance.Dock = DockStyle.Fill;
                ucDashboard.instance.BringToFront();
            }
        }

        private void frmControlPanel_Load(object sender, EventArgs e)
        {
            DataTable getCompanyInfo = piwebDataOps.GetCompanyInfo();
            //Set company Name on form
            this.Text = getCompanyInfo.Rows[0]["Name"].ToString() + " - CPanel";
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            metroPanel3.Controls.Clear();

            selector.Top = btnHome.Top;
            selector.Height = btnHome.Height;
            lblTabLabel.Text = btnHome.Text;
            pictureBoxIcon.Image = global::PiwebSystemsPOS.Properties.Resources.Speedometer_24px;
            pictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;

            if (!metroPanel3.Controls.Contains(ucDashboard.instance))
            {
                metroPanel3.Controls.Add(ucDashboard.instance);
                ucDashboard.instance.Dock = DockStyle.Fill;
                ucDashboard.instance.BringToFront();
            }
        }

        private void btnAdministrative_Click(object sender, EventArgs e)
        {
            metroPanel3.Controls.Clear();

            selector.Top = btnAdministrative.Top;
            selector.Height = btnAdministrative.Height;
            lblTabLabel.Text = btnAdministrative.Text;
            pictureBoxIcon.Image = global::PiwebSystemsPOS.Properties.Resources.Maintenance_64px_1;
            pictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;

            if (!metroPanel3.Controls.Contains(ucAdministrative.instance))
            {
                metroPanel3.Controls.Add(ucAdministrative.instance);
                ucAdministrative.instance.Dock = DockStyle.Fill;
                ucAdministrative.instance.BringToFront();
            }
            
        }

        private void btnTransactions_Click(object sender, EventArgs e)
        {
            metroPanel3.Controls.Clear();

            selector.Top = btnTransactions.Top;
            selector.Height = btnTransactions.Height;
            lblTabLabel.Text = btnTransactions.Text;
            pictureBoxIcon.Image = global::PiwebSystemsPOS.Properties.Resources.POS_Terminal_50px;
            pictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;

            if (!metroPanel3.Controls.Contains(ucTransactions.instance))
            {
                metroPanel3.Controls.Add(ucTransactions.instance);
                ucTransactions.instance.Dock = DockStyle.Fill;
                ucTransactions.instance.BringToFront();
            }
        }

        private void btnConfigurations_Click(object sender, EventArgs e)
        {
            metroPanel3.Controls.Clear();

            selector.Top = btnConfigurations.Top;
            selector.Height = btnConfigurations.Height;
            lblTabLabel.Text = btnConfigurations.Text;
            pictureBoxIcon.Image = global::PiwebSystemsPOS.Properties.Resources.Vertical_Settings_Mixer_50px;
            pictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;

            if (!metroPanel3.Controls.Contains(ucConfigurations.instance))
            {
                metroPanel3.Controls.Add(ucConfigurations.instance);
                ucConfigurations.instance.Dock = DockStyle.Fill;
                ucConfigurations.instance.BringToFront();
            }
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            metroPanel3.Controls.Clear();

            selector.Top = btnReports.Top;
            selector.Height = btnReports.Height;
            lblTabLabel.Text = btnReports.Text;
            pictureBoxIcon.Image = global::PiwebSystemsPOS.Properties.Resources.Bullish_52px;
            pictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;

            if (!metroPanel3.Controls.Contains(ucReports.instance))
            {
                metroPanel3.Controls.Add(ucReports.instance);
                ucReports.instance.Dock = DockStyle.Fill;
                ucReports.instance.BringToFront();
            }
        }

        private void lblUsername_Click(object sender, EventArgs e)
        {

        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to LogOut?", "CPanel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                frmStartPage openStartPage = new frmStartPage();
                openStartPage.Show();
            }
        }
        
    }
}
