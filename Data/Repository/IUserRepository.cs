using System.Collections.Generic;
using System.Threading.Tasks;
using FutterlisteNg.Data.Model;

namespace FutterlisteNg.Data.Repository
{
    public interface IUserRepository
    {
        Task AddAsync(User toAdd);
        Task<IEnumerable<User>> FindAllAsync();
        Task<User> GetByShortNameAsync(string shortName);
        Task UpdateAsync(User user);
    }
}