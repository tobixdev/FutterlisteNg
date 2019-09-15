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
            await _mongoDatabase.GetCollection<User>(CollectionNames.Users).InsertOneAsync(toAdd);
        }

        public async Task<User> FindByNameAsync(string name)
        {
            var filter = new FilterDefinitionBuilder<User>()
                .Eq(u => u.Name, name);
            
            var cursor = await UserCollection.FindAsync(filter);
            var foundUsers = await cursor.ToListAsync();
            
            if(foundUsers.Count == 0)
                throw new NotFoundException($"User with name '{name}' not found.");
            
            return foundUsers.Single();
        }
        
        public async Task<IEnumerable<User>> FindAllAsync()
        {
            var cursor = await UserCollection.FindAsync(FilterDefinition<User>.Empty);
            return cursor.ToEnumerable();
        }
    }
}