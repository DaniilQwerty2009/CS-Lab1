using System.Net.Sockets;

namespace ProductTcpShared
{


    public class ClientSession
    {        
        public ClientSession(TcpClient client)
        {
            Connection = NetworkConnection.Create(client);
        }

        public string? Name { get; private set; }

        public bool IsLogged => Name != null;

        public NetworkConnection Connection { get; private set; }

        public void Login(string username)
        {
            if (IsLogged)
                throw new Exception("Already logged in");
            Name = username;
        }

        public void End()
        {
            if(Connection.IsOpen)
                Connection.Close();
        }
    }


}
