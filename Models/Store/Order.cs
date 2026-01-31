using ComputerStoreApplication.Account;
using ComputerStoreApplication.Helpers;
using ComputerStoreApplication.Models.Customer;
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
        public CustomerAccount OrdCustomer { get; set; }

        public int ShippingInfoId { get; set; }
        public CustomerShippingInfo ShippingInfo { get; set; }

        public DateTime CreationDate { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public decimal ShippingCost { get; set; }

        public decimal TaxCosts { get; set; }
        
        internal Decimal TaxRate = 0.25m;

        internal Decimal DisplayTaxRatePercent;
        public Decimal Subtotal {  get; set; }
        public Decimal TotalCost { get; private set; }

        public int? DeliveryProviderId  { get; set; }
        public DeliveryProvider DeliveryProvider { get; set; }

        public int? PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

      
        public void ChoosePaymentMethod(List<PaymentMethod> paymentMethods)
        {
            Console.WriteLine("Which delviery provider do you want to choose? Input their corresponding Id");
            foreach (var paymentService in paymentMethods)
            {
                Console.WriteLine($"Id: {paymentService.Id} {paymentService.Name}");
            }
            int choice = GeneralHelpers.StringToInt();
            var valid = paymentMethods.FirstOrDefault(x => x.Id == choice);
            if (valid != null)
            {
                Console.WriteLine("Great, you'll get the invoice the following day");
                PaymentMethodId = valid.Id;
                PaymentMethod = valid;
            }
        }
        public void ApplyAdress(List<CustomerShippingInfo> Adresses)
        {
            Console.WriteLine("Which address to ship to?");
            foreach (var customerShippingInfo in Adresses)
            {
                Console.WriteLine($"{customerShippingInfo.Id} {customerShippingInfo.StreetName} {customerShippingInfo.PostalCode} {customerShippingInfo.City.Name}");
            }
        }
        public void ApplyPayMethod(PaymentMethod payservice)
        {
            if (payservice != null)
            {
                //Postnord, instabox kostnader
                PaymentMethodId = payservice.Id;
                PaymentMethod = payservice;
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
            TotalCost = Subtotal + ShippingCost;
            TaxCosts = TotalCost - (TotalCost / (1 + TaxRate));
            Console.WriteLine("====================================");
            Console.WriteLine($"Subtotal is {Subtotal-TaxCosts} (Pre-tax)");
            Console.WriteLine($"Cost for delivery by provider {ShippingCost}");
            Console.WriteLine($"Total costs is: {TotalCost}");
            decimal displayTaxRate = TaxRate;
            displayTaxRate *= 100;
            Console.WriteLine($"({displayTaxRate}% tax which is {TaxCosts})");
            Console.WriteLine($"Payment method: {PaymentMethod.Name}");
            Console.WriteLine($"Delivery service: {DeliveryProvider.Name}");
            Console.WriteLine($"Delivering to {ShippingInfo.StreetName} {ShippingInfo.PostalCode} {ShippingInfo.City.Name} {ShippingInfo.City.Country.Name}");
            Console.WriteLine("====================================");
        }
    }
}
