using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FutterlisteNg.Domain.Model;

namespace FutterlisteNg.Domain.Data
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> FindAllAsync();
        Task Add(User toAdd);
    }
}