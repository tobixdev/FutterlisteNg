
using FluentValidation.Results;
using FutterlisteNg.Domain.Exception;

namespace FutterlisteNg.Domain.Extensions
{
    public static class ValidationResultExtensions
    {
        public static void ThrowIfInvalid(this ValidationResult validationResult)
        {
            if(!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }
    }
}