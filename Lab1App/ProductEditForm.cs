using Interfaces;
using ProductLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LabApp
{
    // Форма редактирования объекта
    public partial class ProductEditForm : Form
    {
        // Копия объекта для редактирвания
        Product product;

        public ProductEditForm(Product product)
        {
            InitializeComponent();

            this.product = product.Clone();

            // Заполняем изменяемые поля данными редактируемого объекта
            textBoxName.Text = product.Name;

            if (product is IIncludeComponents prod)
            {
                foreach (string comp in prod.Components)
                {
                    listBoxComponents.Items.Add(comp);
                }
            }
            else
            {
                labelComponents.Visible = false;
                listBoxComponents.Visible = false;
                textBoxAddComponent.Visible = false;
                btnDelete.Visible = false;
                linkLabelAddComponent.Visible = false;
            }

            UpdateButtonAccessibility();
        }

        public Product Product
        {
            get { return product; }
        }

        // Кнопка Удалить компонент не кликабльна, пока не выбран хотя бы один
        private void UpdateButtonAccessibility()
        {
            if (listBoxComponents.SelectedItems.Count > 0)
                btnDelete.Enabled = true;
            else
                btnDelete.Enabled = false;
        }
        
        // Обработчик нажатия кнопки ОК
        private void BtnOK_Click(object sender, EventArgs e)
        {
            // Проверка на соостветсвие формату, вывод сообщения
            if (!ProductLib.Validator.StringValidate(textBoxName.Text))
            {
                WrongFormatMessage form = new();
                form.Show();
                return;
            }

            product.Name = textBoxName.Text;

            // Проверка на пустую комплекацию
            if (product is IIncludeComponents)
            {
                if (listBoxComponents.Items.Count == 0)
                {
                    ProductLib.EmptyComponentsExceptions ex = new();
                    MessageBox.Show(ex.Message);
                    return;
                }
                else
                {
                    // Переписываем копию объекта с данных формы (текст, список)
                    if (product is IIncludeComponents prod)
                        prod.CloneComponents(listBoxComponents.Items.Cast<string>());
                }
            }
            
            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        // Обработчик нажатия кнопки Отмена
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Обработчик нажатия кнопки Удалить (для комплектации)
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            // Исключаем только из списка
            if (listBoxComponents.SelectedItem != null)
            {     
                listBoxComponents.Items.Remove(listBoxComponents.SelectedItem);
                textBoxAddComponent.Text = "";
                textBoxAddComponent.Focus();
            }
        }

        // Обработчик выбора элемента списка
        private void listBoxComponents_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonAccessibility();
        }

        // Обработчик нажатия текста добавить (для комплектации)
        private void linkLabelAddComponent_Click(object sender, EventArgs e)
        {
            // Добавялем только в список после проверки
            if (ProductLib.Validator.StringValidate(textBoxAddComponent.Text))
            {
                listBoxComponents.Items.Add(textBoxAddComponent.Text);
                textBoxAddComponent.Text = "";
                textBoxAddComponent.Focus();
            }
            else
            {
                WrongFormatMessage msg = new();
                msg.Show();
            }
        }
    }
}
