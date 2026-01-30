using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Helpers.DTO
{
    public class ComponentOrderCount
    {
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public int OrdersCount { get; set; }
    }
}
