using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace FutterlisteNg.Data.Model
{
    [BsonIgnoreExtraElements]
    public class User
    {
        public User() : this(null, null)
        {
        }

        public User(string name, string shortName)
        {
            Name = name;
            ShortName = shortName;
            PayedBy = new List<Payment>();
        }

        public string Name { get; set; }
        public string ShortName { get; set; }
        public List<Payment> PayedBy { get; set; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(ShortName)}: {ShortName}";
        }
    }
}