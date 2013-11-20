using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQUtil
{
    public static class StringExtensions
    {
        public static string MultiInsert(this string str, string insertChar, int step)
        {
            StringBuilder sb = new StringBuilder();            
            for (int i = 0; i < str.Length; i++)
            {
                sb.Append(str[i]);
                if ((i + 1) % step == 0) sb.Append(insertChar);
            }
            return sb.ToString();
        }
    }
}
