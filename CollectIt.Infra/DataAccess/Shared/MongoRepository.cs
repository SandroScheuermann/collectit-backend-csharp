using GameCollector.Domain.ConfigurationModel.Shared;
using GameCollector.Domain.Entity.Shared;
using GameCollector.Domain.Shared;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GameCollector.Infra.DataAccess.Shared
{
    public class MongoRepository<T> : IMongoRepository<T> where T : MongoEntity
    {
        private IMongoCollection<T> Collection { get; set; }

        public MongoRepository(IOptions<IEntitySettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            Collection = mongoDatabase.GetCollection<T>(typeof(T).Name);
        }

        public async Task InsertAsync(T item)
        {
            await Collection.InsertOneAsync(item);
        }

        public async Task DeleteAsync(string id)
        {
            var deleteByIdFilter = Builders<T>.Filter.Eq(entity => entity.Id, id);

            await Collection.DeleteOneAsync(deleteByIdFilter);
        }

        public async Task<List<T>> GetAllAsync()
        {
            var filter = Builders<T>.Filter.Empty;
            return await Collection.Find(filter).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var getByIdFilter = Builders<T>.Filter.Eq(entity => entity.Id, id);

            return await Collection.Find(getByIdFilter).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(T item)
        {
            var replaceFilter = Builders<T>.Filter.Eq(entity => entity.Id, item.Id);

            await Collection.ReplaceOneAsync(replaceFilter, item);
        }
    }
}
