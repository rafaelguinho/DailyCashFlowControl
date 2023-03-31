namespace DailyCashFlowControl.Domain.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetFiltered(Func<T, bool> filter);

        Task<T> Get(int id);
        Task<T> Add(T item);
        Task Edit(T item);
        Task Delete(int id);
    }
}
