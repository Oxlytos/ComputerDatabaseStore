using ComputerStoreApplication.Account;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Store;
using ComputerStoreApplication.Models.Vendors_Producers;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComputerStoreApplication.Crud_Related.CrudHandler;

namespace ComputerStoreApplication.Logic
{
    public class DapperService:IDapperService
    {
        private readonly IConnectionFactory _connectionFactory;
        public DapperService(IConnectionFactory connectionFactory)
        {
            if (connectionFactory ==null)
            {
                throw new ArgumentNullException("Error with connection factory");
            }
            _connectionFactory = connectionFactory;


        }
        private SqlConnection GetConnection () =>  _connectionFactory.CreateConnection();
        //When we querey first or default async
        private async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? param = null)
        {
            using var connection = GetConnection();
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<T>(sql, param);
        }
        //Everything Ieanumerable
        private async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null)
        {
            using var connection = GetConnection();
            await connection.OpenAsync();
            return await connection.QueryAsync<T>(sql, param);
        }
        //Update execeute
        private async Task<int> ExecuteAsync(string sql, object? param = null)
        {
            using var connection = GetConnection();
            await connection.OpenAsync();
            return await connection.ExecuteAsync(sql, param);
        }
        //these just keep the query more or less, call their respective method for correct result (execute for one thing, ieanimerable for more rows etc)
        public async Task<IEnumerable<(string CountryName, decimal TotalSpent)>> GetCountryWithTheMostSpending()
        {
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

            return await QueryAsync<(string CountryName, decimal TotalSpent)>(sql);
        }
       
        public async Task<decimal> GetTotalRevenue()
        {
            var sql = @"
              SELECT 
             SUM(oi.Quantity * com.Price) AS TotalRevenue
            FROM Orders o
            JOIN OrderedProducts oi ON o.Id = oi.OrderId
            JOIN CompuerProducts com ON oi.ComputerPartId = com.Id;";

            //Execute scalar for singular easy value
            return await QueryFirstOrDefaultAsync<decimal?>(sql) ?? 0m;
        }
        public async Task<(int CustomerId, string FirstName, string SurName, decimal TotalSpent)?> GetHighestSpender()
        {
            string sql = @"
                SELECT TOP 1 c.Id, c.FirstName, c.SurName, SUM(oi.Quantity * cp.Price) As TotalSpent
                FROM Customers c
                Join Orders o On o.CustomerId = c.Id
                Join OrderedProducts oi On oi.OrderId = o.Id
                Join CompuerProducts cp On cp.Id = oi.ComputerPartId
                Group By c.Id, c.FirstName, c.SurName
                ORder By TotalSpent DESC;";

            return await QueryFirstOrDefaultAsync<(int Id, string FirstName, string SurName, decimal TotalSpent)?>(sql);
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

            return await QueryFirstOrDefaultAsync<(int Id, string FirstName, string SurName, decimal TotalSpent)?>(sql);
        }
        public async Task<int?> ReadCustomerInfo()
        {

            return null;
        }

        public async Task<(int InStock, decimal StockValue, int UnitsSold, decimal TotalRevenue)> TotalStockValueOnSpecificProduct(ComputerPart part)
        {
            using var connection = GetConnection();
            await connection.OpenAsync();
            string sql = @"
             Select 
                com.stock as InStock,
                Sum(com.Price * com.Stock) as TotalStockValue,     
                ISNULL(Sum(op.Quantity), 0) as TotalSold,       
                ISNULL(Sum(op.Quantity * com.Price), 0) as Revenue  
            From CompuerProducts com
            Left Join OrderedProducts op
                On op.ComputerPartId = com.Id
            Where com.Id = @Id
            Group By com.Id, com.Price, com.Stock;";

            return await QueryFirstOrDefaultAsync<(int InStock, decimal StockValue, int UnitsSold, decimal TotalRevenue)> (sql, new { Id = part.Id });
        }
        //How many active orders include this product?
        public async Task<int> CountOrdersForProduct(int computerPartId)
        {
            using var connection = GetConnection();
            await connection.OpenAsync();
            string sql = @"
                Select Count (Distinct oi.OrderId) as InOrders
                From OrderedProducts oi
                Where oi.ComputerPartId = @Id";

            return await QueryFirstOrDefaultAsync<int>(sql, new { Id = computerPartId } );
        }
        //Most expensive purchase per country
        public async Task<(int CountProductsTotal,  decimal AvgValue, decimal TotalCost, decimal MostExpensiveObject, string MostExpensiveProductOrdered)?> GetOrdersByCustomerAndAvgCost(int inputCustomerId)
        {
            using var connection = GetConnection();
            await connection.OpenAsync();

            string sql = @"
              Select
                Sum(op.Quantity) as TotalProducts,
                AVG(com.Price) as AverageCost,
                Sum(op.Price * op.Quantity) as TotalCosts,
                Max(op.Price) as MostExpensiveProduct,
                (
                    Select TOP 1 com.Name
                    From OrderedProducts op2
                    Join Orders o2 On o2.Id = op2.OrderId
                    Join CompuerProducts com On com.Id = op2.ComputerPartId
                    Where o2.CustomerId = crs.Id
                    Order By op2.Price Desc
                ) As MostExpensiveProductName
            From OrderedProducts op
            Join Orders ords On ords.Id = op.OrderId
            Join Customers crs On crs.Id = ords.CustomerId
            Join CompuerProducts com On com.Id = op.ComputerPartId
            Where crs.Id = @CustomerId
            Group By crs.Id;
            ";

           return await QueryFirstOrDefaultAsync<(int CountProductsTotal, decimal AvgValue, decimal TotalCost, decimal MostExpensiveObjectm,  string MostExpensiveProductOrdered)>( sql, new { CustomerId = inputCustomerId }  );

        }
        //Most expensive purchase per country
        public async Task<( decimal TotalValue, string Email)?> GetMostExpensiveOrderLast24Hours()
        {
            using var connection = GetConnection();
            await connection.OpenAsync();

            string sql = @"
               Select 
                Top 1
                Sum(oi.Quantity * com.Price) As TotalValue,
                crs.Email as AccountThatOrdered
                
                From Orders o
                Join OrderedProducts oi 
                    On oi.OrderId = o.Id
                Join CompuerProducts 
                    com On com.Id = oi.ComputerPartId
                Join Customers
                    crs on crs.Id = o.CustomerId
                Where o.CreationDate >= DATEADD(DAY, -5, GETDATE())
                Group By o.Id, crs.Email
                Order By TotalValue Desc
            ;";

            //A few rows, query first or default (might find 1, maybe more)
            return await QueryFirstOrDefaultAsync<(decimal TotalValue, string Email)>(sql);

        }
        //Get singular customers most spent on brand
        public async Task<(string BrandName, decimal TotalSpent)> MostSpentOnBrand(int customerId)
        {

            string sql = @"Select
	                fav.BrandName,
	                fav.SpentOnBrand,
                    crs.Id as CustomerId
                From Customers crs

                Cross Apply
                (
	                Select Top 1
		                brands.Name as BrandName,
		                Sum (ops.Quantity * ops.Price) as [SpentOnBrand]
	                From Orders ords
	                Left Join OrderedProducts ops
		                On ords.Id=ops.OrderId
	                Left Join CompuerProducts comps
		                On comps.Id = ops.ComputerPartId
	                Left Join BrandManufacturers brands
		                On comps.BrandId = brands.Id
	                Where ords.CustomerId = @CustomerId
	                Group By brands.Name
	                Order By Sum(ops.Quantity*ops.Price) DESC
                ) fav
                Where crs.Id = @CustomerId";

            return await QueryFirstOrDefaultAsync<(string BrandName, decimal TotalSpent)> (sql, new { CustomerId = customerId });
        }
        public async Task<decimal?> TotalRevenueBasedOnBrand(int brandId)
        {
            string sql = @"Select Sum(oi.Quantity * cs.Price) As TotalRevenue
            From OrderedProducts oi
            Join 
                CompuerProducts cs ON cs.Id = oi.ComputerPartId
            Where cs.BrandId = @BrandId;";

            return await QueryFirstOrDefaultAsync<decimal?>(sql, new { BrandId = brandId });
        }
        public async Task<IEnumerable<(int DeliveryProviderId, string DeliveryProviderName, int NumberOfOrders, decimal TotalValue)>> GetOrdersPerDeliveryService()
        {
            string sql = @"
                   Select 
                dp.Id As DeliveryProviderId,
                dp.Name As DeliveryProviderName,
                Count(o.Id) As NumberOfOrders,
                Sum(orps.Price*orps.Quantity) as CarryingWaresValueOf
            From Orders o
            Join 
                DeliveryProviders dp On dp.Id = o.DeliveryProviderId
            Join
                OrderedProducts orps On o.Id = orps.OrderId
            Group By dp.Id, dp.Name
            Order By NumberOfOrders DESC;";

           return await QueryAsync<(int DeliveryProviderId, string DeliveryProviderName, int NumberOfOrders, decimal TotalValue)>(sql);
        }
        public async Task<IEnumerable<(string? PayName, int? Count)>> GetMostCommonPayMethod()
        {

            string sql = @"Select top 1
	        pay.Name as MostCommonPayMethod,
	        Sum(ords.PaymentMethodId) as OrdersUsing
        From PaymentMethods pay
	        Join Orders ords
		        on ords.PaymentMethodId = pay.Id
        Group By pay.Name";

           return await QueryAsync<(string? PayName, int? Count)>(sql);
        }
        public async Task<int?> GetHowManyUseThisPayMethod(int payId)
        {
            string sql = @"Select
	            Sum(ords.PaymentMethodId) as OrdersUsing
            From PaymentMethods pay
	            Join Orders ords
		            on ords.PaymentMethodId = pay.Id
            Where pay.Id = @PayId";

            return await QueryFirstOrDefaultAsync<int?>(sql, new { PayId = payId });
        }
        public async Task<int?> GetHowManyUseThisDeliveryService(int deliveryId)
        {
            string sql = @"Select
	        Sum(ords.DeliveryProviderId) as OrdersUsing
		        From DeliveryProviders drs
			        Join
				        Orders ords
				        On ords.DeliveryProviderId = drs.Id
        Where drs.Id=@DeliveryId";

            return await QueryFirstOrDefaultAsync<int?>(sql, new { DeliveryId = deliveryId });
        }
        public async Task<IEnumerable<(int? UniqueCountProducts, int? TotalStock)>> GetUniqueCountAndTotalStockCountOfBrandsProducts(int brandId)
        {

            string sql = @"Select
	            Count(*) as UniqueProducts,
	            Sum(prods.Stock) as TotalProductsInStock
		            From CompuerProducts prods
			            Join
				            BrandManufacturers brnds
					            On prods.BrandId = brnds.Id
            Where brnds.Id = @BrandId";

           return await QueryAsync<(int? UniqueCountProducts, int? TotalStock)>(sql, new { BrandId = brandId });
        }
        public async Task<IEnumerable<(int? DifferentProducts, int? TotalStock)>> GetCountOfUniqueProductsAndStockInCategory(int categoryId)
        {
            string sql = @"Select
                Count(prs.Id) As DifferentProducts,  
                Sum(prs.Stock) As TotalStock            
            From ComponentCategories cat
            Join
	            CompuerProducts prs ON prs.CategoryId = cat.Id
            Where cat.Id = @CategoryId
            Group By cat.Name
            Order By DifferentProducts Desc, TotalStock Desc;
            ";

          return await QueryAsync<(int? DifferentProducts, int? TotalStock)>(sql, new { CategoryId = categoryId });
        }
        public async Task<int?> GetSoldInCategory(int categoryId)
        {
            string sql = @"Select 
            Count(crps.Id) as TotalSoldOfCategory
            From Orders o
            Join
                OrderedProducts orps On orps.OrderId = o.Id
            Join
                CompuerProducts crps On orps.ComputerPartId = crps.Id
            Join
                ComponentCategories cats On crps.CategoryId = cats.Id
            Where cats.Id = @CatId
        ";

           return await QueryFirstOrDefaultAsync<int> (sql, new { CatId = categoryId });
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
            From CompuerProducts cs
            Join
                OrderedProducts oi ON oi.ComputerPartId = cs.Id
            Join 
                BrandManufacturers brand On brand.Id = cs.BrandId
            Group By brand.Id, brand.Name
            Order By TotalRevenue DESC;";

            return await QueryFirstOrDefaultAsync<(int BrandId, string BrandName, decimal TotalRevenue)>(sql);
        }
        public async Task<IEnumerable<(string CityName, string PartCategory, int TotalProducts)>> GetMostCommanCategoryPerCity()
        {
            //cross apply
            //https://www.geeksforgeeks.org/plsql/cross-apply-vs-inner-join-in-plsql/
            using var connection = GetConnection();
            await connection.OpenAsync();
            string sql = @"Select
                ci.Name  as City,
                topCat.CategoryName,
                topCat.TotalQuantity
            From Cities ci

            Cross Apply
            (
                Select Top 1
                    cat.Name as CategoryName,
                    Sum(op.Quantity) as TotalQuantity
                From Orders o
                Join CustomerShippingInfos si ON si.Id = o.ShippingInfoId
                Join OrderedProducts op ON op.OrderId = o.Id
                Join CompuerProducts cp ON cp.Id = op.ComputerPartId
                Join ComponentCategories cat ON cat.Id = cp.CategoryId
                Where si.CityId = ci.Id
                Group By cat.Name
                Order By Sum(op.Quantity) DESC
            ) topCat
            Order By ci.Name;";

            return await QueryAsync<(string CityName, string PartCategory, int TotalProducts)>(sql);
        }
        public async Task<IEnumerable<(string Country, string City, decimal TotalValue)>> GetTotalCountrySpending()
        {
            using var connection = GetConnection();
            await connection.OpenAsync();
            string sql = @"Select
                co.Name as Country,
                ci.Name as City,
                Sum(o.TotalCost) As TotalSpentPerCity
            From Orders o
            Join CustomerShippingInfos si ON si.Id = o.ShippingInfoId
            Join Cities ci ON ci.Id = si.CityId
            Join Countries co ON co.Id = ci.CountryId
            Group By co.Name, ci.Name
            Order By co.Name DESC;
            ;";

            //All orders all countries
             return await QueryAsync<(string Country, string City, decimal TotalValue)>(sql);
        }

        public async Task ClearSelected()
        {
            using var connection = GetConnection();
            await connection.OpenAsync();
            string sql = @"Update CompuerProducts
            Set SelectedProduct=0
            Where SelectedProduct = 1";

            await ExecuteAsync(sql);

        }

        Task<IEnumerable<(string CountryName, decimal TotalSpent)?>> IDapperService.GetCountryWithTheMostSpending()
        {
            throw new NotImplementedException();
        }
    }
}