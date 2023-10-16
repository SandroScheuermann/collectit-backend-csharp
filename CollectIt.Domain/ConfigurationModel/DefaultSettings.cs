using GameCollector.Domain.ConfigurationModel.Shared;

namespace GameCollector.Domain.ConfigurationModel
{
    public class DefaultSettings : IEntitySettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
