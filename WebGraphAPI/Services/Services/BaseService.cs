using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Repositories;
using Core.Services;

namespace Services.Services
{
    public class BaseService<T> : IBaseService<T>
        where T : class
    {
        private readonly IBaseRepository<T> _baseRepository;

        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public void Delete(T entity)
        {
            _baseRepository.Delete(entity);
        }

        public IEnumerable<T> FindAll()
        {
            return _baseRepository.FindAll();
        }

        public T FindById(int id)
        {
            return _baseRepository.FindById(id);
        }

        public void Insert(T entity)
        {
            _baseRepository.Create(entity);
        }

        public void Update(T entity)
        {
            _baseRepository.Update(entity);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> exp)
        {
            return _baseRepository.Where(exp);
        }
    }
}
