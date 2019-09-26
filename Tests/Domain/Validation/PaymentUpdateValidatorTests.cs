using System;
using FakeItEasy;
using FluentValidation;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Data.Model.Builder;
using FutterlisteNg.Data.Repository;
using FutterlisteNg.Domain.Service;
using FutterlisteNg.Domain.Validation;
using NUnit.Framework;

namespace FutterlisteNg.Tests.Domain.Validation
{
    [TestFixture]
    public class UpdatePaymentValidatorTests : PaymentValidatorTestsBase
    {
        private static readonly Guid s_existingId = new Guid("07C436A3-5458-4AF3-B786-D94E9310AB35");
        
        private IPaymentRepository _paymentRepository;
        
        protected override IValidator<Payment> CreateValidator()
        {
            _paymentRepository = A.Fake<IPaymentRepository>();
            A.CallTo(() => _paymentRepository.ExistsAsync(s_existingId)).Returns(true);
            return new PaymentUpdateValidator(UserRepository, _paymentRepository);
        }

        protected override PaymentBuilder CreatePaymentBuilder(string payedBy)
        {
            return new PaymentBuilder(payedBy).WithId(s_existingId);
        }

        [Test]
        public void Validate_WithNonExistingId_ShouldReturnInvalidResult()
        {
            var id = new Guid("7960D2F3-3A6F-4792-A35B-535EC4971034");
            var payment = PaymentBuilder.Valid.WithId(id).Build();

            var result = Sut.Validate(payment);
            
            AssertSingleError(result, "Id", "");
        }
    }
}