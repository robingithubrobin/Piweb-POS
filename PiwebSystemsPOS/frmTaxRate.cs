using PiwebSystemsPOS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiwebSystemsPOS
{
    public partial class frmTaxRate : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();

        private static string connString = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private static SqlConnection sqlConn = new SqlConnection(connString);
        private static SqlCommand cmd;
        private static SqlDataAdapter sda;
        private static DataTable dt;
        public frmTaxRate()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string taxGroupCode = "",
                taxCategory = txtTaxCategory.Text.Trim(),
                description = txtDescription.Text.Trim(),
                tax = txtTax.Text.Trim();
            if (!string.IsNullOrEmpty(cmbTaxGroup.Text))
                taxGroupCode = cmbTaxGroup.SelectedValue.ToString();

            piwebDataOps.CreateTaxRate(taxGroupCode, description, Convert.ToDecimal(tax), taxCategory);
            MessageBox.Show("Success");
        }

        private void frmTaxRate_Load(object sender, EventArgs e)
        {
            FillTaxGroupCode();
        }

        private void FillTaxGroupCode()
        {
            DataRow dr;
            string _query = "SELECT [Tax Group Code],[Description] FROM [dbo].[COM_Tax Group] ORDER BY [Tax Group Code] ASC";
            try
            {
                cmd = sqlConn.CreateCommand();
                cmd.CommandText = _query;

                sqlConn.Open();

                dt = new DataTable();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                dr = dt.NewRow();
                dr.ItemArray = new object[] { "" };
                dt.Rows.InsertAt(dr, 0);
                cmbTaxGroup.DisplayMember = "Tax Group Code";
                cmbTaxGroup.ValueMember = "Tax Group Code";
                cmbTaxGroup.DataSource = dt;

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }
        }
    }
}
