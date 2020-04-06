using System.Collections.Generic;
using System.Linq;
using BD.Models;
using Core.Services;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;

namespace CoreGraphQL.Resolvers
{
    public class UserResolvers : BaseResolver
    {
        private readonly IUserService _userService;

        public UserResolvers(IUserService userService, ITokenService tokenService,
            IHttpContextAccessor httpContextAccessor) : base(tokenService, httpContextAccessor)
        {
            _userService = userService;
        }

        public Usuarios ResolveUser(ResolveFieldContext context, int id)
        {
            return _userService.FindById(id);
        }

        public List<Usuarios> ResolveUsers(ResolveFieldContext context, int? id, int size, int offSet, out int count)
        {
            var users = _userService.Where(x => x.Idcliente == IdClient && (id == null || x.Idusuario == id));
            count = users.Count();
            return users.Skip(offSet).Take(size).ToList();
        }
    }
}
