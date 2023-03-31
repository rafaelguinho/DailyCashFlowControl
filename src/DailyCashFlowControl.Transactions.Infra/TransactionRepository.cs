using DailyCashFlowControl.Domain.Interfaces;
using DailyCashFlowControl.Domain.Models;

namespace DailyCashFlowControl.Transactions.Infra
{
    public class TransactionRepository : IRepository<Transaction>
    {
        private static IList<Transaction> transactions = new List<Transaction>();

        public async Task<Transaction> Add(Transaction item)
        {
            await Task.Run(() => transactions.Add(item));
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

        public async Task<IEnumerable<Transaction>> GetAll()
        {
            return await Task.Run(() => transactions.AsEnumerable());
        }

        public Task<IEnumerable<Transaction>> GetFiltered(Func<Transaction, bool> filter)
        {
            throw new NotImplementedException();
        }
    }
}