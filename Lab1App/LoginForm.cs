using ProductTcpShared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LabApp
{
    public partial class LoginForm : Form
    {
        public string Username { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            Username = string.Empty;
            //btnLogin.Enabled = false;
        }

        private void TextBoxNameTextChanged(object sender, EventArgs e)
        {
            if (textBoxName.Text.Length == 0)
                btnLogin.Enabled = false;
            else
                btnLogin.Enabled = true;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (ProductLib.Validator.StringValidate(textBoxName.Text))
                Username = textBoxName.Text;
            else
                return;


            this.Close();
        }
    }
}
