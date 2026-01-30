using ComputerStoreApplication.Account;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Pages
{
    public interface IPage 
    {
        public int? AdminId { get; set; }
        public int? CurrentCustomerId { get; set; }
        //Abrstract void, alla sidor ska rendera sig själv som metod, fast det kan se olika ut
        void Load(ApplicationManager appLol);

        void RenderPage();

        void SetPageCommands();

        void DrawAccountProfile();
        //Hantera input som variabel på alla sidor, ge de olika funktioner per sida
        IPage? HandleUserInput(ConsoleKeyInfo UserInput, ApplicationManager applicationLogic);
    }
}
