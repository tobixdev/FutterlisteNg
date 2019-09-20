using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace FutterlisteNg.Data.Model
{
    [BsonIgnoreExtraElements]
    public class User
    {
        public User() : this(string.Empty, string.Empty)
        {
        }

        public User(string name, string username)
        {
            Name = name;
            Username = username;
            PayedBy = new List<Payment>();
        }
        
        [BsonId]
        public string Username { get; set; }
        
        [BsonElement("name")]
        public string Name { get; set; }
        
        [BsonElement("payed_by")]
        public List<Payment> PayedBy { get; set; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Username)}: {Username}";
        }
    }
}