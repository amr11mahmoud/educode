using System.Linq.Expressions;

namespace Educode.Domain.Abstractions
{
    public interface IApplicationRepository<T> where T : class
    {
        T Add(T entity);
        Task<T> AddAsync(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task<int> SaveChangesAsync();
    }
}
