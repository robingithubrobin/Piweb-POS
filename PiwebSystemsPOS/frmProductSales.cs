using PiwebSystemsPOS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiwebSystemsPOS
{
    public partial class frmProductSales : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        DataTable dt;
        public frmProductSales()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void frmProductSales_Load(object sender, EventArgs e)
        {

            //  Load dateTimepickers with Current Date
            dtStartDate.Value = DateTime.Now;
            dtEndDate.Value = DateTime.Now;

            //  Display Start and end Date to labelsc
            DateTime from = Convert.ToDateTime(dtStartDate.Text), to = Convert.ToDateTime(dtEndDate.Text);
            lblFrom.Text = from.ToString("MM/dd/yyyy");
            lblTo.Text = to.ToString("MM/dd/yyyy");
            //DataTable sales = piwebDataOps.GetSalesInvoicesLines();
            DateTime startDate = Convert.ToDateTime(dtStartDate.Text), endDate = Convert.ToDateTime(dtEndDate.Text);
            loadGridView(startDate,endDate);

            // Total Sales
            var sumAmount = dataGridView1.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells[5].Value));
            var formatSum = String.Format("{0:N}", sumAmount);
            lblSum.Text = formatSum;
        }

        void loadGridView(DateTime startDate, DateTime endDate)
        {
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

            dt = new DataTable();

            dt.Columns.Add("Sales Date", typeof(string));
            dt.Columns.Add("Product Code", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Qty", typeof(string));
            dt.Columns.Add("Price", typeof(string));
            dt.Columns.Add("Line Amount", typeof(string));

            foreach (DataRow row in piwebDataOps.GetSalesInvoicesLines(startDate, endDate).Rows)
            {
                DateTime salesDate = Convert.ToDateTime(row["CreatedDate"].ToString());
                double qty = Convert.ToDouble(row["Quantity"].ToString());
                decimal unitPrice = Convert.ToDecimal(row["UnitPrice"].ToString()), amount = Convert.ToDecimal(row["LinePrice"].ToString());
                var formattedUnitPrice = String.Format("{0:N}", unitPrice);
                var formattedLinePrice = String.Format("{0:N}", amount*Convert.ToDecimal(qty));
                var formattedDate = salesDate.ToString("MM/dd/yyyy");
                dt.Rows.Add(
                        formattedDate,
                        row["ProductCode"].ToString(),
                        row["Description"].ToString(),
                        qty.ToString(),
                        formattedUnitPrice,
                        formattedLinePrice
                    );
            }
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Sales Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Sales Date"].HeaderCell.Style.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
            dataGridView1.Columns["Product Code"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Product Code"].HeaderCell.Style.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
            dataGridView1.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Description"].HeaderCell.Style.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
            //dataGridView1.Columns["Qty"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Qty"].HeaderCell.Style.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
            dataGridView1.Columns["Price"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Price"].HeaderCell.Style.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
            dataGridView1.Columns["Line Amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Line Amount"].HeaderCell.Style.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
            dataGridView1.Columns["Sales Date"].ReadOnly = true;
            dataGridView1.Columns["Sales Date"].HeaderCell.Style.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
            dataGridView1.Columns["Product Code"].ReadOnly = true;
            dataGridView1.Columns["Product Code"].HeaderCell.Style.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
            dataGridView1.Columns["Description"].ReadOnly = true;
            dataGridView1.Columns["Description"].HeaderCell.Style.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
            dataGridView1.Columns["Qty"].ReadOnly = true;
            dataGridView1.Columns["Qty"].HeaderCell.Style.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
            dataGridView1.Columns["Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["Price"].ReadOnly = true;
            dataGridView1.Columns["Price"].HeaderCell.Style.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
            dataGridView1.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["Line Amount"].ReadOnly = true;
            dataGridView1.Columns["Line Amount"].HeaderCell.Style.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
            dataGridView1.Columns["Line Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //  Display Start and end Date to labelsc
            DateTime from = Convert.ToDateTime(dtStartDate.Text), to = Convert.ToDateTime(dtEndDate.Text);
            lblFrom.Text = from.ToString("MM/dd/yyyy");
            lblTo.Text = to.ToString("MM/dd/yyyy");

            DateTime startDate = Convert.ToDateTime(dtStartDate.Text), endDate = Convert.ToDateTime(dtEndDate.Text);
            loadGridView(startDate, endDate);

            // Total Sales
            var sumAmount = dataGridView1.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells[5].Value));
            var formatSum = String.Format("{0:N}", sumAmount);
            lblSum.Text = formatSum;
        }
    }
}
