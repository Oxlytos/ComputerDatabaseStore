using ComputerStoreApplication.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Logic
{
    public class ApplicationLogic : IDisposable
    {
        public ComputerDBContext ComputerPartShopDB { get; } //Bara get, ska aldrig kunna set:as någon annan stans
        public IPage CurrentPage { get; set; }

        public ApplicationLogic()
        {
            ComputerPartShopDB = new ComputerDBContext();
            CurrentPage = new CustomerPage();
        }

        public void Dispose() { }   
    }
}
