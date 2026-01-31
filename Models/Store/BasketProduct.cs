using ComputerStoreApplication.Account;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Store
{
    public class BasketProduct
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public  CustomerAccount Customer { get; set; }
        public int ComputerPartId { get; set; }
        public ComputerPart ComputerPart{ get; set; }
        public int Quantity { get; set; }
    }
}
