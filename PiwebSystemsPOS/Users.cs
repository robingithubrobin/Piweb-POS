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
    public partial class Users : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        DataTable dt;
        public Users()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Users_Load(object sender, EventArgs e)
        {
            LoadGridView();
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            frmCreateUser openCreateUser = new frmCreateUser();
            openCreateUser.ShowDialog();
        }
        private void LoadGridView()
        {

            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

            dt = new DataTable();

            dt.Columns.Add("User Name", typeof(string));
            dt.Columns.Add("Full Name", typeof(string));
            dt.Columns.Add("Role", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("Alt Phone", typeof(string));
            dt.Columns.Add("Expiry Date", typeof(DateTime));

            foreach (DataRow dr in piwebDataOps.GetUsers().Rows)
            {
                int roleIndex = Convert.ToInt32(dr["Role"]);
                string role = "";

                switch (roleIndex)
                {
                    case 0:
                        role = "Manager";
                        break;
                    case 1:
                        role = "Supervisor";
                        break;
                    case 2:
                        role = "Cashier";
                        break;
                    case 3:
                        role = "Administrator";
                        break;

                    default:
                        break;
                }


                DateTime expiryDate = Convert.ToDateTime(dr["ExpiryDate"].ToString());

                dt.Rows.Add(dr["UserName"].ToString(), dr["FullName"].ToString(), role, dr["Email"].ToString(), dr["Phone"].ToString(), dr["AltPhone"].ToString(), expiryDate);
            }

            dataGridView1.DataSource = dt;
            dataGridView1.Columns["User Name"].ReadOnly = true;
            dataGridView1.Columns["Full Name"].ReadOnly = true;
            dataGridView1.Columns["Role"].ReadOnly = true;
            dataGridView1.Columns["Email"].ReadOnly = true;
            dataGridView1.Columns["Phone"].ReadOnly = true;
            dataGridView1.Columns["Alt Phone"].ReadOnly = true;
            dataGridView1.Columns["Expiry Date"].ReadOnly = true;

            dataGridView1.Columns["User Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Full Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Role"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Phone"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Alt Phone"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Expiry Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.AllowUserToAddRows = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGridView();
        }
    }
}
