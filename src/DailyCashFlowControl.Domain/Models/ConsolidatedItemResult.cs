namespace DailyCashFlowControl.Domain.Models
{

    public class ConsolidatedItemResult
    {
        public ConsolidatedItemResult(DateTime date, string dateKey, string transactionId, decimal value, decimal totalByDate, int order)
        {
            Date = date;
            DateKey = dateKey;
            TransactionId = transactionId;
            Value = value;
            TotalByDate = totalByDate;
            Order = order;
        }

        public ConsolidatedItemResult()
        {
                
        }

        public string Id { get; set; }

        public int Order { get; set; }

        public DateTime Date { get; set; }

        public string DateKey { get; set; }

        public string TransactionId { get; set; }

        public decimal Value { get; set; }

        public decimal TotalByDate { get; set; }
    }
}
