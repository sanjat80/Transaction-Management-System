namespace TransactionManagementSystem.Exceptions
{
    public class TransactionDataException: Exception
    {
        public TransactionDataException(string message):base(message)
        {

        }
        public TransactionDataException(string message, Exception innerException): base(message, innerException)
        {

        }
    }
}
