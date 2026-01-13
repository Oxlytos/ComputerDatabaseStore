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
            int width = 0;
            string completedWord = String.Join("", Words);
           
            int windowWith = Math.Max(completedWord.Length, Header.Length);
            // Kolla om Header är längre än det längsta ordet i listan
            if (width < Header.Length + 4)
            {
                width = Header.Length + 4;
            }
            ;

            // Rita Header
            int whereToPlaceHeader = (windowWith -Header.Length) / 2;
           
            Console.SetCursorPosition(Left, Top);
            if (Header != "")
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write('┌' );
                
             
                Console.Write(new String('─', windowWith) + '┐');
                Console.ForegroundColor = ConsoleColor.White;

                int headX = Left + 1 + whereToPlaceHeader;


                Console.SetCursorPosition(headX, Top);
                Console.Write(Header);
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            else
            {
                Console.Write('┌' + new String('─', windowWith) + '┐');
            }

            Console.SetCursorPosition(Left, Top + 1);
            Console.Write('│');
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(completedWord.PadRight(windowWith));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write('│');
            // Rita raderna i sträng-Listan

            // Rita undre delen av fönstret
            Console.SetCursorPosition(Left, Top +2);
            Console.Write('└' + new String('─', windowWith) + '┘');


            // Kolla vilket som är den nedersta posotion, i alla fönster, som ritats ut
            if (Lowest.LowestPosition < Top + Words.Count + 1)
            {
                Lowest.LowestPosition = Top + Words.Count + 1;
            }

            Console.SetCursorPosition(0, Lowest.LowestPosition);
            Console.ForegroundColor = ConsoleColor.White;
        }


    }

    public static class Lowest
    {
        public static int LowestPosition { get; set; }
    }
}
