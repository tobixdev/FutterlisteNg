using System.Collections.Generic;
using System.Threading.Tasks;
using FutterlisteNg.Data.Model;
using MongoDB.Driver;

namespace FutterlisteNg.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private static IMongoDatabase Db => new MongoClient("mongodb://localhost:27017").GetDatabase("FutterlisteNg");
        
        public async Task<IEnumerable<User>> FindAllAsync()
        {
            var cursor = await Db.GetCollection<User>("User").FindAsync(FilterDefinition<User>.Empty);
            return cursor.ToEnumerable();
        }

        public async Task Add(User toAdd)
        {
            await Db.GetCollection<User>("User").InsertOneAsync(toAdd);
        }
    }
}