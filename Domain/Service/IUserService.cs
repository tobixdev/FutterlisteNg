using System.Collections.Generic;
using System.Threading.Tasks;
using FutterlisteNg.Data.Model;

namespace FutterlisteNg.Domain.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> FindAllAsync();
        Task<User> GetAsync(string username);
        Task AddAsync(User toAdd);
        Task DeleteAsync(string username);
        Task UpdateAsync(User toUpdate);
    }
}