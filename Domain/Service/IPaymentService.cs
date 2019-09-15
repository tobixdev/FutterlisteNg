using System.Collections.Generic;
using System.Threading.Tasks;
using FutterlisteNg.Data.Model;

namespace FutterlisteNg.Domain.Service
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> FindAllAsync();
    }
}