using GameCollector.Domain.Shared;

namespace GameCollector.Domain.Entity.Shared
{
    public class CollectorItem : MongoEntity
    { 
        public RarityEnum Rarity { get; set; }

        public decimal Value { get; set; }

        public string Name { get; set; }

        public GameConsoleTypeEnum ConsoleType { get; set; }

        public string Serial { get; set; }

        public int LaunchYear { get; set; }

    }
}
