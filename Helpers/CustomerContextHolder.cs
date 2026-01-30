using ComputerStoreApplication.Account;
using ComputerStoreApplication.Models.Customer;
using ComputerStoreApplication.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Helpers
{
    public class CustomerLocationContext
    {
        //customer info we're printing
        public CustomerAccount Customer { get; private set; }
        //all relevant addresses tied to a customer
        public List<CustomerShippingInfo> ShippingInfos { get; private set; }
        //locations
        public List<City> Cities { get; private set; }
        public List<Country> Countries { get; private set; }


        public CustomerLocationContext(CustomerAccount thisCustomer, List<City> cities, List<Country> countires)
        {
            Customer = thisCustomer;
            Cities = cities;
            Countries = countires;
            // Try to populate from orders first, fallback to customer shipping infos
            ShippingInfos = Customer.Orders
                .Where(o => o.ShippingInfo != null)
                .Select(o => o.ShippingInfo)
                .ToList();

            if (!ShippingInfos.Any() && Customer.CustomerShippingInfos != null)
                ShippingInfos = Customer.CustomerShippingInfos.ToList();

            RejuvinateReferences();
        }
        void RejuvinateReferences()
        {
            if (ShippingInfos == null)
            {
                return;
            }

            foreach (var ship in ShippingInfos)
            {
                // Fallback to find city by CityId if EF didn't load it
                if (ship.City == null && ship.CityId != 0)
                {
                    ship.City = Cities.FirstOrDefault(c => c.Id == ship.CityId) ?? ship.City;
                    if (ship.City != null)
                        ship.City.Country = ship.City.Country ?? Countries.FirstOrDefault(c => c.Id == ship.City.CountryId);
                }
                // Fallback to find country by CountryId if EF didn't load it
                if (ship.City != null && ship.City.Country == null && ship.City.CountryId != 0)
                {
                    ship.City.Country = Countries.FirstOrDefault(c => c.Id == ship.City.CountryId);
                }
            }
        }

        public string GetShippingToText(CustomerShippingInfo info)
        {
            if (info == null) return "No address found";
            return $"{info.StreetName} {info.PostalCode} {info.City?.Name ?? "Unknown"} {info.City?.Country?.Name ?? "Unknown"}";
        }
    }


}
