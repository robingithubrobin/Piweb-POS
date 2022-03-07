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
    public partial class frmOnScreenKeyboard : Form
    {

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams param = base.CreateParams;
                param.ExStyle |= 0x08000000;
                return param;
            }
        }

        private string onComingValue;
        public frmOnScreenKeyboard(string value)
        {
            InitializeComponent();
            onComingValue = value;
        }

        private void frmOnScreenKeyboard_Load(object sender, EventArgs e)
        {
            textValue.Focus();
            if (onComingValue == "psw")
            {
                textValue.UseSystemPasswordChar = true;
            }
        }
        private void ckCapsLock_CheckedChanged(object sender, EventArgs e)
        {
            if (ckCapsLock.Checked)
                panelCapsLock.BackColor = Color.Lime;
            else
                panelCapsLock.BackColor = SystemColors.WindowFrame;
        }

        private void btnClick_Letters(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if (ckCapsLock.Checked)
            {
                b.Text.ToUpper();
                textValue.Text = textValue.Text + b.Text.ToUpper();
            }
            else
            {
                b.Text.ToLower();
                textValue.Text = textValue.Text + b.Text.ToLower();
            }

        }
        private void btnClick_Num(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if (ckNum.Checked)
            {
                textValue.Text = textValue.Text + b.Text;
            }
            else
            {
                return;
            }
            //SendKeys.Send(this.Text);
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            int length = textValue.TextLength - 1;
            string text = textValue.Text;
            textValue.Clear();
            for (int i = 0; i < length; i++)
            {
                textValue.Text = textValue.Text + text[i];
            }
        }

        private void ckNum_CheckedChanged(object sender, EventArgs e)
        {
            if (ckNum.Checked)
                panelNumLock.BackColor = Color.Lime;
            else
                panelNumLock.BackColor = SystemColors.WindowFrame;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            switch (onComingValue)
            {
                case "usernameField":
                    UserSession.keyField = "txtuser";
                    UserSession.onComingValue = textValue.Text;
                    break;
                case "psw":
                    UserSession.keyField = "txtpass";
                    UserSession.onComingValue = textValue.Text;
                    break;
            }

            this.Close();

        }

        private void textValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEnter_Click(sender, e);
            }
        }
    }
}
