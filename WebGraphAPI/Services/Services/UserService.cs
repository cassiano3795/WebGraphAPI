using BD.Models;
using Core.Repositories;
using Core.Services;

namespace Services.Services
{
    public class UserService : BaseService<Usuarios>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICriptService _criptService;

        public UserService(IUserRepository userRepository, ICriptService criptService)
            :base(userRepository)
        {
            _userRepository = userRepository;
            _criptService = criptService;
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

            _userRepository.Create(user);
            _userRepository.Create(user2);
            _userRepository.Save();
        }
    }
}
