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
    public partial class frmStockAdjustment : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();

         
        private static string connString = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private static SqlConnection sqlConn = new SqlConnection(connString);
        private static SqlCommand cmd;
        private static SqlDataAdapter sda;
        private static DataTable dt;
        public frmStockAdjustment()
        {
            InitializeComponent();
        }
        public string LoadSerials()
        {
            var serialNo = "";

            cmd = new SqlCommand("(Select isnull(MAX([AdjustmentID]),10000) + 1 AS 'Serial' from INV_Adjustments)", sqlConn);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            serialNo = dt.Rows[0]["Serial"].ToString();

            return serialNo;
        }
        private void frmStockAdjustment_Load(object sender, EventArgs e)
        {
            LoadGridView();
            FillAdjustmentType();
        }
        private void FillAdjustmentType()
        {
            //DataRow dr;
            string _query = "SELECT [AdjustmentTypeID],[AdjustmentType] FROM [dbo].[INV_AdjustmentType]";
            try
            {
                cmd = sqlConn.CreateCommand();
                cmd.CommandText = _query;

                sqlConn.Open();

                dt = new DataTable();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                cmbAdjustmentType.DisplayMember = "AdjustmentType";
                cmbAdjustmentType.ValueMember = "AdjustmentTypeID";
                cmbAdjustmentType.DataSource = dt;


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

        private void LoadGridView()
        {

            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

            dt = new DataTable();

            dt.Columns.Add("Product Code", typeof(string));
            dt.Columns.Add("Product", typeof(string));
            dt.Columns.Add("Current Stock", typeof(decimal));
            dt.Columns.Add("Quantity", typeof(decimal));
            dt.Columns.Add("Remarks", typeof(string));

            foreach (DataRow dr in piwebDataOps.GetProducts().Rows)
            {
                if (dr["CurrentStock"].ToString() == "")
                    dt.Rows.Add(dr["No"].ToString(), dr["ProductName"].ToString(), 0, 0);
                else
                    dt.Rows.Add(dr["No"].ToString(), dr["ProductName"].ToString(), dr["CurrentStock"], 0);
            }

            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Product Code"].ReadOnly = true;
            dataGridView1.Columns["Product"].ReadOnly = true;
            dataGridView1.Columns["Product"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Current Stock"].ReadOnly = true;
            dataGridView1.Columns["Current Stock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["Remarks"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (sqlConn.State == ConnectionState.Open)
            {
                sqlConn.Close();
            }

            //Initialize adjustmentTypeCode
            string adjustmentTypeCode = cmbAdjustmentType.Text;

            switch (adjustmentTypeCode)
            {
                case "Initial stock":
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        string serialNo = LoadSerials(); // Load New Serials for every iteration

                        string productCode = dataGridView1.Rows[i].Cells["Product Code"].Value.ToString();
                        string product = dataGridView1.Rows[i].Cells["Product"].Value.ToString();
                        decimal quantity = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString());
                        string remarks = dataGridView1.Rows[i].Cells["Remarks"].Value.ToString();
                        int adjustmentTypeID = Convert.ToInt32(cmbAdjustmentType.SelectedValue.ToString());

                        if (quantity != 0)
                        {
                            try
                            {

                                //Insert Data into INV_Adjustment
                                piwebDataOps.CreateProductAdjustment(serialNo,productCode, quantity, remarks, UserSession.userName, adjustmentTypeID);

                                //Insert Data int INV_InventoryStock
                                piwebDataOps.CreateInventoryStock("INVADJUST", serialNo, productCode, quantity, 0, UserSession.userName);

                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message, piwebDataOps.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    LoadGridView();
                    break;
                case "Over shipments":
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        string serialNo = LoadSerials(); // Load New Serials for every iteration

                        string productCode = dataGridView1.Rows[i].Cells["Product Code"].Value.ToString();
                        string product = dataGridView1.Rows[i].Cells["Product"].Value.ToString();
                        decimal quantity = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString());
                        string remarks = dataGridView1.Rows[i].Cells["Remarks"].Value.ToString();
                        int adjustmentTypeID = Convert.ToInt32(cmbAdjustmentType.SelectedValue.ToString());

                        if (quantity != 0)
                        {
                            try
                            {

                                //Insert Data into INV_Adjustment
                                piwebDataOps.CreateProductAdjustment(serialNo, productCode, quantity, remarks, UserSession.userName, adjustmentTypeID);

                                //Insert Data int INV_InventoryStock
                                piwebDataOps.CreateInventoryStock("INVADJUST", serialNo, productCode, quantity, 0, UserSession.userName);

                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message, piwebDataOps.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    LoadGridView();
                    break;
                case "Incorrect receivings":
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        string serialNo = LoadSerials(); // Load New Serials for every iteration

                        string productCode = dataGridView1.Rows[i].Cells["Product Code"].Value.ToString();
                        string product = dataGridView1.Rows[i].Cells["Product"].Value.ToString();
                        decimal quantity = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString());
                        string remarks = dataGridView1.Rows[i].Cells["Remarks"].Value.ToString();
                        int adjustmentTypeID = Convert.ToInt32(cmbAdjustmentType.SelectedValue.ToString());

                        if (quantity != 0)
                        {
                            try
                            {
                                //Insert Data into INV_Adjustment
                                piwebDataOps.CreateProductAdjustment(serialNo, productCode, quantity, remarks, UserSession.userName, adjustmentTypeID);

                                //Insert Data int INV_InventoryStock
                                piwebDataOps.CreateInventoryStock("INVADJUST", serialNo, productCode, 0, quantity, UserSession.userName);

                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message, piwebDataOps.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    LoadGridView();
                    break;
                case "Breakage":
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        string serialNo = LoadSerials(); // Load New Serials for every iteration

                        string productCode = dataGridView1.Rows[i].Cells["Product Code"].Value.ToString();
                        string product = dataGridView1.Rows[i].Cells["Product"].Value.ToString();
                        decimal quantity = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString());
                        string remarks = dataGridView1.Rows[i].Cells["Remarks"].Value.ToString();
                        int adjustmentTypeID = Convert.ToInt32(cmbAdjustmentType.SelectedValue.ToString());

                        if (quantity != 0)
                        {
                            try
                            {
                                //Insert Data into INV_Adjustment
                                piwebDataOps.CreateProductAdjustment(serialNo, productCode, quantity, remarks, UserSession.userName, adjustmentTypeID);

                                //Insert Data int INV_InventoryStock
                                piwebDataOps.CreateInventoryStock("INVADJUST", serialNo, productCode, 0, quantity, UserSession.userName);

                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message, piwebDataOps.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    LoadGridView();
                    break;
                case "Wastage":
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        string serialNo = LoadSerials(); // Load New Serials for every iteration

                        string productCode = dataGridView1.Rows[i].Cells["Product Code"].Value.ToString();
                        string product = dataGridView1.Rows[i].Cells["Product"].Value.ToString();
                        decimal quantity = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString());
                        string remarks = dataGridView1.Rows[i].Cells["Remarks"].Value.ToString();
                        int adjustmentTypeID = Convert.ToInt32(cmbAdjustmentType.SelectedValue.ToString());

                        if (quantity != 0)
                        {
                            try
                            {
                                //Insert Data into INV_Adjustment
                                piwebDataOps.CreateProductAdjustment(serialNo, productCode, quantity, remarks, UserSession.userName, adjustmentTypeID);

                                //Insert Data int INV_InventoryStock
                                piwebDataOps.CreateInventoryStock("INVADJUST", serialNo, productCode, 0, quantity, UserSession.userName);

                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message, piwebDataOps.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    LoadGridView();
                    break;
                case "Theft":
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        string serialNo = LoadSerials(); // Load New Serials for every iteration

                        string productCode = dataGridView1.Rows[i].Cells["Product Code"].Value.ToString();
                        string product = dataGridView1.Rows[i].Cells["Product"].Value.ToString();
                        decimal quantity = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString());
                        string remarks = dataGridView1.Rows[i].Cells["Remarks"].Value.ToString();
                        int adjustmentTypeID = Convert.ToInt32(cmbAdjustmentType.SelectedValue.ToString());

                        if (quantity != 0)
                        {
                            try
                            {
                                //Insert Data into INV_Adjustment
                                piwebDataOps.CreateProductAdjustment(serialNo, productCode, quantity, remarks, UserSession.userName, adjustmentTypeID);

                                //Insert Data int INV_InventoryStock
                                piwebDataOps.CreateInventoryStock("INVADJUST", serialNo, productCode, 0, quantity, UserSession.userName);

                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message, piwebDataOps.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    LoadGridView();
                    break;
                case "Loss":
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        string serialNo = LoadSerials(); // Load New Serials for every iteration

                        string productCode = dataGridView1.Rows[i].Cells["Product Code"].Value.ToString();
                        string product = dataGridView1.Rows[i].Cells["Product"].Value.ToString();
                        decimal quantity = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString());
                        string remarks = dataGridView1.Rows[i].Cells["Remarks"].Value.ToString();
                        int adjustmentTypeID = Convert.ToInt32(cmbAdjustmentType.SelectedValue.ToString());

                        if (quantity != 0)
                        {
                            try
                            {
                                //Insert Data into INV_Adjustment
                                piwebDataOps.CreateProductAdjustment(serialNo, productCode, quantity, remarks, UserSession.userName, adjustmentTypeID);

                                //Insert Data int INV_InventoryStock
                                piwebDataOps.CreateInventoryStock("INVADJUST", serialNo, productCode, 0, quantity, UserSession.userName);

                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message, piwebDataOps.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    LoadGridView();
                    break;
                case "Others - StockIn":
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        string serialNo = LoadSerials(); // Load New Serials for every iteration

                        string productCode = dataGridView1.Rows[i].Cells["Product Code"].Value.ToString();
                        string product = dataGridView1.Rows[i].Cells["Product"].Value.ToString();
                        decimal quantity = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString());
                        string remarks = dataGridView1.Rows[i].Cells["Remarks"].Value.ToString();
                        int adjustmentTypeID = Convert.ToInt32(cmbAdjustmentType.SelectedValue.ToString());

                        if (quantity != 0)
                        {
                            try
                            {

                                //Insert Data into INV_Adjustment
                                piwebDataOps.CreateProductAdjustment(serialNo, productCode, quantity, remarks, UserSession.userName, adjustmentTypeID);

                                //Insert Data int INV_InventoryStock
                                piwebDataOps.CreateInventoryStock("INVADJUST", serialNo, productCode, quantity, 0, UserSession.userName);

                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message, piwebDataOps.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    LoadGridView();
                    break;
                case "Others - StockOut":
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        string serialNo = LoadSerials(); // Load New Serials for every iteration

                        string productCode = dataGridView1.Rows[i].Cells["Product Code"].Value.ToString();
                        string product = dataGridView1.Rows[i].Cells["Product"].Value.ToString();
                        decimal quantity = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString());
                        string remarks = dataGridView1.Rows[i].Cells["Remarks"].Value.ToString();
                        int adjustmentTypeID = Convert.ToInt32(cmbAdjustmentType.SelectedValue.ToString());

                        if (quantity != 0)
                        {
                            try
                            {
                                //Insert Data into INV_Adjustment
                                piwebDataOps.CreateProductAdjustment(serialNo, productCode, quantity, remarks, UserSession.userName, adjustmentTypeID);

                                //Insert Data int INV_InventoryStock
                                piwebDataOps.CreateInventoryStock("INVADJUST", serialNo, productCode, 0, quantity, UserSession.userName);

                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message, piwebDataOps.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    LoadGridView();
                    break;
                case "Physical Count-IN":
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        string serialNo = LoadSerials(); // Load New Serials for every iteration

                        string productCode = dataGridView1.Rows[i].Cells["Product Code"].Value.ToString();
                        string product = dataGridView1.Rows[i].Cells["Product"].Value.ToString();
                        decimal quantity = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString());
                        string remarks = dataGridView1.Rows[i].Cells["Remarks"].Value.ToString();
                        int adjustmentTypeID = Convert.ToInt32(cmbAdjustmentType.SelectedValue.ToString());

                        if (quantity != 0)
                        {
                            try
                            {

                                //Insert Data into INV_Adjustment
                                piwebDataOps.CreateProductAdjustment(serialNo, productCode, quantity, remarks, UserSession.userName, adjustmentTypeID);

                                //Insert Data int INV_InventoryStock
                                piwebDataOps.CreateInventoryStock("INVADJUST", serialNo, productCode, quantity, 0, UserSession.userName);

                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message, piwebDataOps.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    LoadGridView();
                    break;
                case "Physical Count-OUT":
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        string serialNo = LoadSerials(); // Load New Serials for every iteration

                        string productCode = dataGridView1.Rows[i].Cells["Product Code"].Value.ToString();
                        string product = dataGridView1.Rows[i].Cells["Product"].Value.ToString();
                        decimal quantity = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString());
                        string remarks = dataGridView1.Rows[i].Cells["Remarks"].Value.ToString();
                        int adjustmentTypeID = Convert.ToInt32(cmbAdjustmentType.SelectedValue.ToString());

                        if (quantity != 0)
                        {
                            try
                            {
                                //Insert Data into INV_Adjustment
                                piwebDataOps.CreateProductAdjustment(serialNo, productCode, quantity, remarks, UserSession.userName, adjustmentTypeID);

                                //Insert Data int INV_InventoryStock
                                piwebDataOps.CreateInventoryStock("INVADJUST", serialNo, productCode, 0, quantity, UserSession.userName);

                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message, piwebDataOps.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    LoadGridView();
                    break;
                case "Spoilt":
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        string serialNo = LoadSerials(); // Load New Serials for every iteration

                        string productCode = dataGridView1.Rows[i].Cells["Product Code"].Value.ToString();
                        string product = dataGridView1.Rows[i].Cells["Product"].Value.ToString();
                        decimal quantity = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString());
                        string remarks = dataGridView1.Rows[i].Cells["Remarks"].Value.ToString();
                        int adjustmentTypeID = Convert.ToInt32(cmbAdjustmentType.SelectedValue.ToString());

                        if (quantity != 0)
                        {
                            try
                            {
                                //Insert Data into INV_Adjustment
                                piwebDataOps.CreateProductAdjustment(serialNo, productCode, quantity, remarks, UserSession.userName, adjustmentTypeID);

                                //Insert Data int INV_InventoryStock
                                piwebDataOps.CreateInventoryStock("INVADJUST", serialNo, productCode, 0, quantity, UserSession.userName);

                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message, piwebDataOps.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    LoadGridView();
                    break;
                case "Contra Count-IN":
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        string serialNo = LoadSerials(); // Load New Serials for every iteration

                        string productCode = dataGridView1.Rows[i].Cells["Product Code"].Value.ToString();
                        string product = dataGridView1.Rows[i].Cells["Product"].Value.ToString();
                        decimal quantity = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString());
                        string remarks = dataGridView1.Rows[i].Cells["Remarks"].Value.ToString();
                        int adjustmentTypeID = Convert.ToInt32(cmbAdjustmentType.SelectedValue.ToString());

                        if (quantity != 0)
                        {
                            try
                            {

                                //Insert Data into INV_Adjustment
                                piwebDataOps.CreateProductAdjustment(serialNo, productCode, quantity, remarks, UserSession.userName, adjustmentTypeID);

                                //Insert Data int INV_InventoryStock
                                piwebDataOps.CreateInventoryStock("INVADJUST", serialNo, productCode, quantity, 0, UserSession.userName);

                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message, piwebDataOps.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    LoadGridView();
                    break;
                case "Contra Count-OUT":
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        string serialNo = LoadSerials(); // Load New Serials for every iteration

                        string productCode = dataGridView1.Rows[i].Cells["Product Code"].Value.ToString();
                        string product = dataGridView1.Rows[i].Cells["Product"].Value.ToString();
                        decimal quantity = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString());
                        string remarks = dataGridView1.Rows[i].Cells["Remarks"].Value.ToString();
                        int adjustmentTypeID = Convert.ToInt32(cmbAdjustmentType.SelectedValue.ToString());

                        if (quantity != 0)
                        {
                            try
                            {
                                //Insert Data into INV_Adjustment
                                piwebDataOps.CreateProductAdjustment(serialNo, productCode, quantity, remarks, UserSession.userName, adjustmentTypeID);

                                //Insert Data int INV_InventoryStock
                                piwebDataOps.CreateInventoryStock("INVADJUST", serialNo, productCode, 0, quantity, UserSession.userName);

                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message, piwebDataOps.appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    LoadGridView();
                    break;

            }
        }
    }
}
