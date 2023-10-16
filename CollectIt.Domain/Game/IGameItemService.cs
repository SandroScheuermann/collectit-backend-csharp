using GameCollector.Domain.Entity;

namespace GameCollector.Domain.Game
{
    public interface IGameItemService
    {
        Task InsertGameItem(GameItem gameItem);

        Task EditGameItem(GameItem gameItem);

        Task DeleteGameItem(string id);

        Task<List<GameItem>> GetAllGameItems(); 
         
        Task<GameItem> GetGameItemById(string id);
    }
}
