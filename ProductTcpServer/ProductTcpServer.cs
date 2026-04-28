using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using System.Text;
using System.Xml;

namespace ProductTcpServer
{
    public enum ServerResponse
    {
        /// <summary>
        /// Indicates that the server sends a list of connected client names.
        /// </summary>
        CLIENTS_INFO,
    };


    internal enum ClientCommand
    {
        CONNECT,
        DISCONNECT,
        LOGIN,
    };


    internal class ProductTcpServer
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            TcpServer server = new(IPAddress.Any, 5000);

            server.Listen();

        }

        internal class TcpServer
        {
            List<ConnectedClient> connectedClients = new List<ConnectedClient>();

            List<ClientSession> sessions = new List<ClientSession>();

            TcpListener listener;

            private readonly object _clientsLock = new();

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

                try
                {
                    while (true)
                    {
                        Console.WriteLine("***Ожидание очередного подключения***");
                        TcpClient newClient = listener.AcceptTcpClient();

                        ClientSession newSession = new(newClient);

                        sessions.Add(newSession);

                        ThreadStart threadStart = () => TalkWith(newSession);

                        Console.WriteLine("***Очередное подключения перенаправлено в отдельный поток***");

                        Thread newThread = new(threadStart);

                        newThread.Start();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to listen {ex.Message}");
                }
                finally
                {
                    foreach (ClientSession session in sessions)
                    {
                        session.Client.Close();
                    }
                }







            } // End of Listen

            private void TalkWith(ClientSession session)
            {
                NetworkStream stream = session.Client.GetStream();

                try
                {
                    while (true)
                    {
                        byte[] data = new byte[1024];

                        int bytesRead = stream.Read(data, 0, data.Length);

                        //string recivedCommand = Encoding.UTF8.GetString(data);

                        foreach (ClientSession s in sessions)
                        {
                            s.Stream.Write(data, 0, bytesRead);
                        }

                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Failed to talk with {session.Name}: {ex.Message}");
                }
                finally
                {
                    stream.Close();
                    session.Client.Close();
                }

            }// End of TalkWith

            //void InitClient(TcpClient client)
            //{
            //    Console.WriteLine($"***К серверу подключился новый пользователь.***");

            //    byte[] buffer = new byte[1024];

            //    var stream = client.GetStream();
            //    int bytesRead = stream.Read(buffer, 0, buffer.Length);

            //    string inputString = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            //    string[] parts = inputString.Split('|');

            //    switch (parts[0])
            //    {
            //        case ("CONNECT"):
            //            ConnectedClient newClient = new(
            //                parts[1],
            //                client,
            //                stream
            //            );
            //            connectedClients.Add(newClient);

            //            Console.WriteLine($"Пользователь идентифицирован: \n {newClient.Name} \n {newClient.Client} \n {newClient.Stream}");


            //            Console.WriteLine("Пользователь подключён");

            //            TalkWith(newClient);


            //            break;
            //        default:
            //            Console.WriteLine($"Ошибка идентификации: команда {parts[0]} не существует");
            //            break;
            //    }
            //}// End of initClient


            //void TalkWith(ConnectedClient client)
            //{
            //    string helloMsg = $"Привет, {client.Name}! Ты подключился к серверу!";

            //    byte[] data = new byte[1024];

            //    data = Encoding.UTF8.GetBytes(helloMsg);

            //    client.Stream.Write(data);

            //    while (true)
            //    {
            //        int readBytes = client.Stream.Read(data, 0, data.Length);

            //        if (readBytes == 0)
            //        {
            //            Console.WriteLine("Пользователь отключен");
            //            Console.ReadLine();
            //            break;
            //        }

            //        string receveMsg = Encoding.UTF8.GetString(data, 0, readBytes);

            //        string chatMessage = $"{client.Name}: {receveMsg}";

            //        byte[] chatMessageData = Encoding.UTF8.GetBytes(chatMessage);

            //        foreach (ConnectedClient cl in connectedClients)
            //        {
            //            cl.Stream.Write(chatMessageData);
            //        }


            //    }


        } // end of TcpServer

    } // end of class ProductTcpServer

} // end of namespace ProductTcpServer

