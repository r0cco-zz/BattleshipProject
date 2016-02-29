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
        private bool _freshGame = true;

        public void PlayGame()
        {
            _player1.GameBoard = new Board();
            _player2.GameBoard = new Board();

            if (_freshGame)
            {
                GetPlayerNames();
            }
            Console.Clear();
            Console.WriteLine("{0}, press enter to start placing ships.\n\n{1} LOOK AWAY!!", _player1.Name,
                _player2.Name);
            Console.ReadLine();
            Console.Clear();
            PlaceShips(_player1);
            Console.Clear();
            Console.WriteLine("{0}, press enter to start placing ships.\n\n{1} LOOK AWAY!!", _player2.Name,
                _player1.Name);
            Console.ReadLine();
            Console.Clear();
            PlaceShips(_player2);
            FireShots();
            PromptPlayAgain();
        }

        private void GetPlayerNames()
        {
            Console.Write("Player 1, What is your name? : ");
            _player1.Name = Console.ReadLine();
            if (_player1.Name == String.Empty)
            {
                _player1.Name = "Player1";
            }

            Console.Write("\n\nPlayer 2, what is your name? : ");
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
            foreach (ShipType stype in Enum.GetValues(typeof (ShipType)))
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
                    bool coordIsValid;
                    do
                    {
                        var isItValid = new IsPlayercoordValid();

                        Console.WriteLine();
                        Console.Write("{0}, pick a coordinate for your {1} : ", player.Name, stype);
                        shipplacecoord = Console.ReadLine();

                        coordIsValid = isItValid.IsItGood(shipplacecoord);

                    } while (!coordIsValid);

                    var xAsLetter = shipplacecoord.Substring(0, 1);
                    var shipX = LetterConverter.ConvertToNumber(xAsLetter); //Convert 1st char from player input to int.
                    var shipY = int.Parse(shipplacecoord.Substring(1)); //Assign 2nd coord.

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
                    TakeTurnsFiring(_player1, _player2.GameBoard);
                }
                while (!_isPlayerOnesTurn && !_gameOver)
                {
                    TakeTurnsFiring(_player2, _player1.GameBoard);
                }
            }
            //TODO: give the victor a splash screen
            Console.Clear();
        }

        public void TakeTurnsFiring(Player player, Board boardToBeFiredUpon)
        {
            BoardUI.DisplayGameBoardForShotFiring(boardToBeFiredUpon);
            string playerShot;
            bool coordIsValid;

            do
            {
                IsPlayercoordValid IsItValid = new IsPlayercoordValid();

                Console.Write("{0}, Take a shot! : ", player.Name);
                playerShot = Console.ReadLine();

                coordIsValid = IsItValid.IsItGood(playerShot);

            } while (!coordIsValid);

            var playerShotx = playerShot.Substring(0, 1);
            var playerShotXAsInt = LetterConverter.ConvertToNumber(playerShotx);
            var playerShoty = int.Parse(playerShot.Substring(1));

            var shotcoord = new Coordinate(playerShotXAsInt, playerShoty);

            var playerFireShotResponse = _player2.GameBoard.FireShot(shotcoord);

            switch (playerFireShotResponse.ShotStatus)
            {
                case ShotStatus.Hit:
                    Console.WriteLine("You hit something! (Press enter)");
                    Console.ReadLine();
                    Console.Clear();
                    if (_isPlayerOnesTurn)
                    {
                        _isPlayerOnesTurn = false;
                    }
                    else if (!_isPlayerOnesTurn)
                    {
                        _isPlayerOnesTurn = true;
                    }
                    break;

                case ShotStatus.Duplicate:
                    Console.WriteLine("You already shot at that spot! (Press enter)");
                    Console.ReadLine();
                    Console.Clear();
                    break;

                case ShotStatus.HitAndSunk:
                    //TODO: player name instead of "opponent"?
                    Console.WriteLine("Hit! You sunk your opponent's " + playerFireShotResponse.ShipImpacted +
                                      " (Press enter)");
                    Console.ReadLine();
                    Console.Clear();
                    if (_isPlayerOnesTurn)
                    {
                        _isPlayerOnesTurn = false;
                    }
                    else if (!_isPlayerOnesTurn)
                    {
                        _isPlayerOnesTurn = true;
                    }
                    break;

                case ShotStatus.Invalid:
                    Console.WriteLine("Invalid coordinate, try again! (Press enter)");
                    Console.ReadLine();
                    Console.Clear();
                    break;

                case ShotStatus.Miss:
                    Console.WriteLine("Your projectile splashes into the ocean, you missed! (Press enter)");
                    Console.ReadLine();
                    Console.Clear();
                    if (_isPlayerOnesTurn)
                    {
                        _isPlayerOnesTurn = false;
                    }
                    else if (!_isPlayerOnesTurn)
                    {
                        _isPlayerOnesTurn = true;
                    }
                    break;

                case ShotStatus.Victory:
                    Console.WriteLine("You have sunk all your opponent's ships, you win! (Press enter)");
                    Console.ReadLine();
                    Console.Clear();
                    _gameOver = true;
                    break;
            }
        }


        private void PromptPlayAgain()
        {
            //Yay, someone won. Play again?
            Console.Write("Play again? Type y or yes to play again. Type anything else to Quit: ");
            var playAgain = Console.ReadLine();

            if (!String.IsNullOrEmpty(playAgain) && (playAgain.ToLower() == "y" || playAgain.ToLower() == "yes"))
            {
                Console.Clear();
                //TODO: ask them if they want to enter new names
                _freshGame = false;
                _gameOver = false;
                PlayGame();
            }
            else
            {
                //TODO: make a splash screen for game exit
                Console.WriteLine("Thanks for playing! Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }
}
