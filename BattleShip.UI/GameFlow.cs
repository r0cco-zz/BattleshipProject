using System;
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

            //Setting game colors.
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();

            SplashScreen.DisplayStart();
            Console.Clear();

            if (_freshGame)
            {
                GetPlayerNames();
            }
            SplashScreen.DisplayLookAwayScreen(_player1, _player2);
            PlaceShips(_player1);
            SplashScreen.DisplayLookAwayScreen(_player2, _player1);
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
            while (true)
            {
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
                        string shipplacecoord;
                        bool coordIsValid;
                        do
                        {
                            BoardUI.DisplayGameBoardForShipPlacement(player.GameBoard);

                            var isItValid = new IsPlayercoordValid();

                            Console.WriteLine();
                            Console.Write("{0}, pick a coordinate for your {1} : ", player.Name, stype);
                            shipplacecoord = Console.ReadLine();

                            coordIsValid = isItValid.IsItGood(shipplacecoord);

                            if (!coordIsValid)
                            {
                                Console.WriteLine("That is not a valid coordinate. Press enter to choose again");
                                Console.ReadLine();
                                Console.Clear();
                            }
                        } while (!coordIsValid);

                        var xAsLetter = shipplacecoord.Substring(0, 1);
                        var shipX = LetterConverter.ConvertToNumber(xAsLetter);
                        var shipY = int.Parse(shipplacecoord.Substring(1));

                        var shipcoord = new Coordinate(shipX, shipY);

                        Console.Write("{0}, Enter a direction (up, down, left, right) for your {1} (length {2}) : ", player.Name, stype, shipLength);
                        var shipPlacementDirection = Console.ReadLine();

                        var isDirInputValid = new IsDirectionValid();

                        var inputResponse = isDirInputValid.WhatIsDirection(shipPlacementDirection);

                        switch (inputResponse)
                        {
                            case 1:
                            {
                                PlaceShipRequest shipRequest = new PlaceShipRequest
                                {
                                    Coordinate = shipcoord, Direction = ShipDirection.Up, ShipType = stype
                                };
                                placementIsGood = CheckShipPlacement(player, shipRequest, stype);
                            }
                                break;
                            case 2:
                            {
                                PlaceShipRequest shipRequest = new PlaceShipRequest
                                {
                                    Coordinate = shipcoord, Direction = ShipDirection.Down, ShipType = stype
                                };
                                placementIsGood = CheckShipPlacement(player, shipRequest, stype);
                            }
                                break;
                            case 3:
                            {
                                PlaceShipRequest shipRequest = new PlaceShipRequest
                                {
                                    Coordinate = shipcoord, Direction = ShipDirection.Left, ShipType = stype
                                };

                                placementIsGood = CheckShipPlacement(player, shipRequest, stype);
                            }
                                break;
                            case 4:
                            {
                                PlaceShipRequest shipRequest = new PlaceShipRequest
                                {
                                    Coordinate = shipcoord, Direction = ShipDirection.Right, ShipType = stype
                                };

                                placementIsGood = CheckShipPlacement(player, shipRequest, stype);
                            }
                                break;
                            default:
                                Console.WriteLine("That is not a valid direction. Please place your {0} again", stype);
                                Console.WriteLine("Press enter");
                                Console.ReadLine();
                                Console.Clear();
                                placementIsGood = false;
                                break;
                        }
                        if (placementIsGood)
                        {
                            Console.Clear();
                            BoardUI.DisplayGameBoardForShipPlacement(player.GameBoard);
                            Console.WriteLine();
                            Console.Write("Is this your desired position for your {0}? y or n : ", stype);
                            var changePosition = Console.ReadLine();
                            if (!String.IsNullOrEmpty(changePosition) && (changePosition.ToLower() == "n" || changePosition.ToLower() == "no"))
                            {
                                placementIsGood = false;
                                player.GameBoard.RemoveLastShipFromBoard(stype);
                                Console.Clear();
                            }
                        }
                    } while (!placementIsGood);
                    Console.Clear();
                }
                BoardUI.DisplayGameBoardForShipPlacement(player.GameBoard);
                Console.WriteLine();
                Console.WriteLine("Would you like to re-position your ships? (This will clear the board)");
                Console.Write("y or n? : ");
                var reposition = Console.ReadLine();
                if (!String.IsNullOrEmpty(reposition) && (reposition.ToLower() == "y" || reposition.ToLower() == "yes"))
                {
                    player.GameBoard = new Board();
                    Console.Clear();
                    continue;
                }
                Console.Clear();
                break;
            }
        }

        private static bool CheckShipPlacement(Player player, PlaceShipRequest request, ShipType stype)
        {
            var whereIsShip = player.GameBoard.PlaceShip(request);
            switch (whereIsShip)
            {
                case ShipPlacement.NotEnoughSpace:
                    Console.WriteLine("Not enough space to place ship there, try again!");
                    Console.WriteLine("Press enter");
                    Console.ReadLine();
                    Console.Clear();
                    return false;
                case ShipPlacement.Overlap:
                    Console.WriteLine("You are overlapping another ship, try again!");
                    Console.WriteLine("Press enter");
                    Console.ReadLine();
                    Console.Clear();
                    return false;
                case ShipPlacement.Ok:
                    ShipCreator.CreateShip(stype);
                    return true;
                default:
                    return false;
            }
        }

        public void FireShots()
        {
            while (!_gameOver)
            {
                while (_isPlayerOnesTurn && !_gameOver)
                {
                    TakeTurnsFiring(_player1, _player2.GameBoard);
                    //TODO put a splash screen in between shots
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
            string playerShot;
            bool coordIsValid;

            do
            {
                BoardUI.DisplayGameBoardForShotFiring(boardToBeFiredUpon);

                IsPlayercoordValid IsItValid = new IsPlayercoordValid();

                Console.WriteLine();
                Console.Write("{0}, Take a shot! : ", player.Name);
                playerShot = Console.ReadLine();

                coordIsValid = IsItValid.IsItGood(playerShot);

                if (!coordIsValid)
                {
                    Console.WriteLine("That is not a valid coordinate. Press enter to choose again");
                    Console.ReadLine();
                    Console.Clear();
                }

            } while (!coordIsValid);

            var playerShotx = playerShot.Substring(0, 1);
            var playerShotXAsInt = LetterConverter.ConvertToNumber(playerShotx);
            var playerShoty = int.Parse(playerShot.Substring(1));

            var shotcoord = new Coordinate(playerShotXAsInt, playerShoty);

            var playerFireShotResponse = boardToBeFiredUpon.FireShot(shotcoord);

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
