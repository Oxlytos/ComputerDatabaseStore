using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerStoreApplication.Account;
using ComputerStoreApplication.Models.Store;

namespace ComputerStoreApplication.Models.Store
{
    public class BasketProduct
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public  CustomerAccount Customer { get; set; }
        public int ProductId { get; set; }
        public StoreProduct Product { get; set; }
        public int Quantity { get; set; }
    }
}
