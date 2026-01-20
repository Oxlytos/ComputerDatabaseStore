using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Vendors_Producers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            //Få alla objket av classen ComputerPart som finns i Assembly, fast som inte är abstrakt
            var compPartCat = typeof(ComputerPart).Assembly.GetTypes().Where(p => computerParts.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);
            //Användbar
            return compPartCat.ToList();
        }

        //Vill vi skiippa och printa tråkiga fälts med ID och annat för användaren
        internal static string[] SkippablePropertiesInPrints()
        {
            string[] skippableFields = ["ManufacturerId", "VendorId", "Products", "SocketId", "CPUArchitectureId", "MemoryTypeId", "CPUSocketId", "MemoryTypeId", "EnergyClassId", "Id"];

            return skippableFields;
        }

        //För speciella fält som Manufacturer, Vendor osv där vi vill printa först
        internal static string[] SpecialFields()
        {
            string[] specials = ["Manufacturer", "Vendor", "Socket", "CPUSocket","MemoryType"];
            return specials;
        }
        internal static object HandleSettingValueOfProperty(Type type)
        {
            Console.WriteLine(GetTypeNameFromNullablePropertyField(type));
            //Int är en typ
            //Int? är en lite av en annan typ, försöker vi få name där blir det skumt, vi behöver få den underliggande typen
            Type underlyingType = Nullable.GetUnderlyingType(type) ?? type;

            if (underlyingType == typeof(string))
            {
                Console.WriteLine("Property is of type string, please input a string");
                return Console.ReadLine();
            }
            if (underlyingType == typeof(Int32))
            {
                Console.WriteLine("Property is of type int, please input an int");
                return StringToInt(Console.ReadLine());
            }
            if (underlyingType == typeof(decimal))
            {
                Console.WriteLine("Propertu is of type decimal, please input a decimal");
                return StringToDecimal(Console.ReadLine());
            }
            Console.ReadLine();
            return null;
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
        internal static int CenterOfTheScreen()
        {
            return Console.WindowHeight / 2;
        }
        internal static int ReturnMiddleOfTheScreenXAxisWithOffsetForSomeStringOrLength(int length)
        {
            int middleOfTheScreen = (Console.WindowWidth - length) / 2;
            return middleOfTheScreen;
        }
        internal static List<string> ReturnNumberedList(List<string> text)
        {
            List<string> newText = new List<string>();
            for (int i = 0; i < text.Count; i++)
            {
                newText.Add($"{i + 1}. {text[i]} ");
            }
            return newText;
        }
        internal static string ReturnedCenteredText(string text, int width)
        {
            int leftPadding = Math.Max(0, (width - text.Length) / 2);
            return (new string(' ', leftPadding) + text).PadRight(width);
        }

        internal static MemoryType ChooseMemoryType(List<MemoryType> mems)
        {
            Console.WriteLine("What memorytype does the graphics card have? Choose by inputting an int");
            foreach (MemoryType memoryType in mems)
            {
                Console.WriteLine($"ID: {memoryType.Id} Name/Type: {memoryType.MemoryTypeName}");
            }
            if (Int32.TryParse(Console.ReadLine(), out int choice))
            {
                var hit = mems.FirstOrDefault(v => v.Id == choice);
                return hit;
            }
            else
            {
                Console.WriteLine("Some kind of errror");
                return null;
            }
        }
        internal static Vendor ChooseVendor(List<Vendor> vendors)
        {
            Console.WriteLine("Which vendor? Choose by inputting an int");
            foreach (Vendor v in vendors)
            {
                Console.WriteLine($"ID: {v.Id} Name: {v.Name}");
            }
            if (Int32.TryParse(Console.ReadLine(), out int choice))
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
        internal static object TryAndUpdateValueOnObject(PropertyInfo thisProperty)
        {
            //Type of property type, just här kändes det användbart
            //https://stackoverflow.com/questions/3723934/using-propertyinfo-to-find-out-the-property-type
            Console.WriteLine($"Editing {thisProperty.Name}");
            Type type = thisProperty.PropertyType;
            string typeNameForClaritiesSake = GetTypeNameFromNullablePropertyField(type);
            var setValue =HandleSettingValueOfProperty(type);
            if (setValue != null)
            {
                Console.WriteLine($"Trying to set value {setValue.ToString()} on property '{thisProperty.Name}'....");
                //Typen kan vara t.ex. int eller int? Vi behöver se till att vi vet att det är i viktigaste läge en int oavsett
                //Om den inte är nullbar ?? ta bara vanliga typen
                Type baseTypeOfProperty = Nullable.GetUnderlyingType(type) ?? type;

                //Här kollar vi om int passar på t.ex. int fält
                if (baseTypeOfProperty.IsAssignableFrom(setValue.GetType()))
                {
                    return setValue;
                }
                //Passar det inte är det ett error
                else
                {
                    Console.WriteLine($"Error! Field required {baseTypeOfProperty}, got {setValue.GetType()}");
                }
            }
            //Typ som inte är hanterad
            else
            {
                Console.WriteLine("Incompatible setting value on property, or other error");
                Console.ReadLine();
            }
            //retunera null i värsta fall
            return null;
        }


        internal static string FindSuitablePropertyByString(List<PropertyInfo> props, string name)
        {
            var foundProperty = props.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
            if (foundProperty != null)
            {
                return foundProperty.ToString();
            }
            else
            {
                return null;
            }
        }
        internal static string GetTypeNameFromNullablePropertyField(Type type)
        {
            if (Nullable.GetUnderlyingType(type) != null)
            {
                return Nullable.GetUnderlyingType(type).Name;
            }
            else
            {
                return type.Name;
            }
        }
        internal static int StringToInt(string userInput)
        {
            if (Int32.TryParse(userInput, out int intVal))
            {
                return intVal;
            }
            //Borde lägga till någon throw error här
            return 0;
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

    }
}
