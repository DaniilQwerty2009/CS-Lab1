using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

    public class NetworkMessage
    {
        public MessageType Type {  get; internal set; }
        public string[]? Payload { get; internal set; }

        internal NetworkMessage()
        {   }
        
        internal NetworkMessage(NetworkMessage other)
        {
            Type = other.Type;
            if(other.Payload != null)
                Payload = other.Payload.ToArray();
            else
                Payload = null;
        }
    }

    public class NetworkConnection
    {
        TcpClient Client { get; set; }

        List<byte> dataBuffer = new();

        public NetworkMessage NetworkMessage { get; private set; }

        bool IsOpen { get; set; }

        public NetworkStream Stream
        {
            get { return Client.GetStream(); }
        }

        public event Action<NetworkMessage>? NewMessageReceved;
        // event ClientWasClosed

        private NetworkConnection(TcpClient client) 
        {
            Client = client;
            NetworkMessage = new NetworkMessage();
        }

        public static NetworkConnection Create(TcpClient client)
        {
            NetworkConnection justCreatedConnection = new NetworkConnection(client);
            justCreatedConnection.IsOpen = true;

            ThreadStart startListening = new ThreadStart(justCreatedConnection.ListenStream);

            Thread listening = new Thread(startListening);
            listening.Name = "listeningThread";
            listening.IsBackground = true;
            listening.Start();

            return justCreatedConnection;
        }


        private void ListenStream()
        {
            try
            {
                while(IsOpen)
                {
                    if (TryReadFrame())
                    {
                        if(TryParseMessage())
                        {
                            NewMessageReceved?.Invoke(new(NetworkMessage));  
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
                Console.WriteLine("The connection has closed properly");   
            }
        }

        private bool TryReadFrame()
        {
            int readBytes = 0;

            int c;
            
            while (true)
            {
                c = Stream.ReadByte();

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

            dataBuffer.Clear();

            string[] parts = message.Split('|', 2);

            if (Enum.TryParse(parts[0], out MessageType result))
            {
                NetworkMessage.Type = result;
            }
            else
                return false;


            if (parts.Length > 1)
            {
                NetworkMessage.Payload = parts[1].Split('|');
            }
            return true;
        }

        private byte[] SerialiseMessage(MessageType type, string?[] payload)
        {
            string message = type.ToString() + '|';

            if (payload != null)
            {
                for (int i = 0; i < payload.Length; i++)
                {
                    message += payload[i];

                    if (i < payload.Length - 1)
                        message += '|';
                }
            }
            
            message += '\n';

            return Encoding.UTF8.GetBytes(message);
        }

        public void SendMessage(MessageType type, string?[] payload)
        {
            byte[] data = SerialiseMessage(type, payload);

            Stream.Write(data, 0, data.Length);

            if (type == MessageType.DISCONNECT)
            {
                if (IsOpen)
                    IsOpen = false;

                try { Stream.Socket.Shutdown(SocketShutdown.Both); }
                catch { }

                Client.Close();
            }
        }

    }
}
