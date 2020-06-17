using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repositories
{
    public interface IUnityOfWork<T> where T : class
    {
        IBaseRepository<T> BaseRepository { get; }
        IClientRepository ClientRepository { get; }
        IUserRepository UserRepository { get; }
        void Commit();
        void Rolback();
    }
}
