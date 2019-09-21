using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FutterlisteNg.Data;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Data.Repository;
using MongoDB.Driver.Core.Misc;

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

        public async Task AddAsync(Payment payment)
        {
            await _paymentRepository.AddPaymentAsync(payment);
        }

        public async Task DeleteAsync(Guid id)
        {
            if(!await _paymentRepository.ExistsAsync(id))
                throw new NotFoundException($"Payment with Id '{id}' not found");
            
            await _paymentRepository.DeleteAsync(id);
        }
    }
}