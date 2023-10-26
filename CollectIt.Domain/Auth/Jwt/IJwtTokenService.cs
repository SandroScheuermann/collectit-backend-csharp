using CollectIt.Domain.Entity.Auth;

namespace CollectIt.Domain.Auth.Jwt
{
    public interface IJwtTokenService
    {
        string GenerateJwtToken(ApplicationUser user); 
    }
}
