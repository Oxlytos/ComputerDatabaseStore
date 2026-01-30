using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Helpers.DTO
{
    public class MostExpensiveOrders
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public decimal TotalCost { get; set; }
        public DateTime CreationDate { get; set; }

        public int OrderItemId { get; set; }
        public int StoreProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
