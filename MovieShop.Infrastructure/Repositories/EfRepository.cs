using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MovieShop.Core.RepositoryInterface;
using MovieShop.Infrastructure.Data;

namespace MovieShop.Infrastructure.Repositories
{
    public class EfRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly MovieShopDbContext db;

        public EfRepository(MovieShopDbContext dbContext)
        {
            db = dbContext;
        }

        public virtual T GetByIdAsync(int id)
        {
            return db.Set<T>().Find(id);
        }

        public IEnumerable<T> ListAllAsync()
        {
            return db.Set<T>().ToList();
        }

        public IEnumerable<T> ListAsync(Expression<Func<T, bool>> filter)
        {
            return db.Set<T>().Where(filter).ToList();
        }

        public int GetCountAsync(Expression<Func<T, bool>> filter = null)
        {
            return filter == null ? db.Set<T>().Count() : db.Set<T>().Where(filter).Count();
        }

        public bool GetExistingAsync(Expression<Func<T, bool>> filter = null)
        {
            return filter != null && db.Set<T>().Where(filter).Any();
        }

        public T AddAsync(T entity)
        {
            db.Set<T>().Add(entity);
            db.SaveChanges();
            return entity;
        }

        public T UpdateAsync(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
            return entity;
        }

        public T DeleteAsync(T entity)
        {
            db.Set<T>().Remove(entity);
            db.SaveChanges();
            return entity;
        }
    }
}