using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Migrations;
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
        internal static void AdjustQuantityOfBasketItems(ICollection<BasketProduct> basketProducts)
        {
            Console.WriteLine("Which product? Choose by inputting the correct Id");
            int? choice = GeneralHelpers.StringToInt(Console.ReadLine());
            if (!choice.HasValue) 
            {
                return;
            }
            var thisProd = basketProducts.FirstOrDefault(x => x.Id == choice);
            if (thisProd == null)
            {
                return;
            }
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
                    thisProd.Quantity++;
                    break;
                case CRUD.Decrease:
                    thisProd.Quantity--;
                    if (thisProd.Quantity == 0 || thisProd.Quantity < 0)
                    {
                        basketProducts.Remove(thisProd);
                    }
                    break;
                case CRUD.Remove:
                        basketProducts.Remove(thisProd);
                    break;
            }
        }
    }
}
