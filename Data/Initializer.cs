using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Examen_Opdracht_.NET_ADVANCED.Model;
using Examen_Opdracht_.NET_ADVANCED.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Examen_Opdracht_.NET_ADVANCED.Data
{
    internal static class Initializer
    {

        internal static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convertir los bytes a una representación de cadena hexadecimal
                var hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                return hashedPassword;
            }
        }

        internal static void DbSetInitializer(MyDBContext context)
        {
            Console.WriteLine("iniciando");

            // Seeders

            if (!context.Customers.Any())
            {
                context.Add(new Customer { Name = "John", Email = "john@gmail.com", Adress = "Ijsstraat 23 A" });
                context.Add(new Customer { Name = "Richy", Email = "richy@gmail.com", Adress = "Palmstraat 3" });
                context.SaveChanges();
            }

            if (!context.Cars.Any())
            {
                context.Add(new Car { Make = "BMW", Model = "M3", LicensePlate = "1-ABC-123", ChassisNumber = "123456789", CustomerId = 1 });
                context.Add(new Car { Make = "Audi", Model = "A3", LicensePlate = "1-DEF-456", ChassisNumber = "987654321", CustomerId = 1 });
                context.Add(new Car { Make = "Mercedes", Model = "C180", LicensePlate = "1-GHI-789", ChassisNumber = "123456789", CustomerId = 2 });
                context.Add(new Car { Make = "Volkswagen", Model = "Golf", LicensePlate = "1-JKL-012", ChassisNumber = "987654321", CustomerId = 2 });
                context.SaveChanges();
            }

            if (!context.Orders.Any())
            {
                context.Add(new Order { CarID = 1, OrderDetails = "Change tyres" });
                context.Add(new Order { CarID = 1, OrderDetails = "Change timing belt" });
                context.Add(new Order { CarID = 2, OrderDetails = "Noise in the transmission" });
                context.Add(new Order { CarID = 2, OrderDetails = "Change oil and filters" });
                context.Add(new Order { CarID = 3, OrderDetails = "Change diesel filter" });
                context.Add(new Order { CarID = 3, OrderDetails = "Change brake pads" });
                context.Add(new Order { CarID = 4, OrderDetails = "Change oil and filters" });
                context.Add(new Order { CarID = 4, OrderDetails = "New wheels" });
                context.SaveChanges();
            }

            if (!context.Appointments.Any())
            {
                context.Add(new Appointment { CarID = 1, AppointmentDate = DateTime.Now.AddDays(1), Status="Confirmed", RequiredService = "Regular checkup" });
                context.Add(new Appointment { CarID = 2, AppointmentDate = DateTime.Now.AddDays(2), Status = "Pending", RequiredService = "Oil change" });
                context.SaveChanges();
            }

            if (!context.Invoices.Any())
            {
                context.Add(new Invoice { CustomerID = 1, IssueDate = DateTime.Now, TotalAmount = 100.50m, Details = "Service invoice", CarID = 2 });
                context.Add(new Invoice { CustomerID = 2, IssueDate = DateTime.Now, TotalAmount = 75.25m, Details = "Parts replacement", CarID = 3 });
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Add(new User { Username = "admin", PasswordHash = HashPassword("admin123"), Email = "admin@gmail.com", IsAdmin = true });
                context.Add(new User { Username = "user1", PasswordHash = HashPassword("user123"), Email = "user1@gmail.com", IsAdmin = false });
                context.Add(new User { Username = "user2", PasswordHash = HashPassword("user456"), Email = "user2@gmail.com", IsAdmin = false });
                context.SaveChanges();
            }

            


        }
    }
}
    
