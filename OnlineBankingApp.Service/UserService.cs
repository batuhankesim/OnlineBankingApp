using Microsoft.Extensions.Configuration;
using OnlineBankingApp.Common.DTO.User;
using OnlineBankingApp.Common.Interface;
using OnlineBankingApp.Entity.DbContexts;
using OnlineBankingApp.Entity.Model;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace OnlineBankingApp.Service
{
    public class UserService : IUserService
    {
        private readonly UserContext _userContext;
        private readonly IConfiguration _configuration;

        public UserService(UserContext userContext, IConfiguration configuration)
        {
            _userContext = userContext;
            _configuration = configuration;
        }

        public async Task<bool> RegisterUserAsync(UserRequest request)
        {
            User user = new User()
            {
                Id = _userContext.Users.Count() + 1,
                Username = request.Username,
                Password = request.Password
            };
            _userContext.Users.Add(user);
            await _userContext.SaveChangesAsync();
            return true;
        }


        public async Task<string> LoginUserAsync(UserRequest request)
        {
            var user = await _userContext.Users.SingleOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password);
            if (user == null)
            {
                return string.Empty;
            }

            var token = GenerateJwtToken(request.Username);

            return token;
        }

        private string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}
