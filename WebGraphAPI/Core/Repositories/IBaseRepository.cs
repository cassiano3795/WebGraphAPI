using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> FindAll();
        T FindById(int id);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveAsync();
    }
}
