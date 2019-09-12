using System.Threading.Tasks;
using FutterlisteNg.Shared;

namespace FutterlisteNg.Web.Client
{
    public interface IUserClient
    {
        Task<UserViewModel[]> All();
        Task Create(UserViewModel user);
    }
}