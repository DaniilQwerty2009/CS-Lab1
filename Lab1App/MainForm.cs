using Interfaces;
using ProductLib;
using System.ComponentModel;
using System.Drawing.Text;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security;
using System.Timers;
using System.Windows.Forms.VisualStyles;

namespace LabApp
{
    enum SpeedOfProductVisualisation { SomePixelPerSecond = 30};
    enum SizeOfPaintedImgEnum {  X = 50, Y = 50};

    internal enum TypesOfProduct { [Description("Мебель")] Furniture = 1, [Description("Посуда")] Dish = 2 };

    public class TypeDisplay
    {
        TypesOfProduct type;
        TypeDisplay(TypesOfProduct type)
        {
            this.type = type;
        }

        //public string Text
        //{
        //    get { return type.
        //}
    }


    // Основная форма приложения
    public partial class ApplicationForm : Form
    {
        // Основная коллекция для хранения объектов
        List<Product> products = new();

        // Текущий выбор из списка объектов
        Product? selectedInListProduct;

        //List<Mover> movers = new();

        /// <summary>
        /// For furniture
        /// </summary>
        Worker worker1;
        Thread? firstThread;

        /// <summary>
        /// For dishes
        /// </summary>
        Worker worker2;
        Thread? secondThread;

        //List<Thread> threads = new();


        public ApplicationForm()
        {

            InitializeComponent();

            worker1 = new();

            worker2 = new();


            CreateExampleProducts(2, 2);

            worker1.AnotherPartOfWorkDone += RedrawPanelVisualisation;
            worker2.AnotherPartOfWorkDone += RedrawPanelVisualisation;

            worker1.StateChanged += FirstThreadStateTextBoxUpdate;
            worker2.StateChanged += SecondThreadStateTextBoxUpdate;
            //worker1.ClearingMoverTrace += ClearPrveviosVisualisation;
            //worker2.ClearingMoverTrace += ClearPrveviosVisualisation;

            // Устанавливаем доступность кнопок
            UpdateButtonAccessibility();

            FirstThreadStateTextBoxUpdate(StateOfWork.Unstarted);
            SecondThreadStateTextBoxUpdate(StateOfWork.Unstarted);
        }

        private void CreateExampleProducts(uint countOfFurniture, uint countOfDishes)
        {
            Product createdObject;
            List<string> comp = new();
            Point beginPos = new();
            Mover mov;
            Point borderOfVisual = new();
            borderOfVisual.X = panelVisualisation.Width - ((int)SizeOfPaintedImgEnum.X);
            borderOfVisual.Y = panelVisualisation.Height - ((int)SizeOfPaintedImgEnum.Y);
            for (int i = 0; i < 4; ++i)
            {
                string c = $"Компонент {i}";
                comp.Add(c);
            }

            for (int i = 0; i < countOfFurniture; ++i)
            {
                string n = $"Мебель {i + 1}";
                beginPos.X = Random.Shared.Next(borderOfVisual.X);
                beginPos.Y = Random.Shared.Next(borderOfVisual.Y);

                createdObject = new Furniture(n, i + 10, "Мебель", comp, beginPos);

                products.Add(createdObject);
                listProduct.Items.Add(createdObject);

                mov = new LineralMover((IDrawable)createdObject, new(0, 0), ((int)SpeedOfProductVisualisation.SomePixelPerSecond));
                worker1.AddMover(mov);
            }

            for (int i = 0; i < countOfDishes; ++i)
            {
                string n = $"Посуда {i + 1}";

                beginPos.X = Random.Shared.Next(borderOfVisual.X);
                beginPos.Y = Random.Shared.Next(borderOfVisual.Y);

                createdObject = new Dishes(n, i + 1, "Посуда", beginPos);

                products.Add(createdObject);
                listProduct.Items.Add(createdObject);

                mov = new RandomMover((IDrawable)createdObject, ((int)SpeedOfProductVisualisation.SomePixelPerSecond), borderOfVisual);
                worker2.AddMover(mov);
            }
        }

        // Кнопки Удалить, Редактировать, Просмотр не доступны для нажатия до выбора элемента
        private void UpdateButtonAccessibility()
        {
            // Делаем кликабельными
            if (listProduct.SelectedItems.Count > 0)
            {
                btnView.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                toolStripBtnEdit.Enabled = true;
                toolStripBtnDelete.Enabled = true;
                toolStripBtnView.Enabled = true;
            }
            // Убираем кликабельность
            else
            {
                btnView.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                toolStripBtnEdit.Enabled = false;
                toolStripBtnDelete.Enabled = false;
                toolStripBtnView.Enabled = false;
            }
        }


        private void FirstThreadStateTextBoxUpdate(StateOfWork state)
        {
            if (textboxFirstThreadState.InvokeRequired)
            {
                textboxFirstThreadState.Invoke(() =>
                {
                    textboxFirstThreadState.Text = state.ToString();
                });
            }
            else
            {
                textboxFirstThreadState.Text = state.ToString();
            }
        }

        private void SecondThreadStateTextBoxUpdate(StateOfWork state)
        {
            if (textboxSecondThreadState.InvokeRequired)
            {
                textboxSecondThreadState.Invoke(() =>
                {
                    textboxSecondThreadState.Text = state.ToString();
                });
            }
            else
            {
                textboxSecondThreadState.Text = state.ToString();
            }
        }

        // Обработчик выбора элемента из списка
        private void ListProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonAccessibility();

            // Обновляем выбранный элемент ApplicationForm.selectedInListProduct
            if (listProduct.SelectedItem is Product product)
            {
                selectedInListProduct = product;
            }
            else if (listProduct.SelectedItem == null)
            {
                selectedInListProduct = null;
            }
        }

        // Обработчик нажатия кнопки Добавить
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // определение границ отрисовки объектов для передачи в форму создания
            int xBorder = panelVisualisation.Width - (int)SizeOfPaintedImgEnum.X;
            int yBorder = panelVisualisation.Height - (int)SizeOfPaintedImgEnum.Y;
            Point borderForVisual = new Point(xBorder, yBorder);

            // Вызов формы добавления объекта
            ProductAddingForm addingForm = new(borderForVisual);
            DialogResult decision = addingForm.ShowDialog();

            // По результатам диалоговой формы пополнем (или не пополняем)
            // основную коллекцию и отображаемый список
            if (decision == DialogResult.OK)
            {
                try
                {
                    products.Add(addingForm.Product);
                    listProduct.Items.Add(addingForm.Product);

                    //movers.Add(addingForm.Mover);

                    if (addingForm.Mover is LineralMover _)
                        worker1.AddMover(addingForm.Mover);

                    else if (addingForm.Mover is RandomMover _)
                        worker2.AddMover(addingForm.Mover);

                }
                catch (Exception ex)
                {
                    // remove from products and listProduct???
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        // Обработчик нажатия кнопкки Просмотр
        private void BtnView_Click(object sender, EventArgs e)
        {
            if (selectedInListProduct != null)
            {
                // Вызов формы просмотра объекта
                ProductVewForm viewForm = new(selectedInListProduct);
                viewForm.Show();
            }
        }

        // Обработчик нажатия кнопки Удалить
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (selectedInListProduct != null)
            {
                // Подтверждение удаления пользователем
                var decition = MessageBox.Show("Удалить объект?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (decition == DialogResult.Yes)
                {
                    Mover removingMover = null;

                    if (selectedInListProduct is Furniture _)
                    {
                        foreach (Mover m in worker1.Movers)
                        {
                            if (m.Visual == selectedInListProduct)
                            {
                                removingMover = m;
                                break;
                            }
                        }

                        if(removingMover != null)
                        {
                            worker1.RemoveMover(removingMover);
                        }
                    }

                    else if (selectedInListProduct is Dishes _)
                    {
                        foreach (Mover m in worker2.Movers)
                        {
                            if (m.Visual == selectedInListProduct)
                            {
                                removingMover = m;
                                break;
                            }

                            if(removingMover != null)
                                worker2.RemoveMover(removingMover);
                        }
                    }



                    listProduct.Items.Remove(selectedInListProduct);
                    products.Remove(selectedInListProduct);

                    listProduct.SelectedItem = null;
                    selectedInListProduct = null;

                    UpdateButtonAccessibility();
                }
            }
        }

        // Обработчик нажатия кнопки Редактировать
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (selectedInListProduct != null)
            {
                // Вызов формы редактирования объекта
                ProductEditForm editForm = new(selectedInListProduct);
                DialogResult decision = editForm.ShowDialog();

                // По результатам диалоговой формы изменяем (или не изменяем)
                // объект глубоким копированием измененного объекта
                if (decision == DialogResult.OK)
                {
                    selectedInListProduct.CopyFrom(editForm.Product);

                    // Явно "изменяем" значение элемента списка,
                    // тем самым побуждая его обновить своё отображение
                    int index = listProduct.SelectedIndex;
                    listProduct.Items[index] = listProduct.Items[index];
                }
            }
        }

        // Обработчик нажатия кнопки Справка
        private void BtnInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа для работы с коллекцией мебели (добавление, редактирование, удаление, просмотр). " +
                "\nВерсия 2.0 " +
                "\nФИО студента: Гольятпов Д.И. " +
                "\nГруппа: ДТ-460");
        }

        private void PanelVisualisation_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;

            foreach (Product product in products)
            {
                if (product is IDrawable p)
                {
                    RectangleF paintBorder = new(p.VisualPosition.X, p.VisualPosition.Y, p.SizeOfVisual.Width, p.SizeOfVisual.Height);
                    g.DrawImage(p.Img, paintBorder);
                }

            }


        }

        //public void ClearPrveviosVisualisation(Worker.EventValues e)
        //{
        //    Region regionToRepaint = new(e.Area);
        //    panelVisualisation.Invalidate(regionToRepaint);
        //}

        public void RedrawPanelVisualisation(Object? sender, EventArgs e)
        {
            if (InvokeRequired)
                panelVisualisation.BeginInvoke(() => panelVisualisation.Invalidate());
            else
                panelVisualisation.Invalidate();
        }


        private void BtnStart_Click(object sender, EventArgs e)
        {
            firstThread = new Thread(new ThreadStart(worker1.Run))
            {
                Name = "Thread for furniture",
                //IsBackground = true,
            };
            secondThread = new Thread(new ThreadStart(worker2.Run))
            {
                Name = "Thread for dishes",
                //IsBackground = true,
            };


            //if (firstThread.ThreadState == (ThreadState.Unstarted &~ThreadState.Background))
            firstThread.Start();
            //if (secondThread.ThreadState == (ThreadState.Unstarted & ~ThreadState.Background))
            secondThread.Start();

            //Console.WriteLine("After Start");
            //Console.WriteLine($"{firstThread.Name} : {firstThread.ThreadState}");
            //Console.WriteLine($"{secondThread.Name} : {secondThread.ThreadState}");

            btnStart.Enabled = false;
        }

        private void BtnfirstThreadPause_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("Before Pause");
            //Console.WriteLine($"{firstThread.Name} : {firstThread.ThreadState}");

            if (worker1.Working)
                worker1.Pause();

            //Console.WriteLine("After Pause");
            //Console.WriteLine($"{firstThread.Name} : {firstThread.ThreadState}");
        }

        private void BtnFirstThreadContinue_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("Before Contuinue");
            //Console.WriteLine($"{firstThread.Name} : {firstThread.ThreadState}");

            if (worker1.Working)
                worker1.Continue();

            lock (Worker.syncObject)
                Monitor.PulseAll(Worker.syncObject);

            //Console.WriteLine("After Contuinue");
            //Console.WriteLine($"{firstThread.Name} : {firstThread.ThreadState}");
        }

        private void BtnSecondThreadContinue_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("Before Continue");
            //Console.WriteLine($"{secondThread.Name} : {secondThread.ThreadState}");

            if (worker2.Working)
                worker2.Continue();

            lock (Worker.syncObject)
                Monitor.PulseAll(Worker.syncObject);

            //Console.WriteLine("After Continue");
            //Console.WriteLine($"{secondThread.Name} : {secondThread.ThreadState}");
        }

        private void BtnSecondThreadPause_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Before Continue");
            Console.WriteLine($"{secondThread.Name} : {secondThread.ThreadState}");

            if (worker2.Working)
                worker2.Pause();

            //Console.WriteLine("After Continue");
            //Console.WriteLine($"{secondThread.Name} : {secondThread.ThreadState}");
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("Before Stop");
            //Console.WriteLine($"{firstThread.Name} : {firstThread.ThreadState}");
            //Console.WriteLine($"{secondThread.Name} : {secondThread.ThreadState}");

            lock (Worker.syncObject)
            {
                worker1.Stop();
                worker2.Stop();

                Monitor.PulseAll(Worker.syncObject);
            }

            btnStart.Enabled = true;

            //Console.WriteLine("After Stop");
            //Console.WriteLine($"{firstThread.Name} : {firstThread.ThreadState}");
            //Console.WriteLine($"{secondThread.Name} : {secondThread.ThreadState}");
        }
    }
}
