using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Muscler.Domain.Entity.Shared
{
    public class MongoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
    }
}
