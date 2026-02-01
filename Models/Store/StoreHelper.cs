using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Store
{
    public class StoreHelper
    {
        public Dictionary<ConsoleKey, CRUD> keyValuePairs;
        private static readonly Dictionary<ConsoleKey, CRUD> Commandos = new()
        {
            {ConsoleKey.I, CRUD.Increase },
            {ConsoleKey.D, CRUD.Decrease },
            { ConsoleKey.R, CRUD.Remove },
        };

        public enum CRUD
        {
            Remove = ConsoleKey.R, //Ta bort, duh
            Decrease = ConsoleKey.O,
            Increase = ConsoleKey.I
        }
        internal static bool ViewProduct(ComputerPart product, ApplicationManager app)
        {
            Console.WriteLine("Some info on this product;");
            Console.WriteLine();
            Console.WriteLine("========================================================");
            //product.Read();
            Console.WriteLine("========================================================");
            Console.ReadLine();
            Console.WriteLine("Add to basket?");
            return GeneralHelpers.YesOrNoReturnBoolean();
        }
        internal static BasketProduct ChooseWhichBasketItem(ICollection<BasketProduct> basketProducts)
        {
            Console.WriteLine("Which product? Choose by inputting the correct Id");
            foreach (var basketProduct in basketProducts)
            {
                Console.WriteLine($"Id: {basketProduct.Id} {basketProduct.ComputerPart.Name}");
            }
            int? choice = GeneralHelpers.StringToInt();
            if (!choice.HasValue)
            {
                return null;
            }
            var thisProd = basketProducts.FirstOrDefault(x => x.Id == choice);
            if (thisProd == null)
            {
                return null;
            }
            else
            {
                return thisProd;
            }
        }
        internal static void AdjustQuantityOfBasketItems(ApplicationManager app, BasketProduct basketProduct)
        {
            Console.WriteLine("How many do you want? Input as an int");
            var productToAdd= app.ComputerPartShopDB.CompuerProducts.FirstOrDefault(x=>x.Id == basketProduct.ComputerPartId);
            int amount = GeneralHelpers.ReturnValidIntOrNone();
            if (amount==0||amount<0)
            {
                basketProduct.Quantity = 0;
                Console.WriteLine("Removing product, it has a quantity of 0 or lesser");
                Console.ReadLine();
            }
            if (amount > productToAdd.Stock)
            {
                Console.WriteLine("Can't add more than available in stock");
            }
            else
            {
                basketProduct.Quantity = amount;
            }


        }

      
    }
}
