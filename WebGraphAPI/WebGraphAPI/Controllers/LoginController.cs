using System;
using System.Linq;
using System.Threading.Tasks;
using BD.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebGraphAPI.Models;

namespace WebGraphAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICriptService _criptService;
        private readonly ITokenService _tokenService;
        public LoginController(IUserService userService, ICriptService criptService, ITokenService tokenService)
        {
            _userService = userService;
            _criptService = criptService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserModel user)
        {
            try
            {
                var credenciaisValidas = false;
                Usuarios userBase = null;
                if (user != null && !string.IsNullOrWhiteSpace(user.User))
                {
                    userBase = await _userService.GetUserAsync(user.User);
                    //userBase = _userService.Where(x =>
                    //    x.Login.Equals(user.User)).FirstOrDefault();
                    var passwordHash = _criptService.Encrypt(user.Password);

                    if (userBase != null)
                    {
                        credenciaisValidas = passwordHash == userBase.Senha;
                    }
                }

                if (!credenciaisValidas)
                    return Ok(new
                    {
                        authenticated = false,
                        message = "Falha ao autenticar"
                    });

                var dataCriacao = DateTime.Now;
                var token = _tokenService.GenerateToken(userBase, out var dataExpiracao);
                //var refreshToken = _tokenService.GenerateRefreshToken(userBase, out var dataExpiracaoRefresh);

                return Ok(
                    new
                    {
                        authenticated = true,
                        created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                        expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                        accessToken = token,
                        // TODO: DESENVOLVER REFRESH TOKEN
                        //refreshToken = refreshToken,
                        message = "OK"
                    });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [AllowAnonymous]
        [Route("refreshToken")]
        [HttpPost]
        public IActionResult RefreshToken([FromBody] UserModel user)
        {
            //TODO: A IMPLEMENTAR
            throw new NotImplementedException();
        }
    }
}