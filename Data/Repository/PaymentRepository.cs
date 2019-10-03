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
            return await PaymentCollection.Find(FilterDefinition<Payment>.Empty).ToListAsync();
        }

        public async Task<Payment> GetAsync(Guid id)
        {
            var filter = new FilterDefinitionBuilder<Payment>().Eq(p => p.Id, id);
            return await PaymentCollection.Find(filter).SingleAsync();
        }

        public async Task<IEnumerable<Payment>> FindPaymentsPayedBy(string shortName)
        {
            var filter = new FilterDefinitionBuilder<Payment>().Eq(p => p.PayedBy, shortName);
            return await PaymentCollection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Payment>> FindPaymentsPayedFor(string shortName)
        {
            var filter = new FilterDefinitionBuilder<Payment>()
                .ElemMatch(p => p.PaymentLines, pl => pl.PaidFor == shortName);
            return await PaymentCollection.Find(filter).ToListAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = new FilterDefinitionBuilder<Payment>().Eq(p => p.Id, id);
            await PaymentCollection.DeleteOneAsync(filter);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            var filter = new FilterDefinitionBuilder<Payment>().Eq(p => p.Id, id);
            return await PaymentCollection.Find(filter).AnyAsync();
        }

        public async Task UpdateAsync(Payment payment)
        {
            var filter = new FilterDefinitionBuilder<Payment>().Eq(p => p.Id, payment.Id);
            await PaymentCollection.ReplaceOneAsync(filter, payment);
        }
    }
}