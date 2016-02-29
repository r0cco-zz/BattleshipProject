using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI
{
    public static class SplashScreen
    {
        public static void DisplayStart()
        {
            Console.WriteLine("\n\n\n\n\n\n");
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("Welcome to Battleship!".Length / 2 + 1)) + "}", "Welcome to Battleship! \n\n"));

            Console.Write(String.Format("{0," + ((Console.WindowWidth / 2) + ("Press (Enter) to continue:".Length / 2 - 2)) + "}", "Press (Enter) to continue:"));
            Console.ReadLine();
            Console.Clear();
        }
    }
}
