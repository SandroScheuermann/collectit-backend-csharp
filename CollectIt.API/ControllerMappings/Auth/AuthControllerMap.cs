using CollectIt.API.ViewModels.Auth;
using CollectIt.Domain.Auth;

namespace GameCollector.API.ControllerMappings
{
    public static class AuthControllerMap
    {
        public static void ConfigureAuthControllerMappings(this WebApplication app)
        {
            _ = app.MapPost("/auth/login", Login);
            _ = app.MapPost("/auth/register", Register);
        }

        private static async Task<IResult> Login(UserLoginRequest loginRequest, IAuthService authService)
        {
            var result = await authService.Login(loginRequest.Email, loginRequest.Password);

            return result.IsSuccess ? Results.Ok(result.Content) : Results.Problem(result.ErrorMessage);
        }

        private static async Task<IResult> Register(UserRegisterRequest registerRequest, IAuthService authService)
        {
            var result = await authService.Register(registerRequest.UserEmail, registerRequest.UserName, registerRequest.Password);

            return result.IsSuccess ? Results.Ok() : Results.Problem(result.ErrorMessage);
        }
    }
}