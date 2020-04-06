using System;
using System.Security.Claims;
using BD.Models;
using JwtSecurity.Classes;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services
{
    public interface ITokenService
    {
        TokenConfigurations GetTokenConfiguration();
        SigningConfigurations GetSigningConfigurations();
        string GenerateToken(Usuarios usuarios, out DateTime dataExpiracao);
        string GenerateRefreshToken(Usuarios usuarios, out DateTime dataExpiracaoRefresh);
        TokenValidationParameters GetValidationParameters();
        ClaimsPrincipal GetClaimsPrincipal();
        int GetIdClient();
    }
}
