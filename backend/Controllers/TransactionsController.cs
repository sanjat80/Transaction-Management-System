using Microsoft.AspNetCore.Mvc;
using TransactionManagementSystem.Models;
using TransactionManagementSystem.Models.Dtos;

namespace TransactionManagementSystem.Controllers
{
    [ApiController]
    [Route("/api/transactions")]
    public class TransactionsController: ControllerBase
    {
        private Services.Contracts.ITransactionManager _transactionService;
        private ILogger<TransactionsController> _logger;
        public TransactionsController(Services.Contracts.ITransactionManager transactionService,
            ILogger<TransactionsController> logger)
        {
            _transactionService = transactionService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Transaction>> GetAllTransactions()
        {
            var allTransactions = _transactionService.GetAllTransactions();

            if (allTransactions.Any())
                return Ok(allTransactions);

            return NotFound("No transactions found.");
        }

        [HttpPost]
        public ActionResult<Transaction> CreateTransaction([FromBody] TransactionCreationDto transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdTransaction = _transactionService.CreateTransaction(transaction);

            return Ok(createdTransaction);
        }
    }
}