using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI.GameFlowResponses
{
    public class IsPlayercoordValid
    {
        private readonly char[] _verifyAgainstChar = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j'};

        public bool IsItGood(string a)
        {
            if (a.Length == 0 || a.Length < 2 || a.Length > 3)
            {
                return false;
            }

            int check = 0;
            int good;
            for (int i = 0; i < _verifyAgainstChar.Length - 1; i++)
            {
                if (a[0] == _verifyAgainstChar[i]) //Ensure first char is letter a-j
                {
                    check = 1;
                }
            }
            if (a.Length == 3 && int.TryParse(a.Substring(1, 2), out good) && good == 10)
            {
                check++;
            }
            else if (a.Length == 2 && int.TryParse(a.Substring(1, 1), out good))
            {
                check++; //Ensure 2nd string element is an int. out good is irrelevent.
            }
            return (check == 2);
        }
    }
}
