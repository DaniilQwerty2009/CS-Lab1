using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ProductTcpShared
{
    public enum MessageType
    {
        /// <summary>
        /// Indicates that the server sends a list of connected client names.
        /// </summary>
        CLIENTS_INFO,

        RQUEST_PRODUCTS_DATA,
        SEND_PRODUCTS_DATA,
        DISCONNECT,
        LOGIN,
    };

    internal class NetworkMessage
    {
        internal MessageType Type {  get; set; }
        internal string[]? Payload { get; set; }
    }

    public class NetworkConnection
    {
        public static NetworkConnection Create(IPAddress ip, int port)
        {
            string _ip = ip.ToString();
            return Create(_ip, port);
        }
        public static NetworkConnection Create(string ip, int port)
        {
            TcpClient client = new TcpClient(ip, port);

            NetworkConnection justCreatedConnection = new NetworkConnection(client);
            justCreatedConnection.IsOpen = true;

            ThreadStart startListening = new ThreadStart(justCreatedConnection.ListenStream);

            Thread listening = new Thread(startListening);
            listening.Name = "listeningThread";
            listening.IsBackground = true;
            listening.Start();

            return justCreatedConnection;
        }

        private NetworkConnection(TcpClient client) 
        {
            Client = client;
            networkMessage = new NetworkMessage();
        }

        TcpClient Client { get; set; }

        public NetworkStream Stream 
        {
            get { return Client.GetStream(); } 
        }

        List<byte> dataBuffer = new();

        NetworkMessage networkMessage;

        bool IsOpen {  get; set; }
        // event ClientWasClosed

        private void ListenStream()
        {
            try
            {
                while(IsOpen)
                {
                    if (TryReadFrame(Client.GetStream()))
                    {
                        if(TryParseMessage())
                        {
                            // Проверка
                            Console.WriteLine(networkMessage.Type.ToString());
                            foreach(string s in networkMessage.Payload)
                                Console.WriteLine('*' + s);
                        }
                    }
                }
                
            }
            catch(IOException e) 
            {
                Console.WriteLine($"ListenStream catch exeption: {e.Message}");
            }
            finally
            {
                Client.Close();
                Console.WriteLine("The connection has closed properly");   
            }
        }

        private bool TryReadFrame(NetworkStream stream)
        {
            int readBytes = 0;

            int c;
            
            while (true)
            {
                c = stream.ReadByte();

                if (c == -1)
                {
                    IsOpen = false;
                    return false;
                }
                else if ((char)c == '\n')
                {
                    break;
                }
                else
                {
                    dataBuffer.Add((byte)c);
                    readBytes++;
                }
            }

            if (readBytes == 0)
                return false;

            return true;
        }

        private bool TryParseMessage()
        {

            if (dataBuffer.Count == 0)
                throw new Exception("TryParseMessage was trying parce empty message");

            string message = Encoding.UTF8.GetString(dataBuffer.ToArray());

            string[] parts = message.Split('|', 2);

            if (Enum.TryParse(parts[0], out MessageType result))
            {
                networkMessage.Type = result;
            }
            else
                return false;


            if (parts.Length > 1)
            {
                networkMessage.Payload = parts[1].Split('|');
            }
            return true;
        }
    }
}
