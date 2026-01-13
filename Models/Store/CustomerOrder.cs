using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Store
{
    public class CustomerOrder
    {
        public int Id { get; set; }

        public int? CustomerId { get; set; } 

        public virtual Customer.Customer? Customer { get; set; }

        public virtual List<OrderProduct> Products { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        public bool Delivered { get; set; } = false;


    }
}
