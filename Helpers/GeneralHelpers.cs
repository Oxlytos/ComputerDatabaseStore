using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Vendors_Producers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Helpers
{
    public class GeneralHelpers
    {
        internal static List<Type> ReturnComputerPartTypes()
        {
        https://stackoverflow.com/questions/26733/getting-all-types-that-implement-an-interface
            Type computerParts = typeof(ComputerPart);

            var computerPartCategories = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => computerParts.IsAssignableFrom(p));

            List<Type> types = computerPartCategories.ToList();
            //Ta bort abstrakta klassen från listan, den ska inte synas för någon 
            types.Remove(typeof(ComputerPart));
            return types;
        }
        internal static List<string> AllCategoriesFoundAsStrings(List<Type> types, bool numbered)
        {
            List<string> categories = new List<string>();
            if (numbered)
            {
                for (int i = 0; i < types.Count; i++)
                {
                    string formattedString = $"{i + 1}. {types[i].Name} ";
                    categories.Add(formattedString);
                }
            }
            else
            {
                for (int i = 0; i < types.Count; i++)
                {
                    categories.Add(types[i].Name);
                }
            }

                return categories;
        }
        internal static List<string> ReturnNumberedList(List<string> text)
        {
            List<string> newText = new List<string>();
            for (int i = 0;i < text.Count; i++)
            {
                newText.Add($"{i+1}. {text[i]} ");
            }
            return newText;
        }
        internal static Vendor ChooseVendor(List<Vendor> vendors)
        {
            Console.WriteLine("Which vendor? Choose by inputting an int");
            foreach (Vendor v in vendors) 
            {
                Console.WriteLine($"ID: {v.Id} Name: {v.Name}");
            }
            if(Int32.TryParse(Console.ReadLine(), out int choice))
            {
                var hit = vendors.FirstOrDefault(v => v.Id == choice);
                return hit;
            }
            else
            {
                Console.WriteLine("Some kind of errror");
                return null;
            }
        }
        internal static Manufacturer ChooseManufacturer(List<Manufacturer> manufacturers)
        {
            Console.WriteLine("Which manufacturer? Choose by inputting an int");
            foreach (Manufacturer m in manufacturers)
            {
                Console.WriteLine($"ID: {m.Id} Name: {m.Name}");
            }
            if (Int32.TryParse(Console.ReadLine(), out int choice))
            {
                var hit = manufacturers.FirstOrDefault(v => v.Id == choice);
                return hit;
            }
            else
            {
                Console.WriteLine("Some kind of errror");
                return null;
            }
        }
        internal static CPUSocket ChooseCPUSocket(List<CPUSocket> cPUSockets)
        {
            Console.WriteLine("Which socket? Choose by inputting an int");
            foreach (CPUSocket m in cPUSockets)
            {
                Console.WriteLine($"ID: {m.Id} Name: {m.CPUSocketName}");
            }
            if (Int32.TryParse(Console.ReadLine(), out int choice))
            {
                var hit = cPUSockets.FirstOrDefault(v => v.Id == choice);
                return hit;
            }
            else
            {
                Console.WriteLine("Some kind of errror");
                return null;
            }
            return null;
        }
        internal static CPUArchitecture ChooseCPUArch(List<CPUArchitecture> archs)
        {
            Console.WriteLine("Which CPU arch? Choose by inputting an int");
            foreach (CPUArchitecture m in archs)
            {
                Console.WriteLine($"ID: {m.Id} Name: {m.CPUArchitectureName}");
            }
            if (Int32.TryParse(Console.ReadLine(), out int choice))
            {
                var hit = archs.FirstOrDefault(v => v.Id == choice);
                return hit;
            }
            else
            {
                Console.WriteLine("Some kind of errror");
                return null;
            }
        }

        internal static int StringToInt(string userInput)
        {
            if(Int32.TryParse(userInput, out int intVal))
            {
                return intVal;
            }
            else
            {
                Console.WriteLine("Error");
                return 0;
            }
         
        }
        internal static decimal StringToDecimal(string userInput)
        {
            if (decimal.TryParse(userInput, out decimal decimalValue))
            {
                return decimalValue;
            }
            else
            {
                Console.WriteLine("Error");
                return 0;
            }
        }
        internal static bool YesOrNoReturnBoolean(string userINput)
        {
            if (userINput.StartsWith("y") || userINput.StartsWith("Y"))
            {
                return true;
            }
            else if (userINput.StartsWith("n") || userINput.StartsWith("N"))
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        internal static void LoadSiteGraphics()
        {
            Console.Clear();
            List<string> pageHeader = new List<string> { "Oscar's Computer Store ", "Most Things PC related :^)" };
            var windowTop1 = new Helpers.WindowStuff.WideWindow("•ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ• ", 2, 0, pageHeader);
            windowTop1.Draw();
        }
        
    }
}
