using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace CollectIt.Domain.Entity.Auth
{
    [CollectionName("Auth-Roles")]
    public class ApplicationRole : MongoIdentityRole<Guid>
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string roleName) : base(roleName) { }
    }
}
