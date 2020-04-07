using System;
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

        public Clientes CreateClient(ResolveFieldContext context)
        {
            var clientInput = context.GetArgument<Clientes>("client");
            _clientService.Create(clientInput);
            _clientService.Save();
            return clientInput;
        }

        public Clientes UpdateClient(ResolveFieldContext context)
        {
            var clientInput = context.GetArgument<Clientes>("client");
            _clientService.Update(clientInput);
            _clientService.Save();
            return clientInput;
        }

        public bool DeleteClient(ResolveFieldContext context)
        {
            try
            {
                var id = context.GetArgument<int>("id");
                var client = _clientService.FindById(id);
                _clientService.Delete(client);
                _clientService.Save();
                return true;
            }
            catch (Exception e)
            {
                //TODO: CREATE A METHOD CATCHEXCPETION AND SAVE "e"
                return false;
            }
        }
    }
}
