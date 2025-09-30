using System.Xml.Serialization;
using TransactionManagementSystem.Models;

namespace TransactionManagementSystem.Repositories.Contracts
{
    public interface ITransactionManager
    {
        IEnumerable<Transaction> GetAllTransactions();
        void SaveTransaction(Transaction transaction);
    }
}