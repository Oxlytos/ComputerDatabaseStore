using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Graphics
{
    public class PageBanners
    {

        internal static void DrawBanner(List<string> textElements, string headerText, ConsoleColor bannerColor)
        {
            int longestLineLength = Math.Max(headerText.Length, textElements.Max(s => s.Length));

            int pageHeaderTotalLength = Math.Max(headerText.Length, longestLineLength);
            int middleOfTheScreen = (Console.WindowWidth - pageHeaderTotalLength) / 2;

            Console.BackgroundColor = bannerColor;
            var windowTop1 = new Helpers.WindowStuff.WideWindow(headerText, middleOfTheScreen, 0, textElements);
            windowTop1.Draw();
            Console.BackgroundColor = ConsoleColor.Black;
        }
        //Nedan alla banners för alla sidor
        /// <summary>
        /// Varje sida har sin egna banner
        /// Roligt med lite grafik, klargör vart man är någonstans och bör kunna göra
        /// </summary>
        internal static void DrawShopBanner()
        {
            List<string> pageHeader = new List<string> { "  Oscar's Computer Store ", "  Most Things PC related :^)" };
            string headerText = "•ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ•";
            ConsoleColor ccolor = ConsoleColor.DarkCyan;
            DrawBanner(pageHeader, headerText, ccolor);
        }
        internal static void DrawAdmingBanner()
        {
            List<string> pageHeader = new List<string> { "  Admin Page ", "  For important stuff" };
            string headerText = "+-+-+-+-+-+-+-+-+-+-+-+-+-+-+";
            ConsoleColor ccolor = ConsoleColor.DarkBlue;
            DrawBanner(pageHeader, headerText, ccolor);
        }

        internal static void DrawBrowsePageBanner()
        {
            List<string> pageHeader = new List<string> { " Catalogue Page ", "  Browse to your hearts content! " };
            string headerText = "-!-!-!-!-!-!-!-!-!-!-!-!-!-";
            ConsoleColor ccolor = ConsoleColor.DarkYellow;
            DrawBanner(pageHeader, headerText, ccolor);
        }
        internal static void DrawSearchedResults()
        {
            List<string> pageHeader = new List<string> { " Browse qureied products ", "  You might just find what you're looking for! " };
            string headerText = "-!-!-!-!-!-!-!-!-!-!-!-!-!-";
            ConsoleColor ccolor = ConsoleColor.DarkYellow;
            DrawBanner(pageHeader, headerText, ccolor);
        }
        internal static void DrawCustomerPage()
        {
            List<string> pageHeader = new List<string> { " Customer Page ", "  Check orders, fill out information, and other stuff " };
            string headerText = ":^) :^) :^) :^) :^) :^) :^) :^) :^) :^) :^) :^)";
            ConsoleColor ccolor = ConsoleColor.DarkBlue;
            DrawBanner(pageHeader, headerText, ccolor);
        }
    }
}
