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

        public static DisplayPackage ToDisplay(this CustomerShippingInfo info, List<City> cities, List<Country> countries)
        {
            if (info == null) return null!;
            var city = info.City ?? cities.FirstOrDefault(c => c.Id == info.CityId);
            var country = city?.Country ?? countries.FirstOrDefault(c => c.Id == city?.CountryId);

            return new DisplayPackage
            {
                Id = info.Id,
                StreetName = info.StreetName,
                PostalCode = info.PostalCode,
                CityName = city?.Name ?? "Unknown",
                CountryName = country?.Name ?? "Unknown"
            };
        }

        public static DisplayOrder ToDisplay(this Order order, List<City> cities, List<Country> countries)
        {
            return new DisplayOrder
            {
                Id = order.Id,
                CreationDate = order.CreationDate,
                Subtotal = order.Subtotal,
                TotalCost = order.TotalCost,
                ShippingCost = order.ShippingCost,
                TaxCosts = order.TaxCosts,
                DeliveryProviderName = order.DeliveryProvider?.Name ?? "None",
                PaymentMethodName = order.PaymentMethod?.Name ?? "None",
                ShippingInfos = order.ShippingInfo != null
                    ? new List<DisplayPackage> { order.ShippingInfo.ToDisplay(cities, countries) }
                    : new List<DisplayPackage>()
            };
        }
    }
}
