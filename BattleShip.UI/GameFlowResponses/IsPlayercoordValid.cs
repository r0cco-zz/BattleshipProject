using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI.GameFlowResponses
{
    public class IsPlayercoordValid
    {
        private char[] verifyAgainstChar = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };
        //private int[] verifyAgainstInt = {1, 2, 3, 4, 5, 6, 7, 8, 9}; Using Int32.TryParse instead.

        public bool IsItGood(string a)
        {
            if (a.Length == 0 || a.Length < 2 || a.Length > 3) //If length isn't reasonable, toss out immediately.
            {
                return false;
            }
            else
            {
                int check = 0;
                int good = 0;
                for (int i = 0; i < verifyAgainstChar.Length -1; i++)
                {
                    if (a[0] == verifyAgainstChar[i]) //Ensure first char is letter a-j
                    {
                        check+=1;
                    }
                    if (Int32.TryParse(a.Substring(1,1), out good)) 
                    {  
                        check+= 1; //Ensure 2nd string element is an int. out good is irrelevent.
                    }
                    if (check == 2) //Two checks means we're good.
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
