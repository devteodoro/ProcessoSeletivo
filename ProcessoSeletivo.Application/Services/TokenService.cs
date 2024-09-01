using Microsoft.IdentityModel.Tokens;
using ProcessoSeletivo.Application.Interfaces;
using ProcessoSeletivo.Domain.Enums;
using ProcessoSeletivo.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProcessoSeletivo.Application.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                      new Claim("UserId", user.Id.ToString()),
                      new Claim("UserName", user.Name),
                      new Claim("UserRole", user.Role.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);

            return tokenHandler.WriteToken(token);
        }
    }
}
