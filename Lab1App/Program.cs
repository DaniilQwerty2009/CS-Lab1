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

            ApplicationConfiguration.Initialize();
            Application.Run(new ApplicationForm());
        }


    }

}