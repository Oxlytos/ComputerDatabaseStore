using ComputerStoreApplication.Models.ComputerComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Logic
{
    public interface IDapperService
    {
         Task<IEnumerable<(string CountryName, decimal TotalSpent)?>> GetCountryWithTheMostSpending();
        Task<decimal> GetTotalRevenue();
        Task<(int CustomerId, string FirstName, string SurName, decimal TotalSpent)?> GetHighestSpender();
        Task<(int CustomerId, string FirstName, string SurName, decimal TotalSpent)?> GetLowestSpender();
        Task<(int InStock, decimal StockValue, int UnitsSold, decimal TotalRevenue)> TotalStockValueOnSpecificProduct(ComputerPart part);
        Task<int> CountOrdersForProduct(int computerPartId);
        Task<(int CountProductsTotal, decimal AvgValue, decimal TotalCost, decimal MostExpensiveObject, string MostExpensiveProductOrdered)?> GetOrdersByCustomerAndAvgCost(int inputCustomerId);

        Task<(decimal TotalValue, string Email)?> GetMostExpensiveOrderLast24Hours();


        Task<(string BrandName, decimal TotalSpent)> MostSpentOnBrand(int customerId);


        Task<decimal?> TotalRevenueBasedOnBrand(int brandId);
        Task<IEnumerable<(int DeliveryProviderId, string DeliveryProviderName, int NumberOfOrders, decimal TotalValue)>> GetOrdersPerDeliveryService();
        Task<IEnumerable<(string? PayName, int? Count)>> GetMostCommonPayMethod();

        Task<int?> GetHowManyUseThisPayMethod(int payId);

        Task<int?> GetHowManyUseThisDeliveryService(int deliveryId);
        Task<IEnumerable<(int? UniqueCountProducts, int? TotalStock)>> GetUniqueCountAndTotalStockCountOfBrandsProducts(int brandId);
        Task<IEnumerable<(int? DifferentProducts, int? TotalStock)>> GetCountOfUniqueProductsAndStockInCategory(int categoryId);
        Task<int?> GetSoldInCategory(int categoryId);

        Task<(int BrandId, string BrandName, decimal TotalRevenue)?> GetMostProfitableBrand();
        Task<IEnumerable<(string CityName, string PartCategory, int TotalProducts)>> GetMostCommanCategoryPerCity();
        Task<IEnumerable<(string Country, string City, decimal TotalValue)>> GetTotalCountrySpending();
        Task ClearSelected();
    }
}
