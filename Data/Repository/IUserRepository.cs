using System.Collections.Generic;
using System.Threading.Tasks;
using FutterlisteNg.Data.Model;

namespace FutterlisteNg.Data.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> FindAllAsync();
        Task<User> GetAsync(string username);
        Task AddAsync(User toAdd);
        Task<bool> Exists(string username);
        Task UpdateAsync(User toUpdate);
    }
}