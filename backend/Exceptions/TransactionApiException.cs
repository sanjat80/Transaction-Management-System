namespace TransactionManagementSystem.Exceptions
{
    public class TransactionApiException: Exception
    {
        public TransactionApiException(string message, Exception innerException): base(message, innerException)
        {

        }
    }
}
