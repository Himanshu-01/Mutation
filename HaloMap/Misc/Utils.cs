using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaloMap
{
    public class Utils
    {
        public static string StrReverse(string Value)
        {
            char[] Chars = Value.ToCharArray();
            Array.Reverse(Chars);
            return new string(Chars);
        }
    }
}
