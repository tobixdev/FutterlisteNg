using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace FutterlisteNg.Data.Model
{
    [BsonIgnoreExtraElements]
    public class Payment
    {
        public Payment() : this(string.Empty, string.Empty)
        {
        }
        
        public Payment(string payedBy, string spentOn)
        {
            PayedBy = payedBy;
            SpentOn = spentOn;
            PaymentLines = new List<PaymentLine>();
        }

        [BsonId(IdGenerator = typeof(BsonObjectIdGenerator))]
        public BsonValue Id { get; set; }
        
        [BsonElement("payed_by")]
        public string PayedBy { get; set; }
        
        [BsonElement("spent_on")]
        public string SpentOn { get; set; }
        
        [BsonElement("payment_lines")]
        public List<PaymentLine> PaymentLines { get; set; }
    }
}