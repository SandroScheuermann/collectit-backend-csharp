using Google.Apis.Auth;
using Muscler.Domain.Entity.Auth;
using System.Security.Claims;

namespace Muscler.Domain.Auth.Jwt
{
    public interface IJwtTokenService
    {
        string GenerateJwtToken(ApplicationUser defaultPayload);
        string GenerateJwtToken(GoogleJsonWebSignature.Payload googlePayload);
        string GenerateJwtToken(ClaimsIdentity claims); 
    }
}
