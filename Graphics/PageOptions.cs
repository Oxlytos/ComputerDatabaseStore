using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Graphics
{
    public class PageOptions
    {
        public static void DrawPageOptions(List<string> options, ConsoleColor inputCOlor)
        {
            int longestLineLength = options.Count;
            int left = 5;
            Console.BackgroundColor = inputCOlor;
            var windowTop1 = new Helpers.WindowStuff.WideWindow("", 0, 0, options);
            windowTop1.Draw();
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
