using System.Collections.Generic;
using System.Threading.Tasks;
using BD.Models;

namespace Core.Services
{
    public interface IUserService : IBaseService<Usuarios>
    {
        void InicializaDb();
        Task IniciaizaDbAsync();

        Usuarios GetUser(string user);
        Task<Usuarios> GetUserAsync(string user);
        IEnumerable<Usuarios> GetUsersByClient(int idClient, int? idUser);
        Task<IEnumerable<Usuarios>> GetUsersByClientAsync(int idClient, int? idUser);
    }
}
