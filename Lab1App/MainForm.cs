using Interfaces;
using ProductLib;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows.Forms.VisualStyles;

namespace LabApp
{
    enum SpeedOfProductVisualisation { SomePixelPerSecond = 30};
    enum SizeOfPaintedImgEnum {  X = 50, Y = 50};

    internal enum TypesOfProduct { Мебель = 1, Посуда = 2 };

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

            //Dishes d = new("f", 1, "g", new(0, 0));
            //Furniture s = new("f", 1, "g", ["g"], new(0, 0));

            //listProduct.Items.Add(s);
            //listProduct.Items.Add(d);

            //products.Add(d);
            //products.Add(s);

            //Mover mover = new LineralMover(s, new(0,0), ((int)SpeedOfProductVisualisation.SomePixelPerSecond));
            //worker1.AddMover(mover);

            //mover = new RandomMover(s, ((int)SpeedOfProductVisualisation.SomePixelPerSecond), new(panelVisualisation.Width, panelVisualisation.Height));
            //worker2.AddMover(mover);


            worker1.AnotherPartOfWorkDone += RedrawPanelVisualisation;
            worker2.AnotherPartOfWorkDone += RedrawPanelVisualisation;
            //worker1.ClearingMoverTrace += ClearPrveviosVisualisation;
            //worker2.ClearingMoverTrace += ClearPrveviosVisualisation;

            // Устанавливаем доступность кнопок
            UpdateButtonAccessibility();
            UpdateThreadsStatusTextBox();
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


        private void UpdateThreadsStatusTextBox()
        {
            ThreadState state;
            TextBox changedTextBox;

            if (firstThread != null)
                state = firstThread.ThreadState;
            else
                state = ThreadState.Unstarted;

            changedTextBox = textboxFirstThreadStatus;

            switch (state)
            {
                case (ThreadState.Unstarted):
                    changedTextBox.Text = "Не запущен";
                    break;
                case (ThreadState.Running):
                    changedTextBox.Text = "В работе";
                    break;
                case (ThreadState.Stopped):
                    changedTextBox.Text = "Завершен";
                    break;
                case (ThreadState.WaitSleepJoin):
                    changedTextBox.Text = "На паузе";
                    break;
            }

            if (secondThread != null)
                state = secondThread.ThreadState;
            else
                state = ThreadState.Unstarted;

            changedTextBox = textboxSecondThreadStatus;

            switch (state)
            {
                case (ThreadState.Unstarted):
                    changedTextBox.Text = "Не запущен";
                    break;
                case (ThreadState.Running):
                    changedTextBox.Text = "В работе";
                    break;
                case (ThreadState.Stopped):
                    changedTextBox.Text = "Завершен";
                    break;
                case (ThreadState.WaitSleepJoin):
                    changedTextBox.Text = "На паузе";
                    break;
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
                    if (selectedInListProduct is Furniture _)
                    {
                        foreach (Mover m in worker1.Movers)
                        {
                            if (m.Visual == selectedInListProduct)
                                worker1.RemoveMover(m);
                        }
                    }

                    else if (selectedInListProduct is Dishes _)
                    {
                        foreach (Mover m in worker2.Movers)
                        {
                            if (m.Visual == selectedInListProduct)
                                worker2.RemoveMover(m);
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
            //if (worker1.Movers.Count() != 0)
            //{
            //    if (!worker1.Paused)
            //    {
            //        foreach (Mover mover in worker1.Movers)
            //        {
            //            PointF leftTopPoint = mover.Visual.VisualPosition;

            //            Size rightBottonPoint = mover.Visual.SizeOfVisual;

            //            RectangleF areaToReapint = new(leftTopPoint, rightBottonPoint);

            //            Region regionToRepaint = new(areaToReapint);

            //            panelVisualisation.Invalidate(regionToRepaint);
            //        }
            //    }

            //    if (!worker2.Paused)
            //    {
            //        foreach (Mover mover in worker2.Movers)
            //        {
            //            PointF leftTopPoint = mover.Visual.VisualPosition;

            //            Size rightBottonPoint = mover.Visual.SizeOfVisual;

            //            RectangleF areaToReapint = new(leftTopPoint, rightBottonPoint);

            //            Region regionToRepaint = new(areaToReapint);

            //            panelVisualisation.Invalidate(regionToRepaint);
            //        }
            //    }
            //}
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

            UpdateThreadsStatusTextBox();

            Console.WriteLine("After Start");
            Console.WriteLine($"{firstThread.Name} : {firstThread.ThreadState}");
            Console.WriteLine($"{secondThread.Name} : {secondThread.ThreadState}");

            btnStart.Enabled = false;
        }

        private void BtnfirstThreadPause_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Before Pause");
            Console.WriteLine($"{firstThread.Name} : {firstThread.ThreadState}");

            if (worker1.Working)
                worker1.Pause();

            UpdateThreadsStatusTextBox();

            Console.WriteLine("After Pause");
            Console.WriteLine($"{firstThread.Name} : {firstThread.ThreadState}");
        }

        private void BtnFirstThreadContinue_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Before Contuinue");
            Console.WriteLine($"{firstThread.Name} : {firstThread.ThreadState}");

            if (worker1.Working)
                worker1.Continue();

            lock (Worker.syncObject)
                Monitor.PulseAll(Worker.syncObject);

            UpdateThreadsStatusTextBox(); // гарантированно ли изменение статуса после Pulse к моменту вызова функции????

            Console.WriteLine("After Contuinue");
            Console.WriteLine($"{firstThread.Name} : {firstThread.ThreadState}");
        }

        private void BtnSecondThreadContinue_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Before Continue");
            Console.WriteLine($"{secondThread.Name} : {secondThread.ThreadState}");
            if (worker2.Working)
                worker2.Continue();

            lock (Worker.syncObject)
                Monitor.PulseAll(Worker.syncObject);

            UpdateThreadsStatusTextBox(); // гарантированно ли изменение статуса после Pulse к моменту вызова функции???

            Console.WriteLine("After Continue");
            Console.WriteLine($"{secondThread.Name} : {secondThread.ThreadState}");
        }

        private void BtnSecondThreadPause_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Before Continue");
            Console.WriteLine($"{secondThread.Name} : {secondThread.ThreadState}");

            if (worker2.Working)
                worker2.Pause();

            UpdateThreadsStatusTextBox();

            Console.WriteLine("After Continue");
            Console.WriteLine($"{secondThread.Name} : {secondThread.ThreadState}");
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Before Stop");
            Console.WriteLine($"{firstThread.Name} : {firstThread.ThreadState}");
            Console.WriteLine($"{secondThread.Name} : {secondThread.ThreadState}");

            lock (Worker.syncObject)
            {
                worker1.Stop();
                worker2.Stop();

                Monitor.PulseAll(Worker.syncObject);
            }

            btnStart.Enabled = true;

            UpdateThreadsStatusTextBox();
            Console.WriteLine("After Stop");
            Console.WriteLine($"{firstThread.Name} : {firstThread.ThreadState}");
            Console.WriteLine($"{secondThread.Name} : {secondThread.ThreadState}");
        }


    }
}
