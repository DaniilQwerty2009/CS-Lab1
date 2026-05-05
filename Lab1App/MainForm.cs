using Interfaces;
using ProductLib;
using ProductTcpShared;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Net.Sockets;
using System.Text;


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

        User _user;

        private readonly object _lockNetworkUsers = new object();
        private readonly object _lockProductsList = new object();

        List<NetworkUser> networkUsers = new();


        public ApplicationForm()
        {

            InitializeComponent();

            worker1 = new();

            worker2 = new();

            //int f = Random.Shared.Next(5, 11);
            //int d = Random.Shared.Next(5, 11);
            CreateExampleProducts(2, 2);

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

            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();

            labelUser.Text = loginForm.Username;

            _user = new(loginForm.Username);

            _user.Connection.NewMessageReceived += HandleMessage;

            _user.Connection.SendMessage(MessageType.LOGIN, new string[] { labelUser.Text });

            btnSendProducts.Enabled = false;
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

            for (int i = 1; i < 4; ++i)
            {
                string c = $"Компонент {i}";
                comp.Add(c);
            }

            for (int i = 1; i < countOfFurniture; ++i)
            {
                string n = $"Мебель {i}";
                beginPos.X = Random.Shared.Next(borderOfVisual.X);
                beginPos.Y = Random.Shared.Next(borderOfVisual.Y);

                createdObject = new Furniture(n, i + 10, TypesOfProduct.Furniture.GetDescription() , comp);

                if (createdObject is IDrawable dish)
                {

                    dish.VisualPosition = beginPos;
                    dish.SizeOfVisual = new Size((int)SizeOfPaintedImgEnum.X, (int)SizeOfPaintedImgEnum.Y);
                }

                products.Add(createdObject);
                listProduct.Items.Add(createdObject);

                mov = new LineralMover((IDrawable)createdObject, new(0, 0), ((int)SpeedOfProductVisualisation.SomePixelPerSecond));
                worker1.AddMover(mov);
            }

            for (int i = 1; i < countOfDishes; ++i)
            {
                string n = $"Посуда {i}";

                beginPos.X = Random.Shared.Next(borderOfVisual.X);
                beginPos.Y = Random.Shared.Next(borderOfVisual.Y);

                createdObject = new Dishes(n, i, TypesOfProduct.Dish.GetDescription());

                if (createdObject is IDrawable furniture)
                {
                    furniture.VisualPosition = beginPos;
                    furniture.SizeOfVisual = new Size((int)SizeOfPaintedImgEnum.X, (int)SizeOfPaintedImgEnum.Y);
                }

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

        internal class User
        {
            private readonly string _name;

            private NetworkConnection _connection;
            internal NetworkConnection Connection { get { return _connection; } }
            internal int ID { get; set; }

            internal User(string name)
            {
                _name = name;

                TcpClient client = new TcpClient("127.0.0.1", 5000);

                _connection = NetworkConnection.Create(client);
            }

            public override string ToString()
            {
                return _name;
            }
        }

        public void HandleMessage(NetworkMessage message)
        {
            switch (message.Type)
            {
                case (MessageType.ASSIGNED_NETWORK_ID):

                    int id;
                    if (int.TryParse(message.Payload[0], out id))
                    {
                        _user.ID = id;
                    }

                    break;

                case (MessageType.CLIENTS_INFO):

                    lock (_lockNetworkUsers)
                    {
                        listNetworkUsers.Invoke(() => listNetworkUsers.Items.Clear());
                        networkUsers.Clear();

                        int idBuffer;
                        string nameBuffer;
                        int index = 0;

                        while (index < message.Payload.Length - 1)
                        {
                            if (int.TryParse(message.Payload[index++], out idBuffer))
                            {
                                nameBuffer = message.Payload[index++];

                                AddNetworkUserToList(idBuffer, nameBuffer);
                            }
                            else
                                throw new WrongFormatExсeption("Wrong input message format");

                        }
                    }
                    break;

                case (MessageType.PRODUCTS_DATA):

                    string[] load = message.Payload;
                    TypesOfProduct type;
                    int i = 0;

                    string name;
                    long article;
                    int componentsCount;
                    List<string> components = new();

                    Product product;

                    do
                    {
                        Enum.TryParse(load[i++], out type);

                        switch (type)
                        {
                            case (TypesOfProduct.Furniture):

                                components.Clear();

                                name = load[i++];
                                article = int.Parse(load[i++]);
                                componentsCount = int.Parse(load[i++]);

                                for (int j = 0; j < componentsCount; ++j)
                                    components.Add(load[i++]);

                                product = new Furniture(name, article, EnumExtentions.GetDescription(TypesOfProduct.Furniture), components);

                                listProduct.Invoke(() => listProduct.Items.Add(product));

                                break;

                            case (TypesOfProduct.Dish):

                                name = load[i++];
                                article = int.Parse(load[i++]);

                                product = new Dishes(name, article, EnumExtentions.GetDescription(TypesOfProduct.Dish));

                                listProduct.Invoke(() => listProduct.Items.Add(product));

                                break;
                        }


                    } while (i < load.Length);

                    break;

                default:

                    break;
            }
        }

        private void AddNetworkUserToList(int id, string name)
        {
            NetworkUser user = new(name, id);

            lock (_lockNetworkUsers)
            {
                networkUsers.Add(user);
                listNetworkUsers.Invoke(() => listNetworkUsers.Items.Add(user));
            }
        }


        private void ListClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listNetworkUsers.SelectedItem is NetworkUser user)
            {
                if (user.ID == _user.ID)
                    btnSendProducts.Enabled = false;
                else
                    btnSendProducts.Enabled = true;
            }
        }

        private void BtnSendProducts_Click(object sender, EventArgs e)
        {
            lock(_lockProductsList)
            {
                string[] payload;
                List<string> load = new();

                if(listNetworkUsers.SelectedItem != null)
                    load.Add(((NetworkUser)listNetworkUsers.SelectedItem).ID.ToString());

                foreach(Product product in products)
                {
                    if(product is Furniture furniture)
                    {
                        load.Add(TypesOfProduct.Furniture.ToString());
                        load.Add(furniture.Name);
                        load.Add(furniture.Article.ToString());
                        load.Add(furniture.Components.Count().ToString());

                        foreach(String component in furniture.Components)
                            load.Add(component);
                    }

                    if(product is Dishes dish)
                    {
                        load.Add(TypesOfProduct.Dish.ToString());
                        load.Add(dish.Name);
                        load.Add(dish.Article.ToString());
                    }
                }

                payload = load.ToArray();

                _user.Connection.SendMessage(MessageType.SEND_PRODUCTS_DATA, payload);
            }
        }
    }
}
