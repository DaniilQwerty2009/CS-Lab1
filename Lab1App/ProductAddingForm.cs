using Interfaces;
using ProductLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using static LabApp.ApplicationForm;
using static ProductLib.Validator;

namespace LabApp
{
    // Форма добавления объекта
    public partial class ProductAddingForm : Form
    {
        // Создаваемый объект
        Product? product;
        Mover? mover;

        Point borderOfVisual;

        //List<TypesOfProduct> types;
        TypeDisplay? selectedType;

        public ProductAddingForm(Point borderOfVisual)
        {
            InitializeComponent();

            this.borderOfVisual = borderOfVisual;

            ComponentsEnable = false;

            foreach(TypesOfProduct s in Enum.GetValues(typeof(TypesOfProduct)))
            {
                TypeDisplay typeForUI = new TypeDisplay(s, s.GetDescription());

                comboBoxTypes.Items.Add(typeForUI);
            }

        }

        public Product Product
        {
            get
            {
                if (product == null)
                    throw new InvalidOperationException("Объект не был создан.");
                return product;
            }
        }

        public Mover Mover
        {
            get
            {
                if (mover == null)
                    throw new InvalidOperationException("Объект не был создан.");
                return mover;
            }
        }

        bool ComponentsEnable
        {
            set
            {
                if(value == true)
                {
                    labelComponents.Visible = true;
                    listBoxComponents.Visible = true;
                    textBoxAddingComponent.Visible = true;
                    linkLabelAddComponent.Visible = true;
                }
                else
                {
                    labelComponents.Visible = false;
                    listBoxComponents.Visible = false;
                    textBoxAddingComponent.Visible = false;
                    linkLabelAddComponent.Visible = false;
                }
            }
        }
        
        // Обработчик нажатия кнопки Добавить
        private void BtnAdd_Click(object sender, EventArgs e)
        {

            if (!Validator.StringValidate(textBoxName.Text))
            {
                MessageBox.Show("Некорректное название товара!");
                return;
            }

            // Проверка артикула, вывод сообщения
            if(string.IsNullOrEmpty(textBoxCode.Text))
            {
                MessageBox.Show("Введите артикул!");
                return;
            }

            long article = 0;
            foreach (char ch in textBoxCode.Text)
            {
                if (ch < '0' || ch > '9')
                {
                    MessageBox.Show("Некорректный артикул!");
                    return;
                }

                // Число-строка -> число
                article = article * 10 + (ch - '0');
            }

            if (comboBoxTypes.SelectedItem == null || selectedType == null)
            {
                MessageBox.Show("Такого типа нет в системе!");
                return;
            }



            Point beginPos;
            //Point borderPoint = new Point(borderOfVisual.X, borderOfVisual.Y);
            switch(selectedType.Value)
            {
                case TypesOfProduct.Dish:

                    
                    beginPos = Mover.GetRandomPoint(borderOfVisual);
                    product = new Dishes(textBoxName.Text, article, selectedType.Text);
                    

                    int speed = ((int)SpeedOfProductVisualisation.ThitryPixelPerSecond);

                    if (product is IDrawable dish)
                    {

                        dish.VisualPosition = beginPos;
                        dish.SizeOfVisual = new Size((int)SizeOfPaintedImgEnum.X, (int)SizeOfPaintedImgEnum.Y);
                        mover = new RandomMover(dish, speed, borderOfVisual);
                    }
                        

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;

                case TypesOfProduct.Furniture:
                    // Проверка на наличие комплектации, вывод сообщения
                    if (listBoxComponents.Items.Count < 1)
                    {
                        MessageBox.Show("Добавьте комплектацию!");
                        return;
                    }
                    else
                    {
                        List<string> componenets = new();
                        foreach (string comp in listBoxComponents.Items)
                        {
                            componenets.Add(comp);
                        }

                        beginPos = Mover.GetRandomPoint(borderOfVisual);

                        product = new Furniture(textBoxName.Text, article, selectedType.Text, componenets);

                        if (product is IDrawable furniture)
                        {
                            furniture.VisualPosition = beginPos;
                            furniture.SizeOfVisual = new Size((int)SizeOfPaintedImgEnum.X, (int)SizeOfPaintedImgEnum.Y);
                            mover = new LineralMover(furniture, new Point(0, 0), (int)SpeedOfProductVisualisation.ThitryPixelPerSecond);
                        }
                            
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    }
                default:
                    throw new Exception("Не удалось создать объект в операторе switch, BtnAdd_Click");
            }           
        }

        // Обработчик нажатия кнопки Отмена
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Обработчик нажатия текста Добавить (для комплектации)
        private void linkLabelAddComponent_LinkClicked(object sender, EventArgs e)
        {
            // Проверка на корректность ввода
            if (ProductLib.Validator.StringValidate(textBoxAddingComponent.Text))
            {
                listBoxComponents.Items.Add(textBoxAddingComponent.Text.ToLower());
                textBoxAddingComponent.Text = "";
                textBoxAddingComponent.Focus();
            }
            // Сообщение о некорректном вводе пользователю
            else
            {
                WrongFormatMessage msg = new();
                msg.ShowDialog();
            }
        }


        private void comboBoxTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTypes.SelectedIndex != -1 && comboBoxTypes.SelectedItem != null)
                selectedType = (TypeDisplay)comboBoxTypes.SelectedItem;

            if(selectedType != null && selectedType.Value == TypesOfProduct.Furniture)
            {
                ComponentsEnable = true;
            }
            else
                ComponentsEnable = false;
        }

        //internal void CreateDravableDish(TypesOfProduct type, string name, long article, out Mover mover, out Dishes product)
        //{
        //    Point beginPos;
        //    Point borderPoint = new Point(borderOfVisual.X, borderOfVisual.Y);

        //    beginPos = Mover.GetRandomPoint(borderPoint);
        //    product = new Dishes(textBoxName.Text, article, "Посуда");

        //    int speed = ((int)SpeedOfProductVisualisation.ThitryPixelPerSecond);

        //    ((IDrawable)product).VisualPosition = beginPos;
        //    ((IDrawable)product).SizeOfVisual = new Size((int)SizeOfPaintedImgEnum.X, (int)SizeOfPaintedImgEnum.Y);
        //    mover = new RandomMover(product, speed, borderOfVisual);

        //}


    }
}
