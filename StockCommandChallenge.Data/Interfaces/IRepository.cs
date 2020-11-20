using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace StockCommandChallenge.Data.Interfaces
{
    public interface IRepository<T> where T : class, IBaseEntity
    {
        DbSet<T> Table { get; }
        IEnumerable<T> Get(Expression<Func<T, bool>> query);
        IEnumerable<T> ListAll();
        T First(Expression<Func<T, bool>> query);
        T Find(int Id);
        bool Exists(Expression<Func<T, bool>> query);
        int Count();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void SaveChanges();
        bool IsActiveTransaction();
    }
}
