using ComputerStoreApplication.Account;
using ComputerStoreApplication.Helpers.DTO;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Store;
using ComputerStoreApplication.Models.Vendors_Producers;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Logic
{
    public class DapperService
    {
        readonly string _connstring;
        public DapperService(ComputerDBContext dBContext)
        {
            _connstring = dBContext.Database.GetConnectionString();
        }
        SqlConnection GetConnection()=>new SqlConnection(_connstring);
    
        public async Task<List<CountrySpending>> GetCountryWithTheMostSpending()
        {
            using var connection = GetConnection();
            await connection.OpenAsync();

            var sql = @"Select TOP 3
            co.Name as CountryName,
            Sum(o.TotalCost) as TotalSpent
        From Orders o
        Join
            CustomerShippingInfos csi On o.ShippingInfoId=csi.Id
        Join
            Cities ci on  csi.CityId=ci.Id
        Join
            Countries co On ci.CountryId = co.Id
        Group By co.Name
        Order By TotalSpent DESC;";

            var result = await connection.QueryAsync<CountrySpending>(sql);
            return result.ToList();
        }
       
        public async Task<decimal> GetTotalRevenue()
        {
            using var connection = GetConnection();
            await connection.OpenAsync();

            var sql = @"
              SELECT 
             SUM(oi.Quantity * com.Price) AS TotalRevenue
            FROM Orders o
            JOIN OrderedProducts oi ON o.Id = oi.OrderId
            JOIN CompuerProducts com ON oi.ComputerPartId = com.Id;";

            //Execute scalar for singular easy value
            var totalRevenue = await connection.ExecuteScalarAsync<decimal>(sql);
            return totalRevenue;
        }
        public async Task<(int CustomerId, string FirstName, string SurName, decimal TotalSpent)?> GetHighestSpender()
        {
            using var connection = GetConnection();
            await connection.OpenAsync();
            string sql = @"
                SELECT TOP 1 c.Id, c.FirstName, c.SurName, SUM(oi.Quantity * cp.Price) As TotalSpent
                FROM Customers c
                Join Orders o On o.CustomerId = c.Id
                Join OrderedProducts oi On oi.OrderId = o.Id
                Join CompuerProducts cp On cp.Id = oi.ComputerPartId
                Group By c.Id, c.FirstName, c.SurName
                ORder By TotalSpent DESC;";

            var result = await connection.QueryFirstOrDefaultAsync<(int Id, string FirstName, string SurName, decimal TotalSpent)?>(sql);
            return result;
        }

        public async Task<(int CustomerId, string FirstName, string SurName, decimal TotalSpent)?> GetLowestSpender()
        {
            using var connection = GetConnection();
            await connection.OpenAsync();
            string sql = @"
                SELECT TOP 1 c.Id, c.FirstName, c.SurName, SUM(oi.Quantity * cp.Price) As TotalSpent
                FROM Customers c
                Join Orders o On o.CustomerId = c.Id
                Join OrderedProducts oi On oi.OrderId = o.Id
                Join CompuerProducts cp On cp.Id = oi.ComputerPartId
                Group By c.Id, c.FirstName, c.SurName
                ORder By TotalSpent ASC;";

            var result = await connection.QueryFirstOrDefaultAsync<(int Id, string FirstName, string SurName, decimal TotalSpent)?>(sql);
            return result;
        }


        public async Task<decimal> TotalStockValueOnSpecificProduct(ComputerPart part)
        {
            using var connection = GetConnection();
            await connection.OpenAsync();
            string sql = @"
            Select Sum(com.Price*com.Stock) as TotalStockValue
            From CompuerProducts com
            Where com.Id = @Id";

            decimal value = await connection.ExecuteScalarAsync<decimal>(sql, new { Id = part.Id });
            return value;
        }
        //How many active orders include this product?
        public async Task<int> CountOrdersForProduct(int computerPartId)
        {
            using var connection = GetConnection();
            await connection.OpenAsync();
            string sql = @"
                Select Count (Distinct oi.OrderId) 
                From OrderedProducts oi
                Where oi.ComputerPartId = @Id";

            int orderCount = await connection.ExecuteScalarAsync<int>(sql, new { Id = computerPartId } );

            return orderCount;
        }
        //Most expensive purchase per country
        public async Task<(int OrderId, decimal TotalValue)?> GetMostExpensiveOrderLast24Hours()
        {
            using var connection = GetConnection();
            await connection.OpenAsync();

            string sql = @"
                Select Top 1 o.Id As OrderId, Sum(oi.Quantity * com.Price) As TotalValue
                From Orders o
                Join OrderedProducts oi On oi.OrderId = o.Id
                Join CompuerProducts com On com.Id = oi.ComputerPartId
                Where o.OrderDate >= DATEADD(DAY, -1, GETDATE())
                Group By o.Id
                Order By TotalValue Desc;";

            //A few rows, query first or default (might find 1, maybe more)
            var result = await connection.QueryFirstOrDefaultAsync<(int OrderId, decimal TotalValue)>(sql);

            return result;
        }
        public async Task<IEnumerable<(string Country, decimal Value)>> GetMostExpensiveOrdersPerCountry()
        {
            using var connection = GetConnection();
            await connection.OpenAsync();

            string sql = @"SELECT 
            co.Name as CountryName,
            o.Id as OrderId,
            Sum(oi.Quantity * com.Price) as TotalValue
            From Orders o
            Join OrderedProducts oi On oi.OrderId = o.Id
            Join CompuerProducts com On com.Id = oi.ComputerPartId
            Join CustomerShippingInfos si On si.Id = o.ShippingInfoId
            Join City ci On ci.Id = si.CityId
            Join Country co On co.Id = ci.CountryId
            Group By co.Name, o.Id
            Order By co.Name, TotalValue DESC;
            ";

            //OrderId limits the total, to get the most expensive
             //Instead of all orders summed up
              var allOrders = await connection.QueryAsync<(string CountryName, int OrderId, decimal TotalValue)>(sql);

                //Group by country, fill the "Country" part with key (id), first big total value
            var sortedByCountries = allOrders.
                GroupBy(o => o.CountryName).
                Select(g => (Country: g.Key, g.First().TotalValue));
            return sortedByCountries;
        }
        public async Task<decimal> TotalRevenueBasedOnBrand(int brandId)
        {
            using var connection = GetConnection();
            await connection.OpenAsync();

            string sql = @"Select Sum(oi.Quantity * cs.Price) As TotalRevenue
            From OrderedProducts oi
            Join CompuerProducts cs ON cs.Id = oi.ComputerPartId
            Where cs.BrandId = @BrandId;";

            var brandRevenue = await connection.QuerySingleOrDefaultAsync<decimal>(sql, new { BrandId = brandId });
            return brandRevenue;
        }
        public async Task<IEnumerable<(int DeliveryProviderId, string DeliveryProviderName, int NumberOfOrders)>> GetOrdersPerDeliveryService()
        {
            using var connection = GetConnection();
            await connection.OpenAsync();
            string sql = @"
                   Select 
                dp.Id As DeliveryProviderId,
                dp.Name As DeliveryProviderName,
                Count(o.Id) As NumberOfOrders
            From Orders o
            Join DeliveryProviders dp On dp.Id = o.DeliveryProviderId
            Group By dp.Id, dp.Name
            Order By NumberOfOrders DESC;";

            var result = await connection.QueryAsync<(int DeliveryProviderId, string DeliveryProviderName, int NumberOfOrders)>(sql);
            return result;
        }
        public async Task<(int BrandId, string BrandName, decimal TotalRevenue)?> GetMostProfitableBrand()
        {
            using var connection = GetConnection();
            await connection.OpenAsync();
            //Baed on all brands, find most profitable
            string sql = @"Select Top 1 
            brand.Id AS BrandId,
            brand.Name AS BrandName,
            Sum(oi.Quantity * cs.Price) AS TotalRevenue
            FROM CompuerProducts cs
            Join OrderedProducts oi ON oi.ComputerPartId = cs.Id
            Join BrandManufacturers brand On brand.Id = cs.BrandId
            Group By brand.Id, brand.Name
            Order By TotalRevenue DESC;";

            var result = await connection.QueryFirstOrDefaultAsync<(int BrandId, string BrandName, decimal TotalRevenue)>(sql);
            return result;
        }
        public async Task<IEnumerable<(string Country, decimal value)>> GetTotalCountrySpending()
        {
            using var connection = GetConnection();
            await connection.OpenAsync();
            //Sum total country spendings by linking orders to countries
            string sql = @"Select 
            co.Name AS CountryName,
            Sum(oi.Quantity * com.Price) As TotalValue
            From Orders o
            Join OrderedProducts oi On oi.OrderId = o.Id
            Join CompuerProducts com On com.Id = oi.ComputerPartId
            Join CustomerShippingInfos si On si.Id = o.ShippingInfoId
            Join Cities ci On ci.Id = si.CityId
            Join Countries co On co.Id = ci.CountryId
            Group By co.Name
            Order By TotalValue Desc;";

            //All orders all countries
             var countryOrders = await connection.QueryAsync<(string Country, decimal TotalValue)>(sql);
            var sortedCountries = countryOrders.
                OrderBy(g=>g.Country)
                  .Select(g => (Country: g.Country, Value: g.TotalValue));

            return sortedCountries;
        }

            
                

    }
}