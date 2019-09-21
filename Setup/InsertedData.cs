using FutterlisteNg.Data.Model;

namespace FutterlisteNg.Setup
{
    public class InsertedData
    {
        public InsertedData()
        {
            InsertedUsers = new Users();
            InsertedPayments = new Payments();
        }

        public Users InsertedUsers { get; internal set; }
        public Payments InsertedPayments { get; internal set; }
        
        public class Users
        {
            public User Eric { get; internal set; }
            public User Stan { get; internal set; }
            public User Kenny { get; internal set; }
            public User Kyle { get; internal set; }
            public User Token { get; internal set; }
        }

        public class Payments
        {
            public Payment KFC { get; internal set; }
            public Payment ComicBooks { get; internal set; }
        }
    }
}