using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Graphics
{
    public class PageBanners
    {
        internal static void DrawShopBanner()
        {
            List<string> pageHeader = new List<string> { "  Oscar's Computer Store ", "  Most Things PC related :^)" };

            string headerText = "•ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ•";

            int longestLineLength = Math.Max(headerText.Length,pageHeader.Max(s => s.Length));

            int pageHeaderTotalLength = Math.Max(headerText.Length, longestLineLength);
            int middleOfTheScreen = (Console.WindowWidth - pageHeaderTotalLength) / 2;

            Console.BackgroundColor = ConsoleColor.Magenta;
            var windowTop1 = new Helpers.WindowStuff.WideWindow(headerText, middleOfTheScreen, 0, pageHeader);
            windowTop1.Draw();
            Console.BackgroundColor = ConsoleColor.Black;
        }
        internal static void DrawAdmingBanner()
        {
            List<string> pageHeader = new List<string> { "  Admin Page ", "  For important stuff" };

            string headerText = "+-+-+-+-+-+-+-+-+-+-+-+-+-+-+";
            int longestLineLength = Math.Max(
            headerText.Length,
            pageHeader.Max(s => s.Length)
        );
            int pageHeaderTotalLength = Math.Max(headerText.Length, longestLineLength);
            int middleOfTheScreen = (Console.WindowWidth - pageHeaderTotalLength) / 2;

            var windowTop1 = new Helpers.WindowStuff.WideWindow(headerText, middleOfTheScreen, 0, pageHeader);
            Console.BackgroundColor = ConsoleColor.Blue;
            windowTop1.Draw();
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
