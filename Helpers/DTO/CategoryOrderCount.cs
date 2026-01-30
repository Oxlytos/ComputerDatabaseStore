using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Helpers.DTO
{
    public class CategoryOrderCount
    {
            public string Category { get; set; } = string.Empty;
            public int OrdersCount { get; set; }
    }
}
