using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDo_API.Services;

namespace ToDo_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string username, string password)
        {
            var user = _authService.AuthenticateUser(username, password);

            if (user == null)
                return Unauthorized();

            var token = _authService.GenerateToken(user);

            return Ok(new { Token = token });
        }
    }

}
