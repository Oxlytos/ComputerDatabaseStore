using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Store
{
    public class OrderProduct
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int OrderedAmount { get; set; }

        public int? CustomerId { get; set; }
        public virtual Customer.Customer? Customer { get; set; }
        public int ProductId { get; set; }
        public StoreProduct Product { get; set; }
    }
}
