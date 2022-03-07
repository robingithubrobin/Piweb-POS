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
    public partial class frmNoSeriesEntry : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        private DataTable dt;
        public frmNoSeriesEntry()
        {
            InitializeComponent();
        }

        private void loadGridView()
        {
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

            dt = new DataTable();

            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("DefaultNos", typeof(string));//
            dt.Columns.Add("ManualNos", typeof(int));

            foreach (DataRow dr in piwebDataOps.GetNoSeriesEntry().Rows)
            {
                string Code = dr["Code"].ToString(),
                    Description = dr["Description"].ToString(),
                    DefaultNos = dr["DefaultNos"].ToString(),
                    ManualNos = dr["ManualNos"].ToString();

                dt.Rows.Add(Code, Description, DefaultNos, ManualNos);
            }

            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.AllowUserToAddRows = false;
        }

        private void frmNoSeriesEntry_Load(object sender, EventArgs e)
        {
            loadGridView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string code = txtCode.Text.Trim();
            string description = txtDescription.Text.Trim();
            int defaultNos = chkDefaultNos.Checked == true ? 1 : 0;
            int manualNos = chkManualNos.Checked == true ? 1 : 0;

            piwebDataOps.CreateNoSeriesEntry(code, description, defaultNos, manualNos, 0);
            loadGridView();
            txtCode.Clear();
            txtDescription.Clear();
            chkDefaultNos.Checked = false;
            chkManualNos.Checked = false;
            txtCode.Focus();

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            TransactionsHelper.noSeriesCode = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            this.Close();
        }
    }
}
