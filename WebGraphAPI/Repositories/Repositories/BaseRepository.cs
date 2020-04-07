using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BD.Contexts;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Repositories
{
    public class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : class
    {
        private readonly Entities _context;
        protected DbSet<T> Set => _context.Set<T>();

        public BaseRepository(Entities context)
        {
            _context = context;
        }
        public void Create(T entity)
        {
            Set.Add(entity);
        }

        public void Delete(T entity)
        {
            Set.Remove(entity);
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
            return Set;
        }

        public T FindById(int id)
        {
            return Set.Find(id);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return Set.Where(expression);
        }

        public void Update(T entity)
        {
            Set.Update(entity);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
