using StockCommandChallenge.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace StockCommandChallenge.Data.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IDbContext _context;
        public DbSet<T> Table { get; }

        public Repository(IDbContext context)
        {
            _context = context;
            Table = context.Set<T>();
        }

        public int Count()
        {
            return Table.Count();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> query)
        {
            return Table.Where(query).ToList();
        }

        public T First(Expression<Func<T, bool>> query)
        {
            return Table.FirstOrDefault(query);
        }

        public bool Exists(Expression<Func<T, bool>> query)
        {
            return Table.Any(query);
        }

        public IEnumerable<T> ListAll()
        {
            return Table.ToList();
        }

        public void Create(T entity)
        {
            Table.Add(entity);
        }

        public void Delete(T entity)
        {
            Table.Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        #region transactions
        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

        public bool IsActiveTransaction()
        {
            return _context.Database.CurrentTransaction != null;
        }
        #endregion
    }
}
