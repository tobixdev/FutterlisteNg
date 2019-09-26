using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FutterlisteNg.Data.Model;

namespace FutterlisteNg.Domain.Service
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> FindAllAsync();
        Task<Payment> GetAsync(Guid id);
        Task AddAsync(Payment payment);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Payment payment);
    }
}