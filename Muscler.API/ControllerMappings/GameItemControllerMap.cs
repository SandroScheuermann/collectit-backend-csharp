using Muscler.Domain.Entity;
using Muscler.Domain.Game;

namespace Muscler.API.ControllerMappings
{
    public static class GameItemControllerMap
    {
        public static void ConfigureGameItemControllerMappings(this WebApplication app)
        {
            _ = app.MapGet("/GameItems/", GetAllGameItems).RequireAuthorization();
            _ = app.MapGet("/GameItems/{id}", GetGameItemById).RequireAuthorization();
            _ = app.MapPost("/GameItems/", InsertGameItem).RequireAuthorization();
            _ = app.MapDelete("/GameItems/{id}", DeleteGameItemById).RequireAuthorization();
            _ = app.MapPut("/GameItems/", UpdateGameItem).RequireAuthorization();
        }

        private static async Task<IResult> GetAllGameItems(IGameItemService gameItemService)
        {
            List<GameItem> gameItems = await gameItemService.GetAllGameItems();

            return gameItems.Any() == true ? Results.Ok(gameItems) : Results.NotFound();
        }
        private static async Task<IResult> UpdateGameItem(GameItem gameItem, IGameItemService gameItemService)
        {
            var result = await gameItemService.EditGameItem(gameItem);

            return result.ModifiedCount > 0 ? Results.Ok() : Results.NoContent(); 
        }
        private static async Task<IResult> DeleteGameItemById(string id, IGameItemService gameItemService)
        {
            var result = await gameItemService.DeleteGameItem(id); 

            return result.DeletedCount > 0 ? Results.Ok() : Results.NoContent();
        }
        private static async Task<IResult> GetGameItemById(string id, IGameItemService gameItemService)
        {
            GameItem gameItem = await gameItemService.GetGameItemById(id);

            return gameItem != null ? Results.Ok(gameItem) : Results.NotFound();
        }
        private static async Task<IResult> InsertGameItem(GameItem gameItem, IGameItemService gameItemService)
        {
            Task task = gameItemService.InsertGameItem(gameItem);

            await task;

            return task.IsCompleted ? Results.Ok(gameItem) : Results.Problem();
        }
    }
}