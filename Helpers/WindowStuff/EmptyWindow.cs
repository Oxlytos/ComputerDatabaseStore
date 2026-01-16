using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Helpers.WindowStuff
{
    internal class EmptyWindow
    {
        public string Header { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public ConsoleColor BgColor { get; set; }

        public void Draw()
        {
            Console.BackgroundColor = BgColor;
            Console.ForegroundColor = ConsoleColor.White;
            // Kolla om Header är längre än det längsta ordet i listan
            if (Width < Header.Length + 4)
            {
                Width = Header.Length + 4;
            };

            // Rita Header
            Console.SetCursorPosition(Left, Top);
            if (Header != "")
            {
                Console.Write('┌' );
                Console.Write(' ');
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(Header);
                Console.Write(' ');
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" " + new string('─', Width - Header.Length-2) + '┐');
            }
            else
            {
                Console.Write('┌' + new string('─', Width + 2) + '┐');
            }

            // Rita raderna i sträng-Listan
            for (int j = 0; j < Height; j++)
            {
                Console.SetCursorPosition(Left, Top + 1 + j);
                Console.Write('│');
                Console.Write(' ');
                Console.Write(new string(' ', Width));
                Console.Write('│');
            }

            // Rita undre delen av fönstret
            Console.SetCursorPosition(Left, Top + Width+ 1);
            Console.Write('└' + new string('─', Width + 2) + '┘');

            int bottom = Top + Height + 2;
            // Kolla vilket som är den nedersta posotion, i alla fönster, som ritats ut
            if (Lowest.LowestPosition < Top + Width + 2)
            {
                Lowest.LowestPosition = bottom;
            }

            Console.SetCursorPosition(0, Lowest.LowestPosition);
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static class Lowest
        {
            public static int LowestPosition { get; set; }
        }
    }
}
