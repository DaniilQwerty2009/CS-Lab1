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
        CLIENTS_INFO,
        ASSIGNED_NETWORK_ID,
        PRODUCTS_DATA,

        SEND_PRODUCTS_DATA,
        DISCONNECT,
        LOGIN,
    };

    public class NetworkMessage
    {
        public MessageType Type {  get; internal set; }
        public string[] Payload { get; internal set; }

        internal NetworkMessage()
        {
            Payload = Array.Empty<string>();
        }
        
    }

    public class NetworkUser
    {
        public string Name { get; private set; }
        public int ID { get; private set; }

        public NetworkUser(string name, int id)
        {
            Name = name;
            ID = id;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class NetworkConnection
    {
        private readonly TcpClient _client;
        private readonly NetworkStream _stream;
        private readonly List<byte> _dataBuffer = new();
        private bool _isOpen;

        private readonly object _sendLock = new object();
        public NetworkStream Stream { get { return _stream; } }
        public bool IsOpen { get { return _isOpen; } }

        public event Action<NetworkMessage>? NewMessageReceived;
        public event EventHandler? ConnectionInterrupted;

        private NetworkConnection(TcpClient client) 
        {
            _client = client;
            _stream = client.GetStream();
            _isOpen = true;
        }

        public static NetworkConnection Create(TcpClient client)
        {
            NetworkConnection justCreatedConnection = new NetworkConnection(client);

            ThreadStart startListening = new ThreadStart(justCreatedConnection.ListenStream);

            Thread listening = new Thread(startListening);
            listening.Name = "listeningThread";
            listening.IsBackground = true;
            listening.Start();

            return justCreatedConnection;
        }

        public void RequestDisconnect()
        {
            if (!_isOpen)
                return;

            try
            {
                SendMessage(MessageType.DISCONNECT, Array.Empty<string>());
            }
            catch
            {
                // Connection could be already closed
            }

            Close();
        }

        public void Close()
        {
            if (!_isOpen)
                return;

            _isOpen = false;

            try
            {
                _client.Client.Shutdown(SocketShutdown.Both);
            }
            catch
            {
                // Connection could be already closed
            }

            _client.Close();
        }

        private void ListenStream()
        {
            try
            {
                while(_isOpen)
                {
                    if (TryReadFrame(out List<byte> frame))
                    {
                        if(TryParseMessage(frame, out NetworkMessage message))
                        {
                            if(message != null)
                                NewMessageReceived?.Invoke(message);  
                        }
                    }
                }
                
            }
            catch(IOException) 
            {
                ConnectionInterrupted?.Invoke(this, EventArgs.Empty);
            }
            finally
            {
                Close();
            }
        }

        private bool TryReadFrame(out List<byte> frame)
        {
            frame = new();
            int readBytes = 0;

            int c;
            
            while (true)
            {
                c = _stream.ReadByte();

                if (c == -1)
                {
                    Close();
                    return false;
                }
                else if ((char)c == '\n')
                {
                    break;
                }
                else
                {
                    frame.Add((byte)c);
                    readBytes++;
                }
            }

            if (readBytes == 0)
                return false;

            return true;
        }

        private bool TryParseMessage(List<byte> frame, out NetworkMessage message)
        {
            message = new();

            if (frame.Count == 0)
                return false;

            message = new();

            string msg = Encoding.UTF8.GetString(frame.ToArray());

            frame.Clear();
            

            string[] parts = msg.Split('|', 2);

            if (Enum.TryParse(parts[0], out MessageType result))
            {
                message.Type = result;
            }
            else
                return false;


            if (parts.Length > 1)
            {
                message.Payload = parts[1].Split('|');
            }

            return true;
        }

        private byte[] SerialiseMessage(MessageType type, string[] payload)
        {
            string message = type.ToString();

            if (payload.Length > 0)
            {
                message += '|';

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

        public void SendMessage(MessageType type, string[] payload)
        {
            byte[] data = SerialiseMessage(type, payload);

            lock(_sendLock)
            {
                _stream.Write(data, 0, data.Length);
            }
        }
    }
}
