using GameCollector.Domain.Entity;
using GameCollector.Domain.Shared;

namespace GameCollector.Domain.Game
{
    public interface IGameItemRepository : IMongoRepository<GameItem>
    {
    }
}
