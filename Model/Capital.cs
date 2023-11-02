using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_Opdracht_.NET_ADVANCED.Model
{
    internal class Capital
    {
        [Key]
        public int TransactionId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Capital()
        {
        }

        public Capital(string description, decimal amount, DateTime date)
        {
            Description = description;
            Amount = amount;
            Date = date;
            TransactionId = GenerateUniqueId();
        }

        private static int nextTransactionId = 1;

        private int GenerateUniqueId()
        {
            return nextTransactionId++;
        }

        public void AddProfit(decimal profit)
        {
            Amount += profit;
        }

        public void DeductLoss(decimal loss)
        {
            Amount -= loss;
        }
    }
}
