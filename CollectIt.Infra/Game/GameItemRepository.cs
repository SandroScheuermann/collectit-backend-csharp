using GameCollector.Domain.ConfigurationModel;
using GameCollector.Domain.Entity;
using GameCollector.Domain.Game;
using GameCollector.Infra.DataAccess.Shared;
using Microsoft.Extensions.Options;

namespace GameCollector.Infra.Game
{
    public class GameItemRepository : MongoRepository<GameItem>, IGameItemRepository
    {
        public GameItemRepository(IOptions<DefaultSettings> settings) : base(settings)
        { 
        }
    }
}
