using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Store
{
    internal class OrderProduct
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int OrderedAmount { get; set; }


    }
}
