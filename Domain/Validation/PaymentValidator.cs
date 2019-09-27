using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Data.Repository;

namespace FutterlisteNg.Domain.Validation
{
    public abstract class PaymentValidatorBase : AbstractValidator<Payment>
    {
        protected PaymentValidatorBase(IUserRepository userRepository)
        {
            RuleFor(p => p.PayedBy)
                .NotEmpty();

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
                    .NotEmpty();

                RuleFor(pl => pl.PaidFor)
                    .MustAsync(async (username, _) => await userRepository.Exists(username))
                    .WithMessage((pl, _) => $"User '{pl.PaidFor}' does not exist.")
                    .When(pl => !string.IsNullOrEmpty(pl.PaidFor));
            }
        }
    }

    public class PaymentCreateValidator : PaymentValidatorBase
    {
        public PaymentCreateValidator(IUserRepository userRepository) : base(userRepository)
        {
            RuleFor(p => p.Id)
                .Equal(Guid.Empty);
        }
    }

    public class PaymentUpdateValidator : PaymentValidatorBase
    {
        public PaymentUpdateValidator(IUserRepository userRepository, IPaymentRepository paymentRepository) : base(userRepository)
        {
            RuleFor(p => p.Id)
                .NotNull();

            RuleFor(p => p.Id)
                .MustAsync(async (id, _) => await paymentRepository.ExistsAsync(id))
                .WithMessage(p => $"Payment with id '{p.Id}' does not exist.");
        }
    }
}