using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ProductTcpServer
{
    internal class ConnectedClient
    {
        public ConnectedClient(string name, TcpClient client, NetworkStream stream)
        {
            Name = name;
            Client = client;
            Stream = stream;
        }

        public string Name {  get; }

        public TcpClient Client { get; }

        public NetworkStream Stream { get; }
    }
}
