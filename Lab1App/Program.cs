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
            var client = new TcpClient("127.0.0.1", 5000);

            var stream = client.GetStream();

            ThreadStart startListening = () => Listener(stream);

            Thread listening = new Thread(startListening);

            listening.Start();

            while (true)
            {
                string? messsage = Console.ReadLine();

                if (messsage == "0")
                {
                    Console.WriteLine("Сеанс завершен");
                    break;
                }

                byte[] data = Encoding.UTF8.GetBytes(messsage);

                stream.Write(data, 0, data.Length);

                Console.WriteLine("Сообщение отправлено");
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
                    Console.WriteLine("Сервер завершил работу");
                    break;
                }

                string receveMsg = Encoding.UTF8.GetString(data, 0, readBytes);

                Console.WriteLine(receveMsg);

            }

        }

    }

}