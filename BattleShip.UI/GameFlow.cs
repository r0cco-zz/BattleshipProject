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
        //public Player player1 { get; set; }
        //public Player player2 { get; set; }

        private Board _player1Board = new Board();
        private Board _player2Board = new Board();

        //private int[,] boardarray = new int[10, 10];

        private bool _isPlayerOnesTurn = true;
        private bool _gameOver = false;

        // get player 1 to place ships
        public void Player1ShipPlacement()
        {

            //TODO Refactoring - Implement Generic PlayerShipPlacement. 
            //TODO HowTo: Create class to accept current player turn. Execute, return done. If player is player 2, move on.
            //TODO Create additional classes to simplify workflow: PlaceShipRequest workflow.

            // display empty game board for player1 (I put this in the loop)

            // prompt player1 for coordinate entry (use letterconverter for xcoordinate)
            foreach (ShipType stype in Enum.GetValues(typeof(ShipType)))
            {
                var placementIsGood = false;
                do
                {
                    BoardUI.DisplayShipBoard(_player1Board);

                    string shipplacecoord;

                    //Old coordinate check.
                    //Console.Write("{0}, pick a coordinate for your {1} : ", Player.Name1, stype);
                    //string shipplacecoord = Console.ReadLine();

                    //Testing if valid input
                    bool coordIsValid;
                    do
                    {
                        var IsItValid = new IsPlayercoordValid();

                        Console.Write("{0}, pick a coordinate for your {1} : ", Player.Name1, stype);
                        shipplacecoord = Console.ReadLine();

                        coordIsValid = IsItValid.IsItGood(shipplacecoord);

                    } while (coordIsValid == false);

                    var xAsLetter = shipplacecoord.Substring(0, 1);
                    var shipX = LetterConverter.ConvertToNumber(xAsLetter); //Convert 1st char from player input to int.
                    var shipY = int.Parse(shipplacecoord.Substring(1, 1)); //Assign 2nd coord.

                    var shipcoord = new Coordinate(shipX, shipY);

                    // and then, asking for ship direction
                    Console.Write("{0}, Enter a direction (up, down, left, right) for your {1} (length {2}) : ",
                        Player.Name1, stype, "length");
                    var shipPlacementDirection = Console.ReadLine();

                    var IsDirInputValid = new IsDirectionValid();

                    var InputResponse = IsDirInputValid.WhatIsDirection(shipPlacementDirection);

                    switch (InputResponse)
                    {
                        case 1:
                        {
                            PlaceShipRequest shipRequest = new PlaceShipRequest
                            {
                                Coordinate = shipcoord,
                                Direction = ShipDirection.Up,
                                ShipType = stype
                            };
                            //checks
                            var WhereIsShip = _player1Board.PlaceShip(shipRequest);
                            //Refactor for class
                            if (WhereIsShip == ShipPlacement.NotEnoughSpace)
                            {
                                Console.Clear();
                                Console.WriteLine("Not enough space to place ship there, Try again!");
                            }
                            else if (WhereIsShip == ShipPlacement.Overlap)
                            {
                                Console.Clear();
                                Console.WriteLine("You are overlapping another ship, try again!");
                            }
                            else if (WhereIsShip == ShipPlacement.Ok)
                            {
                                ShipCreator.CreateShip(stype);
                                placementIsGood = true;
                            }

                            //ShipCreator.CreateShip(stype);
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
                            //checks

                            var WhereIsShip = _player1Board.PlaceShip(shipRequest);
                            //Refactor for class
                            if (WhereIsShip == ShipPlacement.NotEnoughSpace)
                            {
                                Console.Clear();
                                Console.WriteLine("Not enough space to place ship there, Try again!");
                            }
                            else if (WhereIsShip == ShipPlacement.Overlap)
                            {
                                Console.Clear();
                                Console.WriteLine("You are overlapping another ship, try again!");
                            }
                            else if (WhereIsShip == ShipPlacement.Ok)
                            {
                                ShipCreator.CreateShip(stype);
                                placementIsGood = true;
                            }
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

                            //checks
                            var WhereIsShip = _player1Board.PlaceShip(shipRequest);
                            //Refactor for class
                            if (WhereIsShip == ShipPlacement.NotEnoughSpace)
                            {
                                Console.Clear();
                                Console.WriteLine("Not enough space to place ship there, Try again!");
                            }
                            else if (WhereIsShip == ShipPlacement.Overlap)
                            {
                                Console.Clear();
                                Console.WriteLine("You are overlapping another ship, try again!");
                            }
                            else if (WhereIsShip == ShipPlacement.Ok)
                            {
                                ShipCreator.CreateShip(stype);
                                placementIsGood = true;
                            }
                            //ShipCreator.CreateShip(stype);
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

                            //checks
                            var WhereIsShip = _player1Board.PlaceShip(shipRequest);
                            //Refactor for class
                            if (WhereIsShip == ShipPlacement.NotEnoughSpace)
                            {
                                Console.Clear();
                                Console.WriteLine("Not enough space to place ship there, Try again!");
                            }
                            else if (WhereIsShip == ShipPlacement.Overlap)
                            {
                                Console.Clear();
                                Console.WriteLine("You are overlapping another ship, try again!");
                            }
                            else if (WhereIsShip == ShipPlacement.Ok)
                            {
                                ShipCreator.CreateShip(stype);
                                placementIsGood = true;
                            }
                            //ShipCreator.CreateShip(stype);
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

        // get player 2 to place ships
        public void Player2ShipPlacement()
        {
            // display empty game board for player1 (I put this in the loop)

            // prompt player1 for coordinate entry (use letterconverter for xcoordinate)
            foreach (ShipType stype in Enum.GetValues(typeof(ShipType)))
            {
                bool placementIsBad = false;
                do
                {
                    BoardUI.DisplayShipBoard(_player2Board);

                    //Testing if valid input
                    bool coordIsValid = false;
                    string shipplacecoord = "";
                    do
                    {
                        IsPlayercoordValid IsItValid = new IsPlayercoordValid();

                        Console.Write("{0}, pick a coordinate for your {1} : ", Player.Name2, stype);
                        shipplacecoord = Console.ReadLine();

                        coordIsValid = IsItValid.IsItGood(shipplacecoord);

                    } while (coordIsValid == false);
                    //end of testing

                    //Console.Write("{0}, pick a coordinate for your {1} : ", Player.Name2, stype);
                    //shipplacecoord = Console.ReadLine();

                    string xAsLetter = shipplacecoord.Substring(0, 1);
                    int shipX = LetterConverter.ConvertToNumber(xAsLetter); //Convert 1st char from player input to int.
                    //TODO Bug, ensure correct length.
                    int shipY = int.Parse(shipplacecoord.Substring(1)); //Assign 2nd coord.

                    Coordinate shipcoord = new Coordinate(shipX, shipY);

                    // and then, asking for ship direction
                    Console.Write("{0}, Enter a direction (up, down, left, right) for your {1} (length {2}) : ",
                        Player.Name2, stype, "ship length");
                    string shipPlacementDirection = Console.ReadLine();

                    IsDirectionValid IsDirInputValid = new IsDirectionValid();

                    int InputResponse = IsDirInputValid.WhatIsDirection(shipPlacementDirection);

                    if (InputResponse == 1)
                    {
                        PlaceShipRequest shipRequest = new PlaceShipRequest
                        {
                            Coordinate = shipcoord,
                            Direction = ShipDirection.Up,
                            ShipType = stype
                        };
                        var WhereIsShip = _player2Board.PlaceShip(shipRequest);
                        //Refactor for class
                        if (WhereIsShip == ShipPlacement.NotEnoughSpace)
                        {
                            Console.Clear();
                            Console.WriteLine("Not enough space to place ship there, Try again!");
                        }
                        else if (WhereIsShip == ShipPlacement.Overlap)
                        {
                            Console.Clear();
                            Console.WriteLine("You are overlapping another ship, try again!");
                        }
                        else if (WhereIsShip == ShipPlacement.Ok)
                        {
                            ShipCreator.CreateShip(stype);
                            placementIsBad = true;
                        }

                        //checks
                        //ShipCreator.CreateShip(stype);
                    }

                    if (InputResponse == 2)
                    {
                        PlaceShipRequest shipRequest = new PlaceShipRequest
                        {
                            Coordinate = shipcoord,
                            Direction = ShipDirection.Down,
                            ShipType = stype
                        };
                        //checks
                        var WhereIsShip = _player2Board.PlaceShip(shipRequest);
                        //Refactor for class
                        if (WhereIsShip == ShipPlacement.NotEnoughSpace)
                        {
                            Console.Clear();
                            Console.WriteLine("Not enough space to place ship there, Try again!");
                        }
                        else if (WhereIsShip == ShipPlacement.Overlap)
                        {
                            Console.Clear();
                            Console.WriteLine("You are overlapping another ship, try again!");
                        }
                        else if (WhereIsShip == ShipPlacement.Ok)
                        {
                            ShipCreator.CreateShip(stype);
                            placementIsBad = true;
                        }
                        //ShipCreator.CreateShip(stype);
                    }

                    if (InputResponse == 3)
                    {
                        PlaceShipRequest shipRequest = new PlaceShipRequest
                        {
                            Coordinate = shipcoord,
                            Direction = ShipDirection.Left,
                            ShipType = stype
                        };
                        //checks
                        var WhereIsShip = _player2Board.PlaceShip(shipRequest);
                        //Refactor for class
                        if (WhereIsShip == ShipPlacement.NotEnoughSpace)
                        {
                            Console.Clear();
                            Console.WriteLine("Not enough space to place ship there, Try again!");
                        }
                        else if (WhereIsShip == ShipPlacement.Overlap)
                        {
                            Console.Clear();
                            Console.WriteLine("You are overlapping another ship, try again!");
                        }
                        else if (WhereIsShip == ShipPlacement.Ok)
                        {
                            ShipCreator.CreateShip(stype);
                            placementIsBad = true;
                        }
                        //ShipCreator.CreateShip(stype);
                    }

                    if (InputResponse == 4)
                    {
                        PlaceShipRequest shipRequest = new PlaceShipRequest
                        {
                            Coordinate = shipcoord,
                            Direction = ShipDirection.Right,
                            ShipType = stype
                        };
                        //checks
                        var WhereIsShip = _player2Board.PlaceShip(shipRequest);
                        //Refactor for class
                        if (WhereIsShip == ShipPlacement.NotEnoughSpace)
                        {
                            Console.Clear();
                            Console.WriteLine("Not enough space to place ship there, Try again!");
                        }
                        else if (WhereIsShip == ShipPlacement.Overlap)
                        {
                            Console.Clear();
                            Console.WriteLine("You are overlapping another ship, try again!");
                        }
                        else if (WhereIsShip == ShipPlacement.Ok)
                        {
                            ShipCreator.CreateShip(stype);
                            placementIsBad = true;
                        }
                        //ShipCreator.CreateShip(stype);
                    }
                } while (placementIsBad == false);
                Console.Clear();
            }
        }

        //TODO Refactoring - Combine Player1 & 2 shooting & gameplay into a single code base. Class receives input on which player's turn it is.

        // actual shooting and gameplay
        public void GamePlay()
        {
            while (!_gameOver)
            {
                while (_isPlayerOnesTurn && _gameOver == false)
                {
                    BoardUI.DisplayGameBoard(_player2Board);
                    string p1shot = "";//checks

                    //Testing if valid input
                    bool coordIsValid = false;
                    do
                    {
                        IsPlayercoordValid IsItValid = new IsPlayercoordValid();

                        Console.Write("{0}, Take a shot! : ", Player.Name1);
                        p1shot = Console.ReadLine();

                        coordIsValid = IsItValid.IsItGood(p1shot);

                    } while (coordIsValid == false);
                    //end of testing

                    //Console.Write("{0}, Take a shot! : ", Player.Name1);
                    //p1shot = Console.ReadLine();

                    string p1shotx = p1shot.Substring(0, 1);
                    int p1shotxasint = LetterConverter.ConvertToNumber(p1shotx);
                    int p1shoty = int.Parse(p1shot.Substring(1));

                    Coordinate shotcoord = new Coordinate(p1shotxasint, p1shoty);

                    var p1FireShotResponse = _player2Board.FireShot(shotcoord);

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

                while (!_isPlayerOnesTurn && _gameOver == false)
                {
                    BoardUI.DisplayGameBoard(_player1Board);

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
