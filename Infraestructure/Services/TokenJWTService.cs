using Domain;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Services.Classes
{
    public class TokenJWTService
    {
        private IConfiguration _configuration;

        public TokenJWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.UTF8.GetBytes(_configuration["VitalAPI:KeySecret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim( ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim( ClaimTypes.Email, user.Email),
                    new Claim( ClaimTypes.Role, user.Role),
                    new Claim( "CPF", user.CPF),
                }),

                Expires = DateTime.UtcNow.AddHours(Convert.ToInt32(_configuration["VitalAPI:TimeTokenIsValid"])),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
