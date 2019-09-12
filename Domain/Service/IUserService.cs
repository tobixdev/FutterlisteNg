using System.Collections.Generic;
using System.Threading.Tasks;
using FutterlisteNg.Domain.Data;
using FutterlisteNg.Domain.Model;

namespace FutterlisteNg.Domain.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> FindAllAsync();
        Task Add(User toAdd);
    }
}