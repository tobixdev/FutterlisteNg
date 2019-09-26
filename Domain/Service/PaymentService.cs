using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FutterlisteNg.Data;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Data.Repository;
using FutterlisteNg.Domain.Model;
using MongoDB.Driver.Core.Misc;

namespace FutterlisteNg.Domain.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUserRepository _userRepository;

        public PaymentService(IPaymentRepository paymentRepository, IUserRepository userRepository)
        {
            _paymentRepository = paymentRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Payment>> FindAllAsync()
        {
            return await _paymentRepository.FindAllAsync();
        }

        public async Task<Payment> GetAsync(Guid id)
        {
            return await _paymentRepository.GetAsync(id);
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

        public async Task UpdateAsync(Payment payment)
        {
            if(!await _paymentRepository.ExistsAsync(payment.Id))
                throw new NotFoundException($"Payment with Id '{payment.Id}' not found");

            await _paymentRepository.UpdateAsync(payment);
        }

        public async Task<IEnumerable<UserBalanceEntry>> GetUserBalanceList()
        {
            var result = new List<UserBalanceEntry>();
            var users = await _userRepository.FindAllAsync();

            foreach (var user in users)
            {
                var positiveBalance = (await _paymentRepository.FindPaymentsPayedBy(user.Username))
                    .Sum(p => p.TotalAmount);
                var negativeBalance = (await _paymentRepository.FindPaymentsPayedFor(user.Username))
                    .SelectMany(p => p.PaymentLines)
                    .Where(pl => pl.PaidFor == user.Username)
                    .Sum(pl => pl.Amount);
                result.Add(new UserBalanceEntry(user, positiveBalance - negativeBalance));
            }

            return result.OrderByDescending(e => e.Balance);
        }
    }
}