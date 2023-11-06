using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examen_Opdracht_.NET_ADVANCED.Model;
using Microsoft.EntityFrameworkCore;

namespace Examen_Opdracht_.NET_ADVANCED.Data
{
    internal static class Initializer
    {
    
            internal static void DbSetInitializer(MyDBContext context)
            {


            if (!context.Customers.Any())
                {
                    context.Add(new Customer { Name = "Richy", Email = "richy@gmail.com", Adress = "Palmstraat 3" });
                    context.SaveChanges();
                }
            }
        }
}
