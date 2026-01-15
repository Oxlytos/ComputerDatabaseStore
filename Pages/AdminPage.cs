using ComputerStoreApplication.CreateStuff;
using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComponentSpecifications;
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
        static List<string> pageOptions = new List<string> { "[H] to go to Home page", "","[C] for customer page", "","[B] to browse products" };
        public void RenderPage()
        {
            Console.Clear();
            Graphics.PageOptions.DrawPageOptions(pageOptions, ConsoleColor.DarkCyan);
            Graphics.PageBanners.DrawAdmingBanner();
            Console.SetCursorPosition(0, 10);
            Console.WriteLine("Admin page");
            Console.WriteLine("Press (N) to start registering a new product (CPU, GPU, PSU, RAM, Motherboard)");
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
                        var sockets = logic.GetCPUSockets();
                        var archs = logic.GetCPUArchitectures();
                        CPU chudCPU = CreateComponents.RegisterNewCPU(vendors, manufacturers, sockets, archs);
                        logic.SaveCPU(chudCPU);
                        Console.ReadLine();
                        break;
                    case 1:
                        //GPU

                        Console.WriteLine("Fetching function....");
                        var memoryTypes = logic.GetMemoryTypes();
                        GPU newGPU = CreateComponents.RegisterNewGPU(vendors, manufacturers, memoryTypes);
                        logic.SaveGPU(newGPU);
                        break;
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

        public void PageOptions(List<string> stuff)
        {
            
        }

      
    }
}
