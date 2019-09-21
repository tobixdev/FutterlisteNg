using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FutterlisteNg.Data.Model;
using MongoDB.Driver;

namespace FutterlisteNg.Data.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IMongoDatabase _mongoDatabase;

        public PaymentRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        private IMongoCollection<Payment> PaymentCollection => _mongoDatabase.GetCollection<Payment>(CollectionNames.Payments);

        public async Task AddPaymentAsync(Payment payment)
        {
            await PaymentCollection.InsertOneAsync(payment);
        }

        public async Task<IEnumerable<Payment>> FindAllAsync()
        {
            var cursor = await PaymentCollection.FindAsync(FilterDefinition<Payment>.Empty);
            return await cursor.ToListAsync();
        }

        public async Task<IEnumerable<Payment>> FindPaymentsPayedBy(string shortName)
        {
            var filter = new FilterDefinitionBuilder<Payment>().Eq(p => p.PayedBy, shortName);
            var cursor = await PaymentCollection.FindAsync(filter);
            return await cursor.ToListAsync();
        }

        public async Task<IEnumerable<Payment>> FindPaymentsPayedFor(string shortName)
        {
            var filter = new FilterDefinitionBuilder<Payment>()
                .ElemMatch(p => p.PaymentLines, pl => pl.PaidFor == shortName);
            var cursor = await PaymentCollection.FindAsync(filter);
            return await cursor.ToListAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = new FilterDefinitionBuilder<Payment>().Eq(p => p.Id, id);
            await PaymentCollection.DeleteOneAsync(filter);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            var filter = new FilterDefinitionBuilder<Payment>().Eq(p => p.Id, id);
            var result = (await PaymentCollection.FindAsync(filter)).ToEnumerable();
            return result.Any();
        }
    }
}