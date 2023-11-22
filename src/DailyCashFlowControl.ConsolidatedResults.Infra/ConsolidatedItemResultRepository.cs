using DailyCashFlowControl.Domain.Interfaces;
using DailyCashFlowControl.Domain.Models;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace DailyCashFlowControl.ConsolidatedResults.Infra
{
    

    public class ConsolidatedItemResultRepository : IRepository<ConsolidatedItemResult>
    {
        private class ParameterReplacer : System.Linq.Expressions.ExpressionVisitor
        {
            private readonly ParameterExpression _parameter;

            public ParameterReplacer(ParameterExpression parameter)
            {
                _parameter = parameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                // Replace the original parameter with the new parameter
                return _parameter;
            }
        }

        static Expression<Func<ConsolidatedItemResultDbModel, bool>> ConvertFilter(Expression<Func<ConsolidatedItemResult, bool>> originalFilter)
        {
            // Create a parameter expression for ConsolidatedItemResultDbModel
            ParameterExpression parameter = Expression.Parameter(typeof(ConsolidatedItemResultDbModel), "x");

            // Replace references to the original parameter with references to the new parameter
            Expression body = new ParameterReplacer(parameter).Visit(originalFilter.Body);

            // Create a new lambda expression with the updated body and the new parameter
            return Expression.Lambda<Func<ConsolidatedItemResultDbModel, bool>>(body, parameter);
        }

        private readonly IConsolidatedItemResultContext _context;

        public ConsolidatedItemResultRepository(IConsolidatedItemResultContext context)
        {
            _context = context;
        }

        public async Task<ConsolidatedItemResult> Add(ConsolidatedItemResult item)
        {
            ConsolidatedItemResultDbModel dbModel = new ConsolidatedItemResultDbModel(item);
            await _context.ConsolidatedItems.InsertOneAsync(dbModel);

            return item;
        }

        public async Task<IEnumerable<ConsolidatedItemResult>> GetFiltered(Expression<Func<ConsolidatedItemResult, bool>> filter)
        {
            Expression<Func<ConsolidatedItemResultDbModel, bool>> convertedFilter = ConvertFilter(filter);

            IEnumerable<ConsolidatedItemResultDbModel> items = _context.ConsolidatedItems.Find(convertedFilter).ToList();

            return items.Select(i => new ConsolidatedItemResult(i.Date, i.DateKey, i.TransactionId, i.Value, i.TotalByDate, i.Order));
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