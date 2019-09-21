using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Data.Model.Builder;
using FutterlisteNg.Data.Repository;
using NUnit.Framework;

namespace FutterlisteNg.Tests.Data
{
    public class PaymentRepositoryIntegrationTests : IntegrationTestBase
    {
        private IPaymentRepository _paymentRepository;

        [SetUp]
        public void SetUp()
        {
            _paymentRepository = new PaymentRepository(Database);
        }

        [Test]
        public async Task AddPayment_ShouldAddPayment()
        {
            var payment = new PaymentBuilder()
                .WithPayedBy("TestUser")
                .WithDescription("TestExpense")
                .WithPaymentLine("TestUser", 10.5m)
                .Build();

            await _paymentRepository.AddPaymentAsync(payment);

            var fetchedPayment = (await _paymentRepository.FindPaymentsPayedBy("TestUser")).Single();
            fetchedPayment.Should().BeEquivalentTo(payment);
        }

        [Test]
        public async Task FindPaymentsPayedBy_ShouldFilterPaymentsCorrectly()
        {
            var payment1 = new PaymentBuilder()
                .WithPayedBy("Kyle")
                .WithDescription("KFC")
                .WithPaymentLine("Eric", 10.5m)
                .WithPaymentLine("Kyle", 9.75m)
                .Build();
            var payment2 = new PaymentBuilder()
                .WithPayedBy("Stan")
                .WithDescription("Video Games")
                .WithPaymentLine("Eric", 40m)
                .WithPaymentLine("Stan", 40m)
                .Build();
            await AddPayments(payment1, payment2);

            var result = (await _paymentRepository.FindPaymentsPayedBy("Kyle")).ToArray();
            
            result.Should().HaveCount(1);
            result[0].Should().BeEquivalentTo(payment1);
        }

        [Test]
        public async Task FindPaymentsPayedFor_ShouldFilterPaymentsCorrectly()
        {
            var payment1 = new PaymentBuilder()
                .WithPayedBy("Eric")
                .WithDescription("KFC")
                .WithPaymentLine("Eric", 10.5m)
                .WithPaymentLine("Kyle", 9.75m)
                .Build();
            var payment2 = new PaymentBuilder()
                .WithPayedBy("Stan")
                .WithDescription("Video Games")
                .WithPaymentLine("Token", 40m)
                .WithPaymentLine("Stan", 40m)
                .Build();
            await AddPayments(payment1, payment2);

            var insertedPayment = (await _paymentRepository.FindPaymentsPayedFor("Token")).ToArray();
            
            insertedPayment.Should().HaveCount(1);
            insertedPayment[0].Should().BeEquivalentTo(payment2);
        }

        [Test]
        public async Task FindAll_ShouldReturnAllPayments()
        {
            var payments = await _paymentRepository.FindAllAsync();
            
            payments.Should().HaveCount(2);
        }

        [Test]
        public async Task Delete_WithExistentPayment_ShouldDeletePayment()
        {
            var id = TestData.InsertedPayments.KFC.Id;
            
            await _paymentRepository.DeleteAsync(id);

            (await _paymentRepository.ExistsAsync(id)).Should().BeFalse();
        }

        [Test]
        public async Task Exists_WithExistentPayment_ShouldReturnTrue()
        {
            var id = TestData.InsertedPayments.KFC.Id;
            
            var result = await _paymentRepository.ExistsAsync(id);

            result.Should().BeTrue();
        }

        [Test]
        public async Task Exists_WithNonExistentPayment_ShouldReturnFalse()
        {
            var result = await _paymentRepository.ExistsAsync(Guid.Empty);

            result.Should().BeFalse();
        }

        private async Task AddPayments(params Payment[] payments)
        {
            foreach (var payment in payments)
                await _paymentRepository.AddPaymentAsync(payment);
        }
    }
}