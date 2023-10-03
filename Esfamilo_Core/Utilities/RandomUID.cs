using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfamilo_Core.Utilities
{
    public static class RandomUID
    {
        public static string GetRandomUID()
        {
            Random rnd = new Random();
            return rnd.Next(10000000,99999999).ToString();
        }
    }
}
