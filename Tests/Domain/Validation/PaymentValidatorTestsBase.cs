using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Data.Model.Builder;
using FutterlisteNg.Data.Repository;
using FutterlisteNg.Domain.Validation;
using NUnit.Framework;

namespace FutterlisteNg.Tests.Domain.Validation
{
    public abstract class PaymentValidatorTestsBase
    {
        protected IValidator<Payment> Sut;
        protected IUserRepository UserRepository;

        [SetUp]
        public void SetUpBase()
        {
            UserRepository = A.Fake<IUserRepository>();
            A.CallTo(() => UserRepository.Exists("Existing")).Returns(true);
            Sut = CreateValidator();
        }

        protected abstract IValidator<Payment> CreateValidator();
        
        protected abstract PaymentBuilder CreatePaymentBuilder(string payedBy);

        [Test]
        public void Validate_WithValidPayment_ShouldReturnValidResult()
        {
            var payment = CreatePaymentBuilder("Existing")
                .WithDescription("I payed money!")
                .WithPaymentLine("Existing", 10)
                .Build();

            var validationResult = Sut.Validate(payment);

            validationResult.IsValid.Should().BeTrue();
        }

        [Test]
        public void Validate_WithNoPayedBy_ShouldReturnInvalidResult()
        {
            var payment = CreatePaymentBuilder(null)
                .WithDescription("I payed money!")
                .Build();

            var validationResult = Sut.Validate(payment);

            AssertSingleError(validationResult, "PayedBy", "'Payed By' must not be empty.");
        }

        [Test]
        public void Validate_WithNoDescription_ShouldReturnInvalidResult()
        {
            var payment = CreatePaymentBuilder("Existing")
                .WithDescription(null)
                .Build();

            var validationResult = Sut.Validate(payment);

            AssertSingleError(validationResult, "Description", "'Description' must not be empty.");
        }

        [Test]
        public void Validate_WithTooShortDescription_ShouldReturnInvalidResult()
        {
            var payment = CreatePaymentBuilder("Existing")
                .WithDescription("ab")
                .Build();

            var validationResult = Sut.Validate(payment);

            AssertSingleError(validationResult, "Description",
                "The length of 'Description' must be at least 4 characters. You entered 2 characters.");
        }

        [Test]
        public void Validate_WithNotExistingPayedBy_ShouldReturnInvalidResult()
        {
            var payment = CreatePaymentBuilder("NotExisting")
                .WithDescription("I payed money!")
                .Build();

            var validationResult = Sut.Validate(payment);

            AssertSingleError(validationResult, "PayedBy", "User 'NotExisting' does not exist.");
        }

        [Test]
        public void Validate_WithNegativeAmount_ShouldReturnInvalidResult()
        {
            var payment = CreatePaymentBuilder("Existing")
                .WithDescription("I payed money!")
                .WithPaymentLine("Existing", -1)
                .Build();

            var validationResult = Sut.Validate(payment);

            AssertSingleError(validationResult, "PaymentLines[0].Amount", "'Amount' must be greater than '0'.");
        }

        [Test]
        public void Validate_WithNoPaidFor_ShouldReturnInvalidResult()
        {
            var payment = CreatePaymentBuilder("Existing")
                .WithDescription("I payed money!")
                .WithPaymentLine(null, 1)
                .Build();

            var validationResult = Sut.Validate(payment);

            AssertSingleError(validationResult, "PaymentLines[0].PaidFor", "'Paid For' must not be empty.");
        }

        [Test]
        public void Validate_WithNonExistentPaidFor_ShouldReturnInvalidResult()
        {
            var payment = CreatePaymentBuilder("Existing")
                .WithDescription("I payed money!")
                .WithPaymentLine("NonExisting", 1)
                .Build();

            var validationResult = Sut.Validate(payment);

            AssertSingleError(validationResult, "PaymentLines[0].PaidFor", "User 'NonExisting' does not exist.");
        }

        protected void AssertSingleError(ValidationResult validationResult, string propertyName,
            string errorMessage)
        {
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCount(1);
            validationResult.Errors[0].PropertyName.Should().Be(propertyName);
            validationResult.Errors[0].ErrorMessage.Should().Be(errorMessage);
        }
    }
}