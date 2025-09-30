using TransactionManagementSystem.Models;
using TransactionManagementSystem.Models.Dtos;

namespace TransactionManagementSystem.Services.Contracts
{
    public interface ITransactionManager
    {
        Transaction CreateTransaction(TransactionCreationDto transaction);
        IEnumerable<Transaction> GetAllTransactions();
    }
}