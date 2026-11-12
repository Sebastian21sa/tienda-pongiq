using System.Linq.Expressions;

namespace TiendaPoniq.Application.Interfaces
{
    public interface IGenericRepository<T, in TKey> where T : class
    {
        Task<T?> GetByIdAsync(TKey id);
        Task<T?> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>>? include = null);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<(IReadOnlyList<T> Items, int TotalCount)> GetPagedAsync(
            Expression<Func<T, bool>>? predicate,
            int pageNumber,
            int pageSize,
            Func<IQueryable<T>, IQueryable<T>>? include = null);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}