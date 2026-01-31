using ComputerStoreApplication.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Store
{
    public class DeliveryProvider
    {
        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int AverageDeliveryTime { get; set; }

        public DeliveryProvider Create()
        {
            Console.WriteLine("Name?");
            Name = GeneralHelpers.SetName(30);
            Console.WriteLine("Price for service (per order)");
            Price = GeneralHelpers.StringToDecimal();
            Console.WriteLine("Avg. Delivery time (not including weekends)");
            AverageDeliveryTime = GeneralHelpers.StringToInt();
            return this;
        }
        public void Update()
        {
            Console.WriteLine("To keep current properties, leave empty");
            Console.WriteLine("Name?");
            Name = GeneralHelpers.ChangeName(Name, 30);
            Console.WriteLine("Price for service (per order)");
            Price = GeneralHelpers.ChangeDecimal(Price);
            Console.WriteLine("Avg. Delivery time (not including weekends)");
            AverageDeliveryTime = GeneralHelpers.ChangeInt(AverageDeliveryTime);
        }
        public void Read()
        {
            //Avg stats
        }

    }
}
