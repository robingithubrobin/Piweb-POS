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
    public partial class frmSalesOrders : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();
        DataTable dt;
        public frmSalesOrders()
        {
            InitializeComponent();
        }

        private void btnViewQuotes_Click(object sender, EventArgs e)
        {
            frmSalesQuoteList openSalesQuoteList = new frmSalesQuoteList();
            openSalesQuoteList.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmCreateSalesOrder openCreateSalesOrder = new frmCreateSalesOrder();
            openCreateSalesOrder.ShowDialog();
        }

        void loadGridView()
        {
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

            dt = new DataTable();

            dt.Columns.Add("Sales Date", typeof(string));
            dt.Columns.Add("Product Code", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Qty", typeof(string));
            dt.Columns.Add("Price", typeof(string));
            dt.Columns.Add("Line Amount", typeof(string));

            foreach (DataRow row in piwebDataOps.GetSalesInvoicesLines().Rows)
            {
                DateTime salesDate = Convert.ToDateTime(row["CreatedDate"].ToString());
                double qty = Convert.ToDouble(row["Quantity"].ToString());
                decimal unitPrice = Convert.ToDecimal(row["UnitPrice"].ToString()), amount = Convert.ToDecimal(row["LinePrice"].ToString());
                var formattedUnitPrice = String.Format("{0:N}", unitPrice);
                var formattedLinePrice = String.Format("{0:N}", amount * Convert.ToDecimal(qty));
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
        void loadGridView(string text, DateTime from, DateTime to)
        {
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

            dt = new DataTable();

            dt.Columns.Add("Sales Date", typeof(string));
            dt.Columns.Add("Product Code", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Qty", typeof(string));
            dt.Columns.Add("Price", typeof(string));
            dt.Columns.Add("Line Amount", typeof(string));

            foreach (DataRow row in piwebDataOps.GetSalesInvoicesLines(text, from, to).Rows)
            {
                DateTime salesDate = Convert.ToDateTime(row["CreatedDate"].ToString());
                double qty = Convert.ToDouble(row["Quantity"].ToString());
                decimal unitPrice = Convert.ToDecimal(row["UnitPrice"].ToString()), amount = Convert.ToDecimal(row["LinePrice"].ToString());
                var formattedUnitPrice = String.Format("{0:N}", unitPrice);
                var formattedLinePrice = String.Format("{0:N}", amount * Convert.ToDecimal(qty));
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
        void loadGridView(string text)
        {
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

            dt = new DataTable();

            dt.Columns.Add("Sales Date", typeof(string));
            dt.Columns.Add("Product Code", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Qty", typeof(string));
            dt.Columns.Add("Price", typeof(string));
            dt.Columns.Add("Line Amount", typeof(string));

            foreach (DataRow row in piwebDataOps.GetSalesInvoicesLines_stringSearch(text).Rows)
            {
                DateTime salesDate = Convert.ToDateTime(row["CreatedDate"].ToString());
                double qty = Convert.ToDouble(row["Quantity"].ToString());
                decimal unitPrice = Convert.ToDecimal(row["UnitPrice"].ToString()), amount = Convert.ToDecimal(row["LinePrice"].ToString());
                var formattedUnitPrice = String.Format("{0:N}", unitPrice);
                var formattedLinePrice = String.Format("{0:N}", amount * Convert.ToDecimal(qty));
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

        private void frmSalesOrders_Load(object sender, EventArgs e)
        {
            chkDateFilter.Checked = false;
            panelDateFilter.Visible = false;
            panel1.Height = 63;
            loadGridView();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadGridView();
        }

        private void chkDateFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDateFilter.Checked == true)
            {
                panelDateFilter.Visible = true;
                panel1.Height = 103;
            }
            else
            {
                panelDateFilter.Visible = false;
                panel1.Height = 69;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            DateTime fromDate = dtpFrom.Value, toDate = dtpTo.Value;

            if (chkDateFilter.Checked == true)
            {
                loadGridView(searchTerm, fromDate, toDate);
            }
            else
            {
                loadGridView(searchTerm);
            }
            
        }
    }
}