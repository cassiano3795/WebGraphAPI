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
        public T Create(T entity)
        {
            var r = Set.Add(entity);
            return r.Entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var r = await Set.AddAsync(entity);
            return r.Entity;
        }

        public void Delete(T entity)
        {
            Set.Remove(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            Set.Remove(entity);
            await Task.CompletedTask;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> FindAll()
        {
            return Set;
        }

        public async Task<IQueryable<T>> FindAllAsync()
        {
            return await Task.FromResult(Set);
        }

        public T FindById(int id)
        {
            return Set.Find(id);
        }

        public async Task<T> FindByIdAsync(int id)
        {
            var r = await Set.FindAsync(id);
            return r;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return Set.Where(expression);
        }

        public async Task<IQueryable<T>> WhereAsync(Expression<Func<T, bool>> expression)
        {
            return await Task.FromResult(Set.Where(expression));
        }

        public T Update(T entity)
        {
            var r = Set.Update(entity);
            return r.Entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var r = await Task.FromResult(Set.Update(entity));
            return r.Entity;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
