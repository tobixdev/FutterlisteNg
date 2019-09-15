using System;
using FutterlisteNg.Data;
using FutterlisteNg.Data.Model;
using MongoDB.Driver;

namespace FutterlisteNg.Setup
{
    public static class DatabaseSetup
    {
        static void Main(string[] args)
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            mongoClient.DropDatabase("FutterlisteNg");
            InsertTestData(mongoClient.GetDatabase("FutterlisteNg"));
        }

        public static void InsertTestData(IMongoDatabase database)
        {
            var eric = new User("Eric Cartman", "Eric");
            var stan = new User("Stan Marsh", "Stan");
            var kenny = new User("Kenny McCormick", "Kenny");
            var kyle = new User("Kyle Broflovski", "Kyle");
            database.GetCollection<User>(CollectionNames.Users).InsertMany(new[] {eric, stan, kenny, kyle});
        }
    }
}