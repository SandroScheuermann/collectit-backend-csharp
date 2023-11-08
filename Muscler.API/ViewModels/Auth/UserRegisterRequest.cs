namespace Muscler.API.ViewModels.Auth
{
    public class UserRegisterRequest
    {
        public string Password { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty; 
    }
}