using FutterlisteNg.Setup;
using MongoDB.Driver;
using NUnit.Framework;

namespace FutterlisteNg.Tests.Data
{
    public abstract class IntegrationTestBase
    {
        private const string c_databaseName = "FutterlisteNgTest";

        private readonly IMongoClient _mongoClient = new MongoClient("mongodb://localhost:27017");

        protected IMongoDatabase Database => _mongoClient.GetDatabase(c_databaseName);

        protected InsertedData TestData { get; private set; }

        [SetUp]
        public void SetUpBase()
        {
            TestData = DatabaseSetup.InsertTestData(Database);
        }

        [TearDown]
        public void TearDownBase()
        {
            _mongoClient.DropDatabase(c_databaseName);
        }
    }
}