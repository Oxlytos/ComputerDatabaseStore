using ComputerStoreApplication.CreateStuff;
using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComputerComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Pages
{
    public class AdminPage : IPage
    {
        public void RenderPage()
        {
            Console.WriteLine("Admin page");
            Console.WriteLine("Press (N) to start registering a new product (CPU, GPU, PSU, RAM, Motherboard");
        }
        public IPage? HandleUserInput(ConsoleKeyInfo UserInput, ApplicationManager applicationLogic)
        {
            //Specifc admin functions
            if(UserInput.Key == ConsoleKey.N)
            {
                GetInputForNewProduct(applicationLogic);
            }

            //General change page
            if (UserInput.Key == ConsoleKey.C)
            {
                return new CustomerPage();
            }
            if (UserInput.Key == ConsoleKey.H)
            {
                return new HomePage();
            }
            if (UserInput.Key == ConsoleKey.B)
            {
                return new BrowseProducts();
            }
            return null;
        }

        public void GetInputForNewProduct(ApplicationManager logic)
        {
            Console.WriteLine("What type of product?");
            var vendors = logic.GetVendors();
            var manufacturers = logic.GetManufacturers();
            var sockets = logic.GetCPUSockets();
            var archs = logic.GetCPUArchitectures();
            List<Type> types = GeneralHelpers.ReturnComputerPartTypes();
            for (int i = 0; i < types.Count; i++)
            {
                Console.WriteLine($"{i + 1} {types[i].Name}");
            }

            string usIn = Console.ReadLine();
            if(Int32.TryParse(usIn, out int choice))
            {
                choice -= 1;
                switch (choice) 
                {
                    case 0: //CPU
                        Console.WriteLine("Fetching function....");
                        CPU chudCPU = CreateComponents.RegisterNewCPU(vendors, manufacturers, sockets, archs);
                        logic.SaveCPU(chudCPU);
                        Console.ReadLine();
                        break;
                    case 1:
                        Console.WriteLine("Bongos");
                        break;
                    case 2:
                        Console.WriteLine("Bongos");
                        break;
                    case 3:
                        Console.WriteLine("Bongos");
                        break;
                    case 4:
                        Console.WriteLine("Bongos");
                        break;
                    case 5:
                        Console.WriteLine("Bongos");
                        break;
                    case 6:
                        Console.WriteLine("Bongos");
                        break;

                }
            }
        }

       
    }
}
