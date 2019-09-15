using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public Payment(string payedBy, string description)
        {
            Id = Guid.Empty;
            PayedBy = payedBy;
            Description = description;
            PaymentLines = new List<PaymentLine>();
        }

        [BsonId(IdGenerator = typeof(GuidGenerator))]
        public Guid Id { get; set; }
        
        [BsonElement("payed_by")]
        public string PayedBy { get; set; }
        
        [BsonElement("spent_on")]
        public string Description { get; set; }
        
        [BsonElement("payment_lines")]
        public List<PaymentLine> PaymentLines { get; set; }

        [BsonIgnore] public decimal TotalAmount => PaymentLines.Sum(pl => pl.Amount);
    }
}