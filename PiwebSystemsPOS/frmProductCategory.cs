using PiwebSystemsPOS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiwebSystemsPOS
{
    public partial class frmProductCategory : MetroFramework.Forms.MetroForm
    {
        PiwebSystems piwebDataOps = new PiwebSystems();

        private static string connString = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        private static SqlConnection sqlConn = new SqlConnection(connString);
        private static SqlCommand cmd;
        private static SqlDataAdapter sda;
        private static DataTable dt;

        OpenFileDialog openFile = new OpenFileDialog();
        public frmProductCategory()
        {
            InitializeComponent();
            txtNo.Text = LoadSerials();
        }
        public string LoadSerials()
        {
            var serialNo = "";

            cmd = new SqlCommand("(Select isnull(MAX([CategoryCode]),1000) + 1 AS 'Serial' from INV_ProductCategory)", sqlConn);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            serialNo = dt.Rows[0]["Serial"].ToString();

            return serialNo;
        }

        private void lnkImportImage_Click(object sender, EventArgs e)
        {
            openFile.InitialDirectory = "C:\\";
            openFile.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png|All Files(*.*)|*.*";
            openFile.FilterIndex = 1;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFile.FileName);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string no = txtNo.Text.Trim(),
                categoryName = txtCategoryName.Text.Trim(),
                fileName = "", parentCategoryCode = "", discountGroupCode = "";

            if (!string.IsNullOrEmpty(cmbParentCategory.Text))
                parentCategoryCode = cmbParentCategory.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(cmbDiscountGroup.Text))
                discountGroupCode = cmbDiscountGroup.SelectedValue.ToString();

            //if (openFile.CheckFileExists)
            //{
            //    pictureBox1.Image = Image.FromFile(openFile.FileName);
            //    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            //    string path = Application.StartupPath; //.Substring(0, Application.StartupPath.Length - 10);
            //    fileName = Path.GetFileName(openFile.FileName);
            //    File.Copy(openFile.FileName, path + "\\Images\\" + fileName);
            //}

            piwebDataOps.CreateProductCategory(no, parentCategoryCode, categoryName, discountGroupCode, fileName);

            MessageBox.Show("Category "+categoryName+" has been created successfully","Success",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lnkClear_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Image = global::PiwebSystemsPOS.Properties.Resources.icon;
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            openFile.InitialDirectory = "C:\\";
            openFile.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png|All Files(*.*)|*.*";
            openFile.FilterIndex = 1;

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFile.FileName);
            }
        }

        private void frmProductCategory_Load(object sender, EventArgs e)
        {
            txtNo.ReadOnly = true;
            txtNo.Text = LoadSerials();
        }
    }
}
