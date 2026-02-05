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
            List<string> strings = new List<string>();
            if (app.IsLoggedInAsAdmin)
            {
                return ReturnAdminProfileAccountString(app);
            }
            if (!app.IsLoggedInAsCustomer)
            {
                strings.Add("Not logged in");
                return strings;
            }
            using var context = new ComputerDBContext();
            var customer = context.Customers.FirstOrDefault(x=>x.Id==app.CustomerId);
            if(customer == null)
            {
                strings.Add("Not logged in");
                return strings;
            }
            int? totalAmountInBasket = app.BasketItemCount;
         
            if (app.IsLoggedInAsCustomer)
            {
               
                strings.AddRange(customer.FirstName, customer.SurName, customer.Email, " Objects in basket: " + totalAmountInBasket);
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
            List<string> strings = new List<string>();
            if (app.IsLoggedInAsCustomer)
            {
                return ReturnCustomerProfileAccountString(app);
            }
            if (!app.IsLoggedInAsAdmin)
            {
                strings.Add("Not logged in");
                return strings;
            }
            var context = new ComputerDBContext();
            var admin = context.Admins.FirstOrDefault(x => x.Id == app.AdminId);
            if(admin == null)
            {
                strings.Add("Not logged in");
                return strings;
            }
         
            strings.AddRange(admin.UserName, admin.FirstName, admin.SurName, admin.Email);
            return strings;
        }
        internal static void DrawAccountGraphic(List<string> textElements, string headerText, ConsoleColor bannerColor)
        {
            //Width
            if ( textElements == null)
            {
                textElements = new List<string> { "Not logged in" };
            }
            //Adds a little space from the left
            textElements = textElements.Select(s => " "+ s).ToList();
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
            var window = new Helpers.WindowStuff.WideWindow(headerText, rightPageAligned, 0, textElements);
            window.Draw();
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
