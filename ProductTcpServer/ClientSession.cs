using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ProductTcpServer
{
    public class ClientSession
    {
        public ClientSession(TcpClient client)
        {
            Client = client;
            Stream = client.GetStream();
        }

        public string? Name { get; private set; }

        public bool IsLogged => Name != null;

        public TcpClient Client     { get; private set; }

        public NetworkStream Stream { get; private set; }

        public void Login(string username)
        {
            if (IsLogged)
                throw new Exception("Already logged in");
            Name = username;
        }
    }
}
