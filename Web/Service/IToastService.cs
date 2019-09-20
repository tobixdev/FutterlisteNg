using System.Threading.Tasks;

namespace FutterlisteNg.Web.Service
{
    public interface IToastService
    {
        Task Error(string message);
        Task Success(string message);
    }
}