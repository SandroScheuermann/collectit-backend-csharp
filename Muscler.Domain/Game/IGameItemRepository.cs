using Muscler.Domain.Entity;
using Muscler.Domain.Shared;

namespace Muscler.Domain.Game
{
    public interface IGameItemRepository : IMongoRepository<GameItem>
    {
    }
}
