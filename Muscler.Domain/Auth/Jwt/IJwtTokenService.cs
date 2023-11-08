using Muscler.Domain.Entity.Auth;

namespace Muscler.Domain.Auth.Jwt
{
    public interface IJwtTokenService
    {
        string GenerateJwtToken(ApplicationUser user); 
    }
}
