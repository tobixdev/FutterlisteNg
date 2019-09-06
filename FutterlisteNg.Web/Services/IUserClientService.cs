using System.Threading.Tasks;
using FutterlisteNg.Domain.Model;

namespace FutterlisteNg.Web.Services
{
    public interface IUserClientService
    {
        Task<User[]> All();
    }
}