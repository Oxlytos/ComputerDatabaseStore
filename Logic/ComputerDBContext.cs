using ComputerStoreApplication.Account;
using ComputerStoreApplication.Models.ComponentSpecifications;
using ComputerStoreApplication.Models.ComputerComponents;
using ComputerStoreApplication.Models.Customer;
using ComputerStoreApplication.Models.Store;
using ComputerStoreApplication.Models.Vendors_Producers;
using Microsoft.EntityFrameworkCore;
using ComputerStoreApplication.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ComputerStoreApplication.Logic
{
    public class ComputerDBContext : DbContext
    {
        //https://www.entityframeworktutorial.net/code-first/simple-code-first-example.aspx
        //DB Sets for things to query later for convinience
        //Component specifcations
        public DbSet<ComponentSpecification> AllComponentSpecifcations { get; set; }

        public DbSet<CPUArchitecture> CPUArchitectures { get; set; }
        public DbSet<CPUSocket> CPUSockets { get; set; }
        public DbSet<EnergyClass> EnergyClasses { get; set; }
        public DbSet<MemoryType> MemoryTypes { get; set; }
        public DbSet<RamProfileFeatures> RamProfiles { get; set; }
        //Components
        public DbSet<ComputerPart> CompuerProducts { get; set; }
        public DbSet<ComponentCategory> ComponentCategories { get; set; }

        public DbSet<CustomerAccount> Customers { get; set; }
        public DbSet<AdminAccount> Admins { get; set; }
        public DbSet<CustomerShippingInfo> CustomerShippingInfos { get; set; }
        //Store and orders related
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderedProducts { get; set; }
        //Producers
        public DbSet<Brand> BrandManufacturers { get; set; }
  
        public DbSet<BasketProduct> BasketProducts { get; set; }
        public DbSet<DeliveryProvider> DeliveryProviders { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().AddUserSecrets<ComputerDBContext>().Build();
            var loginPassword = config["Password"];
          //  optionsBuilder.UseSqlServer($@"Server=tcp:oscardbassigment.database.windows.net,1433;Initial Catalog=ComputerShopDbOscar;Persist Security Info=False;User ID=dbadmin;Password={loginPassword};MultipleActiveResultSets=False; Encrypt=True;TrustServerCertificate=False; Connection Timeout=30;");
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS; Database=ComputerShopDb; Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //A part => CPU
            modelBuilder.Entity<ComputerPart>().
                HasOne(k=>k.ComponentCategory). //Is part of CPU category
                WithMany(s=>s.ComputerParts). //Many computer parts are CPU
                HasForeignKey(k=>k.CategoryId). //We check this with FK categoryId
                OnDelete(DeleteBehavior.Restrict); //Can't delete a category if its used by a computer part

            modelBuilder.Entity<ComputerPart>(). //A part
                HasOne(c => c.BrandManufacturer). //has a manufacturer i e a ram stick from gskill
                WithMany(m => m.Products). //A manufacturer has many products
                HasForeignKey(q => q.BrandId).//We link the manufacturer with the id as FK
                OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<CustomerAccount>()
                .HasMany(c => c.CustomerShippingInfos)
                .WithOne(s => s.Customer)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CustomerAccount>().
                HasMany(C => C.Orders).
                WithOne(C => C.OrdCustomer).
                HasForeignKey(Q => Q.CustomerId).
                OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BasketProduct>().
                HasOne(s => s.Customer).
                WithMany(c => c.ProductsInBasket).
                HasForeignKey(k => k.CustomerId).
                OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BasketProduct>().
                HasIndex(bps => new { bps.CustomerId, bps.ComputerPartId}).IsUnique()

                ;
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems).
                WithOne(o => o.Order).
                HasForeignKey(s => s.OrderId).
                OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(d => d.DeliveryProvider).
                WithMany().
                HasForeignKey(k => k.DeliveryProviderId).
                OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>().
                HasOne(p => p.PaymentMethod).WithMany()
                .HasForeignKey(s => s.PaymentMethodId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<City>()
            .HasOne(c => c.Country)
            .WithMany(c => c.Cities)
            .HasForeignKey(c => c.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<Order>()
        .HasOne(o => o.ShippingInfo)
        .WithMany() // Each ShippingInfo belongs to one order (or optional)
        .HasForeignKey(o => o.ShippingInfoId)
        .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<City>()
                .HasIndex(c => new { c.Name, c.CountryId })
                .IsUnique();


        }


    }
}
