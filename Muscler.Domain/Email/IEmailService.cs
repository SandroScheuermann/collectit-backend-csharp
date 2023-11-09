using Muscler.Domain.Entity.Auth;
using SendGrid;

namespace Muscler.Domain.Email
{
    public interface IEmailService
    {
        Task<Response?> SendConfirmationLink(string confirmationLink, ApplicationUser user);
    }
}
