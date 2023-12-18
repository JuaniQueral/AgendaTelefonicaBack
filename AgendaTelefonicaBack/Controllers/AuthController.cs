using AgendaTelefonicaBack.Data.Interfaces;
using AgendaTelefonicaBack.Data.Repository;
using AgendaTelefonicaBack.Models;
using AgendaTelefonicaBack.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AgendaTelefonicaBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly InterfazUsuario _usuarioRepository;
        private readonly IConfiguration _config;
        public AuthController(InterfazUsuario usuarioRepository, IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _config = config;
        }

        [HttpPost("login")]
        public Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            /// Paso 1: Validamos las credenciales
            var user = _usuarioRepository.ValidateUser(login.Username , login.Password); //Lo primero que hacemos es llamar a una función que valide los parámetros que enviamos.

            if (user is null) //Si el la función de arriba no devuelve nada es porque los datos son incorrectos, por lo que devolvemos un Unauthorized (un status code 401).
                return Task.FromResult<IActionResult>(Unauthorized(new { message = "Credenciales inválidas" }));


            //Paso 2: Crear el token
            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"])); //Traemos la SecretKey del Json. agregar antes: using Microsoft.IdentityModel.Tokens;

            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            //Los claims son datos en clave->valor que nos permite guardar data del usuario.
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.Id.ToString())); //"sub" es una key estándar que significa unique user identifier, es decir, si mandamos el id del usuario por convención lo hacemos con la key "sub".
            claimsForToken.Add(new Claim("given_name", user.Nombre)); //Lo mismo para given_name y family_name, son las convenciones para nombre y apellido. Ustedes pueden usar lo que quieran, pero si alguien que no conoce la app
            claimsForToken.Add(new Claim("family_name", user.Apellido)); //quiere usar la API por lo general lo que espera es que se estén usando estas keys.

            var jwtSecurityToken = new JwtSecurityToken( //agregar using System.IdentityModel.Tokens.Jwt; Acá es donde se crea el token con toda la data que le pasamos antes.
              _config["Authentication:Issuer"],
              _config["Authentication:Audience"],
              claimsForToken,
              DateTime.UtcNow,
              DateTime.UtcNow.AddHours(1),
              credentials);

            var tokenToReturn = new JwtSecurityTokenHandler() //Pasamos el token a string
                .WriteToken(jwtSecurityToken);

            return Task.FromResult<IActionResult>(Ok(new { success = true, message = "Inicio de sesión exitoso", token = tokenToReturn }));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Usuario usuario)
        {
            if ((await _usuarioRepository.GetListUsuario()).Any(x => x.UserName == usuario.UserName))
            {
                return Conflict(new { message = "El usuario ya existe" });
            }

            await _usuarioRepository.AddUser(usuario);

            return Ok(new { success = true, message = "Registro exitoso" });
        }

    }
}

