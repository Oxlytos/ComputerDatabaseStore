using ComputerStoreApplication.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Store
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer.Customer OrdCustomer { get; set; }

        public DateTime CreationDate { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public decimal ShippingCost { get; set; }

        public decimal TaxCosts { get; set; }
        Decimal TaxRate = 0.25m;
        public Decimal Subtotal {  get; set; }
        public Decimal TotalCost { get; private set; }

        public int DeliveryProviderId  { get; set; }
        public DeliveryProvider DeliveryProvider { get; set; }

        public void ChooseDeliveryProvider(List<DeliveryProvider> deliveryProviders)
        {
            Console.WriteLine("Which delviery provider do you want to choose? Input their corresponding Id");
            foreach (var deliveryProvider in deliveryProviders)
            {
                Console.WriteLine($"Id: {deliveryProvider.Id} {deliveryProvider.Name}, cost (€): {deliveryProvider.Price}");
            }
            int choice = GeneralHelpers.StringToInt(Console.ReadLine());
            var valid = deliveryProviders.FirstOrDefault(x => x.Id == choice);
            if (valid != null) 
            {
                ApplyDelivertyMethodAndProvider(valid);

            }
        }
        public void ApplyDelivertyMethodAndProvider(DeliveryProvider deliveryProvider)
        {
            if (deliveryProvider != null)
            {
                //Postnord, instabox kostnader
                DeliveryProviderId = deliveryProvider.Id;
                DeliveryProvider = deliveryProvider;
                ShippingCost = deliveryProvider.Price;
            }
       
        }
        public void CalculateTotalPrice()
        {
            //Innan skatt
            Subtotal = OrderItems.Sum(s => s.Quantity * s.Price);
            Console.WriteLine($"Subtotal is {Subtotal}");
            //25% moms ungefär som i Sverige

            TotalCost = Subtotal + ShippingCost;
            Console.WriteLine($"Total costs is: {TotalCost}");
            TaxCosts = TotalCost - (TotalCost / (1+TaxRate));
            Console.WriteLine($"({TaxRate} tax which is {TaxCosts})");
        }
    }
}
