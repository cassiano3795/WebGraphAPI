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

        public void InicializaDb()
        {
            var client = new Clientes
            {
                IdCliente = 1,
                Nome = "Cliente Teste",
                Email = "teste@emp.com.br",
                Celular = "(xx) xxxxx-xxxx",
                Cidade = "Porto Alegre",
                Cnpj = "99999999999",
                Endereco = "Lima e Silva, 578",
                Site = "teste.com.br",
                Telefone = "(xx) xxxxx-xxxx"
            };

            _clientRepository.Create(client);
            _clientRepository.Save();
        }
    }
}
