using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IBaseService<T> where T : class
    {
        IEnumerable<T> FindAll();
        T FindById(int id);
        IEnumerable<T> Where(Expression<Func<T, bool>> exp);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
        Task SaveAsync();
    }
}
