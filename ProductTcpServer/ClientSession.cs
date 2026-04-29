using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ProductTcpServer
{
    internal class ClientSession
    {
        public ClientSession(TcpClient client)
        {
            Client = client;
            Stream = client.GetStream();
            IsConnect = true;
        }

        public string? Name { get; private set; }

        public bool IsLogged => Name != null;

        public bool IsConnect { get; private set; }

        public TcpClient Client     { get; private set; }

        public NetworkStream Stream { get; private set; }

        public void Login(string username)
        {
            if (IsLogged)
                throw new Exception("Already logged in");
            Name = username;
        }

        public void Disconnect() => IsConnect = false;
    }
}
