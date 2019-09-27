using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Data.Repository;
using FutterlisteNg.Domain.Model;
using FutterlisteNg.Domain.Validation;
using NUnit.Framework;

namespace FutterlisteNg.Tests.Domain.Validation
{
    public abstract class UserValidatorTestsBase
    {
        protected IValidator<User> Sut;
        protected IUserRepository UserRepository;

        [SetUp]
        public void SetUpBase()
        {
            UserRepository = A.Fake<IUserRepository>();
            // TODO better solution needed
            A.CallTo(() => UserRepository.Exists("Existing")).Returns(true);
            A.CallTo(() => UserRepository.Exists("Eric")).Returns(true);
            A.CallTo(() => UserRepository.Exists("Stan")).Returns(true);
            A.CallTo(() => UserRepository.Exists("Token")).Returns(true);
            Sut = CreateValidator();
        }
        
        protected abstract IValidator<User> CreateValidator();
        protected abstract User CreateValidUser();
        
        [Test]
        public void Validate_WithValidUser_ShouldReturnValidResult()
        {
            var user = CreateValidUser();

            var validationResult = Sut.Validate(user);

            validationResult.IsValid.Should().BeTrue();
        }

        [Test]
        public void Validate_WithNoUsername_ShouldReturnInvalidResult()
        {
            var user = new User(null, "real name");

            var validationResult = Sut.Validate(user);

            AssertSingleError(validationResult, "Username", "'Username' must not be empty.");
        }

        [Test]
        public void Validate_WithTooShortUsername_ShouldReturnInvalidResult()
        {
            var user = new User("u", "real name");

            var validationResult = Sut.Validate(user);

            AssertSingleError(validationResult, "Username", "The length of 'Username' must be at least 2 characters. You entered 1 characters.");
        }

        [Test]
        public void Validate_WithNoName_ShouldReturnInvalidResult()
        {
            var user = CreateValidUser();
            user.Name = null;

            var validationResult = Sut.Validate(user);

            AssertSingleError(validationResult, "Name", "'Name' must not be empty.");
        }

        [Test]
        public void Validate_WithTooShortName_ShouldReturnInvalidResult()
        {
            var user = CreateValidUser();
            user.Name = "a";

            var validationResult = Sut.Validate(user);

            AssertSingleError(validationResult, "Name", "The length of 'Name' must be at least 2 characters. You entered 1 characters.");
        }

        protected void AssertSingleError(ValidationResult validationResult, string propertyName, string errorMessage)
        {
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCount(1);
            validationResult.Errors[0].PropertyName.Should().Be(propertyName);
            validationResult.Errors[0].ErrorMessage.Should().Be(errorMessage);
        }
        
    }
}