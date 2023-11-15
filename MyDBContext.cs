using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Examen_Opdracht_.NET_ADVANCED.Data;
using Examen_Opdracht_.NET_ADVANCED.Model;
using Examen_Opdracht_.NET_ADVANCED.Models;
using Microsoft.EntityFrameworkCore;

namespace Examen_Opdracht_.NET_ADVANCED
{
    internal class MyDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Capital> Capital { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Order> Orders { get; set; }


        public void InitiateDatabase()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Cars)
                .WithOne(car => car.Customer)
                .HasForeignKey(car => car.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Car>()
                .HasMany(car => car.Orders)
                .WithOne(order => order.Car)
                .HasForeignKey(order => order.CarID)
                .OnDelete(DeleteBehavior.NoAction);

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

            modelBuilder.Entity<Invoice>()
                .Property(i => i.TotalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .HasOne(order => order.Car)
                .WithMany()
                .HasForeignKey(order => order.CarID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Capital>()
                .Ignore(c => c.TransactionId);

            // capital no tiene clave principal
            modelBuilder.Entity<Capital>().HasNoKey();

            modelBuilder.Entity<Capital>()
            .Property(c => c.Amount)
            .HasColumnType("decimal(18,2)");

            

            base.OnModelCreating(modelBuilder);
        }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Garage;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true");
        }

        public bool RegisterUser(string username, string password)
        {
            // verificar si el usuario ya existe
            if (Users.Any(u => u.Username == username))
            {
                return false;
            }


            var newUser = new User
            {
                Username = username,
                PasswordHash = HashPassword(password) 
            };

            // agregar el nuevo usuario y guardar los cambios en la base de datos
            Users.Add(newUser);
            SaveChanges();

            return true; 
        }

        public bool AuthenticateUser(string username, string password)
        {
            // buscamos al usuario por el nombre de usuario

            var user = Users.FirstOrDefault(u => u.Username == username);

            // verificar si el usuario existe y la contraseña coincide
            
            return user != null && VerifyPassword(password, user.PasswordHash);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            // Verificar si la contraseña coincide con el hash almacenado
            return hashedPassword == HashPassword(password);
        }
    }
}

