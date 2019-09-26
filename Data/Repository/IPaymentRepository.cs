using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FutterlisteNg.Data.Model;

namespace FutterlisteNg.Data.Repository
{
    public interface IPaymentRepository
    {
        Task AddPaymentAsync(Payment payment);
        Task<IEnumerable<Payment>> FindAllAsync();
        Task<Payment> GetAsync(Guid id);
        Task<IEnumerable<Payment>> FindPaymentsPayedBy(string shortName);
        Task<IEnumerable<Payment>> FindPaymentsPayedFor(string shortName);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task UpdateAsync(Payment payment);
    }
}