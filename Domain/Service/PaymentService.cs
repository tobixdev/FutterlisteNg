using System.Collections.Generic;
using System.Threading.Tasks;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Data.Repository;

namespace FutterlisteNg.Domain.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<Payment>> FindAllAsync()
        {
            return await _paymentRepository.FindAllAsync();
        }
    }
}