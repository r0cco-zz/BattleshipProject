using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.Requests;

namespace BattleShip.UI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Setting game colors.
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();

            var gf = new GameFlow();
            SplashScreen.DisplayStart();
            Console.Clear();

            gf.PlayGame();

            Console.ReadLine();
        }
    }
}
