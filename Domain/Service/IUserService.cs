using System.Collections.Generic;
using System.Threading.Tasks;
using FutterlisteNg.Data.Model;

namespace FutterlisteNg.Domain.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> FindAllAsync();
        Task Add(User toAdd);
        Task Delete(string username);
    }
}