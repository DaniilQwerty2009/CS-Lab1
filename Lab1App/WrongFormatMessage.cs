using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LabApp
{
    // Форма-сообщение об ошибке формата введенных данных
    public partial class WrongFormatMessage : Form
    {
        public WrongFormatMessage()
        {
            InitializeComponent();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
