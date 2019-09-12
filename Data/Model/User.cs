namespace FutterlisteNg.Data.Model
{
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
    }
}