using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Helpers.DTO
{
    public class CountrySpending
    {
        public string CountryName { get; set; } = string.Empty;
        public decimal TotalSpent { get; set; }
    }
}
