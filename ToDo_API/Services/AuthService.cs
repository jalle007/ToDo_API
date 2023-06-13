using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDo_API.Models;

namespace ToDo_API.Services
{
    public interface IAuthService
    {
        User AuthenticateUser(string username, string password);
        string GenerateToken(User user);
    }

    public class AuthService : IAuthService
    {
        private readonly ToDoDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ToDoDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public User AuthenticateUser(string username, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == password);
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
          {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role.ToString())
          }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}
