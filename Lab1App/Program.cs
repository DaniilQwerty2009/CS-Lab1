using System.Net;
using System.Net.Sockets;
using ProductTcpServer;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;



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

            ThreadStart startListening = () => Listening(stream);

            Thread listening = new Thread(startListening);

            listening.Start();

            while (true)
            {
                string? messsage = Console.ReadLine();

                byte[] data = Encoding.UTF8.GetBytes(messsage);

                stream.Write(data, 0, data.Length);

                Console.WriteLine("Сообщение отправлено");
            }

            //ApplicationConfiguration.Initialize();
            //Application.Run(new ApplicationForm());
        
        } // end of Main

        static void Listening(NetworkStream stream)
        {

            string receveMsg;
            int bytesCounter = 0;

            while (true)
            {
                int messageSize = 0;
                char buf = ' ';
                while (buf != '|')
                {
                    buf = (char)stream.ReadByte();

                    if (!Char.IsDigit(buf) || buf == '|')
                        return;                                             //need to throw Exeption or do smth
                    else
                        messageSize += (int)buf;                    
                }

                byte[] buffer = new byte[messageSize];
                while(bytesCounter <= messageSize)
                {
                    bytesCounter = stream.Read(buffer, 0, messageSize);
                }

                receveMsg = Encoding.UTF8.GetString(buffer);

                ServerResponseInterpretator(receveMsg);

                //if (readBytes == 0)
                //{
                //    Console.WriteLine("Сервер завершил работу");
                //    break;
                //}

                //receveMsg = Encoding.UTF8.GetString(data, 0 , readBytes);

                //ServerResponseInterpretator(receveMsg);


            }

        } // end of Listener

        static void ServerResponseInterpretator(string response)
        {
            if (response.IsWhiteSpace() || string.IsNullOrEmpty(response))
                return;                                                         //need to throw Exeption or do smth

            string[] parts = response.Split('|');

            if (!int.TryParse(parts[0], out int dataSyze))
                return;

            if(!Enum.TryParse<ServerResponse>(parts[1], out ServerResponse command))
            {
                return;                                                          //need to throw Exeption or do smth
            }
            else
            {
                switch (command)
                {
                    case (ServerResponse.CLIENTS_INFO):
                        foreach (string s in parts)
                        {
                            Console.WriteLine(s);
                        }
                        break;

                    default:
                        break;                                                  //need to throw Exeption or do smth
                }
            }
            
        }

    } // end of class Program

} // end of LabApp