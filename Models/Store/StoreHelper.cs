using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Logic;
using ComputerStoreApplication.Models.ComputerComponents;
using System;
using System.Collections.Generic;
using System.Linq;
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
        internal static void AdjustQuantityOfBasketItems(BasketProduct basketProducts)
        {
            foreach (var key in Commandos)
            {

                Console.WriteLine($"[{key.Key}] to {key.Value} a product");
            }

            var userInputC = Console.ReadKey(true);
            if (!Commandos.TryGetValue(userInputC.Key, out var userCrudValue))
            {
                Console.WriteLine("Some error here bud");
                return;
            }
            switch (userCrudValue)
            {
                case CRUD.Increase:
                    basketProducts.Quantity++;
                    break;
                case CRUD.Decrease:
                    basketProducts.Quantity--;
                    break;
               
                case CRUD.Remove:
                    basketProducts.Quantity = 0;
                    break;
            }
        }
    }
}
