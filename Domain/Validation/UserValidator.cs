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
    }

    public class UserCreateValidator : UserValidatorBase
    {
        public UserCreateValidator(IUserRepository userRepository) : base()
        {
            RuleFor(u => u.Username)
                .MustAsync(async (username, _) => !await userRepository.Exists(username))
                .WithMessage(p => $"User with username '{p.Username}' already exists.")
                .When(u => !string.IsNullOrEmpty(u.Username));
        }
    }

    public class UserUpdateValidator : UserValidatorBase
    {
        public UserUpdateValidator(IUserRepository userRepository) : base()
        {
            RuleFor(u => u.Username)
                .MustAsync(async (username, _) => await userRepository.Exists(username))
                .WithMessage(p => $"User with username '{p.Username}' does not exist.")
                .When(u => !string.IsNullOrEmpty(u.Username));
        }
    }
}