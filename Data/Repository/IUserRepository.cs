using System.Collections.Generic;
using System.Threading.Tasks;
using FutterlisteNg.Data.Model;

namespace FutterlisteNg.Data.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> FindAllAsync();
        Task AddAsync(User toAdd);
    }
}