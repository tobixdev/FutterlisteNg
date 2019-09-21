using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using FutterlisteNg.Data;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Data.Repository;
using FutterlisteNg.Domain.Service;
using NUnit.Framework;

namespace FutterlisteNg.Tests.Domain
{
    [TestFixture]
    public class PaymentServiceTests
    {
        private IPaymentService _sut;
        private IPaymentRepository _paymentRepository;

        [SetUp]
        public void SetUp()
        {
            _paymentRepository = A.Fake<IPaymentRepository>();
            _sut = new PaymentService(_paymentRepository);
        }

        [Test]
        public async Task Add_ShouldDelegateCallToRepository()
        {
            var payment = new Payment();

            await _sut.AddAsync(payment);

            A.CallTo(() => _paymentRepository.AddPaymentAsync(payment)).MustHaveHappened(1, Times.Exactly);
        }

        [Test]
        public async Task FindAllAsync_ShouldReturnResultFromRepository()
        {
            var payments = new List<Payment>();
            A.CallTo(() => _paymentRepository.FindAllAsync()).Returns(payments);

            var result = await _sut.FindAllAsync();

            result.Should().BeSameAs(payments);
        }

        [Test]
        public async Task Delete_WithNonExistentPayment_ShouldThrowNotFoundException()
        {
            Func<Task> act = async () => await _sut.DeleteAsync(Guid.Empty);

            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Payment with Id '00000000-0000-0000-0000-000000000000' not found");
        }

        [Test]
        public async Task Delete_WithExistentPayment_ShouldDelegateCallToRepository()
        {
            var id = new Guid("497AABB3-8F7C-4173-964F-564DB53B0732");
            A.CallTo(() => _paymentRepository.ExistsAsync(id)).Returns(true);
            
            await _sut.DeleteAsync(id);

            A.CallTo(() => _paymentRepository.DeleteAsync(id)).MustHaveHappened(1, Times.Exactly);
        }
    }
}