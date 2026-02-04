using ComputerStoreApplication.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Graphics
{
    internal class PageAccount
    {
        internal static List<string> ReturnCustomerProfileAccountString(ApplicationManager app)
        {
            if (!app.IsLoggedInAsCustomer)
            {
                return null;
            }
            int? totalAmountInBasket = 0;
            List<string> strings = new List<string>();
            if (app.IsLoggedInAsCustomer)
            {
                //totalAmountInBasket = currentCustomer.ProductsInBasket?.Sum(p => p.Quantity) ?? 0;
               
                //strings.AddRange(currentCustomer.FirstName, currentCustomer.SurName, currentCustomer.Email, " Objects in basket: " + totalAmountInBasket);
            }
            else
            {
                strings.Add(" Not logged in as a customer");
                totalAmountInBasket = 0;
            }
            return strings;
        }
        internal static List<string> ReturnAdminProfileAccountString(ApplicationManager app)
        {
            if (!app.IsLoggedInAsAdmin)
            {
                return null;
            }
            List<string> strings = new List<string>();
                //strings.AddRange(admin.UserName, admin.FirstName, admin.SurName, admin.Email);
            return strings;
        }
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
