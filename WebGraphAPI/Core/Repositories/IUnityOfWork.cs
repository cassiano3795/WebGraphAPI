using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IUnityOfWork<T> where T : class
    {
        IBaseRepository<T> BaseRepository { get; }
        IClientRepository ClientRepository { get; }
        IUserRepository UserRepository { get; }
        void Commit();
        Task CommitAsync();
        void Rollback();
        Task RollbackAsync();
    }
}
