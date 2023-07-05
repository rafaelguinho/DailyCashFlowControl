using DailyCashFlowControl.Domain.Interfaces;
using DailyCashFlowControl.Domain.Models;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;

namespace DailyCashFlowControl.ConsolidatedResults.Infra
{
    public class ConsolidatedItemResultRepository : IRepository<ConsolidatedItemResult>
    {
        private readonly IConsolidatedItemResultContext _context;

        public ConsolidatedItemResultRepository(IConsolidatedItemResultContext context)
        {
            _context = context;
        }

        public async Task<ConsolidatedItemResult> Add(ConsolidatedItemResult item)
        {
            await _context.ConsolidatedItems.InsertOneAsync(item);

            return item;
        }

        public async Task<IEnumerable<ConsolidatedItemResult>> GetFiltered(Expression<Func<ConsolidatedItemResult, bool>> filter)
        {
            return _context.ConsolidatedItems.Find(filter).ToList();
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