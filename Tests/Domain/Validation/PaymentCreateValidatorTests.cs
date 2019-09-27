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
    public class CreatePaymentValidatorTests : PaymentValidatorTestsBase
    {
        
        protected override IValidator<Payment> CreateValidator()
        {
            return new PaymentCreateValidator(UserRepository);
        }

        protected override PaymentBuilder CreatePaymentBuilder(string payedBy)
        {
            return new PaymentBuilder(payedBy);
        }

        [Test]
        public void Validate_WithIdSet_ShouldReturnInvalidResult()
        {
            var payment = PaymentBuilder.Valid.WithId(new Guid("EA36362C-B35E-479B-8802-EAE46D6525C4")).Build();

            var result = Sut.Validate(payment);
            
            AssertSingleError(result, "Id", "'Id' must be equal to '00000000-0000-0000-0000-000000000000'.");
        }
    }
}