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
    public partial class frmDeviceList : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        DataTable dt;
        public frmDeviceList()
        {
            InitializeComponent();
        }

        private void LoadGridView()
        {

            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

            dt = new DataTable();

                dt.Columns.Add("Model", typeof(string));
                dt.Columns.Add("Serial No", typeof(string));
                dt.Columns.Add("WorkStation", typeof(string));

                foreach (DataRow dr in piwebDataOps.GetDevices().Rows)
                {

                    dt.Rows.Add(dr["Model"].ToString(), dr["SerialNo"].ToString(), dr["WorkStation"].ToString());
                }

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["Model"].ReadOnly = true;
                dataGridView1.Columns["Serial No"].ReadOnly = true;
                dataGridView1.Columns["WorkStation"].ReadOnly = true;

                dataGridView1.Columns["Model"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns["Serial No"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns["WorkStation"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.AllowUserToAddRows = false;
        }

        private void frmDeviceList_Load(object sender, EventArgs e)
        {
            LoadGridView();
        }

        private void btnNewDevice_Click(object sender, EventArgs e)
        {
            frmCreateDevice openCreateDevice = new frmCreateDevice();
            openCreateDevice.ShowDialog();
            LoadGridView();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGridView();
        }
    }
}
