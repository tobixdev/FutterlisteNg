using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FutterlisteNg.Domain.Model;
using MongoDB.Driver;

namespace FutterlisteNg.Domain.Data
{
    public class UserRepository : IUserRepository
    {
        private static IMongoDatabase Db => new MongoClient("mongodb://mongodb:27017/FutterlisteNg").GetDatabase("FutterlisteNg");
        
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