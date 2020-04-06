using BD.Contexts;
using BD.Models;
using Core.Repositories;

namespace Repositories.Repositories
{
    public class ClientRepository : BaseRepository<Clientes>, IClientRepository
    {
        private readonly Entities _context;

        public ClientRepository(Entities context)
            :base(context)
        {
            _context = context;
        }
    }
}
