using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Graphics
{
    internal class PageAccount
    {
        internal static void DrawAccountGraphic(List<string> textElements, string headerText, ConsoleColor bannerColor)
        {
            //Width
            int longestLineLength = Math.Max(headerText.Length, textElements.Max(s => s.Length));

            //Banner width more or less
            int graphicWidth = longestLineLength;

            int totalWidth = graphicWidth + 4;
            ///Padding for some extra space
            int padding = 2;
            //Right of the screen - our length
            int rightPageAligned = Console.WindowWidth - graphicWidth - padding;
            rightPageAligned = Math.Clamp(rightPageAligned, 0, Console.BufferWidth - totalWidth);
            Console.BackgroundColor = bannerColor;
            var windowTop1 = new Helpers.WindowStuff.WideWindow(headerText, rightPageAligned, 0, textElements);
            windowTop1.Draw();
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
