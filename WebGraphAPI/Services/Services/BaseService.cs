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

        public async Task DeleteAsync(T entity)
        {
            await _unityOfWork.BaseRepository.DeleteAsync(entity);
        }

        public void Save()
        {
            _unityOfWork.BaseRepository.Save();
        }

        public async Task SaveAsync()
        {
            await _unityOfWork.BaseRepository.SaveAsync();
        }

        public IEnumerable<T> FindAll()
        {
            return _unityOfWork.BaseRepository.FindAll();
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            var r = await _unityOfWork.BaseRepository.FindAllAsync();
            return r;
        }

        public T FindById(int id)
        {
            return _unityOfWork.BaseRepository.FindById(id);
        }

        public async Task<T> FindByIdAsync(int id)
        {
            var r = await _unityOfWork.BaseRepository.FindByIdAsync(id);
            return r;
        } 

        public T Create(T entity)
        {
            var r = _unityOfWork.BaseRepository.Create(entity);
            return r;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var r = await _unityOfWork.BaseRepository.CreateAsync(entity);
            return r;
        }

        public T Update(T entity)
        {
            var r = _unityOfWork.BaseRepository.Update(entity);
            return r;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var r = await _unityOfWork.BaseRepository.UpdateAsync(entity);
            return r;
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> exp)
        {
            return _unityOfWork.BaseRepository.Where(exp);
        }
    }
}
