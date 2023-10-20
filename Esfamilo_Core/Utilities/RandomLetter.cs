using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Core.Utilities
{
    public static class RandomLetter
    {
        public static string GetRandomLetter(string usedLetter)
        {
            var alphabet = "ا ب پ ت ج چ خ د ر ز س ش ع ف ق ک گ ل م ن و ه ی";
            var arrayalphabet = alphabet.Split(" ");
            var random = new Random();
            int rndnum = -1;
            for(var i =0; i < arrayalphabet.Length; i++)
            {
                rndnum = random.Next(0, 24);
                if (!usedLetter.Contains(arrayalphabet[rndnum]))
                {
                    break;
                }
            }
            if(rndnum != -1)
            {
                var getword = arrayalphabet[rndnum];
                return getword;
            }
            else
            {
                return "";
            }
        }
    }
}
