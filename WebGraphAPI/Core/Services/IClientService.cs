using System.Threading.Tasks;
using BD.Models;

namespace Core.Services
{
    public interface IClientService : IBaseService<Clientes>
    {
        void InicializaDb();
        Task IniciaizaDbAsync();
    }
}
