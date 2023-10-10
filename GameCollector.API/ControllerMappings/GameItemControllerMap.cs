using GameCollector.Domain.Entity;
using GameCollector.Domain.Game;

namespace GameCollector.API.ControllerMappings
{
    public static class GameItemControllerMap
    {
        public static void ConfigureGameItemControllerMappings(this WebApplication app)
        {
            _ = app.MapGet("/GameItems/", GetAllGameItems);
            _ = app.MapGet("/GameItems/{id}", GetGameItemById);
            _ = app.MapPost("/GameItems/", InsertGameItem);
            _ = app.MapDelete("/GameItems/{id}", DeleteGameItemById);
            _ = app.MapPut("/GameItems/", UpdateGameItem);
        }

        private static async Task<IResult> GetAllGameItems(IGameItemService gameItemService)
        {
            List<GameItem> gameItems = await gameItemService.GetAllGameItems();

            return gameItems == null ? Results.NotFound() : Results.Ok(gameItems);
        }
        private static async Task<IResult> UpdateGameItem(GameItem gameItem, IGameItemService gameItemService)
        {
            Task result = gameItemService.EditGameItem(gameItem);

            await result;

            return !result.IsCompleted ? Results.Problem() : Results.Ok();
        }
        private static async Task<IResult> DeleteGameItemById(string id, IGameItemService gameItemService)
        {
            Task result = gameItemService.DeleteGameItem(id);

            await result;

            return !result.IsCompleted ? Results.NotFound() : Results.Ok();
        }
        private static async Task<IResult> GetGameItemById(string id, IGameItemService gameItemService)
        {
            GameItem gameItem = await gameItemService.GetGameItemById(id);

            return gameItem == null ? Results.NotFound() : Results.Ok(gameItem);
        }
        private static async Task<IResult> InsertGameItem(GameItem gameItem, IGameItemService gameItemService)
        {
            Task task = gameItemService.InsertGameItem(gameItem);

            await task;

            return !task.IsCompleted ? Results.Problem() : Results.Ok(gameItem);
        }
    }
} 