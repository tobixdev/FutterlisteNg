using MongoDB.Bson.Serialization.Attributes;

namespace FutterlisteNg.Data.Model
{
    [BsonIgnoreExtraElements]
    public class User
    {
        public User()
        {
        }

        public User(string name, string shortName)
        {
            Name = name;
            ShortName = shortName;
        }

        public string Name { get; set; }
        public string ShortName { get; set; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(ShortName)}: {ShortName}";
        }
    }
}