using FluentValidation;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Data.Repository;

namespace FutterlisteNg.Domain.Validation
{
    public abstract class UserValidatorBase : AbstractValidator<User>
    {
        protected UserValidatorBase()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(u => u.Username)
                .NotEmpty()
                .MinimumLength(2);
        }

        protected bool ValidUserName(User user)
        {
            return !string.IsNullOrEmpty(user.Username) && user.Username.Length >= 2;
        }
    }

    public class UserCreateValidator : UserValidatorBase
    {
        public UserCreateValidator(IUserRepository userRepository)
        {
            RuleFor(u => u.Username)
                .MustAsync(async (username, _) => !await userRepository.Exists(username))
                .WithMessage(p => $"User with username '{p.Username}' already exists.")
                .When(ValidUserName);
        }
    }

    public class UserUpdateValidator : UserValidatorBase
    {
        public UserUpdateValidator(IUserRepository userRepository)
        {
            RuleFor(u => u.Username)
                .MustAsync(async (username, _) => await userRepository.Exists(username))
                .WithMessage(p => $"User with username '{p.Username}' does not exist.")
                .When(ValidUserName);
        }
    }
}