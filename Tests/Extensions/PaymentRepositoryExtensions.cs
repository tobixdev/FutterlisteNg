using System.Threading.Tasks;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Data.Repository;

namespace FutterlisteNg.Tests.Extensions
{
    public static class PaymentRepositoryExtensions
    {
        public static async Task AddPaymentsAsync(this IPaymentRepository paymentRepository, params Payment[] payments)
        {
            foreach (var payment in payments)
                await paymentRepository.AddPaymentAsync(payment);
        }
    }
}