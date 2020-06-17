using BD.Models;
using Core.Repositories;
using Core.Services;

namespace Services.Services
{
    public class UserService : BaseService<Usuarios>, IUserService
    {
        private readonly IUnityOfWork<Usuarios> _unityOfWork;
        private readonly ICriptService _criptService;

        public UserService(ICriptService criptService, IUnityOfWork<Usuarios> unityOfWork)
            :base(unityOfWork)
        {
            _criptService = criptService;
            _unityOfWork = unityOfWork;
        }

        public void InicializaDb()
        {
            var senha = _criptService.Encrypt("senhaDeTeste");
            var user = new Usuarios
            {
                Idusuario = 1,
                Ativo = true,
                Email = "cassiano.raupp@outlook.com",
                Excluido = false,
                Idcliente = 1,
                Login = "cassiano3795",
                Nome = "Cassiano",
                Senha = senha
            };

            var user2 = new Usuarios
            {
                Idusuario = 2,
                Ativo = true,
                Email = "cassiano.raupp@hotmail.com.com",
                Excluido = false,
                Idcliente = 1,
                Login = "cassianos.raupp",
                Nome = "Cassiano",
                Senha = senha
            };

            _unityOfWork.UserRepository.Create(user);
            _unityOfWork.UserRepository.Create(user2);
            _unityOfWork.Commit();
        }
    }
}
