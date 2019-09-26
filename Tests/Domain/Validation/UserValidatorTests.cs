using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Data.Repository;
using FutterlisteNg.Domain.Validation;
using NUnit.Framework;

namespace FutterlisteNg.Tests.Domain.Validation
{
    [TestFixture]
    public class UserValidatorTests
    {
        private IValidator<User> _userValidator;
        private IUserRepository _userRepository;

        [SetUp]
        public void SetUp()
        {
            _userRepository = A.Fake<IUserRepository>();
            _userValidator = new UserValidator(_userRepository);
        }

        [Test]
        public void Validate_WithValidUser_ShouldReturnValidResult()
        {
            var user = new User("Username", "real name");

            var validationResult = _userValidator.Validate(user);

            validationResult.IsValid.Should().BeTrue();
        }

        [Test]
        public void Validate_WithNoUsername_ShouldReturnInvalidResult()
        {
            var user = new User(null, "real name");

            var validationResult = _userValidator.Validate(user);

            AssertSingleError(validationResult, "Username", "'Username' must not be empty.");
        }

        [Test]
        public void Validate_WithTooShortUsername_ShouldReturnInvalidResult()
        {
            var user = new User("u", "real name");

            var validationResult = _userValidator.Validate(user);

            AssertSingleError(validationResult, "Username", "The length of 'Username' must be at least 2 characters. You entered 1 characters.");
        }

        [Test]
        public void Validate_WithExistingUsername_ShouldReturnInvalidResult()
        {
            A.CallTo(() => _userRepository.Exists("Username")).Returns(true);
            var user = new User("Username", "real name");

            var validationResult = _userValidator.Validate(user);

            AssertSingleError(validationResult, "Username", "User with username 'Username' already exists.");
        }

        [Test]
        public void Validate_WithNoName_ShouldReturnInvalidResult()
        {
            var user = new User("Username", null);

            var validationResult = _userValidator.Validate(user);

            AssertSingleError(validationResult, "Name", "'Name' must not be empty.");
        }

        [Test]
        public void Validate_WithTooShortName_ShouldReturnInvalidResult()
        {
            var user = new User("Username", "a");

            var validationResult = _userValidator.Validate(user);

            AssertSingleError(validationResult, "Name", "The length of 'Name' must be at least 2 characters. You entered 1 characters.");
        }


        private static void AssertSingleError(ValidationResult validationResult, string propertyName, string errorMessage)
        {
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCount(1);
            validationResult.Errors[0].PropertyName.Should().Be(propertyName);
            validationResult.Errors[0].ErrorMessage.Should().Be(errorMessage);
        }
        
    }
}