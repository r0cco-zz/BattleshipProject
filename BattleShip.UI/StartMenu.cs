using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI
{
    class StartMenu
    {
        public void DisplayStart()
        {
            Console.WriteLine("\n\n\n\n\n\n");
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("Welcome to Battleship!".Length / 2 + 1)) + "}", "Welcome to Battleship! \n\n"));

            Console.Write(String.Format("{0," + ((Console.WindowWidth / 2) + ("Press (Enter) to continue:".Length / 2 - 2)) + "}", "Press (Enter) to continue:"));
            Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Player 1, What is your name? : ");
            var player1Name = Console.ReadLine();
            if (player1Name == String.Empty)
            {
                player1Name = "Player1";
            }

            //Player player1 = new Player(player1Name);
            Player.Name1 = player1Name;
            Console.Write("Player 2, what is your name? : ");
            var player2Name = Console.ReadLine();
            if (player2Name == String.Empty)
            {
                player2Name = "Player2";
            }
            //Player player2 = new Player(player2Name);
            Player.Name2 = player2Name;

            Console.Clear();
        }
    }
}
