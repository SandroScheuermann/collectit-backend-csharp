using CollectIt.Domain.Entity.Auth;
using Microsoft.AspNetCore.Identity; 

namespace CollectIt.Domain.Auth
{
    public class AuthService
    {
        private UserManager<ApplicationUser> UserManager { get; init; }

        public AuthService(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public async Task<IResult> Register(string email, string password)
        {
            var user = new ApplicationUser(email, email);
            var result = await UserManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                // Usuário registrado com sucesso
                return await ;
            }
            else
            {
                // Erros durante o registro
                await Task.CompletedTask;
            }
        }

        public async Task Login(string email, string password)
        {
            var user = await UserManager.FindByEmailAsync(email);

            if (user != null)
            {
                // Verifica a senha
                var isPasswordValid = await UserManager.CheckPasswordAsync(user, password);
                if (isPasswordValid)
                {
                    // Gere o token JWT aqui e retorne para o usuário
                    await Task.CompletedTask;
                }
            }

            await Task.CompletedTask;
        }

    }
}
