using Muscler.Domain.Shared.ErrorHandling;

namespace Muscler.Domain.Auth
{
    public interface IAuthService
    {
        public Task<ProcessResult> Register(string email, string username, string password);
        public Task<ProcessResult<string>> Login(string email, string password);
    }
}
