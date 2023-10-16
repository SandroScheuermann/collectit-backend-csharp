using GameCollector.Domain.Entity.Shared;

namespace GameCollector.Domain.Shared
{
    public interface IMongoRepository<T> where T : MongoEntity
    {
        public Task<List<T>> GetAllAsync();
        public Task<T> GetByIdAsync(string id);
        public Task InsertAsync(T item);
        public Task DeleteAsync(string id);
        public Task UpdateAsync(T item);
    }
}