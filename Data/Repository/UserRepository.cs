using System.Collections.Generic;
using System.Threading.Tasks;
using FutterlisteNg.Data.Model;
using MongoDB.Driver;

namespace FutterlisteNg.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _mongoDatabase;

        public UserRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        private IMongoCollection<User> UserCollection => _mongoDatabase.GetCollection<User>("User");

        public async Task<IEnumerable<User>> FindAllAsync()
        {
            var cursor = await UserCollection.FindAsync(FilterDefinition<User>.Empty);
            return cursor.ToEnumerable();
        }

        public async Task AddAsync(User toAdd)
        {
            await _mongoDatabase.GetCollection<User>("User").InsertOneAsync(toAdd);
        }
    }
}