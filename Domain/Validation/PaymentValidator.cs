using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Data.Repository;

namespace FutterlisteNg.Domain.Validation
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator(IUserRepository userRepository)
        {
            RuleFor(p => p.PayedBy)
                .NotNull();

            RuleFor(p => p.PayedBy)
                .MustAsync(async (username, _) => await userRepository.Exists(username))
                .WithMessage((p, _) => $"User '{p.PayedBy}' does not exist.")
                .When(p => !string.IsNullOrEmpty(p.PayedBy));

            RuleFor(p => p.Description)
                .NotNull()
                .MinimumLength(4);

            RuleFor(p => p.PaymentLines)
                .NotNull();

            RuleForEach(p => p.PaymentLines)
                .SetValidator(new PaymentLineValidator(userRepository));
        }

        private class PaymentLineValidator : AbstractValidator<PaymentLine>
        {
            public PaymentLineValidator(IUserRepository userRepository)
            {
                RuleFor(pl => pl.Amount)
                    .GreaterThan(0);

                RuleFor(pl => pl.PaidFor)
                    .NotNull();

                RuleFor(pl => pl.PaidFor)
                    .MustAsync(async (username, _) => await userRepository.Exists(username))
                    .WithMessage((pl, _) => $"User '{pl.PaidFor}' does not exist.")
                    .When(pl => !string.IsNullOrEmpty(pl.PaidFor));
            }
        }
    }
}