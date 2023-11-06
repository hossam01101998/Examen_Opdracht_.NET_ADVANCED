using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examen_Opdracht_.NET_ADVANCED.Model;

namespace Examen_Opdracht_.NET_ADVANCED.Model
{
    internal class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public List<Car> Cars { get; set; }
        
        //[ForeignKey("Car")]
        public int CarID { get; set; }

        public Customer (){
        }
        public Customer(string name, string email, string adress, List<Car> Cars)
        {
            CustomerId = GenerateUniqueId();
            Name = name;
            Email = email;
            Adress = adress;
            Cars = new List<Car>();
        }

       
        private static int nextCustomerId = 1;

        private int GenerateUniqueId()
        {
            return nextCustomerId++;
        }
    }
}
