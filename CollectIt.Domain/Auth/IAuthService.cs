using CollectIt.Domain.Shared.ErrorHandling;

namespace CollectIt.Domain.Auth
{
    public interface IAuthService
    {
        public Task<ProcessResult> Register(string email, string username, string password);
        public Task<ProcessResult<string>> Login(string email, string password);
    }
}
