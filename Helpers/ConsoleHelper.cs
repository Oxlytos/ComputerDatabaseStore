using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Helpers
{
    public class ConsoleHelper
    {
        public static void WriteCentered(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;

            //Middle of the screen
            int windowWidth = Console.WindowWidth;
            //Width
            int textLength = text.Length;
            //Left is startfts with a offset from center, making it correctly centerd
            int left = (windowWidth - textLength) / 2;
            //Don't wrap around
            if (left < 0) left = 0;


            Console.SetCursorPosition(left, Console.CursorTop);
            Console.WriteLine(text);
        }
        //For spacing
        public static void WriteCenteredEmptyLine()
        {
            Console.WriteLine();
        }
        public static void ResetConsole()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            // Optional: shrink buffer to window size
            try
            {
                Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            }
            catch { }
        }
    }
}
