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

        public User(string username, string name) : this(username, name, true)
        {
        }

        public User(string username, string name, bool active)
        {
            Name = name;
            Username = username;
            Active = active;
            PayedBy = new List<Payment>();
        }
        
        [BsonId]
        public string Username { get; set; }
        
        [BsonElement("name")]
        public string Name { get; set; }
        
        [BsonElement("payed_by")]
        public List<Payment> PayedBy { get; set; }
        
        [BsonElement("active")]
        public bool Active { get; set; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Username)}: {Username}";
        }
    }
}