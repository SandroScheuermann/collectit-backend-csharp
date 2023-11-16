using Muscler.API.ViewModels.Auth;
using Muscler.Domain.Auth;

namespace Muscler.API.ControllerMappings
{
    public static class AuthControllerMap
    {
        public static void ConfigureAuthControllerMappings(this WebApplication app)
        {
            _ = app.MapPost("/auth/login", Login);
            _ = app.MapPost("/auth/login/oauth2", OAuth2Login);
            _ = app.MapPost("/auth/register", Register);
            _ = app.MapGet("/auth/confirm-account", ConfirmAccount);
        }

        private static async Task<IResult> Login(UserLoginRequest loginRequest, IAuthService authService)
        {
            var result = await authService.Login(loginRequest.UserEmail, loginRequest.Password);

            return result.IsSuccess ? Results.Ok(result.Content) : Results.BadRequest(result.ErrorMessage);
        }
        private static async Task<IResult> OAuth2Login(OAuth2GoogleLoginRequest googleLoginRequest, IAuthService authService)
        {
            var result = await authService.OAuth2Login(googleLoginRequest.Token);

            return result.IsSuccess ? Results.Ok(result.Content) : Results.BadRequest(result.ErrorMessage);
        }

        private static async Task<IResult> Register(UserRegisterRequest registerRequest, IAuthService authService)
        {
            var result = await authService.Register(registerRequest.UserEmail, registerRequest.UserName, registerRequest.Password);

            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.ErrorMessage);
        }
        private static async Task<IResult> ConfirmAccount(string userId, string token, IAuthService authService)
        {
            var result = await authService.ConfirmAccount(userId, token);

            return result.Succeeded ? Results.Redirect("https://muscler.pro/register/email-confirmation?confirmed=true") : Results.BadRequest(string.Join(',', result.Errors.Select(x => x.Description)));
        }
    }
}