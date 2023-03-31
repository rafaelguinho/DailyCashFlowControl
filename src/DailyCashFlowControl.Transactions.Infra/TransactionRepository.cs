using DailyCashFlowControl.Domain.Interfaces;
using DailyCashFlowControl.Domain.Models;

namespace DailyCashFlowControl.Transactions.Infra
{
    public class TransactionRepository : IRepository<Transaction>
    {
        private static Dictionary<string, Transaction> transactions = new Dictionary<string, Transaction>();

        public async Task<Transaction> Add(Transaction item)
        {
            await Task.Run(() => transactions.Add(item.Id, item));
            return item;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Edit(Transaction item)
        {
            throw new NotImplementedException();
        }

        public Task<Transaction> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> GetFiltered(Func<Transaction, bool> filter)
        {
            throw new NotImplementedException();
        }
    }
}