using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

                    _sessions.Add(newSession);

                    ThreadStart threadStart = () => Listen(newSession);

                    Console.WriteLine("***Очередное подключения перенаправлено в отдельный поток***");

                    Thread newThread = new(threadStart);

                    newThread.Start();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to listen: {ex.Message}");
            }
            finally
            {
                foreach (ClientSession session in _sessions)
                {
                    session.Client.Close();
                }
                _sessions.Clear();

            }

        } // End of Listen

        private void Listen(ClientSession session)
        {
            NetworkStream stream = session.Client.GetStream();

            try
            {
                while (session.IsConnect)
                {
                    if(TryReadFrame(session.Stream, out string message, out int readBytes))
                    {
                        if (readBytes == -1)
                        {
                            Console.WriteLine($"Peer socket perfomed a gracefull shotdown: {session.Name}");
                            break;
                        }

                        if(TryParseMessage(message, out ClientCommand command, out string[]? points))
                        {
                            if(HandleCommand(session, command, points))
                            {
                                ////Проверка

                                //byte[] bytes = new byte[Encoding.UTF8.GetByteCount(command.ToString())];

                                //bytes = Encoding.UTF8.GetBytes(command.ToString());

                                //string point = string.Empty;

                                //if (points != null)
                                //{
                                //    foreach (string s in points)
                                //    {
                                //        point += s;
                                //    }
                                //}

                                ////if(session.Name != null)
                                ////    byte[] bytes1 = new byte[Encoding.UTF8.GetByteCount(session.Name)];
                                //byte[] bytes1 = new byte[1024];

                                //if (session.Name != null)
                                //    bytes1 = Encoding.UTF8.GetBytes(session.Name);

                                //lock (_clientsLock)
                                //{
                                //    foreach (ClientSession s in _sessions)
                                //    {
                                //        s.Stream.Write(bytes, 0, bytes.Length);
                                //        s.Stream.WriteByte((byte)'\n');
                                //        s.Stream.Write(bytes1, 0, bytes1.Length);
                                //    }
                                //}
                            } 
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Failed to read {session.Name}: {ex.Message}");
            }
            finally
            {
                session.Client.Client.Shutdown(SocketShutdown.Both);
                session.Client.Close();
                lock(_clientsLock)
                {
                    _sessions.Remove(session);
                }
            }

        }// End of TalkWith

        private void SendSessionsList()
        {
            lock(_clientsLock)
            {
                string sessions = string.Empty;

                foreach (ClientSession s in _sessions)
                {
                    sessions += s.Name + '|';
                }

                if(sessions.Length > 0)
                {
                    byte[] sessionsData = Encoding.UTF8.GetBytes(sessions);

                    foreach (ClientSession session in _sessions)
                    {
                        if(session.Name != null)
                            session.Stream.Write(sessionsData, 0, sessionsData.Length);
                    }

                }
            }
        } // end of SendSessionsList

        static private bool HandleCommand(ClientSession session, ClientCommand command, string[]? points)
        {
            switch(command)
            {
                case (ClientCommand.LOGIN):
                    if (points == null)
                        return false;
                    session.Login(points[0]);
                    break;

                case (ClientCommand.DISCONNECT):
                    session.Disconnect();
                    break;

                default:
                        
                    break;
            }


            return true;
        }

        static private bool TryParseMessage(string message, out ClientCommand command, out string[]? points)
        {
            command = default;
            points = null;

            if (message.Length == 0)
                throw new Exception("TryParseMessage was passed empty message");


            string[] parts = message.Split('|', 2);

            if( ! Enum.TryParse(parts[0], out command))
            {
                return false;
            }

            if (parts.Length > 1)
            {
                int pointsCount = parts.Length - 1;

                points = parts[1].Split('|');                    
            }
            return true;
        }

        static private bool TryReadFrame(NetworkStream stream, out string message, out int readBytes)
        {
            message = string.Empty;
            readBytes = 0;

            int c;
            List<byte> bytes = new();

            while(true)
            {
                c = stream.ReadByte();

                if (c == -1)
                {
                    readBytes = -1;
                    break;
                }
                else if ((char)c == '\n')
                {
                    break;
                }
                else
                {
                    bytes.Add((byte)c);
                    readBytes++;
                }
            }

            if (readBytes == 0)
                return false;

            message = Encoding.UTF8.GetString(bytes.ToArray());

            return true;
        }



    } // end of class TcpServer

}  // end of namespace ProductTcpServer



