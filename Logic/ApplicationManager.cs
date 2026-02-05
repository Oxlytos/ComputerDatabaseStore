using ComputerStoreApplication.Account;
using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Store;
using ComputerStoreApplication.Models.Vendors_Producers;
using ComputerStoreApplication.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ComputerStoreApplication.Logic
{
    //Application manager ser till att vi har en databas igång
    //Den skickar sedan vidare förfrågningar till service som kan validera och hämta info från databasen
    //Vi hanterar även sidor här
    public class ApplicationManager : IDisposable
    {
        private readonly ComponentService _services;

        public  MongoConnection _mongoConnection;
        public IDapperService Dapper { get; }
        public ComputerDBContext ComputerPartShopDB { get; } //
        private readonly ContextFactory _contextFactory;
        public IPage CurrentPage { get; set; }
        public int CustomerId { get; set; }
        public bool IsLoggedInAsCustomer => CustomerId != 0;

        //application holds reference to basket items on a customer
        private List<BasketProduct> _currentBasket = new();
        //ireadonly should like suggested only read and not be able to modify
        public IReadOnlyList<BasketProduct> CurrentBasket => _currentBasket;

        public int AdminId { get; set; }
        public bool IsLoggedInAsAdmin => AdminId != 0;
        public List<ComputerPart> ProductsInBasket { get; set; }

        public ApplicationManager(ComponentService service, MongoConnection mongo, ContextFactory contextFactory, IDapperService dapper)
        {
            //Instansiera db kontextet här EN gång

            //Startsidan blir där man kan browse:a produkter
            CurrentPage = new HomePage();
            ProductsInBasket = new List<ComputerPart>();
            _services = service;
            _mongoConnection = mongo;
            CustomerId = 0;
            AdminId = 0;
            _contextFactory = contextFactory;
            Dapper = dapper;
        }
        public void RefreshBasket()
        {
            if (!IsLoggedInAsCustomer)
            {
                return;
            }
            using var contex = _contextFactory.Create();
            _currentBasket = contex.BasketProducts.Include(p=>p.ComputerPart).Where(s=>s.CustomerId == CustomerId).ToList();
        }

        public int BasketItemCount => _currentBasket.Sum(b=>b.Quantity);
        public ComponentCategory GetCategory(int productId)
        {
           return _services.GetCategory(productId);
        }
        public Brand GetBrand(int productId)
        {
            return _services.GetBrand( productId);
        }
        public void EditCustomerProfile()
        {
            _services.EditCustomerProfile();
        }
        public List<CustomerAccount> GetCustomers()
        {
            return _services.GetCustomers();
        }
        public bool LoginAsAdmin(string username, string password)
        {
           
            Console.WriteLine("Trying to login as admin");
            var adminId = _services.LoginAdmin(username, password);
            if (adminId != 0)
            {
                Console.WriteLine("Success");
                AdminId = adminId;
                _ = MongoConnection.AdminLoginAttempt(username, adminId, true);
                Console.ReadLine();
                return true;
            }
            else
            {
                Console.WriteLine("Wrong credentials, returning");
                Console.ReadLine();
                return false;
            }
        }
        public void CreateAccount(string email)
        {
            _services.CreateAccount(email);
        }
        public List<ComputerPart> GetFrontPageProducts()
        {
            return _services.GetFrontPageProducts();
        }
        public List<Order> GetCustomerOrders(int customerId)
        {
            return _services.DisplayCurrentCustomerOrders(customerId);
        }

        public bool LogoutAsAdmin()
        {
            Console.WriteLine("Logged out");
            AdminId = 0;
            return false;
        }
        public AdminAccount GetAdminInfo(int id)
        {
            return _services.GetAdminInfo(id);
        }
        public CustomerAccount GetCustomerInfo(int id)
        {
            return _services.GetCustomerInfo(id);
        }
        public bool LoginAsCustomer(string email, string password)
        {
            Console.WriteLine("Login attempt started");
            var thisCustomerId = _services.LoginCustomer(email, password);
            if (thisCustomerId != 0)
            {
                CustomerId = thisCustomerId;
                _ = MongoConnection.CustomerLoginAttempt(email, thisCustomerId, true);
                return true;
            }
            _ = MongoConnection.CustomerLoginAttempt(email, 0, false);
            return false;

        }
        public bool LogoutAsCustomer()
        {
            Console.WriteLine("Logged out");
            CustomerId = 0;
            return false;
        }
        public void HandleCustomerPurchase(int customerId)
        {
            _services.HandleCustomerPurchase(customerId);
        }
    
      
  
        public List<ComputerPart> GetStoreProducts()
        {
            return _services.GetStoreProducts();
        }
        public List<BasketProduct> GetBasketProductsFromCustomerId(int customerId)
        {
            return _services.GetCustomerItems(customerId);
        }
        //public IEnumerable<ComputerPart> GetComputerComponentsByType(ComputerPart type)
        //{
        //    return _services.GetObjectsOfTheSameType(type);
        //}
        //public List<Models.Vendors_Producers.ChipsetVendor> GetVendors()
        //{
        //    return _services.GetVendors();
        //}
        public List<ComputerPart> GetComputerParts()
        {
            using var context = new ComputerDBContext();
            return context.CompuerProducts.ToList();
        }
        public List<ComponentCategory> GetCategories()
        {
            using var context = new ComputerDBContext();
            return context.ComponentCategories.ToList();
        }
        public List<Models.Vendors_Producers.Brand> GetManufacturers()
        {
            using var context = new ComputerDBContext();
            return context.BrandManufacturers.ToList();
        }
        public void AddProductToBasket(ComputerPart prod, int customerId)
        {
            _services.AddProductToBasket(prod, customerId);
            RefreshBasket();
        }
       
        public void SaveNewCustomer(CustomerAccount cus)
        {
            _services.SaveNewCustomer(cus);
        }
        public void RefreshCurrentCustomerBasket(CustomerAccount customer)
        {
            if (customer == null) 
            {
                throw new Exception("Could not find customer");
            }
            using var context = new ComputerDBContext();
            customer.ProductsInBasket = context.BasketProducts
          .Where(b => b.CustomerId == customer.Id && b.Quantity > 0)
          .Include(b => b.ComputerPart)
          .ToList();
        }
       
        public void Dispose() { }
        public void VerifyStoreItems()
        {
            //Make sure user can't buy products no longer in stock
            //we load this when they load the checkout page

            //var itemtsToRemove =ComputerPartShopDB.BasketProducts.Where
            //    (b=>b.Quantity==0||ComputerPartShopDB.CompuerProducts.
            //    Any(p=>p.Id==b.ComputerPartId&&p.Stock==0))
            //    .ToList();

            //ComputerPartShopDB.RemoveRange(itemtsToRemove);
            //ComputerPartShopDB.SaveChanges();

            using var context = new ComputerDBContext();
            var outOfStockItems = context.CompuerProducts
               .Where(p => p.Stock == 0)
               .Select(p => p.Id)
               .ToList();

        }
        public void VerifyBasketItems(int? currentCustomerId)
        {
            //var zeroItems = ComputerPartShopDB.BasketProducts .Where(b => b.CustomerId == CustomerId && b.Quantity == 0).ToList();
            using var context = new ComputerDBContext();
            var basketItems = context.BasketProducts.Where(b => b.CustomerId == currentCustomerId && b.Quantity > 0) .ToList();

            //ComputerPartShopDB.BasketProducts.RemoveRange(zeroItems);
            //ComputerPartShopDB.SaveChanges();
        }
        public void InformOfQuittingOperation()
        {
            Console.WriteLine("Quitting operation... (Press any key to continue)");
            Console.ReadKey();
        }

        internal List<ComputerPart> GetDBProductsFromList(List<ComputerPart>? selectedProducts)
        {
            if (selectedProducts == null)
            {
                return null;
            }
            using var context = new ComputerDBContext();

            //We want these objects, select their Ids
            var relevantIds = selectedProducts.Select(p => p.Id).ToList();

            //Matching and relevant objects are those that share the same Id
            var relevantObjects = context.CompuerProducts.Where(x=>relevantIds.Contains(x.Id)).Include(x=>x.ComponentCategory).Include(k=>k.BrandManufacturer).ToList();

            return relevantObjects;
        }

        internal ComputerPart? ChooseProductFromList(List<ComputerPart> parts)
        {
            Console.WriteLine("Input the corresponding objects Id");
            int choice = GeneralHelpers.ReturnValidIntOrNone();
            if (choice == 0) { return null; }
            var valid = parts.FirstOrDefault(x => x.Id == choice);
            if (valid == null) 
            {
                return null;
            }
            return valid;

        }

        internal void CheckoutObject( List<ComputerPart>? selectedProducts)
        {
            Console.Clear();
            Console.WriteLine("What object do you wanna view, and maybe add to your basket? Input their corresponding Id");
            var products = GetDBProductsFromList(selectedProducts);
            foreach(var product in products)
            {
                string onSale = product.Sale ? "Yes" : "No";
                Console.WriteLine($"Id: [{product.Id}] Name: [{product.Name}] Price: [{product.Price}]€ On Sale?: [{onSale}] Category: [{product.ComponentCategory.Name}] Brand:[{product.BrandManufacturer.Name}]");
            }
            var choosenObject = ChooseProductFromList(products);
            if (choosenObject == null)
            {
                return;
            }
            Console.WriteLine($"Id: {choosenObject.Id} Name; {choosenObject.Name}");

            var category = GetCategory(choosenObject.Id) ?? throw new Exception("Could not find category");
            var brand = GetBrand(choosenObject.Id) ?? throw new Exception("Could not find brand");
            choosenObject.Read(brand, category);
            Console.SetCursorPosition(5, 40);
            Console.WriteLine("Add to basket?");
            bool yes = GeneralHelpers.ChangeYesOrNo(false);
            if (!yes)
            {
                InformOfQuittingOperation();
                return;
            }
            //if (yes && currentCustomerId != null)
            //{
            //    AddProductToBasket(choosenObject, currentCustomerId.Value);
            //}
            //else if (currentCustomerId == null)
            //{
            //    Console.WriteLine("You need to be logged in to add to basket");
            //    InformOfQuittingOperation();
            //}
        }

        //internal void AdjustBasketItems(CustomerAccount? currentCustomer, List<BasketProduct> basketProducts)
        //{
        //    if (basketProducts.Count == 0)
        //    {
        //        return;
        //    }
        //    else if (basketProducts.Count > 1)
        //    {
        //        Console.WriteLine("Which basket item? Input their Id, cancel operation by submitting 0");
        //        foreach (var product in basketProducts)
        //        {
        //            Console.WriteLine($"Id: {product.Id} {product.ComputerPart.Name} Quantity: {product.Quantity}");
        //        }
        //        int choice = GeneralHelpers.ReturnValidIntOrNone();
        //        if(choice == 0)
        //        {
        //            return;
        //        }
        //        var valid = basketProducts.FirstOrDefault(X => X.Id == choice);
        //        if (valid == null)
        //        {
        //            throw new Exception("Could not find that product, returning");
        //        }
        //        var storeItem = GetStoreProducts().FirstOrDefault(x => x.Id == valid.ComputerPartId);
        //        Console.WriteLine($"How many of this product? Maximum is {storeItem.Stock}");
        //        int amount = GeneralHelpers.StringToInt();
        //        if (amount > storeItem.Stock)
        //        {
        //            Console.WriteLine("Input is greater than available stock, we've capped it at available amount");
        //            amount= storeItem.Stock;
        //        }
        //    }
        //    else
        //    {
        //        var item = basketProducts.First(); 
        //        var storeItem = GetStoreProducts().FirstOrDefault(x => x.Id == item.ComputerPartId);
        //        Console.WriteLine($"How many of this product? Maximum is {storeItem.Stock}");
        //        int amount = GeneralHelpers.StringToInt();
        //        if (amount > storeItem.Stock)
        //        {
        //            Console.WriteLine("Input is greater than available stock, we've capped it at available amount");
        //            amount = storeItem.Stock;
        //        }

        //    }
        //        return;

        //}
    }
}
