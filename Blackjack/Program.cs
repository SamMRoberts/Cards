using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blackjack
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //Console.WriteLine("Game started.");
            //Environment environment = new Environment();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Blackjack.GameUI());
        }
    }
}