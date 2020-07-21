using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IBaseService<T> where T : class
    {
        IEnumerable<T> FindAll();
        Task<IEnumerable<T>> FindAllAsync();
        T FindById(int id);
        Task<T> FindByIdAsync(int id);
        T Create(T entity);
        Task<T> CreateAsync(T entity);
        T Update(T entity);
        Task<T> UpdateAsync(T entity);
        void Delete(T entity);
        Task DeleteAsync(T entity);
        void Save();
        Task SaveAsync();
    }
}
