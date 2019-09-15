using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace FutterlisteNg.Data.Model
{
    [BsonIgnoreExtraElements]
    public class PaymentLine
    {
        public PaymentLine(string paidFor, decimal amount)
        {
            PaidFor = paidFor;
            Amount = amount;
        }

        [BsonId(IdGenerator = typeof(BsonObjectIdGenerator))]
        public BsonValue Id { get; set; }
        
        [BsonElement("payed_for")]
        public string PaidFor { get; set; }
        
        [BsonElement("amount")]
        public decimal Amount { get; set; }
    }
}