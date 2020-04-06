using BD.Models;
using Core.Services;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;

namespace CoreGraphQL.Resolvers
{
    public class ClientResolvers : BaseResolver
    {
        private readonly IClientService _clientService;

        public ClientResolvers(IClientService clientService, IUserService userService, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
            :base(tokenService, httpContextAccessor)
        {
            _clientService = clientService;
        }
        public Clientes ResolveClient(ResolveFieldContext context)
        {
            return _clientService.FindById(IdClient);
        }
    }
}
