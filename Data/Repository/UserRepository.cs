using System.Collections.Generic;
using System.Linq;
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

        private IMongoCollection<User> UserCollection => _mongoDatabase.GetCollection<User>(CollectionNames.Users);

        public async Task AddAsync(User toAdd)
        {
            var usersWithName = await FindUsersWithUsername(toAdd.Username);

            if(usersWithName.Count > 0)
                throw new DuplicateException($"User with short name '{toAdd.Username}' already exists.");
            
            await _mongoDatabase.GetCollection<User>(CollectionNames.Users).InsertOneAsync(toAdd);
        }

        private async Task<List<User>> FindUsersWithUsername(string username)
        {
            var filter = new FilterDefinitionBuilder<User>()
                .Eq(u => u.Username, username);

            var cursor = await UserCollection.FindAsync(filter);
            return await cursor.ToListAsync();
        }

        public async Task<IEnumerable<User>> FindAllAsync()
        {
            var cursor = await UserCollection.FindAsync(FilterDefinition<User>.Empty);
            return cursor.ToEnumerable();
        }

        public async Task<bool> Exists(string username)
        {
            return (await FindUsersWithUsername(username)).Count > 0;
        }

        public async Task DeleteAsync(string username)
        {
            var filter = new FilterDefinitionBuilder<User>()
                .Eq(u => u.Username, username);

            await UserCollection.DeleteOneAsync(filter);
        }
    }
}