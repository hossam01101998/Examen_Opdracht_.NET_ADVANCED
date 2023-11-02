using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examen_Opdracht_.NET_ADVANCED.Model;
using Microsoft.EntityFrameworkCore;

namespace Examen_Opdracht_.NET_ADVANCED
{
    internal class MyDBContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Capital> Capital { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Order> Orders { get; set; }


        public void InitiateDatabase()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Garage;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Cars)
                .WithOne(car => car.Customer)
                .HasForeignKey(car => car.CustomerID)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Car>()
                .HasMany(car => car.Orders)
                .WithOne(order => order.Car)
                .HasForeignKey(order => order.CarID)
                .OnDelete(DeleteBehavior.NoAction); ;

            modelBuilder.Entity<Invoice>()
                .HasOne(invoice => invoice.Customer)
                .WithMany()
                .HasForeignKey(invoice => invoice.CustomerID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Invoice>()
                .HasOne(invoice => invoice.Car)
                .WithMany()
                .HasForeignKey(invoice => invoice.CarID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(order => order.Car)
                .WithMany()
                .HasForeignKey(order => order.CarID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Capital>()
                .Ignore(c => c.TransactionId);
            // Capital has no key
            modelBuilder.Entity<Capital>().HasNoKey();


            base.OnModelCreating(modelBuilder);
        }

    }
}
