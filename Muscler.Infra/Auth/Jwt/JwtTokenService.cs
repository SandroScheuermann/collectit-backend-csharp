using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Muscler.Domain.ConfigurationModel;
using Muscler.Domain.Entity.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Muscler.Domain.Auth.Jwt
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly string _secretKey;

        public JwtTokenService(IOptions<JwtSettings> jwtSettings)
        {
            _secretKey = jwtSettings.Value.Key;
        }

        public string GenerateJwtToken(ApplicationUser defaultPayload)
        {
            var claims = new ClaimsIdentity(new Claim[] {
                         new Claim(ClaimTypes.Name, defaultPayload.UserName),
                         new Claim(ClaimTypes.NameIdentifier, defaultPayload.Id.ToString()),
                         new Claim(ClaimTypes.Email, defaultPayload.Email),
            });

            return GenerateJwtToken(claims);
        }

        public string GenerateJwtToken(GoogleJsonWebSignature.Payload googlePayload)
        {
            var claims = new ClaimsIdentity(new Claim[] {
                         new Claim(ClaimTypes.Name, googlePayload.Name),
                         new Claim(ClaimTypes.NameIdentifier, googlePayload.Subject),
                         new Claim(ClaimTypes.Email, googlePayload.Email),
            });

            return GenerateJwtToken(claims);
        }

        public string GenerateJwtToken(ClaimsIdentity claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(2),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
