using FutterlisteNg.Data;
using FutterlisteNg.Data.Model;
using MongoDB.Driver;
using NUnit.Framework;

namespace FutterlisteNg.UnitTests.Data
{
    public abstract class IntegrationTestBase
    {
        private const string c_databaseName = "FutterlisteNgTest";

        private readonly IMongoClient _mongoClient = new MongoClient("mongodb://localhost:27017");

        protected IMongoDatabase Database => _mongoClient.GetDatabase(c_databaseName);

        [SetUp]
        public void SetUpBase()
        {
            Seed();
        }

        [TearDown]
        public void TearDownBase()
        {
            _mongoClient.DropDatabase(c_databaseName);
        }

        private void Seed()
        {
            var eric = new User("Eric Cartman", "Eric");
            var stan = new User("Stan Marsh", "Stan");
            var kenny = new User("Kenny McCormick", "Kenny");
            var kyle = new User("Kyle Broflovski", "Kyle");
            Database.GetCollection<User>(CollectionNames.Users).InsertMany(new[] {eric, stan, kenny, kyle});
        }
    }
}