using System.Diagnostics;
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

                    ClientSession newSession = new(newClient);

                    newSession.Connection.NewMessageReceved += message =>
                    {
                        HandleCommand(newSession, message);
                    };

                    _sessions.Add(newSession);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to listen: {ex.Message}");
            }
            finally
            {
                lock(_clientsLock)
                {
                    foreach (ClientSession session in _sessions)
                    {
                        try { session.Client.Client.Shutdown(SocketShutdown.Both); }
                        catch { }

                        session.Client.Close();
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
                    string[] payload = new string[_sessions.Count];
                    MessageType type = MessageType.CLIENTS_INFO;


                    for (int i = 0; i < _sessions.Count; i++)
                    {
                        if (_sessions[i].IsConnect && _sessions[i].IsLogged)
                        {
                            payload[i] = _sessions[i].Name;
                        }
                    }
                
                    foreach (ClientSession session in _sessions)
                    {
                        if (session.IsLogged && session.IsConnect)
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
                    if (message.Payload == null)
                        return;

                    session.Login(message.Payload[0]);

                    SendOutClientsInfo();

                    break;

                case (MessageType.DISCONNECT):

                    lock(_clientsLock)
                    {

                        _sessions.Remove(session);

                        session.Disconnect();

                        try { session.Stream.Socket.Shutdown(SocketShutdown.Both); }
                        catch { }

                        session.Client.Close();

                        SendOutClientsInfo();
                    }
                    
                    break;

                default:
                        
                    break;
            }

        } // end of HandkeCommand

       



    } // end of class TcpServer

}  // end of namespace ProductTcpServer



