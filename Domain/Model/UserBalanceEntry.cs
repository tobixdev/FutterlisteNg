using FutterlisteNg.Data.Model;

namespace FutterlisteNg.Domain.Model
{
    public class UserBalanceEntry
    {
        public User User { get; }
        public decimal Balance { get; }

        public UserBalanceEntry(User user, decimal balance)
        {
            User = user;
            Balance = balance;
        }
    }
}