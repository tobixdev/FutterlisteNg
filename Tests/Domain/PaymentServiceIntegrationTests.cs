using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using FutterlisteNg.Data;
using FutterlisteNg.Data.Model.Builder;
using FutterlisteNg.Data.Repository;
using FutterlisteNg.Domain.Service;
using FutterlisteNg.Tests.Data;
using FutterlisteNg.Tests.Extensions;
using NUnit.Framework;

namespace FutterlisteNg.Tests.Domain
{
    [TestFixture]
    public class PaymentServiceIntegrationTests : IntegrationTestBase
    {
        private IPaymentService _sut;
        private IPaymentRepository _paymentRepository;

        [SetUp]
        public void SetUp()
        {
            // NOTE: Helps to keep an overview of inserted TestData.
            Database.DropCollection(CollectionNames.Payments);
            
            _paymentRepository = new PaymentRepository(Database);
            _sut = new PaymentService(_paymentRepository, new UserRepository(Database));
        }

        [Test]
        public async Task GetUserBalanceList_WithPaymentFromUser_ShouldReturnPositiveBalance()
        {
            var payment = new PaymentBuilder("Eric").WithPaymentLine("Eric", 10).Build();
            await _paymentRepository.AddPaymentAsync(payment);
                
            var result = (await _sut.GetUserBalanceList()).ToArray();

            result.Should().HaveCount(5);
            result.Select(e => e.Balance).Should().AllBeEquivalentTo(0);
        }

        [Test]
        public async Task GetUserBalanceList_WithMultiplePayments_ShouldCalculateBalancesCorrectly()
        {
            var payment1 = new PaymentBuilder("Eric")
                .WithPaymentLine("Eric", 10)
                .WithPaymentLine("Stan", 5)
                .WithPaymentLine("Kyle", 7.5m)
                .WithPaymentLine("Token", 15)
                .Build();
            var payment2 = new PaymentBuilder("Token")
                .WithPaymentLine("Eric", 15)
                .WithPaymentLine("Token", 15)
                .Build();
            var payment3 = new PaymentBuilder("Stan")
                .WithPaymentLine("Kenny", 3.33m)
                .WithPaymentLine("Eric", 3.33m)
                .WithPaymentLine("Kyle", 3.33m)
                .Build();
            await _paymentRepository.AddPaymentsAsync(payment1, payment2, payment3);
                
            var result = (await _sut.GetUserBalanceList()).ToArray();
            using (new AssertionScope())
            {
                result.Should().HaveCount(5);
                result[0].User.Username.Should().Be("Eric");
                result[0].Balance.Should().Be(9.17m);
                result[1].User.Username.Should().Be("Stan");
                result[1].Balance.Should().Be(4.99m);
                result[2].User.Username.Should().Be("Token");
                result[2].Balance.Should().Be(0);
                result[3].User.Username.Should().Be("Kenny");
                result[3].Balance.Should().Be(-3.33m);
                result[4].User.Username.Should().Be("Kyle");
                result[4].Balance.Should().Be(-10.83m);
            }
        }
    }
}