using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using TransactionManagementSystem.Exceptions;
using TransactionManagementSystem.Models;
using TransactionManagementSystem.Models.Mappers;
using TransactionManagementSystem.Repositories.Contracts;

namespace TransactionManagementSystem.Repositories
{
    public class TransactionManager : ITransactionManager
    {
        private string _csvPath;
        private ILogger<TransactionManager> _logger;

        public TransactionManager(ILogger<TransactionManager> logger)
        {
            string? solutionDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)
                                ?.Parent?.Parent?.Parent?.Parent?.FullName;

            if (solutionDir == null)
            {
                throw new InvalidOperationException("Cannot determine solution directory.");
            }

            _csvPath = Path.Combine(solutionDir, "backend","Repositories", "Data", "data.csv");

            if (!File.Exists(_csvPath))
            {
                throw new FileNotFoundException(
                    $"CSV file not found at: {_csvPath}. " +
                    $"Current directory: {Directory.GetCurrentDirectory()}");
            }

            _logger = logger;
        }
        public IEnumerable<Transaction> GetAllTransactions()
        {
            if (!File.Exists(_csvPath))
            {
                _logger.LogWarning("CSV data file could not be found: {_csvPath}", _csvPath);
                return new List<Transaction>();
            }
            
            try
            {
                using (var reader = new StreamReader(_csvPath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<TransactionMapper>();
                    var transactions = csv.GetRecords<Transaction>().ToList();
                    return transactions;
                }
            }
            catch(IOException ioex)
            {
                _logger.LogError("I/O error happened while trying to read data from csv file at path: {_csvPath}", _csvPath);
                throw new Exceptions.TransactionDataException("Error while reading transactions from csv file.", ioex);
            }
            catch(CsvHelperException csvEx)
            {
                _logger.LogError("CSV parsing data error in file at path: {_csvPath}", _csvPath);
                throw new Exceptions.TransactionDataException("Error while trying to parse data from csv file.", csvEx);
            }
        }

        public void SaveTransaction(Transaction transaction)
        {
            var fileExists = File.Exists(_csvPath);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = !fileExists,
                NewLine = Environment.NewLine
            };
            try
            {
                using (var writer = new StreamWriter(_csvPath, true))
                using (var csv = new CsvWriter(writer, config))
                {
                    if (!fileExists)
                    {
                        csv.WriteHeader<Transaction>();
                        csv.NextRecord();
                    }

                    csv.WriteRecord(transaction);
                    csv.NextRecord();
                }
            }
            catch (IOException ioEx)
            {
                _logger.LogError(ioEx, "I/O error happened while trying to write data to csv file at path: {_csvPath}", _csvPath);
                throw new TransactionDataException("Error while writing transaction to CSV file.", ioEx);
            }
            catch (CsvHelperException csvEx)
            {
                _logger.LogError("CSV parsing data error in file at path: {_csvPath}", _csvPath);
                throw new Exceptions.TransactionDataException("Error while trying to parse data.", csvEx);
            }
        }
    }
}