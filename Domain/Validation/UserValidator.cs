using FluentValidation;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Data.Repository;

namespace FutterlisteNg.Domain.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator(IUserRepository userRepository)
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(u => u.Username)
                .NotEmpty()
                .MinimumLength(2)
                .MustAsync(async (username, _) => !await userRepository.Exists(username))
                .WithMessage(p => $"User with username '{p.Username}' already exists.");
        }
    }
}