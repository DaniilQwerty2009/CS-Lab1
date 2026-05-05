using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ProductTcpShared;


namespace ProductTcpServer
{
    internal class ProductTcpServer
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            TcpServer server = new(IPAddress.Any, 5000);

            server.Launch();

        }
    }

    internal class TcpServer
    {
        private int _id = 1;

        List<ClientSession> _sessions = new List<ClientSession>();

        TcpListener _portListener;

        private readonly object _clientsLock = new();

        public TcpServer(IPAddress address, int port)
        {
            _portListener = new TcpListener(IPAddress.Any, port);

            Console.WriteLine("***Создан локальный сервер***");
            Console.Write(address);
            Console.WriteLine($":{port}");
        }

        public void Launch()
        {
            _portListener.Start();

            Console.WriteLine("***Сервер запущен***");

            try
            {
                while (true)
                {
                    Console.WriteLine("***Ожидание очередного подключения***");
                    TcpClient newClient = _portListener.AcceptTcpClient();

                    ClientSession newSession = new(newClient, _id);
                    _id++;

                    newSession.Connection.NewMessageReceived += message =>
                    {
                        HandleCommand(newSession, message);
                    };

                    newSession.Connection.ConnectionInterrupted += (sender, e) =>
                    {
                        ExcludeSession(newSession);
                    };

                    _sessions.Add(newSession);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Server acception tcp client failed : {ex.Message}");
            }
            finally
            {
                lock(_clientsLock)
                {
                    foreach (ClientSession session in _sessions)
                    {
                        try 
                        { 
                            session.Connection.Close();
                        }
                        catch 
                        { 
                            // Connection could be already closed
                        }

                        
                    }
                    _sessions.Clear();
                    _portListener.Stop();
                }
            }

        } // End of Launch


        private void SendOutClientsInfo()
        {
            lock (_clientsLock)
            {
                if (_sessions.Count > 0)
                {
                    string[] payload = new string[_sessions.Count * 2];
                    MessageType type = MessageType.CLIENTS_INFO;


                    for (int i = 0, j = 0; i < _sessions.Count; i++)
                    {
                        if (_sessions[i].Connection.IsOpen && _sessions[i].IsLogged)
                        {
                            payload[j++] = _sessions[i].ID.ToString();
                            payload[j++] = _sessions[i].Name;
                        }
                    }
                    
                    foreach (ClientSession session in _sessions)
                    {
                        if (session.IsLogged && session.Connection.IsOpen)
                            session.Connection.SendMessage(type, payload);
                    }
                }
            }




        } // end of SendOutClientsInfo

        private void HandleCommand(ClientSession session, NetworkMessage message)
        {
            switch (message.Type)
            {
                case (MessageType.LOGIN):
                    if (message.Payload == Array.Empty<string>())
                        return;

                    try
                    {
                        session.Login(message.Payload[0]);

                        session.Connection.SendMessage(MessageType.ASSIGNED_NETWORK_ID, new string[] { session.ID.ToString() });

                        SendOutClientsInfo();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                    }


                    break;

                case (MessageType.DISCONNECT):

                    lock(_clientsLock)
                    {
                        Console.WriteLine("***Клиент инициировал завершение сессии***");

                        _sessions.Remove(session);

                        session.End();

                        SendOutClientsInfo();
                    }
                    
                    break;

                case (MessageType.SEND_PRODUCTS_DATA):

                    int id;

                    if(!int.TryParse(message.Payload[0], out id))
                    {
                        return;
                    }


                    int index = 0;
                    ClientSession destination;

                    lock(_clientsLock)
                    {
                        for(; index < _sessions.Count; ++index)
                        {
                            if (_sessions[index].ID == id)
                            {
                                destination = _sessions[index];

                                message.Payload.TrimStart(message.Payload[0]);

                                destination.Connection.SendMessage(MessageType.PRODUCTS_DATA, message.Payload);

                                break;
                            }
                        } 
                    }

                    
                    

                    break;

                default:
                        
                    break;
            }

        } // end of HandleCommand


        private void ExcludeSession(ClientSession session)
        {
            _sessions.Remove(session);

            session.End();

            SendOutClientsInfo();
        }


    } // end of class TcpServer

}  // end of namespace ProductTcpServer



