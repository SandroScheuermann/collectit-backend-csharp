using GameCollector.Domain.Entity.Shared;
using MongoDB.Driver;

namespace GameCollector.Domain.Shared
{
    public interface IMongoRepository<T> where T : MongoEntity
    {
        public Task<List<T>> GetAllAsync();
        public Task<T> GetByIdAsync(string id);
        public Task InsertAsync(T item);
        public Task<DeleteResult> DeleteAsync(string id);
        public Task<ReplaceOneResult> UpdateAsync(T item);
    }
}