namespace Muscler.API.ViewModels.Auth
{
    public class UserConfirmAccountRequest
    {
        public string UserId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}