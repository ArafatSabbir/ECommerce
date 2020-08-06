using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data
{
    public interface IRepogitory<T> where T : class
    {
        T Get(int id);
        IList<T> GetAll();
        void Add(T item);
        void Delete(T item);
        void Update(T item);
        void Delete(int id);
        

    }
}
