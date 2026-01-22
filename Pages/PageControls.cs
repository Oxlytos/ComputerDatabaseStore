using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Pages
{
    public class PageControls
    {
        //Alla sido val
        //Vi kan kolla om input var "HOme" istället för 'H' på olika sidor
       public enum PageOption
        {
            Home,
            CustomerLogin,
            CustomerPage,
            AdminLogin,
            AdminCreateComponent,
            AdminCreateCategory,
            AdminEdit,
            AdminPage,
            Browse,
            Exit
        }
        //Ett page command, en enum och en beskrivning som används vid printing av valen
        public record PageCommand(PageOption PageCommandOptionInteraction, string CommandDescription);
        
        //Alla fina kontroller
        //Ger giltiga kommandon och beskrivningar

        public static readonly PageCommand HomeCommand =
           new(PageOption.Home, "Go to home page");

        public static readonly PageCommand BrowseCommand =
            new(PageOption.Browse, "Browse products");

        public static readonly PageCommand ExitCommand =
            new(PageOption.Exit, "Exit application");

        public static readonly PageCommand Admin =
            new(PageOption.AdminPage, "Login to Admin");

        public static readonly PageCommand AdminCreateProduct =
           new(PageOption.AdminCreateComponent, "To register a new product");

        public static readonly PageCommand AdminCreateCategory =
           new(PageOption.AdminCreateCategory, "To register a component specification");

        public static readonly PageCommand AdminEdit = new(PageOption.AdminEdit, "To edit a existing product");

        public static readonly PageCommand CustomerHomePage = new(PageOption.CustomerPage, "To view account");
    }
}
