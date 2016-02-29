using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI.GameFlowResponses
{
    public class IsPlayercoordValid
    {
        private char[] verifyAgainstChar = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j'};

        public bool IsItGood(string a)
        {
            if (a.Length == 0 || a.Length < 2 || a.Length > 3)
            {
                return false;
            }

            int check = 0;
            for (int i = 0; i < verifyAgainstChar.Length - 1; i++)
            {
                int good;
                if (a[0] == verifyAgainstChar[i]) //Ensure first char is letter a-j
                {
                    check++;
                }
                if (int.TryParse(a.Substring(1, 2), out good) && good == 10)
                {
                    check++;
                }
                else if (int.TryParse(a.Substring(1, 1), out good))
                {
                    check++; //Ensure 2nd string element is an int. out good is irrelevent.
                }
                if (check == 2) //Two checks means we're good.
                {
                    return true;
                }
            }

            return false;
        }
    }
}
