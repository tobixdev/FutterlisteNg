using System.Collections.Generic;
using FluentValidation.Results;

namespace FutterlisteNg.Domain.Exception
{
    public class ValidationException : System.Exception
    {
        public IList<ValidationFailure> Failures { get; }

        public ValidationException(IList<ValidationFailure> failures) : base($"Validation failed with {failures.Count} errors.")
        {
            Failures = failures;
        }
    }
}