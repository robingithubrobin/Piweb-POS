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
    public partial class frmCreateDevice : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();

        string createdBy = "SYSTEM";
        public frmCreateDevice()
        {
            InitializeComponent();
        }

        private void metroTabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //
            // Tab 1
            //
            string deviceModel = txtDeviceName.Text.Trim(),
                serialNo = txtSerialNo.Text.Trim(),
                workStation = txtWorkStation.Text.Trim(),
                computerName = txtCompName.Text.Trim();
            

            piwebDataOps.CreateDevice(deviceModel, serialNo, workStation,computerName, createdBy);
            MessageBox.Show("Device Registered successfully","Device Register", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnSetThis_Click(object sender, EventArgs e)
        {
            txtCompName.Text = Environment.MachineName;
        }

        private void chkSetThis_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSetThis.Checked)
                txtCompName.Text = Environment.MachineName;
            else
                txtCompName.Text = string.Empty;
        }
    }
}
