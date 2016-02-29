using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;

namespace BattleShip.UI
{
    internal class BoardUI
    {
        private static readonly string[] _aToJ = {" A ", " B ", " C ", " D ", " E ", " F ", " G ", " H ", " I ", " J "};

        public static void DisplayGameBoard(Board playerBoard)
        {
            //Board newBoard = new Board();

            string Displaychar = "~";
            //get Displaychar to change based on ship placement & shot history

            for (int i = 0; i < 10; i++)
            {
                Console.Write("    {0}", _aToJ[i]);
            } //Generating across board.

            Console.Write("\n");

            for (int i = 0; i < 35; i++)
            {
                Console.Write("__");
            } //Generating Border.

            // loop to get all values (including row 10) for values to fill coordinate object (necessary?)

            for (int i = 0; i < 9; i++) //9
            {
                Console.Write("\n" + (i + 1) + " |");
                for (int j = 0; j < 10; j++) //10
                {
                    Coordinate coord = new Coordinate(j + 1, i + 1);
                    if (playerBoard.ShotHistory.ContainsKey(coord) &&
                        playerBoard.ShotHistory[coord].Equals(ShotHistory.Hit)) //Make sure Shot is valid && is a hit.
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Displaychar = "H";
                        Console.Write("  {0}    ", Displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;

                    }
                    else if (playerBoard.ShotHistory.ContainsKey(coord) &&
                             playerBoard.ShotHistory[coord].Equals(ShotHistory.Miss))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Displaychar = "M";
                        Console.Write("  {0}    ", Displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    //(playerBoard.ShotHistory.ContainsKey(coord) && playerBoard.ShotHistory.ContainsValue(ShotHistory.Unknown))
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Displaychar = "~";
                        Console.Write("  {0}    ", Displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                }
                Console.Write("\n  |");
            }
            // loop again for row 10 because of the extra char in the label
            for (int i = 9; i < 10; i++) //10
            {
                Console.Write("\n" + (i + 1) + "|");
                for (int j = 0; j < 10; j++) //10
                {
                    Coordinate coord = new Coordinate(j + 1, i + 1);
                    if (playerBoard.ShotHistory.ContainsKey(coord) &&
                        playerBoard.ShotHistory.ContainsValue(ShotHistory.Hit))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Displaychar = "H";
                        Console.Write("  {0}    ", Displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;

                    }
                    if (playerBoard.ShotHistory.ContainsKey(coord) &&
                        playerBoard.ShotHistory.ContainsValue(ShotHistory.Miss))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Displaychar = "M";
                        Console.Write("  {0}    ", Displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    //(playerBoard.ShotHistory.ContainsKey(coord) && playerBoard.ShotHistory.ContainsValue(ShotHistory.Unknown))
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Displaychar = "~";
                        Console.Write("  {0}    ", Displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    //Console.Write("  {0}    ", Displaychar);
                }
                Console.WriteLine("\n  |");

            }
        }

        public static void DisplayShipBoard(Board playerBoard)
        {
            string Displaychar = "~";

            for (int i = 0; i < 10; i++)
            {
                Console.Write("    {0}", _aToJ[i]);
            } //Generating across board.		
            Console.Write("\n");

            for (int i = 0; i < 35; i++)
            {
                Console.Write("__");
            } //Generating Border.		

            // loop to get all values (including row 10) for values to fill coordinate object (necessary?)		

            for (int i = 0; i < 9; i++) //9		
            {
                Console.Write("\n" + (i + 1) + " |");
                for (int j = 0; j < 10; j++) //10		
                {
                    Coordinate coord = new Coordinate(j + 1, i + 1);
                    if (playerBoard.ShipHistory.ContainsKey(coord) &&
                        playerBoard.ShipHistory[coord].Equals(ShipType.Battleship))
                        //Make sure Shot is valid && is a hit.		
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Displaychar = "B";
                        Console.Write("  {0}    ", Displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;

                    }
                    else if (playerBoard.ShipHistory.ContainsKey(coord) &&
                             playerBoard.ShipHistory[coord].Equals(ShipType.Carrier))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Displaychar = "C";
                        Console.Write("  {0}    ", Displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (playerBoard.ShipHistory.ContainsKey(coord) &&
                             playerBoard.ShipHistory[coord].Equals(ShipType.Cruiser))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Displaychar = "C";
                        Console.Write("  {0}    ", Displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (playerBoard.ShipHistory.ContainsKey(coord) &&
                             playerBoard.ShipHistory[coord].Equals(ShipType.Destroyer))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Displaychar = "D";
                        Console.Write("  {0}    ", Displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (playerBoard.ShipHistory.ContainsKey(coord) &&
                             playerBoard.ShipHistory[coord].Equals(ShipType.Submarine))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Displaychar = "S";
                        Console.Write("  {0}    ", Displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Displaychar = "~";
                        Console.Write("  {0}    ", Displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                }
                Console.Write("\n  |");
            }
            // loop again for row 10 because of the extra char in the label		
            for (int i = 9; i < 10; i++) //10		
            {
                Console.Write("\n" + (i + 1) + "|");
                for (int j = 0; j < 10; j++) //10		
                {
                    Coordinate coord = new Coordinate(j + 1, i + 1);
                    if (playerBoard.ShipHistory.ContainsKey(coord) &&
                        playerBoard.ShipHistory[coord].Equals(ShipType.Battleship))
                        //Make sure Shot is valid && is a hit.		
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Displaychar = "B";
                        Console.Write("  {0}    ", Displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;

                    }
                    else if (playerBoard.ShipHistory.ContainsKey(coord) &&
                             playerBoard.ShipHistory[coord].Equals(ShipType.Carrier))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Displaychar = "C";
                        Console.Write("  {0}    ", Displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (playerBoard.ShipHistory.ContainsKey(coord) &&
                             playerBoard.ShipHistory[coord].Equals(ShipType.Cruiser))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Displaychar = "C";
                        Console.Write("  {0}    ", Displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (playerBoard.ShipHistory.ContainsKey(coord) &&
                             playerBoard.ShipHistory[coord].Equals(ShipType.Destroyer))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Displaychar = "D";
                        Console.Write("  {0}    ", Displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (playerBoard.ShipHistory.ContainsKey(coord) &&
                             playerBoard.ShipHistory[coord].Equals(ShipType.Submarine))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Displaychar = "S";
                        Console.Write("  {0}    ", Displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Displaychar = "~";
                        Console.Write("  {0}    ", Displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                }

                Console.Write("\n  |");
            }
        }
    }
}
