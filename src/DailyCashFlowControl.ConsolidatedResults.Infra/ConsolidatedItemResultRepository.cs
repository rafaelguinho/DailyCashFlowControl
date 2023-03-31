using DailyCashFlowControl.Domain.Interfaces;
using DailyCashFlowControl.Domain.Models;

namespace DailyCashFlowControl.ConsolidatedResults.Infra
{
    public class ConsolidatedItemResultRepository : IRepository<ConsolidatedItemResult>
    {
        private static IList<ConsolidatedItemResult> transactions = new List<ConsolidatedItemResult>();

        public async Task<ConsolidatedItemResult> Add(ConsolidatedItemResult item)
        {
            await Task.Run(() => transactions.Add(item));
            return item;
        }

        public async Task<IEnumerable<ConsolidatedItemResult>> GetFiltered(Func<ConsolidatedItemResult, bool> filter)
        {
            return await Task.Run(() => transactions.Where(filter));
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Edit(ConsolidatedItemResult item)
        {
            throw new NotImplementedException();
        }

        Task<ConsolidatedItemResult> IRepository<ConsolidatedItemResult>.Get(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<ConsolidatedItemResult>> IRepository<ConsolidatedItemResult>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}