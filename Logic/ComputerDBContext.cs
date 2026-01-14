using ComputerStoreApplication.Models.ComponentSpecifications;
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

namespace ComputerStoreApplication.Logic
{
    public class ComputerDBContext :DbContext
    {
        //https://www.entityframeworktutorial.net/code-first/simple-code-first-example.aspx
        //DB Sets for things to query later for convinience
        //Component specifcations
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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerShippingInfo> CustomerShippingInfos { get; set; }
        //Store and orders related
        public DbSet<CustomerOrder> CustomerOrders { get; set; }    
        public DbSet<OrderProduct> OrderedProducts { get; set; }
        public DbSet<StoreProduct> StoreProducts { get; set; }
        //Producers
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
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
            ////
            /// All abstract parts here
            modelBuilder.Entity<ComputerPart>(). //A part
                HasOne(c=>c.Manufacturer). //has a manufacturer i e a ram stick from gskill
                WithMany(m=>m.Products). //A manufacturer has many products
                HasForeignKey(q=>q.ManufacturerId).//We link the manufacturer with the id as FK
                OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ComputerPart>(). 
                HasOne(v=>v.Vendor).
                WithMany(m=>m.Parts).
                HasForeignKey(q=>q.VendorId).
                 OnDelete(DeleteBehavior.SetNull);

            //BAsE CPU relations
            modelBuilder.Entity<CPU>(). //A CPU
                HasOne(c=>c.SocketType). //Has a socket type
                WithMany(s=>s.CPUs). //Loads (Many) of CPUs in that socket can fit here (to one) ^^
                HasForeignKey(s=>s.SocketId). //We handle that connection with a FK
                OnDelete(DeleteBehavior.Restrict); //Don't delete a socket if there's a cpu that's using it

            modelBuilder.Entity<CPU>(). //A CPU
                HasOne(c=>c.CPUArchitecture). //Is based on a CPU architcture, a must have
                WithMany(s=>s.CPUs). //Is used by loads of CPUs
                HasForeignKey(s=>s.CPUArchitectureId) //FK connection to all architecture types
                . OnDelete(DeleteBehavior.Restrict); //dont

            //GPU 
            modelBuilder.Entity<GPU>().
                HasOne(m=>m.MemoryType). //This memory type
                WithMany(s=>s.GPUs). //Is used by loads of GPUs
                HasForeignKey(g=>g.MemoryTypeId). //FK
                OnDelete(DeleteBehavior.Restrict);

            //Motherboard
            modelBuilder.Entity<Motherboard>().
                HasOne(c=>c.CPUSocket).
                WithMany(c=>c.Motherboards).
                HasForeignKey(c=>c.CPUSocketId).
                OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Motherboard>().
                HasOne(m=>m.MemoryType).
                WithMany(m=>m.Motherboards).
                HasForeignKey(m=>m.MemoryTypeId).
                OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Motherboard>().
                HasOne(m=>m.CPUSocketArchitecture). 
                WithMany(m=>m.Motherboards).
                HasForeignKey(m=>m.CPUArchitectureId).
                OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PSU>().
                HasOne(p=>p.EnergyClass).
                WithMany(p=>p.PSUs).
                HasForeignKey(p=>p.EnergyClassId).
                OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RAM>().
                HasOne(r=>r.MemoryType).
                WithMany(r=>r.RAMs).
                HasForeignKey(r=>r.MemoryTypeId).
                OnDelete(DeleteBehavior.Restrict);

            //RAM is a special case for many to many
            modelBuilder.Entity<RAM>().
                HasMany(r => r.SupportedRamProfiles). //Should be handled like a many to many relationships here
                WithMany(r => r.RAMs);                      //With a internal join table

            modelBuilder.Entity<Customer>().
                HasMany(C=>C.CustomerShippingInfo). //Many addresses
                WithOne(C=>C.Customer). //To one customer
                HasForeignKey(C=>C.CustomerId). //With one ID FK
                OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>().
                HasMany(C=>C.Orders).
                WithOne(C=>C.Customer).
                HasForeignKey(Q=>Q.CustomerId).
                OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StoreProduct>().
                HasOne(SP=>SP.PartType).
                WithMany(SP=>SP.Products).
                HasForeignKey(s=>s.PartTypeId).
                OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StoreProduct>(). //Mostly for filtering products
                HasOne(s=>s.Manufacturer). //A manufacturer
                WithMany(q=>q.StoreProducts). //Has many products in the store
                HasForeignKey(s=>s.ManufacturerId). //FK to manufacturer info
                OnDelete(DeleteBehavior.SetNull);
        }


    }
}
