using FakeItEasy;
using FluentValidation;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Domain.Model;
using FutterlisteNg.Domain.Validation;
using NUnit.Framework;

namespace FutterlisteNg.Tests.Domain.Validation
{
    [TestFixture]
    public class UserCreateValidatorTests : UserValidatorTestsBase
    {
        protected override IValidator<User> CreateValidator()
        {
            return new UserCreateValidator(UserRepository);
        }

        [Test]
        public void Validate_WithExistingUsername_ShouldReturnInvalidResult()
        {
            A.CallTo(() => UserRepository.Exists("Username")).Returns(true);
            var user = new User("Username", "real name");

            var validationResult = Sut.Validate(user);

            AssertSingleError(validationResult, "Username", "User with username 'Username' already exists.");
        }
    }
}