using ComputerStoreApplication.Models.ComputerComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Models.Store
{
    //Order items i en order
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ComputerPartId { get; set; }
        public ComputerPart ComputerPart { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }

     
    }
}
