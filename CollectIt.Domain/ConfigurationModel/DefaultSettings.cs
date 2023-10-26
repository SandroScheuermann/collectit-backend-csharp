using GameCollector.Domain.ConfigurationModel.Shared;

namespace GameCollector.Domain.ConfigurationModel
{
    public class DefaultSettings : IDefaultSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
