using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MovieShop.Core.RepositoryInterface
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> ListAllAsync();
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter); //filter: LINQ - where condition
        Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null); //filter=null means default value of filter is null
        Task<bool> GetExistingAsync(Expression<Func<T, bool>> filter = null);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<T> DeleteAsync(T entity);
    }
}