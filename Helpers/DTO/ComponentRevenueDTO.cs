using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Helpers.DTO
{
    public class ComponentRevenueDTO
    {
        public string ProductName { get; set; }
        public decimal TotalRevenue { get; set; }
        public int OrdersCount { get; set; }
    }
}
