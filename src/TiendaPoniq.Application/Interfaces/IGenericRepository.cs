using System.Linq.Expressions;

namespace TiendaPoniq.Application.Interfaces
{
    public interface IGenericRepository<T, in TKey> where T : class
    {
        Task<T?> GetByIdAsync(TKey id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}