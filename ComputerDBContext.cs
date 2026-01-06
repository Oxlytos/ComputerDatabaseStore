using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Customer;
using ComputerStoreApplication.Models.Store;
using ComputerStoreApplication.Models.Vendors_Producers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication
{
    public class ComputerDBContext :DbContext
    {
        //https://www.entityframeworktutorial.net/code-first/simple-code-first-example.aspx
        public ComputerDBContext() : base()
        {

        }
        public DbSet<CPU> CPUs { get; set; }
        public DbSet<GPU> GPUs { get; set; }
        public DbSet<Motherboard> Motherboards { get; set; }
        public DbSet<PSU> PSUs { get; set; }    
        public DbSet<RAM> RAMs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }    

        public DbSet<Vendor> Vendors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS; Database=ComputerComponentWebshop; Trusted_Connection=True;TrustServerCertificate=True;");
        }


    }
}
