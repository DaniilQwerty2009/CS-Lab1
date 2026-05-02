using Interfaces;
using ProductLib;
using ProductTcpShared;
using System.ComponentModel;
using System.Drawing.Text;
using System.Formats.Nrbf;
using System.Net.Sockets;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Timers;
using System.Windows.Forms.VisualStyles;

namespace LabApp
{
    enum SpeedOfProductVisualisation { SomePixelPerSecond = 30};
    enum SizeOfPaintedImgEnum {  X = 50, Y = 50};

    internal enum TypesOfProduct 
    { 
        [Description("Мебель")] Furniture = 1, 
        [Description("Посуда")] Dish = 2,
    };



    // Основная форма приложения
    public partial class ApplicationForm : Form
    {
        // Основная коллекция для хранения объектов
        readonly List<Product> products = new();

        // Текущий выбор из списка объектов
        Product? selectedInListProduct;

        /// <summary>
        /// Объект, отвечающий за перемещение объектов типа мебель (для LineralMover)
        /// </summary>
        readonly Worker worker1;
        Thread? firstThread;

        /// <summary>
        /// Объект, отвечающий за перемещение объектов типа посуда (для RandomMover)
        /// </summary>
        readonly Worker worker2;
        Thread? secondThread;

        NetworkConnection networkConnection;


        public ApplicationForm()
        {

            InitializeComponent();

            worker1 = new();

            worker2 = new();

            int f = Random.Shared.Next(5, 11);
            int d = Random.Shared.Next(5, 11);
            CreateExampleProducts((uint)f, (uint)d);

            worker1.AnotherPartOfWorkDone += RedrawPanelVisualisation;
            worker2.AnotherPartOfWorkDone += RedrawPanelVisualisation;

            worker1.StateChanged += FirstThreadStateTextBoxUpdate;
            worker2.StateChanged += SecondThreadStateTextBoxUpdate;


            UpdateButtonAccessibility();
            btnStop.Enabled = false;
            btnFirstThreadContinue.Enabled = false;
            btnFirtsThreadPause.Enabled = false;

            btnSecondThreadContinue.Enabled = false;
            btnSecondThreadPause.Enabled = false;

            FirstThreadStateTextBoxUpdate(StateOfWork.Unstarted);
            SecondThreadStateTextBoxUpdate(StateOfWork.Unstarted);

            FillPriorityComboBoxes();


            TcpClient client = new TcpClient("127.0.0.1", 5000);

            networkConnection = NetworkConnection.Create(client);

            networkConnection.NewMessageReceived += HandleMessage;

            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();

            labelUser.Text = loginForm.Username;

            networkConnection.SendMessage(MessageType.LOGIN, new string[] { labelUser.Text });
        }

        /// <summary>
        /// Создание в системе объектов мебели и посуды
        /// </summary>
        private void CreateExampleProducts(uint countOfFurniture, uint countOfDishes)
        {
            Product createdObject;
            List<string> comp = new();
            Point beginPos = new();
            Mover mov;
            Point borderOfVisual = new()
            {
                X = panelVisualisation.Width - ((int)SizeOfPaintedImgEnum.X),
                Y = panelVisualisation.Height - ((int)SizeOfPaintedImgEnum.Y),
            };

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

        /// <summary>
        /// Обновление доступности кнопок просмотра, редактирования, удаления
        /// </summary>
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

        /// <summary>
        /// Изменение строки, отображающей состояния первого потока
        /// </summary>
        private void FirstThreadStateTextBoxUpdate(StateOfWork state)
        {
            if (textboxFirstThreadState.InvokeRequired)
            {
                textboxFirstThreadState.Invoke(() =>
                {
                    textboxFirstThreadState.Text = state.GetDescription();
                });
            }
            else
            {
                textboxFirstThreadState.Text = state.GetDescription();
            }
        }


        /// <summary>
        /// Изменение строки, отображающей состояния второго потока
        /// </summary>
        private void SecondThreadStateTextBoxUpdate(StateOfWork state)
        {
            if (textboxSecondThreadState.InvokeRequired)
            {
                textboxSecondThreadState.Invoke(() =>
                {
                    textboxSecondThreadState.Text = state.GetDescription();
                });
            }
            else
            {
                textboxSecondThreadState.Text = state.GetDescription();
            }
        }

        /// <summary>
        /// Обработчик выбора элемента списка продуктов. Включает изменение selectedInListProduct и вызов UpdateButtonAccessibility();
        /// </summary>
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

        /// <summary>
        /// Обработчик нажатия кнопки Добавить. Вызов панели добавления продукта, обработка результата вызова
        /// </summary>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // определение границ отрисовки объектов для передачи в форму создания
            int xBorder = panelVisualisation.Width - (int)SizeOfPaintedImgEnum.X;
            int yBorder = panelVisualisation.Height - (int)SizeOfPaintedImgEnum.Y;
            Point borderForVisual = new(xBorder, yBorder);

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
        /// <summary>
        /// Обработчик нажатия кнопкки Просмотр
        /// </summary>
        private void BtnView_Click(object sender, EventArgs e)
        {
            if (selectedInListProduct != null)
            {
                // Вызов формы просмотра объекта
                ProductVewForm viewForm = new(selectedInListProduct);
                viewForm.Show();
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки Удалить. Вызов диалогового окна, обработка результата вызова
        /// </summary>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (selectedInListProduct != null)
            {
                // Подтверждение удаления пользователем
                var decition = MessageBox.Show("Удалить объект?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (decition == DialogResult.Yes)
                {
                    Mover? removingMover = null;

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

                        if (removingMover != null)
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
                        }
                        if (removingMover != null)
                            worker2.RemoveMover(removingMover);
                    }



                    listProduct.Items.Remove(selectedInListProduct);
                    products.Remove(selectedInListProduct);

                    listProduct.SelectedItem = null;
                    selectedInListProduct = null;

                    UpdateButtonAccessibility();
                }
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки Редактировать. Вызов панели редактирования продукта, обработка результата вызова
        /// </summary>
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

        /// <summary>
        /// Обработчик нажатия кнопки Справка
        /// </summary>
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

        /// <summary>
        /// Обработчик обновления координат виуальной составляющей продуктов. Включает Invalidate панели отображения визуала
        /// </summary>
        public void RedrawPanelVisualisation(Object? sender, EventArgs e)
        {
            if (InvokeRequired)
                panelVisualisation.BeginInvoke(() => panelVisualisation.Invalidate());
            else
                panelVisualisation.Invalidate();
        }


        /// <summary>
        /// Обработчик кнопни запуска потоков
        /// </summary>
        private void BtnStart_Click(object sender, EventArgs e)
        {
            firstThread = new Thread(new ThreadStart(worker1.Run))
            {
                Name = "Thread for furniture",
                IsBackground = true,
            };
            secondThread = new Thread(new ThreadStart(worker2.Run))
            {
                Name = "Thread for dishes",
                IsBackground = true,
            };

            {
                if (comboBoxFirstThreadPriority.SelectedItem is PriorityItem item)
                {
                    firstThread.Priority = item.Value;
                }
            }


            {
                if (comboBoxSecondThreadPriority.SelectedItem is PriorityItem item)
                {
                    secondThread.Priority = item.Value;
                }
            }

            firstThread.Start();

            secondThread.Start();

            btnStart.Enabled = false;
            btnStop.Enabled = true;
            btnFirstThreadContinue.Enabled = false;
            btnFirtsThreadPause.Enabled = true;

            btnSecondThreadContinue.Enabled = false;
            btnSecondThreadPause.Enabled = true;

        }

        private void BtnFirstThreadPause_Click(object sender, EventArgs e)
        {
            if (worker1.Working)
            {
                worker1.Pause();
                btnFirtsThreadPause.Enabled = false;
                btnFirstThreadContinue.Enabled = true;
            }
        }

        private void BtnFirstThreadContinue_Click(object sender, EventArgs e)
        {
            if (worker1.Working)
            {
                worker1.Continue();
                btnFirstThreadContinue.Enabled = false;
                btnFirtsThreadPause.Enabled = true;
            }


            lock (Worker.syncObject)
                Monitor.PulseAll(Worker.syncObject);
        }

        private void BtnSecondThreadContinue_Click(object sender, EventArgs e)
        {
            if (worker2.Working)
            {
                worker2.Continue();
                btnSecondThreadContinue.Enabled = false;
                btnSecondThreadPause.Enabled = true;
            }


            lock (Worker.syncObject)
                Monitor.PulseAll(Worker.syncObject);
        }

        private void BtnSecondThreadPause_Click(object sender, EventArgs e)
        {
            if (worker2.Working)
            {
                worker2.Pause();
                btnSecondThreadContinue.Enabled = true;
                btnSecondThreadPause.Enabled = false;
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            lock (Worker.syncObject)
            {
                worker1.Stop();
                worker2.Stop();

                Monitor.PulseAll(Worker.syncObject);
            }

            btnStart.Enabled = true;
            btnFirstThreadContinue.Enabled = false;
            btnFirtsThreadPause.Enabled = false;

            btnSecondThreadContinue.Enabled = false;
            btnSecondThreadPause.Enabled = false;
        }

        /// <summary>
        /// Заполнение списка доступных приоритетов потоков на панели глваной формы
        /// </summary>
        private void FillPriorityComboBoxes()
        {
            comboBoxFirstThreadPriority.Items.Add(new PriorityItem
            {
                Text = "Низкий",
                Value = ThreadPriority.Lowest,
            });
            comboBoxSecondThreadPriority.Items.Add(new PriorityItem
            {
                Text = "Низкий",
                Value = ThreadPriority.Lowest,
            });

            comboBoxFirstThreadPriority.Items.Add(new PriorityItem
            {
                Text = "Средний",
                Value = ThreadPriority.Normal,
            });
            comboBoxSecondThreadPriority.Items.Add(new PriorityItem
            {
                Text = "Средний",
                Value = ThreadPriority.Normal,
            });

            comboBoxFirstThreadPriority.SelectedItem = comboBoxFirstThreadPriority.Items[1];
            comboBoxSecondThreadPriority.SelectedItem = comboBoxSecondThreadPriority.Items[1];

            comboBoxFirstThreadPriority.Items.Add(new PriorityItem
            {
                Text = "Высокий",
                Value = ThreadPriority.Highest,
            });
            comboBoxSecondThreadPriority.Items.Add(new PriorityItem
            {
                Text = "Высокий",
                Value = ThreadPriority.Highest,
            });
        }

        private void ComboBoxFirstThreadPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (firstThread != null)
            {
                if (firstThread.IsAlive && comboBoxFirstThreadPriority.SelectedItem != null)
                {
                    if (comboBoxFirstThreadPriority.SelectedItem is PriorityItem p)
                        firstThread.Priority = p.Value;
                }
            }
        }

        private void ComboBoxSecondThreadPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (secondThread != null)
            {
                if (secondThread.IsAlive && comboBoxSecondThreadPriority.SelectedItem != null)
                {
                    if (comboBoxSecondThreadPriority.SelectedItem is PriorityItem p)
                        secondThread.Priority = p.Value;
                }

            }

        }


        public void HandleMessage(NetworkMessage message)
        {
            switch (message.Type)
            {
                case (MessageType.CLIENTS_INFO):
                    listClients.Invoke(() => listClients.Items.Clear());

                    

                    foreach(string client in message.Payload)
                        listClients.Invoke(() => listClients.Items.Add(client));


                    break;

                default:

                    break;
            }
        }
    }
}
