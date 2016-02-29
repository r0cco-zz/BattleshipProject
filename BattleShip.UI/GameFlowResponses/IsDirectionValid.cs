using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI.GameFlowResponses
{
    public class IsDirectionValid
    {
        //public int IsGood { get; set; }

        public int WhatIsDirection(string isValid)
        {
            switch (isValid.ToLower()) //Is ship placement valid?
            {
                case "up":
                case "u":
                    return 1;
                case "down":
                case "d":
                    return 2;
                case "left":
                case "l":
                    return 3;
                case "right":
                case "r":
                    return 4;
                default:
                    return 5; //If garbage, return 5.
            }
        }
    }
}
