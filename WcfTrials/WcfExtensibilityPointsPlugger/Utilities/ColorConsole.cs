using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WcfExtensibilityPointsPlugger
{
    static class ColorConsole
    {
        static object syncRoot = new object();

        public static void WriteLine(ConsoleColor color, string text, params object[] args)
        {
            if (args != null && args.Length > 0)
            {
                text = string.Format(CultureInfo.InvariantCulture, text, args);
            }

            lock (syncRoot)
            {
                Console.ForegroundColor = color;
                Console.WriteLine("[{0}] {1}", DateTime.Now.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture), text);
                Console.ResetColor();
            }

            Thread.Sleep(50);
        }

        public static void WriteLine(string text, params object[] args)
        {
            Console.WriteLine(text, args);
        }

        public static void WriteLine(object obj)
        {
            Console.WriteLine(obj);
        }
    }
}
