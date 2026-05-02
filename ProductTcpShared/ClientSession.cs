using System.Net.Sockets;

namespace ProductTcpShared
{


    public class ClientSession
    {        
        public ClientSession(TcpClient client)
        {
            Client = client;
            Stream = client.GetStream();

            Connection = NetworkConnection.Create(client);
            
            IsConnect = true;
        }

        public string? Name { get; private set; }

        public bool IsLogged => Name != null;

        public bool IsConnect { get; private set; }

        public TcpClient Client { get; private set; }

        public  NetworkStream Stream { get; private set; }

        public NetworkConnection Connection { get; private set; }

        public void Login(string username)
        {
            if (IsLogged)
                throw new Exception("Already logged in");
            Name = username;
        }

        public void Disconnect() => IsConnect = false;
    }


}
