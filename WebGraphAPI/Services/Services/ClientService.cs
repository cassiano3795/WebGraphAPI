using System.Threading.Tasks;
using BD.Models;
using Core.Repositories;
using Core.Services;

namespace Services.Services
{
    public class ClientService : BaseService<Clientes>, IClientService
    {
        private readonly IUnityOfWork<Clientes> _unityOfWork;

        public ClientService(IUnityOfWork<Clientes> unityOfWork)
            :base(unityOfWork)
        {
            _unityOfWork = unityOfWork;
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

            _unityOfWork.ClientRepository.Create(client);
            _unityOfWork.Commit();
        }

        public async Task IniciaizaDbAsync()
        {
            await Task.Run(async () =>
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

                await _unityOfWork.ClientRepository.CreateAsync(client);
                await _unityOfWork.CommitAsync();
            });
        }
    }
}
