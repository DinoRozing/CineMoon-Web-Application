using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cinema.Model;
using Cinema.Repository.Common;
using Cinema.Service.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Cinema.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository; 
        private readonly IConfiguration _configuration;
        private readonly Guid userRoleId = new Guid("9a130690-f7b4-4ab6-b75a-a1348a9781dc");

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            user.Id = Guid.NewGuid();
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.IsActive = true;
            user.DateCreated = DateTime.UtcNow;
            user.DateUpdated = user.DateCreated;
            user.RoleId = userRoleId;
            
            return await _userRepository.RegisterUserAsync(user);
        }

        public async Task<string> LoginUserAsync(UserLogin userLogin)
        {
            var user = await _userRepository.GetUserByEmailAsync(userLogin.Email);

            if (user == null)
            {
                throw new Exception("User not found.");
            }
            
            if (!BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password))
            {
                throw new Exception("Wrong password.");
            }

            string token = CreateToken(user);
            return token;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));
            
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
            
    }
}