using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Vendors_Producers;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Crud_Related
{
    public class CrudHandler
    {
        public Dictionary<ConsoleKey, CRUD> keyValuePairs;
        private static readonly Dictionary<ConsoleKey, CRUD> Commandos = new()
        {
            {  ConsoleKey.C, CRUD.Create },
            { ConsoleKey.D, CRUD.Delete },
            { ConsoleKey.U, CRUD.Update },
            {ConsoleKey.R, CRUD.Read },
        };

        public enum CRUD
        {
            Create = ConsoleKey.C, //Skapa product
            Read = ConsoleKey.R, //Läs all info på product => Bara printa upp allt?
            Update = ConsoleKey.U, //Uppdatera som i att vi ändrar värden
            Delete = ConsoleKey.D, //Ta bort, duh
        }
        //Vill vi skiippa och printa tråkiga fälts med ID och annat för användaren
        public static void ComponentInput(ApplicationManager logic)
        {
            ComputerPart componentToCreate = AskWhatProductType(logic);

            var relevantObjects = logic.GetComputerComponentsByType(componentToCreate);

            Console.WriteLine($"You chose: {componentToCreate.GetType().ToString()}");
            Console.WriteLine("Current components in this category;");
            if (relevantObjects != null)
            {
                foreach (var part in relevantObjects)
                {
                    Console.WriteLine($"-\tId: {part.Id} Name: {part.Name}");
                }
            }
            else
            {
                Console.WriteLine("No component of this type yet");
            }
            Console.WriteLine("What CRUD action?");
            if (relevantObjects != null)
            {
                foreach (var key in Commandos)
                {

                    Console.WriteLine($"[{key.Key}] to {key.Value} a product");
                }
            }
            else
            {
                Console.WriteLine($"Only action available is to Create, by pressing [C]");
            }

            var userInputC = Console.ReadKey(true);
            if (!Commandos.TryGetValue(userInputC.Key, out var userCrudValue))
            {
                Console.WriteLine("Some error here bud");
                return;
            }
            if (userCrudValue != CRUD.Create)
            {
                var selectedComponent = GetObjectByTypeAndId(relevantObjects.ToList());
                switch (userCrudValue)
                {
                    case CRUD.Read:
                        if (selectedComponent != null)
                        {
                            selectedComponent.Read(logic);
                        }
                        break;
                    case CRUD.Update:
                        if (selectedComponent != null)
                        {
                            selectedComponent.Update(logic);
                        }
                        break;
                    case CRUD.Delete:
                        if (selectedComponent != null)
                        {
                            selectedComponent.Delete(logic);
                        }
                        break;
                }
            }
            else
            {
                componentToCreate.Create(logic);
            }
        }
        public static void CategoryInput(ApplicationManager logic)
        {
            ComponentSpecification specCreate = AskWhatComponentSpecification(logic);

            var relevantObjects = logic.GetComponentSpecifications(specCreate);

            Console.WriteLine($"You chose: {relevantObjects.GetType().ToString()}");
            Console.WriteLine("Current components in this category;");
            if (relevantObjects != null)
            {
                foreach (var spec in relevantObjects)
                {
                    Console.WriteLine($"-\tId: {spec.Id} {spec.Name}");
                }
            }
            Console.WriteLine("What CRUD action?");
            if (relevantObjects != null)
            {
                foreach (var key in Commandos)
                {
                    Console.WriteLine($"[{key.Key}] to {key.Value} a product");
                }
            }
            else
            {
                Console.WriteLine($"Only action available is to Create, by pressing [C]");
            }

            var userInputC = Console.ReadKey(true);
            if (!Commandos.TryGetValue(userInputC.Key, out var userCrudValue))
            {
                Console.WriteLine("Some error here bud");
                return;
            }
            if (userCrudValue != CRUD.Create)
            {
                var selectedComponent = GetSpecByTypeAndId(relevantObjects.ToList());
                switch (userCrudValue)
                {
                    case CRUD.Read:
                        if (selectedComponent != null)
                        {
                            selectedComponent.Read(logic);
                        }
                        break;
                    case CRUD.Update:
                        if (selectedComponent != null)
                        {
                            selectedComponent.Update(logic);
                        }
                        break;
                    case CRUD.Delete:
                        if (selectedComponent != null)
                        {
                            selectedComponent.Delete(logic);
                        }
                        break;
                }
            }
            else
            {
                specCreate.Create(logic);
            }
        }
        static ComputerPart GetObjectByTypeAndId(List<ComputerPart> compObjs)
        {
            Console.WriteLine("What object (by ID) do you want to interact with?");
            int idChoice = GeneralHelpers.StringToInt(Console.ReadLine());
            if (compObjs.Any(x => x.Id == idChoice))
            {
                return compObjs.FirstOrDefault(c => c.Id == idChoice);
            }
            else
            {
                return null;
            }
        }
        static ComponentSpecification GetSpecByTypeAndId(List<ComponentSpecification> compObjs)
        {
            Console.WriteLine("What object (by ID) do you want to interact with?");
            int idChoice = GeneralHelpers.StringToInt(Console.ReadLine());
            if (compObjs.Any(x => x.Id == idChoice))
            {
                return compObjs.FirstOrDefault(c => c.Id == idChoice);
            }
            else
            {
                return null;
            }
        }
        static ComputerPart AskWhatProductType(ApplicationManager logic)
        {
            Console.WriteLine("What type of product?");
            List<Type> types = GeneralHelpers.ReturnComputerPartTypes();
            for (int i = 0; i < types.Count; i++)
            {
                Console.WriteLine($"{i + 1} {types[i].Name}");
            }

            string usIn = Console.ReadLine();
            if (Int32.TryParse(usIn, out int choice))
            {
                choice -= 1;
                switch (choice)
                {
                    case 0:
                        return new CPU();
                    case 1:
                        return new GPU();
                    case 2:
                        return new Motherboard();
                    case 3:
                        return new PSU();
                    case 4:
                        return new RAM();
                }
            }
            return null;
        }
        static ComponentSpecification AskWhatComponentSpecification(ApplicationManager logic)
        {
            Console.WriteLine("What type of category?");
            List<Type> types = GeneralHelpers.ReturnComponentSpecificationTypes();
            Console.WriteLine(types.Count);
            for (int i = 0; i < types.Count; i++)
            {
                Console.WriteLine($"{i + 1} {types[i].Name}");
            }

            string usIn = Console.ReadLine();
            if (Int32.TryParse(usIn, out int choice))
            {
                choice -= 1;
                switch (choice)
                {
                    case 0:
                        return new CPUArchitecture();
                    case 1:
                        return new CPUSocket();
                    case 2:
                        return new EnergyClass();
                    case 3:
                        return new MemoryType();
                    case 4:
                        return new RamProfileFeatures();
                }
            }
            return null;
        }
        static void LoadBackgroundForm()
        {
            int startPosX = GeneralHelpers.ReturnMiddleOfTheScreenXAxisWithOffsetForSomeStringOrLength(15);
            Helpers.WindowStuff.EmptyWindow form = new Helpers.WindowStuff.EmptyWindow
            {
                Header = " Questionnaire",
                Left = startPosX,
                Top = 15,
                Height = 25,
                BgColor = ConsoleColor.Red,
            };
            form.Draw();
        }
    }
}
