using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppTienda.Constants;
using WebAppTienda.Models;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace WebAppTienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public IActionResult Login(LoginUser UserLogin)
        {
            var user = Authenticate(UserLogin);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("Usuario No Encontrado");
        }

        private Usuario Authenticate(LoginUser UserLogin)
        {
            var currentUser = UsuarioConstant.Usuarios.FirstOrDefault(user =>
                user.Nombre.ToLower() == UserLogin.Nombre.ToLower() &&
                user.Clave == UserLogin.Clave);

            return currentUser;
        }

        private string Generate(Usuario User)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["WebAppTienda:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Crear los Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, User.Nombre),
                new Claim(ClaimTypes.HomePhone, User.Celular),
                new Claim(ClaimTypes.Email, User.Email),
                new Claim(ClaimTypes.Role, User.Roles)
            };

            // Crear el token
            var token = new JwtSecurityToken(
                issuer: _config["WebAppTienda:Issuer"],
                audience: _config["WebAppTienda:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
