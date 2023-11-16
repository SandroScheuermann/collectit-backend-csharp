using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Muscler.Domain.Auth.Jwt;
using Muscler.Domain.Email;
using Muscler.Domain.Entity.Auth;
using Muscler.Domain.Shared.ErrorHandling;
using System.Net;

namespace Muscler.Domain.Auth
{
    public class AuthService : IAuthService
    {
        private UserManager<ApplicationUser> UserManager { get; init; }
        private IJwtTokenService JwtTokenService { get; set; }
        public IEmailService EmailService { get; set; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public AuthService(UserManager<ApplicationUser> userManager, IJwtTokenService jwtTokenService, IEmailService emailService, IHttpContextAccessor httpContextoAcessor)
        {
            UserManager = userManager;
            JwtTokenService = jwtTokenService;
            EmailService = emailService;
            HttpContextAccessor = httpContextoAcessor;
        }

        public async Task<ProcessResult> Register(string email, string username, string password)
        {
            var user = new ApplicationUser(username, email);

            var result = await UserManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await SendConfirmationLink(user);

                return ProcessResult.Success();
            }
            else
            {
                return ProcessResult.Fail(string.Join(',', result.Errors.Select(e => e.Description)));
            }
        }

        private async Task<SendGrid.Response> SendConfirmationLink(ApplicationUser user)
        {
            var token = await UserManager.GenerateEmailConfirmationTokenAsync(user);

            var encodedToken = WebUtility.UrlEncode(token);

            var confirmationLink = $"{HttpContextAccessor.HttpContext.Request.Scheme}://{HttpContextAccessor.HttpContext.Request.Host.Value}/auth/confirm-account?userId={user.Id}&token={encodedToken}";

            return await EmailService.SendConfirmationLink(confirmationLink, user);
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

        public async Task<IdentityResult> ConfirmAccount(string userId, string token)
        {
            var user = await UserManager.FindByIdAsync(userId);

            return await UserManager.ConfirmEmailAsync(user, token);
        }

        public async Task<ProcessResult<string>> OAuth2Login(string googleLoginToken)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string> { "teste" }
                };

                GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(googleLoginToken, settings);

                var loginToken = JwtTokenService.GenerateJwtToken(payload);

                return ProcessResult<string>.Success(loginToken);
            }
            catch (Exception ex)
            {
                return ProcessResult<string>.Fail(ex.Message);
            }
        }
    }
}
