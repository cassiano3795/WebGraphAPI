using System.Security.Claims;
using Core.Services;
using Microsoft.AspNetCore.Http;

namespace CoreGraphQL.Resolvers
{
    public class BaseResolver
    {
        protected readonly int IdClient;
        protected readonly ClaimsPrincipal ClaimsPrincipal;
        public BaseResolver(ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
        {
            ClaimsPrincipal = tokenService.GetClaimsPrincipal();
            IdClient = tokenService.GetIdClient();
        }
    }
}
