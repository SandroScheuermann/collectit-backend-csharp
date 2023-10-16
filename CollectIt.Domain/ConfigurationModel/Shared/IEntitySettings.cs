namespace GameCollector.Domain.ConfigurationModel.Shared
{
    public interface IEntitySettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; } 
    }
}
