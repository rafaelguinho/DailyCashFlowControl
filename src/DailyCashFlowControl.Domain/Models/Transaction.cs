using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyCashFlowControl.Domain.Models
{
    public class Transaction
    {
        public Transaction(string type, decimal value)
        {
            Type = type;
            Value = value;
        }

        public string Id { get; private set; } = Guid.NewGuid().ToString();    
        public string Type { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; private set; } = DateTime.UtcNow;
    }
}
