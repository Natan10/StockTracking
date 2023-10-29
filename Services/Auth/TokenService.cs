using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StockTracking.Config;
using StockTracking.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockTracking.Services.Auth
{
    public interface ITokenService
    {
        public string GenerateJwtToken(int UserId, EEmployeeRole role);
    }

    public class TokenService : ITokenService
    {
        private readonly JwtOptions _options;

        public TokenService(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateJwtToken(int userId, EEmployeeRole role)
        {
            var claims = new List<Claim> { 
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim("Role", role.ToString()),
            };
                
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_options.SecurityKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _options.Issuer,
                Audience = _options.Audience,
                Expires = DateTime.UtcNow.AddMinutes(_options.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

        }

    }
}
