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
            var gf = new GameFlow();
            gf.PlayGame();
        }
    }
}
