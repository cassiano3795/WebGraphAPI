using System.Linq;
using System.Threading.Tasks;
using BD.Models;

namespace Core.Repositories
{
    public interface IUserRepository : IBaseRepository<Usuarios>
    {
        Usuarios GetUser(string user);
        Task<Usuarios> GetUserAsync(string user);
        IQueryable<Usuarios> GetUsersByClient(int idClient, int? idUser);
        Task<IQueryable<Usuarios>> GetUsersByClientAsync(int idClient, int? idUser);
    }
}
