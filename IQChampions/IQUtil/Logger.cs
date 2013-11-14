using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQUtil
{
    public enum Errorlevel
    {
        INFO, DEBUG, WARN, ERROR
    }
    public static class Logger
    {
        public static void log(Errorlevel level, string message)
        {
            Console.WriteLine(DateTime.Now.ToString().PadRight(20) + "[" + level + "] " + message);
        }
    }
}
