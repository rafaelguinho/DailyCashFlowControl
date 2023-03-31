namespace DailyCashFlowControl.Domain.Models
{
    public class Transaction
    {
        public Transaction(string type, decimal value, string description)
        {
            Type = type;
            Value = value;
            Description = description;
        }

        public Transaction(string id, string type, decimal value, string description, DateTime date)
        {
            Id = id;
            Type = type;
            Value = value;
            Description = description;
            Date = date;
        }

        public string Id { get; private set; } = Guid.NewGuid().ToString();    
        public string Type { get; set; }
        public decimal Value { get; set; }

        public string Description { get; set; }
        public DateTime Date { get; private set; } = DateTime.UtcNow;
    }
}
