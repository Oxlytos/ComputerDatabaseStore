using ComputerStoreApplication.Helpers.DTO;
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
    public class DapperHelper
    {
        readonly string _connstring;
        public DapperHelper(ComputerDBContext dBContext)
        {
            _connstring = dBContext.Database.GetConnectionString();
        }
        SqlConnection GetConnection()=>new SqlConnection(_connstring);

        public async Task<List<CategoryOrderCount>> GetMostPopularCategories()
        {
            using var connection = GetConnection();
            connection.OpenAsync().Wait();

            string sql = @"--Räkna unika orders per category
            SELECT 
                Category,
                COUNT(DISTINCT o.Id) AS OrdersCount
            FROM (
                SELECT ap.Id,
                    CASE --Switch case fast sql
			            --Category per table
                        WHEN ap.Id IN (SELECT Id FROM CPUs) THEN 'CPU' 
                        WHEN ap.Id IN (SELECT Id FROM GPUs) THEN 'GPU'
                        WHEN ap.Id IN (SELECT Id FROM RAMs) THEN 'RAM'
                        WHEN ap.Id IN (SELECT Id FROM PSUs) THEN 'PSU'
                        WHEN ap.Id IN (SELECT Id FROM Motherboards) THEN 'Motherboard'
                        ELSE 'Other'
                    END AS Category
                FROM AllParts ap
            ) AS categorizedParts
            JOIN StoreProducts sp ON sp.ComputerPartId = categorizedParts.Id
            JOIN OrderedProducts oi ON oi.ProductId = sp.Id
            JOIN Orders o ON o.Id = oi.OrderId
            GROUP BY Category
            ORDER BY OrdersCount DESC;";
            
            var result = await connection.QueryAsync<CategoryOrderCount>(sql);
            return result.ToList();
        }
        public async Task<List<MostPopularProductByCountry>> GetMostPopularProductByCountry()
        {
            using var connection = GetConnection();
            connection.OpenAsync().Wait();

            var sql = @"SELECT
                co.Name AS CountryName,
                ap.Name AS ProductName,
                COUNT(DISTINCT o.Id) AS OrdersCount
            FROM Orders o
            JOIN CustomerShippingInfos csi ON o.ShippingInfoId = csi.Id
            JOIN Cities ci ON csi.CityId = ci.Id
            JOIN Countries co ON ci.CountryId = co.Id
            JOIN OrderedProducts oi ON o.Id = oi.OrderId
            JOIN StoreProducts sp ON oi.ProductId = sp.Id
            JOIN AllParts ap ON sp.ComputerPartId = ap.Id
            GROUP BY co.Name, ap.Name
            ORDER BY co.Name, OrdersCount DESC;";

            var result = await connection.QueryAsync<MostPopularProductByCountry>(sql);
            return result.ToList();
        }
        public async Task<List<CountrySpending>> GetCountryWithTheMostSpending()
        {
            using var connection = GetConnection();
            connection.OpenAsync().Wait();

            var sql = @"Select
            co.Name as CountryName,
            Sum(o.TotalCost) as TotalSpent
        From Orders o
        JOIN
            CustomerShippingInfos csi On o.ShippingAdressId=csi.Id
        Join
            Cities ci on  csi.CityId=ci.Id
        Join
            Countries co On ci.CountryId = co.Id
        Group By co.Name
        Order By TotalSpent DESC;";

            var result = await connection.QueryAsync<CountrySpending>(sql);
            return result.ToList();
        }
        public async Task<List<MostExpensiveOrders>> GetMostExpensiveOrders()
        {
            using var connection = GetConnection();
            connection.OpenAsync().Wait();

            var sql = @"
                WITH TopOrders AS (
                    SELECT TOP 3 *
                    FROM Orders
                    ORDER BY TotalCost DESC
                )
                SELECT 
                    o.Id AS OrderId,
                    o.CustomerId,
                    c.FirstName + ' ' + c.SurName AS CustomerName,
                    o.TotalCost,
                    o.CreationDate,
                    oi.Id AS OrderItemId,
                    sp.Id AS StoreProductId,
                    ap.Name AS ProductName,
                    oi.Quantity AS HowMuch
                FROM TopOrders o
                    JOIN
                        Customers c ON o.CustomerId = c.Id
                     JOIN
                         OrderedProducts oi ON o.Id = oi.OrderId
                JOIN 
                        StoreProducts sp ON oi.ProductId = sp.Id
                JOIN 
                        AllParts ap ON sp.ComputerPartId = ap.Id
                ORDER BY o.TotalCost DESC, oi.Id;
            ";

            var result = await connection.QueryAsync<MostExpensiveOrders>(sql);
            return result.ToList();
        }
        public async Task<List<ComponentOrderCount>> GetMostPopularOrders()
        {
            using var connection = GetConnection();
            connection.OpenAsync().Wait();

            string sql = @"SELECT 
	            cp.Id AS ComponentId, 
	            cp.Name AS ComponentName, 
	            COUNT(DISTINCT o.Id) AS OrdersCount
            FROM Orders o
	            JOIN 
		            OrderedProducts oi ON o.Id = oi.OrderId
	            JOIN 
		            StoreProducts sp ON oi.ProductId = sp.Id
	            JOIN AllParts cp ON sp.ComputerPartId = cp.Id
            GROUP BY 
            cp.Id, cp.Name
            ORDER BY 
            OrdersCount DESC;";

            var result = await connection.QueryAsync<ComponentOrderCount>(sql);
            return result.ToList();
        }
    }
}