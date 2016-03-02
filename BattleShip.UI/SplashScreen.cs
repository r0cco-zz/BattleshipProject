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
            Console.WriteLine(@"
                                     |__
                                     |\/
                                     ---
                                     / | [
                              !      | |||
                            _/|     _/|-++'
                        +  +--|    |--|--|_ |-
                     { /|__|  |/\__|  |--- |||__/
                    +---------------___[}-_===_.'____                 /\
                ____`-' ||___-{]_| _[}-  |     |_[___\==--            \/   _
 __..._____--==/___]_|__|_____________________________[___\==--____,------' .7
|                                                                           /
 \_________________________________________________________________________|

                          Welcome to Battleship!!
                          Press enter to continue");
            Console.ReadLine();
            Console.Clear();
        }

        public static void DisplayExitScreen()
        {
            Console.WriteLine(@"                              ?
   ~~~~~~~~~~~~~~~~~~~~~~~~~~~|^'~~~~~~~~~~~~~~~~~~~~~~~~~o~~~~~~~~~~~
            o                 |                          __o
             o                |                       | X__ >
       ___  o                 |          
     (X___ >                __| __    
                            |     \                    
                            |      \                
    ________________________|_______\________________
   <                                                \____________   _
    \                                                            \ (_)
     \    O       O       O                                       >=)
      \__________________________________________________________ / (_)

                             ___
                            / o \
                      __    \   /    _
                        \__ / | \__ / \
                      \___/  /|\ \___/\
                        ___ / | \___
                              |     \
                             / 
            
                    Thanks for playing!!
                    Press enter to exit
");
            Console.ReadLine();
            Console.Clear();
        }

        public static void DisplayVictoryScreen(Player winningPlayer, Player losingPlayer)
        {
            Console.WriteLine(@".  * . .    *. .  *  . . *  . * . .  * .  .    *
 *     *    /\  .  .   *  .  *  . *  .  .  * .
  . *  . . //\\/\ .  * . . .   .  .  * . * .  *
* . .  *  ///\//\\  .  *  ,+----+ .    *  .  .
     /\  ////\\/\\\ .  __/  ,+---,    /\      *
 ~  //\\/////\\\\\\\ \/\/  /     + ~ //\\/\  ~~
 ~ ///\//////\\\\\\\\/ /  /   8\ /   ~~~~~~  ~  ~
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.ReadLine();
            Console.ReadLine();
            Console.WriteLine("Congratulations {0}, you trounced {1}!!", winningPlayer.Name, losingPlayer.Name);
            Console.ReadLine();
            Console.Clear();
        }

        public static void DisplayLookAwayScreen(Player playerBeingAddressed, Player playerBeingToldToLookAway)
        {
            Console.WriteLine("{0}, press enter to start placing ships.\n\n{1} LOOK AWAY!!", playerBeingAddressed.Name,
               playerBeingToldToLookAway.Name);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(@"888888888888888888888888888888888888888888888888888888888888
888888888888888888888888888888888888888888888888888888888888
8888888888888888888888888P''  ''9888888888888888888888888888
8888888888888888P'88888P          988888'9888888888888888888
8888888888888888  '9888            888P'  888888888888888888
888888888888888888bo '9  d8o  o8b  P' od88888888888888888888
888888888888888888888bob 98'  '8P dod88888888888888888888888
888888888888888888888888    db    88888888888888888888888888
88888888888888888888888888      8888888888888888888888888888
88888888888888888888888P'9bo  odP'98888888888888888888888888
88888888888888888888P' od88888888bo '98888888888888888888888
888888888888888888   d88888888888888b   88888888888888888888
8888888888888888888oo8888888888888888oo888888888888888888888
888888888888888888888888888888888888888888888888888888888888");
            Console.WriteLine();
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            Console.Clear();
        }

        public static void DisplayBeginFiringScreen()
        {
            Console.WriteLine("Prepare to begin firing!!!");
            Console.WriteLine();
            Console.WriteLine(@"                                     # #  ( )
                                  ___#_#___|__
                              _  |____________|  _
                       _=====| | |            | | |==== _
                 =====| |.---------------------------. | |====
   <--------------------'   .  .  .  .  .  .  .  .   '--------------/
     \                                                             /
      \_______________________________________________WWS_________/
  wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww
wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww
   wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww ");
            Console.WriteLine();
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
