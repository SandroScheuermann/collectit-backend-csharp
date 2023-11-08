using Muscler.Domain.Entity;
using MongoDB.Driver;

namespace Muscler.Domain.Game
{
    public interface IGameItemService
    {
        Task InsertGameItem(GameItem gameItem);

        Task<ReplaceOneResult> EditGameItem(GameItem gameItem);

        Task<DeleteResult> DeleteGameItem(string id);

        Task<List<GameItem>> GetAllGameItems(); 
         
        Task<GameItem> GetGameItemById(string id);
    }
}
