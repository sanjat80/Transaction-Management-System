using CsvHelper.Configuration;

namespace TransactionManagementSystem.Models.Mappers
{
    public class TransactionMapper:ClassMap<Transaction>
    {
        public TransactionMapper()
        {
            Map(t => t.TransactionDate).Name("Transaction Date");
            Map(t => t.AccountNumber).Name("Account Number");
            Map(t => t.AccountHolderName).Name("Account Holder Name");
            Map(t => t.Amount).Name("Amount");
            Map(t => t.Status).Name("Status");
        }
    }
}
