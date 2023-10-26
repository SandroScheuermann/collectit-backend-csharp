using CollectIt.Domain.Auth.Jwt;
using CollectIt.Domain.Entity.Auth;
using CollectIt.Domain.Shared.ErrorHandling;
using Microsoft.AspNetCore.Identity;

namespace CollectIt.Domain.Auth
{
    public class AuthService : IAuthService
    {
        private UserManager<ApplicationUser> UserManager { get; init; }

        private IJwtTokenService JwtTokenService { get; set; }

        public AuthService(UserManager<ApplicationUser> userManager, IJwtTokenService jwtTokenService)
        {
            UserManager = userManager;
            JwtTokenService = jwtTokenService;
        }

        public async Task<ProcessResult> Register(string email, string username, string password)
        {
            var user = new ApplicationUser(username, email);

            var result = await UserManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return ProcessResult.Success();
            }
            else
            {
                return ProcessResult.Fail(string.Join(',', result.Errors.Select(e => e.Description)));
            }
        }

        public async Task<ProcessResult<string>> Login(string email, string password)
        {
            var user = await UserManager.FindByEmailAsync(email);

            if (user != null)
            {
                var isPasswordValid = await UserManager.CheckPasswordAsync(user, password);

                if (isPasswordValid)
                {
                    var userToken = JwtTokenService.GenerateJwtToken(user);
                    return ProcessResult<string>.Success(userToken);
                }
                else
                {
                    return ProcessResult<string>.Fail("Login ou senha inválidos!");
                }
            }

            return ProcessResult<string>.Fail("Erro não catalogado");
        }
    }
}
