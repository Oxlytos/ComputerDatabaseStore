using ComputerStoreApplication.Models.ComputerComponents;
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
            return computerPartCategories.ToList();
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

        internal static void LoadSiteGraphics()
        {
            Console.Clear();
            List<string> pageHeader = new List<string> { "Oscar's Computer Store ", "Most Things PC related :^)" };
            var windowTop1 = new Helpers.WindowStuff.WideWindow("•ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ• •ᴗ• ", 2, 0, pageHeader);
            windowTop1.Draw();
        }
    }
}
