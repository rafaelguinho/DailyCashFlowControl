

namespace DailyCashFlowControl.Domain.Models
{
    public class ConsolidatedItemResult
    {
        public ConsolidatedItemResult(DateTime date, string transactionId, decimal value, decimal totalByDate, int order)
        {
            Date = date;
            TransactionId = transactionId;
            Value = value;
            TotalByDate = totalByDate;
            Order = order;
        }

        public string Id { get; private set; } = Guid.NewGuid().ToString();

        public int Order { get; set; }

        public DateTime Date { get; set; }

        public string TransactionId { get; set; }

        public decimal Value { get; set; }

        public decimal TotalByDate { get; set; }
    }
}
