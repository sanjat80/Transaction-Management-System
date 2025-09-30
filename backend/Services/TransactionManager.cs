using TransactionManagementSystem.Exceptions;
using TransactionManagementSystem.Models;
using TransactionManagementSystem.Models.Dtos;
using TransactionManagementSystem.Services.Contracts;

namespace TransactionManagementSystem.Services
{
    public class TransactionManager : ITransactionManager
    {
        private Repositories.Contracts.ITransactionManager _transactionRepo;
        private static readonly Random _random = new Random();

        public TransactionManager(Repositories.Contracts.ITransactionManager transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _transactionRepo.GetAllTransactions();
        }

        public Transaction CreateTransaction(TransactionCreationDto transaction)
        {
            var newTransaction = new Transaction()
            {
                TransactionDate = transaction.TransactionDate,
                AccountNumber = transaction.AccountNumber,
                AccountHolderName = transaction.AccountHolderName,
                Amount = transaction.Amount,
                Status = GetRandomStatus()
            };
            _transactionRepo.SaveTransaction(newTransaction);

            return newTransaction;
        }

        private static Status GetRandomStatus()
        {
            var values = (Status[])Enum.GetValues(typeof(Status));
            return values[_random.Next(values.Length)];
        }

    }
}
