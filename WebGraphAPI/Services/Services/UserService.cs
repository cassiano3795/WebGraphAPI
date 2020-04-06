using BD.Models;
using Core.Repositories;
using Core.Services;

namespace Services.Services
{
    public class UserService : BaseService<Usuarios>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
            :base(userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
