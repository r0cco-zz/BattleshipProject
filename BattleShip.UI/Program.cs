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

            Player player1;
            Player player2;

            GameFlow gf = new GameFlow();
            StartMenu sm = new StartMenu();

            sm.DisplayStart();
            Console.Clear();

            // move this crap to gameflow
            Console.WriteLine("{0}, press enter to start placing ships.\n\n{1} LOOK AWAY!!", Player.Name,
                Player.Name2);
            Console.ReadLine();
            Console.Clear();

            gf.Player1ShipPlacement();
            Console.Clear();

            Console.WriteLine("{0}, press enter to start placing ships.\n\n{1} LOOK AWAY!!", Player.Name2,
                Player.Name1);
            Console.ReadLine();
            Console.Clear();

            gf.Player2ShipPlacement();
            Console.Clear();

            gf.GamePlay();

            Console.ReadLine();
        }
    }
}
