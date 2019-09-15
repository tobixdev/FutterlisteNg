using System;
using FutterlisteNg.Data;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Data.Model.Builder;
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
            var token = new User("Token Black", "Token");
            database.GetCollection<User>(CollectionNames.Users).InsertMany(new[] {eric, stan, kenny, kyle, token});

            var payment1 = new PaymentBuilder().WithPayedBy("Eric")
                .WithDescription("Kentucky Fried Chicken")
                .WithPaymentLine("Eric", 7.5m)
                .WithPaymentLine("Stan", 7.5m)
                .WithPaymentLine("Kenny", 7.5m)
                .WithPaymentLine("Kyle", 7.5m)
                .Build();
            var payment2 = new PaymentBuilder().WithPayedBy("Stan")
                .WithDescription("Comic books")
                .WithPaymentLine("Stan", 7.5m)
                .WithPaymentLine("Kyle", 10m)
                .Build();
            database.GetCollection<Payment>(CollectionNames.Payments).InsertMany(new[] { payment1, payment2});
        }
    }
}