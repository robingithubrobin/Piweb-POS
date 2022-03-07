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
    public partial class frmSplashScreen : Form
    {
        public frmSplashScreen()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panelSlide.Left += 2;

            if (panelSlide.Left > 290)
            {
                panelSlide.Left = 0;
            }
            //if (panelSlide.Left < 0)
            //{
            //    panelSlide.Left += 2;
            //}
        }

        private void frmSplashScreen_Load(object sender, EventArgs e)
        {
            timer1.Start();
            //frmStartPage openStartPage = new frmStartPage();
            //openStartPage.Show();
            //this.Close();
        }
    }
}
