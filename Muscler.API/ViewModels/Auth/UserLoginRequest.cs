namespace Muscler.API.ViewModels.Auth
{
    public class UserLoginRequest
    {
        public string Password { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty; 
    }
}