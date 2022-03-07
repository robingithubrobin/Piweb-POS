using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiwebSystemsPOS
{
    public partial class ucProductCard : MetroFramework.Controls.MetroUserControl
    {
        private static ucProductCard _instance;
        public static ucProductCard instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucProductCard();
                return _instance;
            }
        }
        //public string productName;
        #region Product Details
        public String _prodName
        {
            get
            {
                return lblProductName.Text;
            }
            set { lblProductName.Text = value; }
        }
        public String _prodDescription
        {
            set { txtDescription.Text = value; }
        }
        public String _category
        {
            set { lblCategory.Text = value; }
        }
        public String _uom
        {
            set { lblUOM.Text = value; }
        }
        public String _unitPrice
        {
            set { lblUnitPrice.Text = value; }
        }
        public String _photo
        {
            get
            {
                string _image = "";
                return _image;
            }
            set 
            {
                string _image = value;
                if(!string.IsNullOrEmpty(_image))
                {
                    string path = Application.StartupPath; //.Substring(0, Application.StartupPath.Length - 10);
                    pictureBox1.Image = Image.FromFile(path + "\\Images\\" + _image);
                }
                else
                {
                    pictureBox1.Image = global::PiwebSystemsPOS.Properties.Resources.icon;
                }
            }
        }
        public String _quantity
        {
            get
            {
                return lblQty.Text;
            }
            set 
            {
                string qty = value;
                if (qty == "")
                    lblQty.Text = "0";
                else
                    lblQty.Text = qty;


            }
        }
        #endregion
        public ucProductCard()
        {
            InitializeComponent();

        }

        private void ucProductCard_Load(object sender, EventArgs e)
        {

        }
    }
}
