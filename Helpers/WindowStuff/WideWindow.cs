using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Helpers.WindowStuff
{
    internal class WideWindow
    {

        public string Header { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public List<string> Words { get; set; }

        public int? InputedInt { get; set; }

        public WideWindow(string header, int left, int top, List<string> textRows)
        {
            Header = header;
            Left = left;
            Top = top;
            Words = textRows;
        }

        public void Draw()
        {
            //Width with header and completetd text length in mind
            int contentWidth = Math.Max(
                Header.Length,
                Words.Max(w => w.Length)
            );

            //Padding
            int windowWidth = contentWidth + 2; 

            //Top part
            Console.SetCursorPosition(Left, Top);
            Console.Write('┌' + new string('─', windowWidth) + '┐');

            // Header
            if (!string.IsNullOrEmpty(Header))
            {
                int headerX = Left + 1 + (windowWidth - Header.Length) / 2;
                Console.SetCursorPosition(headerX, Top);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(Header);
            }
    
            //Content
            for (int i = 0; i < Words.Count; i++)
            {
                Console.SetCursorPosition(Left, Top + 1 + i);
                Console.Write('│');
                string line = Words[i];

                string centerdLine = GeneralHelpers.ReturnedCenteredText(line, contentWidth);
                centerdLine = centerdLine.PadRight(windowWidth); //The padding

                Console.Write(centerdLine);
                Console.Write('│');
            }

            //Botten
            Console.SetCursorPosition(Left, Top + Words.Count + 1);
            Console.Write('└' + new string('─', windowWidth) + '┘');

        }
    }
        public static class Lowest
    {
        public static int LowestPosition { get; set; }
    }
}
