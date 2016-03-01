using System;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;

namespace BattleShip.UI
{
    internal class BoardUI
    {
        private static readonly string[] AtoJ = {" A ", " B ", " C ", " D ", " E ", " F ", " G ", " H ", " I ", " J "};

        public static void DisplayGameBoardForShotFiring(Board playerBoard)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Write("    {0}", AtoJ[i]);
            }

            Console.Write("\n");

            for (int i = 0; i < 35; i++)
            {
                Console.Write("__");
            } 

            for (int i = 0; i < 10; i++) 
            {
                if (i < 9)
                {
                    Console.Write("\n" + (i + 1) + " |");
                }
                else if (i == 9)
                {
                    Console.Write("\n" + (i + 1) + "|");
                }
                for (int j = 0; j < 10; j++) 
                {
                    string displaychar;
                    Coordinate coord = new Coordinate(j + 1, i + 1);
                    if (playerBoard.ShotHistory.ContainsKey(coord) &&
                        playerBoard.ShotHistory[coord].Equals(ShotHistory.Hit))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        displaychar = "H";
                        Console.Write("  {0}    ", displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;

                    }
                    else if (playerBoard.ShotHistory.ContainsKey(coord) &&
                             playerBoard.ShotHistory[coord].Equals(ShotHistory.Miss))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        displaychar = "M";
                        Console.Write("  {0}    ", displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        displaychar = "~";
                        Console.Write("  {0}    ", displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                }
                Console.Write("\n  |");
            }
        }

        public static void DisplayGameBoardForShipPlacement(Board playerBoard)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Write("    {0}", AtoJ[i]);
            }
            		
            Console.Write("\n");

            for (int i = 0; i < 35; i++)
            {
                Console.Write("__");
            }	

            for (int i = 0; i < 10; i++) 	
            {
                if (i < 9)
                {
                    Console.Write("\n" + (i + 1) + " |");
                }
                else if (i == 9)
                {
                    Console.Write("\n" + (i + 1) + "|");
                }
                for (int j = 0; j < 10; j++) 		
                {
                    string displaychar;
                    Coordinate coord = new Coordinate(j + 1, i + 1);
                    if (playerBoard.ShipHistory.ContainsKey(coord) &&
                        playerBoard.ShipHistory[coord].Equals(ShipType.Battleship))		
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        displaychar = "B";
                        Console.Write("  {0}    ", displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;

                    }
                    else if (playerBoard.ShipHistory.ContainsKey(coord) &&
                             playerBoard.ShipHistory[coord].Equals(ShipType.Carrier))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        displaychar = "C";
                        Console.Write("  {0}    ", displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (playerBoard.ShipHistory.ContainsKey(coord) &&
                             playerBoard.ShipHistory[coord].Equals(ShipType.Cruiser))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        displaychar = "C";
                        Console.Write("  {0}    ", displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (playerBoard.ShipHistory.ContainsKey(coord) &&
                             playerBoard.ShipHistory[coord].Equals(ShipType.Destroyer))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        displaychar = "D";
                        Console.Write("  {0}    ", displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (playerBoard.ShipHistory.ContainsKey(coord) &&
                             playerBoard.ShipHistory[coord].Equals(ShipType.Submarine))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        displaychar = "S";
                        Console.Write("  {0}    ", displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        displaychar = "~";
                        Console.Write("  {0}    ", displaychar);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                }
                Console.Write("\n  |");
            }
        }
    }
}
