namespace CollectIt.API.ViewModels.Auth
{
    public class UserLoginRequest
    {
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; 
    }
}