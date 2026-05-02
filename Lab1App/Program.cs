using ProductTcpShared;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Collections.Specialized.BitVector32;



namespace LabApp
{
    internal  class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            TcpClient client = new TcpClient("127.0.0.1", 5000);

            NetworkConnection networkConnection = NetworkConnection.Create(client);

            networkConnection.NewMessageReceved += HandleMessage;

            //TcpClient client = new TcpClient("127.0.0.1", 5000);

            //NetworkStream stream = client.GetStream();

            //ThreadStart startListening = () => Listener(stream);

            //Thread listening = new Thread(startListening);
            //listening.Name = "listeningThread";

            //listening.Start();

            while (true)
            {
                networkConnection.SendMessage(MessageType.LOGIN, ["user"]);

                networkConnection.SendMessage(MessageType.DISCONNECT, [string.Empty]);

            }



            //ApplicationConfiguration.Initialize();
            //Application.Run(new ApplicationForm());
        }

        public static void HandleMessage(NetworkMessage message)
        {
            switch (message.Type)
            {
                case (MessageType.CLIENTS_INFO):
                    if (message.Payload != null)
                    {
                        foreach (string s in message.Payload)
                            Console.WriteLine(s);
                    }
                    
                    break;

                default:

                    break;
            }
        }

    }

}