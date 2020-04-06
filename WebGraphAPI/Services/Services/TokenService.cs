using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using BD.Models;
using Core.Services;
using JwtSecurity.Classes;
using Microsoft.IdentityModel.Tokens;

namespace Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly TokenConfigurations _tokenConfiguraions;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;

        public TokenService(TokenConfigurations tokenConfigurations, SigningConfigurations signingConfigurations,
            Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            _tokenConfiguraions = tokenConfigurations;
            _signingConfigurations = signingConfigurations;
            _httpContextAccessor = httpContextAccessor;
        }

        public TokenConfigurations GetTokenConfiguration()
        {
            return _tokenConfiguraions;
        }

        public SigningConfigurations GetSigningConfigurations()
        {
            return _signingConfigurations;
        }

        public string GenerateToken(Usuarios user, out DateTime dataExpiracao)
        {
            var tokenConfigurations = GetTokenConfiguration();
            var signingConfigurations = GetSigningConfigurations();

            var identity = new ClaimsIdentity(
                new GenericIdentity(user.Idusuario.ToString(), "Login"),
                new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Idusuario.ToString()),
                    new Claim("IdCliente", user.Idcliente.ToString()),
                    new Claim("IdUsuario", user.Idusuario.ToString()),
                    // TODO: CLAIMS QUE JULGAR NECESSÁRIO
                    new Claim(ClaimTypes.Name, user.Nome)
                }
            );

            var dataCriacao = DateTime.Now;
            dataExpiracao = dataCriacao +
                                TimeSpan.FromSeconds(tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });

            return handler.WriteToken(securityToken);
        }

        public string GenerateRefreshToken(Usuarios usuarios, out DateTime dataExpiracaoRefresh)
        {
            //TODO: REGRA DE CRIAR O TOKEN DE REFRESH
            throw new NotImplementedException();
        }

        public ClaimsPrincipal GetClaimsPrincipal()
        {
            var header = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            var token = header.Split(" ")[1];
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            return tokenHandler.ValidateToken(token, validationParameters, out var securityToken);
        }

        public int GetIdClient()
        {
            var claimsPrincipal = GetClaimsPrincipal();
            int.TryParse(claimsPrincipal.FindFirst("IdCliente").Value, out var idClient);
            return idClient;
        }

        public TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = _tokenConfiguraions.Issuer,
                ValidAudience = _tokenConfiguraions.Audience,
                IssuerSigningKey = _signingConfigurations.Key
            };
        }
    }
}
