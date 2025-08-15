using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopAccAPI.Data;
using ShopAccAPI.Models.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ShopAccAPI.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        public JwtService(IConfiguration configuration, DBContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<LoginResponseModel?> Authenticate(LoginRequestModel loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return null; // Invalid credentials
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginRequest.Username && u.PasswordHash == loginRequest.Password);
            if (user == null)
            {
                return null; // Invalid credentials
            }

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = _configuration["Jwt:Key"];
            var tokenValidityInMinutes = _configuration.GetValue<int>("Jwt:TokenValidityInMinutes");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityInMinutes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role ?? "User"),
                    new Claim("UserId", user.Id.ToString())
                }),
                Expires = tokenExpiryTimeStamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key)),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return new LoginResponseModel
            {
                AccessToken = accessToken,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds,
                Id = user.Id,
                Username = user.Username,
                Role = user.Role ?? "User",
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                Balance = user.Balance,
                CreatedAt = user.CreatedAt,
                AvatarUrl = user.AvatarUrl
            };

        }
    }

}
