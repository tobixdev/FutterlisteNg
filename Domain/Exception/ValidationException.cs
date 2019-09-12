namespace FutterlisteNg.Domain.Exception
{
    public class ValidationException : System.Exception
    {
        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}