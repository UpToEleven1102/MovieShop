using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MovieShop.Core.RepositoryInterface
{
    public interface IAsyncRepository<T> where T : class
    {
        T GetByIdAsync(int id);
        IEnumerable<T> ListAllAsync();
        IEnumerable<T> ListAsync(Expression<Func<T, bool>> filter); //filter: LINQ - where condition
        int GetCountAsync(Expression<Func<T, bool>> filter = null); //filter=null means default value of filter is null
        bool GetExistingAsync(Expression<Func<T, bool>> filter = null);

        T AddAsync(T entity);

        T UpdateAsync(T entity);

        T DeleteAsync(T entity);
    }
}