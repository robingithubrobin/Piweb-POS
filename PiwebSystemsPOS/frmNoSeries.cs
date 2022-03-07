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
    public partial class frmNoSeries : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        private DataTable dt;
        public frmNoSeries()
        {
            InitializeComponent();
        }

        private void btnNoSeriesEntry_Click(object sender, EventArgs e)
        {
            frmNoSeriesEntry openNoSeriesEntry = new frmNoSeriesEntry();
            openNoSeriesEntry.ShowDialog();

            txtCode.Text = TransactionsHelper.noSeriesCode;
            TransactionsHelper.noSeriesCode = "";
        }

        private void frmNoSeries_Load(object sender, EventArgs e)
        {

        }

        private void loadGridView()
        {
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

            dt = new DataTable();

            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("DefaultNos", typeof(string));//
            dt.Columns.Add("ManualNos", typeof(int));
            dt.Columns.Add("DateOrder", typeof(int));

            foreach (DataRow dr in piwebDataOps.GetNoSeriesEntry().Rows)
            {
                string Code = dr["Code"].ToString(),
                    Description = dr["Description"].ToString(),
                    DefaultNos = dr["DefaultNos"].ToString(),
                    ManualNos = dr["ManualNos"].ToString(),
                    DateOrder = dr["DateOrder"].ToString();

                dt.Rows.Add(Code, Description, DefaultNos, ManualNos, DateOrder);
            }

            dataGridView1.DataSource = dt;

            dataGridView1.Columns["Code"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["DefaultNos"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["ManualNos"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["DateOrder"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.AllowUserToAddRows = false;
        }
    }
}
