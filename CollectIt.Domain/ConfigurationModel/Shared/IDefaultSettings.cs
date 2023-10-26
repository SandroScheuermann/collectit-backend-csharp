namespace GameCollector.Domain.ConfigurationModel.Shared
{
    public interface IDefaultSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; } 
    }
}
