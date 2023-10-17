using GameCollector.Domain.Entity;
using GameCollector.Domain.Game;

namespace GameCollector.API.ControllerMappings
{
    public static class AuthControllerMap
    {
        public static void ConfigureAuthControllerMappings(this WebApplication app)
        {
            _ = app.MapGet("/auth/login", Login);
            _ = app.MapGet("/auth/register", Register); 
        }

        private static async Task<IResult> Login(GameItem gameItem, IGameItemService gameItemService)
        {
            Task task = gameItemService.InsertGameItem(gameItem);

            await task;

            return task.IsCompleted ? Results.Ok(gameItem) : Results.Problem();
        }
        private static async Task<IResult> Register(GameItem gameItem, IGameItemService gameItemService)
        {
            Task task = gameItemService.InsertGameItem(gameItem);

            await task;

            return task.IsCompleted ? Results.Ok(gameItem) : Results.Problem();
        }

    }
}