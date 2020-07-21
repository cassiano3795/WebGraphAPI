using System.Threading.Tasks;
using BD.Contexts;
using Core.Repositories;
using Repositories.Repositories;

namespace Repositories
{
    public class UnityOfWork<T> : IUnityOfWork<T> where T : class
    {
        private readonly Entities _context;

        private IBaseRepository<T> _baseRepository;
        private IClientRepository _clientRepository;
        private IUserRepository _userRepository;

        private bool Disposed { get; set; }

        public UnityOfWork(Entities context)
        {
            _context = context;
        }

        public IBaseRepository<T> BaseRepository => _baseRepository = _baseRepository ?? new BaseRepository<T>(_context);
        public IClientRepository ClientRepository => _clientRepository = _clientRepository ?? new ClientRepository(_context);
        public IUserRepository UserRepository => _userRepository = _userRepository ?? new UserRepository(_context);

        public void Commit()
        {
            if (Disposed) return;

            _context.SaveChanges();
            Disposed = true;
        }

        public async Task CommitAsync()
        {
            if (Disposed) await Task.CompletedTask;

            await _context.SaveChangesAsync();
            Disposed = true;
        }
        public void Rollback()
        {
            Disposed = true;
            _context.Dispose();
        }

        public async Task RollbackAsync()
        {
            Disposed = true;
            await _context.DisposeAsync();
        }
    }
}
