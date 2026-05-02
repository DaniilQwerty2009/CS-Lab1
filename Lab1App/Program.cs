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

            //TcpClient client = new TcpClient("127.0.0.1", 5000);

            //NetworkConnection networkConnection = NetworkConnection.Create(client);

            //networkConnection.NewMessageReceived += HandleMessage;


            //while (true)
            //{
            //    string strType;
            //    MessageType type = default;
            //    string[] payload = new string[1];

            //    strType = Console.ReadLine();

            //    if(Enum.TryParse(strType, out type))
            //    {
            //        payload[0] = Console.ReadLine();

            //        networkConnection.SendMessage(type, payload);
            //    }
            //}



            ApplicationConfiguration.Initialize();
            Application.Run(new ApplicationForm());
        }

        //public static void HandleMessage(NetworkMessage message)
        //{
        //    switch (message.Type)
        //    {
        //        case (MessageType.CLIENTS_INFO):
        //            if (message.Payload != null)
        //            {
        //                foreach (string s in message.Payload)
        //                    Console.WriteLine(s);
        //            }
                    
        //            break;

        //        default:

        //            break;
        //    }
        //}

    }

}