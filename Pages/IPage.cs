using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Pages
{
    public interface IPage
    {
        //Abrstract void, alla sidor ska rendera sig själv som metod, fast det kan se olika ut
        void RenderPage();

        //Hantera input som variabel på alla sidor, ge de olika funktioner per sida
        IPage? HandleUserInput(ConsoleKeyInfo UserInput);
    }
}
