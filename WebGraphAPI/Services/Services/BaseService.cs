using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Repositories;
using Core.Services;

namespace Services.Services
{
    public class BaseService<T> : IBaseService<T>
        where T : class
    {
        private readonly IUnityOfWork<T> _unityOfWork;

        public BaseService(IUnityOfWork<T> unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }
        public void Delete(T entity)
        {
            _unityOfWork.BaseRepository.Delete(entity);
        }

        public void Save()
        {
            // DEPRECATED
            _unityOfWork.BaseRepository.Save();
        }

        public Task SaveAsync()
        {
            // DEPRECATED
            return _unityOfWork.BaseRepository.SaveAsync();
        }

        public IEnumerable<T> FindAll()
        {
            return _unityOfWork.BaseRepository.FindAll();
        }

        public T FindById(int id)
        {
            return _unityOfWork.BaseRepository.FindById(id);
        }

        public void Create(T entity)
        {
            _unityOfWork.BaseRepository.Create(entity);
        }

        public void Update(T entity)
        {
            _unityOfWork.BaseRepository.Update(entity);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> exp)
        {
            return _unityOfWork.BaseRepository.Where(exp);
        }
    }
}
