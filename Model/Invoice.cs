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
    internal class Invoice //factuur
    {
        public int InvoiceId { get; set; }
        //[ForeignKey("Car")]
        public int CarID { get; set; }

        //[ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public DateTime IssueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Details { get; set; }

        public Customer Customer { get; set; }
        public Car Car { get; set; }

        public Invoice()
        {

        }

        public Invoice(DateTime issueDate, decimal totalAmount, string details, Car car, Customer customer)
        {
            
            IssueDate = issueDate;
            TotalAmount = totalAmount;
            Details = details;
            Car = car;
            Customer = customer;
            InvoiceId = GenerateUniqueId();
        }

        /*public void AssignCustomer(Customer customer)
        {
            Customer = customer;
        }
        public void AssignCar(Car car)
        {
            Car = car;
        }*/



        private static int nextInvoiceId = 1;

        private int GenerateUniqueId()
        {
            return nextInvoiceId++;
        }
    }
}
