using BD.Contexts;
using BD.Models;
using Core.Repositories;

namespace Repositories.Repositories
{
    public class UserRepository : BaseRepository<Usuarios>, IUserRepository
    {
        private readonly Entities _context;
        public UserRepository(Entities context)
            :base(context)
        {
            _context = context;
        }
    }
}
