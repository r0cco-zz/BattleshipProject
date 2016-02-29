using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;
using BattleShip.UI.GameFlowResponses;

namespace BattleShip.UI
{
    internal class GameFlow
    {
        private Player _player1 = new Player();
        private Player _player2 = new Player();

        private bool _isPlayerOnesTurn = true;
        private bool _gameOver;

        public void PlayGame()
        {
            GetPlayerNames();
            PlaceShips(_player1);
            PlaceShips(_player2);
            FireShots();
            //PromptPlayAgain();
        }

        private void GetPlayerNames()
        {
            Console.Write("Player 1, What is your name? : ");
            _player1.Name = Console.ReadLine();
            if (_player1.Name == String.Empty)
            {
                _player1.Name = "Player1";
            }

            Console.Write("Player 2, what is your name? : ");
            _player2.Name = Console.ReadLine();
            if (_player2.Name == String.Empty)
            {
                _player2.Name = "Player2";
            }

            Console.Clear();
        }

        private void PlaceShips(Player player)
        {

            //TODO Refactoring - Implement Generic PlayerShipPlacement. 
            //TODO HowTo: Create class to accept current player turn. Execute, return done. If player is player 2, move on.
            //TODO Create additional classes to simplify workflow: PlaceShipRequest workflow.

            // display empty game board for player1 (I put this in the loop)

            // prompt player1 for coordinate entry (use letterconverter for xcoordinate)
            foreach (ShipType stype in Enum.GetValues(typeof(ShipType)))
            {
                int shipLength;
                switch (stype)
                {
                    case ShipType.Destroyer:
                        shipLength = 2;
                        break;
                    case ShipType.Cruiser:
                    case ShipType.Submarine:
                        shipLength = 3;
                        break;
                    case ShipType.Battleship:
                        shipLength = 4;
                        break;
                    case ShipType.Carrier:
                        shipLength = 5;
                        break;
                    default:
                        shipLength = 0;
                        break;
                }
                bool placementIsGood;

                do
                {
                    BoardUI.DisplayGameBoardForShipPlacement(player.GameBoard);

                    string shipplacecoord;

                    //Old coordinate check.
                    //Console.Write("{0}, pick a coordinate for your {1} : ", Player.Name1, stype);
                    //string shipplacecoord = Console.ReadLine();

                    //Testing if valid input
                    bool coordIsValid;
                    do
                    {
                        var isItValid = new IsPlayercoordValid();

                        Console.Write("{0}, pick a coordinate for your {1} : ", player.Name, stype);
                        shipplacecoord = Console.ReadLine();

                        coordIsValid = isItValid.IsItGood(shipplacecoord);

                    } while (coordIsValid == false);

                    var xAsLetter = shipplacecoord.Substring(0, 1);
                    var shipX = LetterConverter.ConvertToNumber(xAsLetter); //Convert 1st char from player input to int.
                    var shipY = int.Parse(shipplacecoord.Substring(1, 1)); //Assign 2nd coord.

                    var shipcoord = new Coordinate(shipX, shipY);

                    // and then, asking for ship direction
                    Console.Write("{0}, Enter a direction (up, down, left, right) for your {1} (length {2}) : ",
                        player.Name, stype, shipLength);
                    var shipPlacementDirection = Console.ReadLine();

                    var isDirInputValid = new IsDirectionValid();

                    var inputResponse = isDirInputValid.WhatIsDirection(shipPlacementDirection);

                    switch (inputResponse)
                    {
                        //TODO: move this switch inside the shiprequest constructor
                        case 1:
                        {
                            PlaceShipRequest shipRequest = new PlaceShipRequest
                            {
                                Coordinate = shipcoord,
                                Direction = ShipDirection.Up,
                                ShipType = stype
                            };
                            placementIsGood = CheckShipPlacement(player, shipRequest, stype);
                        }
                            break;
                        case 2:
                        {
                            PlaceShipRequest shipRequest = new PlaceShipRequest
                            {
                                Coordinate = shipcoord,
                                Direction = ShipDirection.Down,
                                ShipType = stype
                            };
                            placementIsGood = CheckShipPlacement(player, shipRequest, stype);
                        }
                            break;
                        case 3:
                        {
                            PlaceShipRequest shipRequest = new PlaceShipRequest
                            {
                                Coordinate = shipcoord,
                                Direction = ShipDirection.Left,
                                ShipType = stype
                            };

                            placementIsGood = CheckShipPlacement(player, shipRequest, stype);
                        }
                            break;
                        case 4:
                        {
                            PlaceShipRequest shipRequest = new PlaceShipRequest
                            {
                                Coordinate = shipcoord,
                                Direction = ShipDirection.Right,
                                ShipType = stype
                            };

                            placementIsGood = CheckShipPlacement(player, shipRequest, stype);
                        }
                            break;
                        default:
                            placementIsGood = false;
                            break;
                    }
                } while (!placementIsGood);
                Console.Clear();
            }
        }

        private bool CheckShipPlacement(Player player, PlaceShipRequest request, ShipType stype)
        {
            var whereIsShip = player.GameBoard.PlaceShip(request);
            switch (whereIsShip)
            {
                case ShipPlacement.NotEnoughSpace:
                    Console.Clear();
                    Console.WriteLine("Not enough space to place ship there, try again!");
                    return false;
                case ShipPlacement.Overlap:
                    Console.Clear();
                    Console.WriteLine("You are overlapping another ship, try again!");
                    return false;
                case ShipPlacement.Ok:
                    ShipCreator.CreateShip(stype);
                    return true;
                default:
                    return false;
            }
        }

        //TODO Refactoring - Combine Player1 & 2 shooting & gameplay into a single code base. Class receives input on which player's turn it is.
        // actual shooting and gameplay
        public void FireShots()
        {
            while (!_gameOver)
            {
                while (_isPlayerOnesTurn && !_gameOver)
                {
                    //TODO:reverse these boards so player is taking shots in his own board and vice versa
                    BoardUI.DisplayGameBoardForShotFiring(_player2.GameBoard);
                    string p1Shot;

                    //Testing if valid input
                    bool coordIsValid;
                    do
                    {
                        IsPlayercoordValid IsItValid = new IsPlayercoordValid();

                        Console.Write("{0}, Take a shot! : ", _player1.Name);
                        p1Shot = Console.ReadLine();

                        coordIsValid = IsItValid.IsItGood(p1Shot);

                    } while (coordIsValid == false);

                    var p1Shotx = p1Shot.Substring(0, 1);
                    var p1Shotxasint = LetterConverter.ConvertToNumber(p1Shotx);
                    var p1Shoty = int.Parse(p1Shot.Substring(1));

                    var shotcoord = new Coordinate(p1Shotxasint, p1Shoty);

                    var p1FireShotResponse = _player2.GameBoard.FireShot(shotcoord);

                    if (p1FireShotResponse.ShotStatus == ShotStatus.Hit)
                    {
                        Console.WriteLine("You hit something! (Press enter)");
                        Console.ReadLine();
                        Console.Clear();
                        _isPlayerOnesTurn = false;
                    }
                    if (p1FireShotResponse.ShotStatus == ShotStatus.Duplicate)
                    {
                        Console.WriteLine("You already shot at that spot! (Press enter)");
                        Console.ReadLine();
                        Console.Clear();
                    }
                    if (p1FireShotResponse.ShotStatus == ShotStatus.HitAndSunk)
                    {
                        Console.WriteLine("Hit! You sunk your opponent's " + p1FireShotResponse.ShipImpacted + " (Press enter)");
                        Console.ReadLine();
                        Console.Clear();
                        _isPlayerOnesTurn = false;
                    }
                    if (p1FireShotResponse.ShotStatus == ShotStatus.Invalid)
                    {
                        Console.WriteLine("Invalid coordinate, try again! (Press enter)");
                        Console.ReadLine();
                        Console.Clear();
                    }
                    if (p1FireShotResponse.ShotStatus == ShotStatus.Miss)
                    {
                        Console.WriteLine("Your projectile splashes into the ocean, you missed! (Press enter)");
                        Console.ReadLine();
                        Console.Clear();
                        _isPlayerOnesTurn = false;
                    }
                    if (p1FireShotResponse.ShotStatus == ShotStatus.Victory)
                    {
                        Console.WriteLine("You have sunk all your opponent's ships, you win! (Press enter)");
                        Console.ReadLine();
                        Console.Clear();
                        _gameOver = true;
                        break;
                    }
                }

                while (!_isPlayerOnesTurn && !_gameOver)
                {
                    BoardUI.DisplayGameBoardForShotFiring(_player1Board);

                    string p2shot = "";
                    string p2shotx = "";
                    //checks

                    //Testing if valid input
                    bool coordIsValid = false;
                    do
                    {
                        IsPlayercoordValid IsItValid = new IsPlayercoordValid();

                        Console.Write("{0}, Take a shot! : ", Player.Name2);
                        p2shot = Console.ReadLine();

                        coordIsValid = IsItValid.IsItGood(p2shot);

                    } while (coordIsValid == false);
                    //end of testing

                    //do
                    //{
                    //    Console.Write("{0}, Take a shot! : ", Player.Name2);
                    //    p2shot = Console.ReadLine();
                    //} while (p2shotx.Length > 2);
                    p2shotx = p2shot.Substring(0, 1);
                    int p2shotxasint = LetterConverter.ConvertToNumber(p2shotx);
                    int p2shoty = int.Parse(p2shot.Substring(1));


                    Coordinate shotcoord = new Coordinate(p2shotxasint, p2shoty);

                    var p2FireShotResponse = _player1Board.FireShot(shotcoord);

                    if (p2FireShotResponse.ShotStatus == ShotStatus.Hit)
                    {
                        Console.WriteLine("You hit something! (Press enter)");
                        Console.ReadLine();
                        Console.Clear();
                        _isPlayerOnesTurn = true;
                    }
                    if (p2FireShotResponse.ShotStatus == ShotStatus.Duplicate)
                    {
                        Console.WriteLine("You already shot at that spot! (Press enter)");
                        Console.ReadLine();
                        Console.Clear();
                    }
                    if (p2FireShotResponse.ShotStatus == ShotStatus.HitAndSunk)
                    {
                        Console.WriteLine("Hit! You sunk your opponent's " + p2FireShotResponse.ShipImpacted + " (Press enter)");
                        Console.ReadLine();
                        Console.Clear();
                        _isPlayerOnesTurn = true;
                    }
                    if (p2FireShotResponse.ShotStatus == ShotStatus.Invalid)
                    {
                        Console.WriteLine("Invalid coordinate, try again! (Press enter)");
                        Console.ReadLine();
                        Console.Clear();
                    }
                    if (p2FireShotResponse.ShotStatus == ShotStatus.Miss)
                    {
                        Console.WriteLine("Your projectile splashes into the ocean, you missed! (Press enter)");
                        Console.ReadLine();
                        Console.Clear();
                        _isPlayerOnesTurn = true;
                    }
                    if (p2FireShotResponse.ShotStatus == ShotStatus.Victory)
                    {
                        Console.WriteLine("You have sunk all your opponent's ships, you win! (Press enter)");
                        Console.ReadLine();
                        Console.Clear();
                        _gameOver = true;
                    }
                }
            }
            Console.Clear();

            //TODO: break this into a different method PromptPlayAgain()
            //Yay, someone won. Play again?
            Console.Write("Play again? Type y or yes to play again. Type anything else to Quit: ");
            string playAgain = Console.ReadLine();

            if (playAgain == "y" || playAgain == "yes")
            {
                NewGame goAgain = new NewGame();
                //Restarting game.
                Console.Clear();
                goAgain.StartNewGame();
            }
            else
            {
                Console.WriteLine("Thanks for playing! Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }
}
