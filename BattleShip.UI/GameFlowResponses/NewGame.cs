using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;

namespace BattleShip.UI.GameFlowResponses
{
    public class NewGame
    {
        public void StartNewGame()
        {
            GameFlow gf = new GameFlow();

            gf.Player1ShipPlacement();

            gf.Player2ShipPlacement();

            gf.GamePlay();
        }
    }
}
