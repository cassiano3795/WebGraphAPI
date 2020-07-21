using System.Linq;
using System.Threading.Tasks;
using BD.Contexts;
using BD.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;

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

        public Usuarios GetUser(string user)
        {
            var userDb = _context.Usuarios.FirstOrDefault(x => x.Nome == user);
            return userDb;
        }

        public async Task<Usuarios> GetUserAsync(string user)
        {
            var userDb = await _context.Usuarios.FirstOrDefaultAsync(x => x.Nome == user);
            return userDb;
        }

        public IQueryable<Usuarios> GetUsersByClient(int idClient, int? idUser)
        {
            var users = _context.Usuarios.Where(x =>
                x.Idcliente == idClient && (idUser == null || x.Idusuario == idUser));

            return users;
        }

        public async Task<IQueryable<Usuarios>> GetUsersByClientAsync(int idClient, int? idUser)
        {
            var users = _context.Usuarios.Where(x =>
                x.Idcliente == idClient && (idUser == null || x.Idusuario == idUser));

            return await Task.FromResult(users);
        }
    }
}
