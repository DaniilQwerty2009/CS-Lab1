using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using System.Text;
using System.Xml;
using System.Diagnostics.SymbolStore;

namespace ProductTcpServer
{
    /// <summary>
    /// Represents commands sent from the server to the client.
    /// </summary>
    public enum ServerResponse {
        /// <summary>
        /// Indicates that the server sends a list of connected client names.
        /// </summary>
        CLIENTS_INFO,
    };


    internal enum clientCommand {
        CONNECT,
        DISCONNECT,
    };

    internal class ProductTcpServer
    {
        static void Main(string[] args)
        {
            TcpServer server = new(IPAddress.Any, 5000);

            server.Listen(); 
        }

        internal class TcpServer
        {
            static int i = 1;
            static string threadName = "myThread" + i.ToString();

            List<ConnectedClient> connectedClients = new List<ConnectedClient>();

            //List<Thread> threads = new List<Thread>();

            TcpListener listener;

            public static object syncObject = new object();

            public TcpServer(IPAddress address, int port)
            {
                listener = new TcpListener(IPAddress.Any, port);

                Console.WriteLine("Создан локальный сервер");
                Console.Write(address);
                Console.WriteLine($":{port}");
            }

            public void Listen()
            {
                listener.Start();

                Console.WriteLine("Сервер запущен");

                while (true)
                {
                    Console.WriteLine("Ожидание очередного подключения");
                    TcpClient newClient = listener.AcceptTcpClient();

                    Thread anotherThread = new Thread(new ThreadStart(() => InitClient(newClient)));
                    anotherThread.IsBackground = true;
                    anotherThread.Name = threadName;
                    ++i;

                    //threads.Add(anotherThread);

                    anotherThread.Start();

                }
            } // end of Listen

            void InitClient(TcpClient client)   // default to switch!!
            {
                Console.WriteLine($"К серверу подключился новый пользователь. Идентификация...");

                byte[] buffer = new byte[1024];

                var stream = client.GetStream();
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                string inputString = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                string[] parts = inputString.Split('|');

                switch (parts[0])
                {
                    case ("CONNECT"):
                        ConnectedClient newClient = new(
                            parts[1],
                            client,
                            stream
                        );
                        connectedClients.Add(newClient);

                        Console.WriteLine($"Пользователь идентифицирован: \n {newClient.Name} \n {newClient.Client} \n {newClient.Stream}");


                        Console.WriteLine("Пользователь подключён");

                        TalkWith(newClient);

                        clientsListMailing();

                        break;
                    default:
                        Console.WriteLine($"Ошибка идентификации: команда {parts[0]} не существует");
                        break;
                }
            }// end of initClient


            void TalkWith(ConnectedClient client)
            {
                string helloMsg = $"Привет, {client.Name}! Ты подключился к серверу!";

                byte[] data = new byte[1024];

                data = Encoding.UTF8.GetBytes(helloMsg);

                client.Stream.Write(data);

                while (true)
                {
                    try
                    {
                        int readBytes = client.Stream.Read(data, 0, data.Length);


                        if (readBytes == 0)
                        {
                            Console.WriteLine("Пользователь отключен");

                            clientsListMailing();

                            break;
                        }

                        string receveMsg = Encoding.UTF8.GetString(data, 0, readBytes);

                        if (receveMsg == "DISCONNECT")
                        {
                            string goodbyMsg = $"***Пользователь {client.Name} оключился от сервера***";

                            byte[] goodbyMsgData = Encoding.UTF8.GetBytes(goodbyMsg);


                            lock (syncObject)
                            {
                                foreach (ConnectedClient cl in connectedClients)
                                {
                                    cl.Stream.Write(goodbyMsgData);
                                }
                                connectedClients.Remove(client);

                                clientsListMailing();

                                break;
                            }

                        }

                        string chatMessage = $"{client.Name}: {receveMsg}";

                        byte[] chatMessageData = Encoding.UTF8.GetBytes(chatMessage);


                        lock (syncObject)
                        {
                            foreach (ConnectedClient cl in connectedClients)
                            {
                                cl.Stream.Write(chatMessageData);
                            }
                        }
                    }
                    catch (Exception _)
                    {
                        {
                            string goodbyMsg = $"***Пользователь {client.Name} оключился от сервера***";

                            byte[] goodbyMsgData = Encoding.UTF8.GetBytes(goodbyMsg);


                            lock (syncObject)
                            {
                                connectedClients.Remove(client);

                                foreach (ConnectedClient cl in connectedClients)
                                {
                                    cl.Stream.Write(goodbyMsgData);
                                }
                                

                                clientsListMailing();

                                break;
                            }
                        }

                    }
                }
            } // end of TalkWith

            void clientsListMailing()
            {
                lock (syncObject)
                {
                    string clients = ServerResponse.CLIENTS_INFO.ToString() + '|';

                    foreach (ConnectedClient cl in connectedClients)
                    {
                        clients += cl.Name;
                        clients += '|';
                    }

                    int bytes = Encoding.UTF8.GetByteCount(clients);

                    string clientsInfo = bytes.ToString() + '|';

                    clientsInfo += clients;

                    byte[] clientInfoData = Encoding.UTF8.GetBytes(clientsInfo);

                    foreach (ConnectedClient cl in connectedClients)
                    {
                        cl.Stream.Write(clientInfoData);
                    }

                }
            } // end of clientsListMailing
        } // end of class TcpServer
    }// end of class ProductTcpServer
}// end of namespace ProductTcpServer
