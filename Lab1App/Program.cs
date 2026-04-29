using System.Net.Sockets;
using System.Net;
using System.Text;



namespace LabApp
{
    internal static class Program
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
            
            NetworkStream stream = client.GetStream();

            ThreadStart startListening = () => Listener(stream);

            Thread listening = new Thread(startListening);
            listening.Name = "listeningThread";

            listening.Start();

            while (true)
            {
                string? messsage = Console.ReadLine();

                    if(messsage != null)
                {
                    messsage += '\n';

                    byte[] data = Encoding.UTF8.GetBytes(messsage);

                    stream.Write(data, 0, data.Length);

                    Console.WriteLine("***Сообщение отправлено***");
                }
            }



            //ApplicationConfiguration.Initialize();
            //Application.Run(new ApplicationForm());
        }


        static void Listener(NetworkStream stream)
        {
            while (true)
            {
               
                byte[] data = new byte[1024];

                int readBytes = stream.Read(data, 0, data.Length);

                if (readBytes == 0)
                {
                    Console.WriteLine("Session perfomed a gracefull shotdown");
                    break;
            }

                string receveMsg = Encoding.UTF8.GetString(data, 0, readBytes);

                Console.WriteLine(receveMsg);

        }

        }
        
    }

}