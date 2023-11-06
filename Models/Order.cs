using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examen_Opdracht_.NET_ADVANCED.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Examen_Opdracht_.NET_ADVANCED.Model
{
    internal class Order
    {
        public int OrderId { get; set; }
        public string OrderDetails { get; set; } 

        

        //[ForeignKey("Car")] 
        public int CarID { get; set; }
        

        public Car Car { get; set; }
        public Order()
        {
        }

        public Order(Car car, string orderdetails)
        {
            Car = car;
            OrderId = GenerateUniqueId();
            
            OrderDetails = orderdetails;
        }
        

        private static int nextOrderId = 1;

        
        

        private int GenerateUniqueId()
        {
            return nextOrderId++;
        }
    }
}
