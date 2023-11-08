using Muscler.Domain.ConfigurationModel.Shared;

namespace Muscler.Domain.ConfigurationModel
{
    public class DefaultSettings : IDefaultSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
