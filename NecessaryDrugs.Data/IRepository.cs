using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NecessaryDrugs.Data
{
    public interface IRepository<T> where T:class
    {
        void Add(T entity);
        void Edit(T entityToUpdate);
        T GetById(int id);
        T GetByIdWithIncludeProperty(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        IEnumerable<T> GetAll();
        int GetCount(Expression<Func<T, bool>> filter = null);
        IList<T> Get(Expression<Func<T, bool>> filter);

        (IList<T> data, int total, int totalDisplay) Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false);

        (IList<T> data, int total, int totalDisplay) GetDynamic(
            Expression<Func<T, bool>> filter = null,
            string orderBy = null,
            string includeProperties = "", int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false);

        IList<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", bool isTrackingOff = false);

        IList<T> GetDynamic(Expression<Func<T, bool>> filter = null,
            string orderBy = null,
            string includeProperties = "", bool isTrackingOff = false);
        void Remove(Expression<Func<T, bool>> filter);
        void Remove(int id);
        void Remove(T entityToDelete);
    }
}
