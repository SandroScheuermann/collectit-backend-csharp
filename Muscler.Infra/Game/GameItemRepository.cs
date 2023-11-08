using Muscler.Domain.ConfigurationModel;
using Muscler.Domain.Entity;
using Muscler.Domain.Game;
using Muscler.Infra.DataAccess.Shared;
using Microsoft.Extensions.Options;

namespace Muscler.Infra.Game
{
    public class GameItemRepository : MongoRepository<GameItem>, IGameItemRepository
    {
        public GameItemRepository(IOptions<DefaultSettings> settings) : base(settings)
        { 
        }
    }
}
