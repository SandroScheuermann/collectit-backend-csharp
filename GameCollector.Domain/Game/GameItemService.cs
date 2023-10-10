using GameCollector.Domain.Entity;

namespace GameCollector.Domain.Game
{
    public class GameItemService : IGameItemService
    {
        public IGameItemRepository Repository { get; set; }

        public GameItemService(IGameItemRepository repository)
        {
            Repository = repository;
        }

        public async Task InsertGameItem(GameItem gameItem)
        {
            await Repository.InsertAsync(gameItem);
        }

        public async Task EditGameItem(GameItem gameItem)
        { 
            await Repository.UpdateAsync(gameItem);
        }

        public async Task DeleteGameItem(string id)
        {  
            await Repository.DeleteAsync(id);
        }

        public async Task<GameItem> GetGameItemById(string id)
        {  
            return await Repository.GetByIdAsync(id);
        }

        public async Task<List<GameItem>> GetAllGameItems()
        {
            return await Repository.GetAllAsync();
        }

    }
}
