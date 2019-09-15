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
            var usersWithName = await FindUsersWithShortName(toAdd.ShortName);

            if(usersWithName.Count > 0)
                throw new DuplicateException($"User with short name '{toAdd.ShortName}' already exists.");
            
            await _mongoDatabase.GetCollection<User>(CollectionNames.Users).InsertOneAsync(toAdd);
        }

        public async Task<User> GetByShortNameAsync(string shortName)
        {
            var foundUsers = await FindUsersWithShortName(shortName);

            if(foundUsers.Count == 0)
                throw new NotFoundException($"User with name '{shortName}' not found.");
            
            return foundUsers.Single();
        }

        public async Task UpdateAsync(User user)
        {
            var filterDefinition = CreateShortNameFilter(user);

            await GetByShortNameAsync(user.ShortName); // NOTE: Ensure the user exists

            await UserCollection.FindOneAndReplaceAsync(filterDefinition, user);
        }

        private static FilterDefinition<User> CreateShortNameFilter(User user)
        {
            return new FilterDefinitionBuilder<User>()
                .Eq(u => u.ShortName, user.ShortName);
        }

        private async Task<List<User>> FindUsersWithShortName(string shortName)
        {
            var filter = new FilterDefinitionBuilder<User>()
                .Eq(u => u.ShortName, shortName);

            var cursor = await UserCollection.FindAsync(filter);
            return await cursor.ToListAsync();
        }

        public async Task<IEnumerable<User>> FindAllAsync()
        {
            var cursor = await UserCollection.FindAsync(FilterDefinition<User>.Empty);
            return cursor.ToEnumerable();
        }
    }
}