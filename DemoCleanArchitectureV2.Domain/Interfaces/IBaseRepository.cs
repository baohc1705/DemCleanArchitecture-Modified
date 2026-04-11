using System.Linq.Expressions;

namespace DemoCleanArchitectureV2.Domain.Interfaces
{
    public interface IBaseRepository<T, TKey> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(TKey key);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

        // Test IQueryable
        Task<IQueryable<T>> GetAllQueryAsync();
      

        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
