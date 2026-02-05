using ComputerStoreApplication.Models.Customer;
using ComputerStoreApplication.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComputerStoreApplication.Helpers
{
    public static class DisplayHelpers
    {
        public class DisplayPackage
        {
            public int Id { get; set; }
            public string StreetName { get; set; } = "";
            public int PostalCode { get; set; } = 0;
            public string CityName { get; set; } = "";
            public string CountryName { get; set; } = "";
        }

        public class DisplayOrder
        {
            public int Id { get; set; }
            public DateTime CreationDate { get; set; }
            public decimal Subtotal { get; set; }
            public decimal TotalCost { get; set; }
            public decimal ShippingCost { get; set; }
            public decimal TaxCosts { get; set; }
            public string DeliveryProviderName { get; set; } = "None";
            public string PaymentMethodName { get; set; } = "None";
            public List<DisplayPackage> ShippingInfos { get; set; } = new List<DisplayPackage>();
        }
        //Big ol display objc to somehwat show everything in a nice enough manner
        //But just package, not whole order
        public static DisplayPackage ToDisplay(this CustomerShippingInfo info, List<City> cities, List<Country> countries)
        {
            if (info == null) 
            {
                return null;
            }
            var city = cities.FirstOrDefault(c => c.Id == info.CityId);
            var country = countries.FirstOrDefault(c => c.Id == city?.CountryId);
            //Display easy like this
            return new DisplayPackage
            {
                Id = info.Id,
                StreetName = info.StreetName,
                PostalCode = info.PostalCode,
                //Could be null fields
                CityName = city?.Name ?? "Unknown/Not Found",
                CountryName = country?.Name ?? "Unknown/Not Found"
            };
        }
        //Display whole order
        public static DisplayOrder ToDisplay(this Order order, List<City> cities, List<Country> countries)
        {
            //Order to display
            return new DisplayOrder
            {
                Id = order.Id,
                CreationDate = order.CreationDate,
                Subtotal = order.Subtotal,
                TotalCost = order.TotalCost,
                ShippingCost = order.ShippingCost,
                TaxCosts = order.TaxCosts,
                DeliveryProviderName = order.DeliveryProvider?.Name ?? "None",
                PaymentMethodName = order.PaymentMethod?.Name ?? "None", //Display shippinginfo if not null, display within this method :otherwise create a new list of display package
                ShippingInfos = order.ShippingInfo != null ? new List<DisplayPackage> { order.ShippingInfo.ToDisplay(cities, countries) }: new List<DisplayPackage>()
            };
        }
    }
}
