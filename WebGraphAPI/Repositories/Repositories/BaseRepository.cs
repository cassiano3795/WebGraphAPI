using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BD.Contexts;
using Core.Repositories;

namespace Repositories.Repositories
{
    public class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : class
    {
        private readonly Entities _context;

        public BaseRepository(Entities context)
        {
            _context = context;
        }
        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public IQueryable<T> FindAll()
        {
            return _context.Set<T>();
        }

        public T FindById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
