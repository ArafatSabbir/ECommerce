using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ECommerce.Data
{
    public class Repository<T> : IRepogitory<T> where T : class
    {
        private DbContext _dbContext;
        private DbSet<T> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();

        }
        public void Add(T item)
        {
            _dbSet.Add(item);
        }

        public void Delete(T item)
        {
            _dbSet.Remove(item);
        }

        public void Delete(int id)
        {
            var item = Get(id);
            _dbSet.Remove(item);
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IList<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Update(T item)
        {
            try
            {
                _dbSet.Attach(item);
            }
            catch
            {

            }
        }
    }
}
