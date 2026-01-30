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
        public DbSet<ComputerPart> AllParts { get; set; }
        public DbSet<CPU> CPUs { get; set; }
        public DbSet<GPU> GPUs { get; set; }
        public DbSet<Motherboard> Motherboards { get; set; }
        public DbSet<PSU> PSUs { get; set; }
        public DbSet<RAM> RAMs { get; set; }
        //Customers and their addresses
        public DbSet<CustomerAccount> Customers { get; set; }
        public DbSet<AdminAccount> Admins { get; set; }
        public DbSet<CustomerShippingInfo> CustomerShippingInfos { get; set; }
        //Store and orders related
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderedProducts { get; set; }
        public DbSet<StoreProduct> StoreProducts { get; set; }
        //Producers
        public DbSet<Brand> BrandManufacturers { get; set; }
        public DbSet<ChipsetVendor> ChipsetVendors { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }
        public DbSet<DeliveryProvider> DeliveryProviders { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().AddUserSecrets<ComputerDBContext>().Build();
            var loginPassword = config["Password"];
            //optionsBuilder.UseSqlServer($@"Server=tcp:oscardbassigment.database.windows.net,1433;Initial Catalog=ComputerShopDbOscar;Persist Security Info=False;User ID=dbadmin;Password={loginPassword};MultipleActiveResultSets=False; Encrypt=True;TrustServerCertificate=False; Connection Timeout=30;");
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS; Database=ComputerComponentWebshop; Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CPU>().ToTable("CPUs");
            modelBuilder.Entity<GPU>().ToTable("GPUs");
            modelBuilder.Entity<PSU>().ToTable("PSUs");
            modelBuilder.Entity<Motherboard>().ToTable("Motherboards");
            modelBuilder.Entity<RAM>().ToTable("RAMs");

            modelBuilder.Entity<CPUArchitecture>().ToTable("CPUArchitectures");



            modelBuilder.Entity<ComputerPart>(). //A part
                HasOne(c => c.BrandManufacturer). //has a manufacturer i e a ram stick from gskill
                WithMany(m => m.Products). //A manufacturer has many products
                HasForeignKey(q => q.BrandId).//We link the manufacturer with the id as FK
                OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ComputerPart>().
                HasOne(v => v.ChipsetVendor).
                WithMany(m => m.Parts).
                HasForeignKey(q => q.ChipsetVendorId).
                 OnDelete(DeleteBehavior.SetNull);

            //BAsE CPU relations
            modelBuilder.Entity<CPU>(). //A CPU
                HasOne(c => c.SocketType). //Has a socket type
                WithMany(s => s.CPUs). //Loads (Many) of CPUs in that socket can fit here (to one) ^^
                HasForeignKey(s => s.SocketId). //We handle that connection with a FK
                OnDelete(DeleteBehavior.Restrict); //Don't delete a socket if there's a cpu that's using it

            modelBuilder.Entity<CPU>(). //A CPU
                HasOne(c => c.CPUArchitecture). //Is based on a CPU architcture, a must have
                WithMany(s => s.CPUs). //Is used by loads of CPUs
                HasForeignKey(s => s.CPUArchitectureId) //FK connection to all architecture types
                .OnDelete(DeleteBehavior.Restrict); //dont

            //GPU 
            modelBuilder.Entity<GPU>().
                HasOne(m => m.MemoryType). //This memory type
                WithMany(s => s.GPUs). //Is used by loads of GPUs
                HasForeignKey(g => g.MemoryTypeId). //FK
                OnDelete(DeleteBehavior.Restrict);

            //Motherboard
            modelBuilder.Entity<Motherboard>().
                HasOne(c => c.CPUSocket).
                WithMany(c => c.Motherboards).
                HasForeignKey(c => c.CPUSocketId).
                OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Motherboard>().
                HasOne(m => m.MemoryType).
                WithMany(m => m.Motherboards).
                HasForeignKey(m => m.MemoryTypeId).
                OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Motherboard>().
                HasOne(m => m.CPUSocketArchitecture).
                WithMany(m => m.Motherboards).
                HasForeignKey(m => m.CPUArchitectureId).
                OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PSU>().
                HasOne(p => p.EnergyClass).
                WithMany(p => p.PSUs).
                HasForeignKey(p => p.EnergyClassId).
                OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RAM>().
                HasOne(r => r.MemoryType).
                WithMany(r => r.RAMs).
                HasForeignKey(r => r.MemoryTypeId).
                OnDelete(DeleteBehavior.Restrict);

            //RAM is a special case for many to many
            modelBuilder.Entity<RAM>().
                HasMany(r => r.SupportedRamProfiles). //Should be handled like a many to many relationships here
                WithMany(r => r.RAMs);                      //With a internal join table


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

            //En store product
            modelBuilder.Entity<StoreProduct>().
                HasOne(SP => SP.ComputerPart).
                WithMany(C => C.Products).
                HasForeignKey(s => s.ComputerPartId).
                OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StoreProduct>(). //Mostly for filtering products
                HasOne(s => s.Manufacturer). //A manufacturer
                WithMany(q => q.StoreProducts). //Has many products in the store
                HasForeignKey(s => s.ManufacturerId). //FK to manufacturer info
                OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<BasketProduct>().
                HasOne(s => s.Customer).
                WithMany(c => c.ProductsInBasket).
                HasForeignKey(k => k.CustomerId).
                OnDelete(DeleteBehavior.Cascade);

            //Unik basketproduct table per användare och product
            modelBuilder.Entity<BasketProduct>().
                HasIndex(bps => new { bps.CustomerId, bps.ProductId }).IsUnique()

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
