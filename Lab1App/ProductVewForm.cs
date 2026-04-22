using Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LabApp
{
    // Форма просмотра объекта
    public partial class ProductVewForm : Form
    {
        public ProductVewForm(ProductLib.Product product)
        {
            InitializeComponent();

            // Заполняем поля формы - вся информация об изделии
            textBoxName.Text = product.Name;
            textBoxType.Text = product.Type;
            textBoxCode.Text = product.Article.ToString();

            if(product is IIncludeComponents prod)
            {
                foreach(string comp in prod.Components)
                {
                    listBoxComponents.Items.Add(comp);
                }
            }
            else
            {
                listBoxComponents.Visible = false;
                labelComponents.Visible = false;
            }
        }
    }
}
