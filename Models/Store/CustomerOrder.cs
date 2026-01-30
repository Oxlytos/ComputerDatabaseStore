using ComputerStoreApplication.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Store
{
    public class CustomerOrder
    {
        [Key]
        public int Id { get; set; }

        public int? CustomerId { get; set; } 

        public virtual CustomerAccount? Customer { get; set; }

        public virtual ICollection<OrderItem> Products { get; set; } = new List<OrderItem>();

        public DateTime CreatedDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        public bool Delivered { get; set; } = false;


    }
}
