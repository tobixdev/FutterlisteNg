using FluentValidation;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Domain.Model;
using FutterlisteNg.Domain.Validation;
using NUnit.Framework;

namespace FutterlisteNg.Tests.Domain.Validation
{
    [TestFixture]
    public class UserUpdateValidatorTests : UserValidatorTestsBase
    {
        protected override IValidator<User> CreateValidator()
        {
            return new UserUpdateValidator(UserRepository);
        }

        protected override User CreateValidUser()
        {
            return new User("Existing", "real name");
        }
    }
}