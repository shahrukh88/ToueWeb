using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using ToueWebAPI.Models;

namespace ToueWebAPI.Repository.IRepository
{
    public interface IRepository<T> where T: class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeproperties = null);

        Task CreateAsync(T entity);


        Task RemoveAsync(T entity);

        Task SaveAsync();

    }
}
