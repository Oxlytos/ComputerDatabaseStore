using ComputerStoreApplication.Account;
using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Customer;
using ComputerStoreApplication.Models.Store;
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
        //Choose what cateogry to affect, call the right method based on that and query correct data later
        private static readonly Dictionary<int, Category> ActionableCategories = new()
        {
            { 1, Category.ComputerParts },
            { 2, Category.Categories },
            {3, Category.Customers },
            { 4, Category.DeliveryServices },
            { 5, Category.PaymentMethods },
        };
        public enum Category
        {
            ComputerParts = 1,
            Categories,
            Customers,
            DeliveryServices,
            PaymentMethods
        }
        private static readonly Dictionary<Category, Action<ApplicationManager>> CategoryHandlers = new()
        {
            {Category.ComputerParts, CRUDComputerParts},
            {Category.Categories, CRUDCategories},
            {Category.PaymentMethods, CRUDPaymentMethods},
            {Category.Customers, CRUDCustomers},
            {Category.DeliveryServices, CRUDDeliveryServices},
        };
        public enum CRUD
        {
            Create = ConsoleKey.C, //Skapa product
            Read = ConsoleKey.R, //Läs all info på product => Bara printa upp allt?
            Update = ConsoleKey.U, //Uppdatera som i att vi ändrar värden
            Delete = ConsoleKey.D, //Ta bort, duh
        }
        public static void ChooseCategory(ApplicationManager logic)
        {
            Console.WriteLine("What category? Input the correspond number for that category");

            //Print categorys
            foreach (var category in ActionableCategories) 
            {
                Console.WriteLine($"{category.Key}. {category.Value}");
            }
            int choice = GeneralHelpers.StringToInt();
            //Make sure its valid
            if (!ActionableCategories.TryGetValue(choice, out var categoryChoice))
            {
                Console.WriteLine("Some error, returning");
                return;
            }
            //If its valid, and we have a method for it
            if(!CategoryHandlers.TryGetValue(categoryChoice, out var categoryCRUD))
            {
                Console.WriteLine("No action available for this category, returning");
                return;
            }
            //We go to that categorys method and do its crud
            categoryCRUD(logic);
            }
        private static void CRUDCustomers(ApplicationManager logic)
        {
            var customers = logic.GetCustomers();
            if (customers == null)
            {
                Console.WriteLine("No customer, empty collection");
                Console.WriteLine("Add something to this collection?");
                bool yes = GeneralHelpers.YesOrNoReturnBoolean();
                if (yes)
                {
                    //Create
                   
                    return;
                }
                else
                {
                    return;
                }
            }
            else
            {
                Console.WriteLine("These are the registerd customer of the store");
                foreach (var product in customers)
                {
                    Console.WriteLine($"Id: {product.Id} Name: {product.FirstName} {product.SurName} Number: {product.PhoneNumber} Orders: {product.Orders.Count}");
                }

                PrintCrudCommands();
                var userInputC = Console.ReadKey(true);
                if (!Commandos.TryGetValue(userInputC.Key, out var userCrudValue))
                {
                    Console.WriteLine("Some error here bud");
                    return;
                }
                var allManufactuers = logic.GetManufacturers();
                var categories = logic.GetCategories();
                if (userCrudValue != CRUD.Create)
                {
                    Console.WriteLine("Which object from the list? Input its Id");
                    int valid = GeneralHelpers.StringToInt();
                    var thing = customers.FirstOrDefault(x => x.Id == valid);
                    if (thing == null)
                    {
                        return;
                    }
                    //Singular object brand
                    //Singular part
                    switch (userCrudValue)
                    {
                        case CRUD.Read:
                            thing.Read();
                            break;
                        case CRUD.Update:
                            thing.EditCustomerAccount();
                            break;
                        case CRUD.Delete:
                            logic.ComputerPartShopDB.Remove(thing);
                            break;
                    }
                }
                else
                {
                    CustomerAccount newCustomer;
                    return;
                }
            }
        }

        private static void CRUDPaymentMethods(ApplicationManager logic)
        {
            var paymentMethods = logic.ComputerPartShopDB.PaymentMethods;
            if (paymentMethods == null)
            {
                Console.WriteLine("No objects, empty collection");
                Console.WriteLine("Add something to this collection?");
                bool yes = GeneralHelpers.YesOrNoReturnBoolean();
                if (yes)
                {
                    //Create
                    return;
                }
                else
                {
                    return;
                }
            }
            else
            {
                Console.WriteLine("These are the store producs");
                foreach (var product in paymentMethods)
                {
                    Console.WriteLine($"{product.Id} {product.Name}");
                }

                PrintCrudCommands();
                var userInputC = Console.ReadKey(true);
                if (!Commandos.TryGetValue(userInputC.Key, out var userCrudValue))
                {
                    Console.WriteLine("Some error here bud");
                    return;
                }
                var allManufactuers = logic.GetManufacturers();
                var categories = logic.GetCategories();
                if (userCrudValue != CRUD.Create)
                {
                    Console.WriteLine("Which object from the list? Input its Id");
                    int valid = GeneralHelpers.StringToInt();
                    var thing = paymentMethods.FirstOrDefault(x => x.Id == valid);
                    if (thing == null)
                    {
                        return;
                    }
                    switch (userCrudValue)
                    {
                        case CRUD.Read:
                            thing.Read();
                            break;
                        case CRUD.Update:
                            thing.Update();
                            break;
                        case CRUD.Delete:
                            logic.ComputerPartShopDB.Remove(thing);
                            break;
                    }
                }
                else
                {
                    PaymentMethod newStoreProduct = new PaymentMethod();
                    var parts = logic.GetComputerParts();
                    newStoreProduct.Create();
                    logic.ComputerPartShopDB.Add(newStoreProduct);
                    return;
                }
            }
        }

        private static void CRUDComputerParts(ApplicationManager logic)
        {
            var storeObjects = logic.GetComputerParts();
            if (storeObjects == null)
            {
                Console.WriteLine("No objects, empty collection");
                Console.WriteLine("Add something to this collection?");
                bool yes = GeneralHelpers.YesOrNoReturnBoolean();
                if (yes)
                {
                    //Create
                    return;
                }
                else
                {
                    return;
                }
            }
            else
            {
                Console.WriteLine("These are the store producs");
                foreach (var product in storeObjects)
                {
                    Console.WriteLine($"{product.Id} {product.Name} {product.Price}");
                }

                PrintCrudCommands();
                var userInputC = Console.ReadKey(true);
                if (!Commandos.TryGetValue(userInputC.Key, out var userCrudValue))
                {
                    Console.WriteLine("Some error here bud");
                    return;
                }
                var allManufactuers = logic.GetManufacturers();
                var categories = logic.GetCategories();
                if (userCrudValue != CRUD.Create)
                {
                    Console.WriteLine("Which object from the list? Input its Id");
                    int valid = GeneralHelpers.StringToInt();
                    var thing = storeObjects.FirstOrDefault(x => x.Id == valid);
                    if (thing == null)
                    {
                        return;
                    }
                    //Singular object brand
                    var objectsBrand = allManufactuers.FirstOrDefault(x => x.Id == thing.BrandId);
                    //Singular part
                    var thisCategory = categories.FirstOrDefault(x => x.Id == thing.CategoryId);
                    switch (userCrudValue)
                    {
                        case CRUD.Read:
                            thing.Read(objectsBrand, thisCategory);
                            break;
                        case CRUD.Update:
                            thing.UpdateForm(allManufactuers, categories);
                            break;
                        case CRUD.Delete:
                            logic.ComputerPartShopDB.Remove(thing);
                            break;
                    }
                }
                else
                {
                    ComputerPart newStoreProduct = new ComputerPart();
                    newStoreProduct.Create(allManufactuers, categories);
                    logic.ComputerPartShopDB.Add(newStoreProduct);
                    return;
                }
            }
        }

        private static void CRUDDeliveryServices(ApplicationManager logic)
        {
            var deliveryServices = logic.ComputerPartShopDB.DeliveryProviders;
            if (deliveryServices == null)
            {
                Console.WriteLine("No deliver services registered, empty collection");
                Console.WriteLine("Add something to this collection?");
                bool yes = GeneralHelpers.YesOrNoReturnBoolean();
                if (yes)
                {
                    //Create
                    return;
                }
                else
                {
                    return;
                }
            }
            else
            {
                Console.WriteLine("These are the current delivery services registered");
                foreach (var postal in deliveryServices)
                {
                    Console.WriteLine($"Id: {postal.Id} {postal.Name} ");
                }

                PrintCrudCommands();
                var userInputC = Console.ReadKey(true);
                if (!Commandos.TryGetValue(userInputC.Key, out var userCrudValue))
                {
                    Console.WriteLine("Some error here bud");
                    return;
                }
                var allManufactuers = logic.GetManufacturers();
                if (userCrudValue != CRUD.Create)
                {
                    Console.WriteLine("Which object from the list? Input its Id");
                    int valid = GeneralHelpers.StringToInt();
                    var thing = deliveryServices.FirstOrDefault(x => x.Id == valid);
                    if (thing == null)
                    {
                        return;
                    }
                    switch (userCrudValue)
                    {
                        case CRUD.Read:
                            thing.Read();
                            break;
                        case CRUD.Update:
                            thing.Update();
                            break;
                        case CRUD.Delete:
                            logic.ComputerPartShopDB.Remove(thing);
                            break;
                    }
                }
                else
                {
                    DeliveryProvider newDelivery = new DeliveryProvider();
                    newDelivery.Create();
                    logic.ComputerPartShopDB.Add(newDelivery);
                    return;
                }
            }
        }

        private static void CRUDCategories(ApplicationManager logic)
        {
            var storeCategories = logic.GetCategories();
            if (storeCategories == null)
            {
                Console.WriteLine("No categories, empty collection");
                Console.WriteLine("Add something to this collection?");
                bool yes = GeneralHelpers.YesOrNoReturnBoolean();
                if (yes)
                {
                    //Create
                    return;
                }
                else
                {
                    return;
                }
            }
            else
            {
                Console.WriteLine("These are the store producs");
                foreach (var cat in storeCategories)
                {
                    Console.WriteLine($"{cat.Id} {cat.Name}");
                }

                PrintCrudCommands();
                var userInputC = Console.ReadKey(true);
                if (!Commandos.TryGetValue(userInputC.Key, out var userCrudValue))
                {
                    Console.WriteLine("Some error here bud");
                    return;
                }
                var allManufactuers = logic.GetManufacturers();
                if (userCrudValue != CRUD.Create)
                {
                    Console.WriteLine("Which object from the list? Input its Id");
                    int valid = GeneralHelpers.StringToInt();
                    var thing = storeCategories.FirstOrDefault(x => x.Id == valid);
                    if (thing == null)
                    {
                        return;
                    }
                    switch (userCrudValue)
                    {
                        case CRUD.Read:
                            thing.Read();
                            break;
                        case CRUD.Update:
                            thing.Update();
                            break;
                        case CRUD.Delete:
                            logic.ComputerPartShopDB.Remove(thing);
                            break;
                    }
                }
                else
                {
                    ComponentCategory newCat = new ComponentCategory();
                    //Console.WriteLine("Name of category?");
                    //newCat.Name = GeneralHelpers.SetName(50);
                    newCat.Create();
                    logic.ComputerPartShopDB.Add(newCat);
                    logic.ComputerPartShopDB.SaveChanges();
                    return;
                }
            }
        }

        static void PrintCrudCommands()
        {
            Console.WriteLine("What action?");
            foreach (var key in Commandos)
            {
                Console.WriteLine($"[{key.Key}] to {key.Value}");
            }
        }
        public static void SetProductAsSelected(ApplicationManager logic)
        {
            Console.Clear();
            Console.WriteLine("Mark a store product as selected?");
            bool yes = GeneralHelpers.YesOrNoReturnBoolean();
            if (yes)
            {
                var storeObjects = logic.GetStoreProducts();
                foreach(var storeObject in storeObjects)
                {
                    Console.WriteLine($"Id: {storeObject.Id} Name: {storeObject.Name} Selected? {storeObject.SelectedProduct}");
                }
                Console.WriteLine("Input the corresponding Id of the product you want to mark as 'Selected'");
                int choice = GeneralHelpers.StringToInt();
                var thisObject = storeObjects.FirstOrDefault(x=>x.Id==choice);
                if (thisObject != null) 
                {
                    Console.WriteLine("Mark as selected or not? Choose with y and n");
                    bool selected = GeneralHelpers.YesOrNoReturnBoolean();
                    thisObject.SelectedProduct = selected;
                }
            }
            else
            {

            }
        }
        public static void AddNewBrand(ApplicationManager logic)
        {
            Console.Clear();
            Console.WriteLine("Current brands");
            var brands = logic.GetManufacturers();
            foreach (var brand in brands)
            {
                Console.WriteLine($"{brand.Name}");
            }
            Console.WriteLine("Add new brand?");
            bool yes = GeneralHelpers.YesOrNoReturnBoolean();
            if (yes) 
            {
                Console.WriteLine("Name?");
                string name = GeneralHelpers.SetName(40);
                Brand newBrand = new Brand
                {
                    Name = name,
                };
                var currentBrands = logic.GetManufacturers();
                var valid = currentBrands.FirstOrDefault(x => x.Name == newBrand.Name);
                //Dosen't already exist
                if (valid == null)
                {
                    Console.WriteLine("Adding new brand!");
                    logic.ComputerPartShopDB.Add(newBrand);
                    logic.ComputerPartShopDB.SaveChanges();
                    Console.ReadLine();
                }

            }
        }
        //Vill vi skiippa och printa tråkiga fälts med ID och annat för användaren
        //public static void ComponentInput(ApplicationManager logic)
        //{
        //    ComputerPart componentToCreate = AskWhatProductType(logic);

        //    var relevantObjects = logic.GetComputerComponentsByType(componentToCreate);

        //    Console.WriteLine($"You chose: {componentToCreate.GetType().Name}");
        //    Console.WriteLine("Current components in this category;");
        //    if (relevantObjects != null)
        //    {
        //        foreach (var part in relevantObjects)
        //        {
        //            Console.WriteLine($"-\tId: {part.Id} Name: {part.Name}");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("No component of this type yet");
        //    }
        //    Console.WriteLine("What CRUD action?");
        //    if (relevantObjects != null || relevantObjects.Count()>0)
        //    {
        //        foreach (var key in Commandos)
        //        {

        //            Console.WriteLine($"[{key.Key}] to {key.Value} a product");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Only action available is to Create, by pressing [C]");
        //    }

        //    var userInputC = Console.ReadKey(true);
        //    if (!Commandos.TryGetValue(userInputC.Key, out var userCrudValue))
        //    {
        //        Console.WriteLine("Some error here bud");
        //        return;
        //    }
        //    if (userCrudValue != CRUD.Create)
        //    {
        //        var selectedComponent = GetObjectByTypeAndId(relevantObjects.ToList());
        //        switch (userCrudValue)
        //        {
        //            case CRUD.Read:
        //                if (selectedComponent != null)
        //                {
        //                    selectedComponent.Read(logic);
        //                }
        //                break;
        //            case CRUD.Update:
        //                if (selectedComponent != null)
        //                {
        //                    selectedComponent.Update(logic);
        //                }
        //                break;
        //            case CRUD.Delete:
        //                if (selectedComponent != null)
        //                {
        //                    selectedComponent.Delete(logic);
        //                }
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        componentToCreate.Create(logic);
        //    }
        //}
       
        //public static void StoreProductInput(ApplicationManager logic)
        //{
        //    Console.WriteLine("These are the current store products on the website");
        //    var storeObjs = logic.GetStoreProducts();
        //    if (storeObjs != null) 
        //    {
        //        foreach (var storeObj in storeObjs)
        //        {
        //            Console.WriteLine($"Id: {storeObj.Id} Name: {storeObj.Name} Price: {storeObj.Price} Sale: {storeObj.Sale} Stock: {storeObj.Stock} CompId: {storeObj.ComputerPartId}");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("No store objects as of now");
        //    }
        //    Console.WriteLine("");
        //    Console.WriteLine("What CRUD action?");
        //    if (storeObjs != null)
        //    {
        //        foreach (var key in Commandos)
        //        {
        //            Console.WriteLine($"[{key.Key}] to {key.Value} a product");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Only action available is to Create, by pressing [C]");
        //    }

        //    var userInputC = Console.ReadKey(true);
        //    if (!Commandos.TryGetValue(userInputC.Key, out var userCrudValue))
        //    {
        //        Console.WriteLine("Some error here bud");
        //        return;
        //    }
        //    if (userCrudValue != CRUD.Create)
        //    {
        //        var selectedComponent = GetStoreProductById(storeObjs.ToList());
        //        switch (userCrudValue)
        //        {
        //            case CRUD.Read:
        //                if (selectedComponent != null)
        //                {
        //                    selectedComponent.Read(logic);
        //                }
        //                break;
        //            case CRUD.Update:
        //                if (selectedComponent != null)
        //                {
        //                    selectedComponent.Update(logic);
        //                }
        //                break;
        //            case CRUD.Delete:
        //                if (selectedComponent != null)
        //                {
        //                    selectedComponent.Delete(logic);
        //                }
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        ComputerPart part = ChooseWhatPartToMakeAStoreProductOutOf(logic);
        //        StoreProduct newStoreProduct = new StoreProduct();
        //        if(part != null)
        //        {
        //            newStoreProduct.Create(logic, part);
        //        }
        //        else
        //        {
        //            Console.WriteLine("error");
        //        }
               
        //    }


        //}
        //public static ComputerPart ChooseWhatPartToMakeAStoreProductOutOf(ApplicationManager logic)
        //{
        //    Console.WriteLine("What category?");
        //    ComputerPart componentToCreate = AskWhatProductType(logic);

        //    var relevantObjects = logic.GetComputerComponentsByType(componentToCreate);

        //    Console.WriteLine($"You chose: {componentToCreate.GetType().Name}");
        //    Console.WriteLine("Current components in this category;");
        //    if (relevantObjects != null)
        //    {
        //        foreach (var part in relevantObjects)
        //        {
        //            Console.WriteLine($"-\tId: {part.Id} Name: {part.Name}");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("No component of this type yet");
        //    }
        //    Console.WriteLine("Input the Id of the object you'd like to make a product out of");
        //    int choice =GeneralHelpers.StringToInt();
        //    var chosenObject = relevantObjects.FirstOrDefault(x => x.Id == choice);
        //    var currentStoreObjects = logic.GetStoreProducts();
        //    if (chosenObject != null) 
        //    {
        //        Console.WriteLine("Valid object");
        //        if (currentStoreObjects != null) 
        //        {
        //            Console.WriteLine("We have a refernce object");
        //            if(currentStoreObjects.Any(x => x.ComputerPartId == chosenObject.Id))
        //            {
        //                Console.WriteLine("That already exists on the store page");
        //                Console.WriteLine("Press Enter to Continue");
        //                Console.ReadLine();
        //                return null;
        //            }
        //            else
        //            {
        //                return chosenObject;
        //            }
        //        }
        //    }
        //    return null;
        //}
        //public static void CategoryInput(ApplicationManager logic)
        //{
        //    ComponentSpecification specCreate = AskWhatComponentSpecification(logic);


        //    Console.WriteLine($"You chose: {specCreate.GetType().Name}");
        //    Console.WriteLine("Current components in this category;");
        //    if (relevantObjects != null)
        //    {
        //        foreach (var spec in relevantObjects)
        //        {
        //            Console.WriteLine($"-\tId: {spec.Id} {spec.Name}");
        //        }
        //    }
        //    Console.WriteLine("What CRUD action?");
        //    if (relevantObjects != null)
        //    {
        //        foreach (var key in Commandos)
        //        {
        //            Console.WriteLine($"[{key.Key}] to {key.Value} a product");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Only action available is to Create, by pressing [C]");
        //    }

        //    var userInputC = Console.ReadKey(true);
        //    if (!Commandos.TryGetValue(userInputC.Key, out var userCrudValue))
        //    {
        //        Console.WriteLine("Some error here bud");
        //        return;
        //    }
        //    if (userCrudValue != CRUD.Create)
        //    {
        //        var selectedComponent = GetSpecByTypeAndId(relevantObjects.ToList());
        //        switch (userCrudValue)
        //        {
        //            case CRUD.Read:
        //                if (selectedComponent != null)
        //                {
        //                    selectedComponent.Read(logic);
        //                }
        //                break;
        //            case CRUD.Update:
        //                if (selectedComponent != null)
        //                {
        //                    selectedComponent.Update(logic);
        //                }
        //                break;
        //            case CRUD.Delete:
        //                if (selectedComponent != null)
        //                {
        //                    selectedComponent.Delete(logic);
        //                }
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        specCreate.Create(logic);
        //    }
        //}
        static ComputerPart GetObjectByTypeAndId(List<ComputerPart> compObjs)
        {
            Console.WriteLine("What object (by ID) do you want to interact with?");
            int idChoice = GeneralHelpers.StringToInt();
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
            int idChoice = GeneralHelpers.StringToInt();
            if (compObjs.Any(x => x.Id == idChoice))
            {
                return compObjs.FirstOrDefault(c => c.Id == idChoice);
            }
            else
            {
                return null;
            }
        }
        //static StoreProduct GetStoreProductById(List<StoreProduct> compObjs)
        //{
        //    Console.WriteLine("What object (by ID) do you want to interact with?");
        //    int idChoice = GeneralHelpers.StringToInt();
        //    if (compObjs.Any(x => x.Id == idChoice))
        //    {
        //        return compObjs.FirstOrDefault(c => c.Id == idChoice);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        //static ComputerPart AskWhatProductType(ApplicationManager logic)
        //{
        //    Console.WriteLine("What type of product?");
        //    List<Type> types = GeneralHelpers.ReturnComputerPartTypes();
        //    for (int i = 0; i < types.Count; i++)
        //    {
        //        Console.WriteLine($"{i + 1} {types[i].Name}");
        //    }

        //    string usIn = Console.ReadLine();
        //    if (Int32.TryParse(usIn, out int choice))
        //    {
        //        choice -= 1;
        //        switch (choice)
        //        {
        //            case 0:
        //                return new CPU();
        //            case 1:
        //                return new GPU();
        //            case 2:
        //                return new Motherboard();
        //            case 3:
        //                return new PSU();
        //            case 4:
        //                return new RAM();
        //        }
        //    }
        //    return null;
        //}
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
        internal static CustomerShippingInfo GetCustomerShippingInfoInput()
        {
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
