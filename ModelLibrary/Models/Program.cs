using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Windows.Forms;
using ModelLibrary.Models;

namespace ModelLibrary.Models
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        //[STAThread]
        public static double Money { get; set; } = 1000;
        public static double Income { get; set; }

        public static double Cost { get; set; }

        public static double Profit { get; set; }

        static void Main()
        {
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Console.SetWindowPosition(1, 1);
            //Console.WindowTop = 0;
            //Console.WriteLine(Console.WindowLeft);

            //przygotowanie delegata
            //public delegate void OnTransactionDoneDelegate(Facility c, EventArgs e);

            //przygotować deklarację zdarzenia na podstawie powyższego delagata:
            //public event OnTransactionDoneDelegate OnTransactionDone;

            World world = new World();
            //usuniete
            //Form1 form1 = new Form1();

            //form1.Text = "Round: " + Round.RoundNumber;
            //usuniete
            //Application.Run(form1);

            //Round.Go();


        }
    }
}
