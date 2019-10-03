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

        public static InsertedData InsertTestData(IMongoDatabase database)
        {
            var insertedData = new InsertedData();
            
            var eric = new User("Eric", "Eric Cartman");
            var stan = new User("Stan", "Stan Marsh");
            var kenny = new User("Kenny", "Kenny McCormick");
            var kyle = new User("Kyle", "Kyle Broflovski");
            var token = new User("Token", "Token Black", false);
            database.GetCollection<User>(CollectionNames.Users).InsertMany(new[] {eric, stan, kenny, kyle, token});

            insertedData.InsertedUsers.Eric = eric;
            insertedData.InsertedUsers.Stan = stan;
            insertedData.InsertedUsers.Kenny = kenny;
            insertedData.InsertedUsers.Kyle = kyle;
            insertedData.InsertedUsers.Token = token;
            
            var payment1 = new PaymentBuilder("Eric")
                .WithDescription("Kentucky Fried Chicken")
                .WithPaymentLine("Eric", 7.5m)
                .WithPaymentLine("Stan", 7.5m)
                .WithPaymentLine("Kenny", 7.5m)
                .WithPaymentLine("Kyle", 7.5m)
                .Build();
            var payment2 = new PaymentBuilder("Stan")
                .WithDescription("Comic books")
                .WithPaymentLine("Stan", 7.5m)
                .WithPaymentLine("Kyle", 10m)
                .Build();
            database.GetCollection<Payment>(CollectionNames.Payments).InsertMany(new[] { payment1, payment2});

            insertedData.InsertedPayments.KFC = payment1;
            insertedData.InsertedPayments.ComicBooks = payment2;

            return insertedData;
        }
    }
}