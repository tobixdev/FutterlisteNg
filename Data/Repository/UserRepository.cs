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

        public async Task<User> Get(string username)
        {
            var filter = new FilterDefinitionBuilder<User>().Eq(u => u.Username, username);
            return await (await UserCollection.FindAsync(filter)).SingleAsync();
        }

        public async Task AddAsync(User toAdd)
        {
            var usersWithName = await FindUsersWithUsername(toAdd.Username);

            if(usersWithName.Count > 0)
                throw new DuplicateException($"User with short name '{toAdd.Username}' already exists.");
            
            await _mongoDatabase.GetCollection<User>(CollectionNames.Users).InsertOneAsync(toAdd);
        }

        private async Task<List<User>> FindUsersWithUsername(string username)
        {
            var filter = CreateUsernameFilter(username);
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
            await UserCollection.DeleteOneAsync(CreateUsernameFilter(username));
        }

        public async Task UpdateAsync(User toUpdate)
        {
            var filter = CreateUsernameFilter(toUpdate.Username);
            await UserCollection.FindOneAndReplaceAsync(filter, toUpdate);
        }

        private static FilterDefinition<User> CreateUsernameFilter(string username)
        {
            return new FilterDefinitionBuilder<User>()
                .Eq(u => u.Username, username);
        }
    }
}