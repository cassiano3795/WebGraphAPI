using BD.Models;
using Core.Repositories;
using Core.Services;

namespace Services.Services
{
    public class ClientService : BaseService<Clientes>, IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
            :base(clientRepository)
        {
            _clientRepository = clientRepository;
        }
    }
}
