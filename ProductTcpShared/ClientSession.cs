using System.Net.Sockets;

namespace ProductTcpShared
{


    public class ClientSession
    {
        public ClientSession(TcpClient client, int id)
        {
            Connection = NetworkConnection.Create(client);
            ID = id;
            Name = string.Empty;
        }

        public int ID { get; }

        public string Name { get; private set; }

        public bool IsLogged => Name != string.Empty;

        public NetworkConnection Connection { get; private set; }

        public void Login(string username)
        {
            if (IsLogged)
                throw new Exception("Already logged in");
            if (username == string.Empty)
                throw new Exception("Empty string perfomed");
            
            Name = username;
        }

        public void End()
        {
            if(Connection.IsOpen)
                Connection.Close();
        }
    }


}
