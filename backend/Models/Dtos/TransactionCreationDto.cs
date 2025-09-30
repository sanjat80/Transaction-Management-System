using System.ComponentModel.DataAnnotations;

namespace TransactionManagementSystem.Models.Dtos
{
    public class TransactionCreationDto
    {
        [Required(ErrorMessage = "Transaction date is required")]
        public DateTime TransactionDate { get; set; }
        [Required(ErrorMessage = "Account number is required")]
        [RegularExpression(@"^\d{4}-\d{4}-\d{4}$", ErrorMessage = "Account number must be in format xxxx-xxxx-xxxx")]
        public string AccountNumber { get; set; } = null!;
        [Required(ErrorMessage = "Account holder name is required")]
        [StringLength(100, ErrorMessage = "Account holder name cannot be longer than 100 characters")]
        public string AccountHolderName { get; set; } = null!;
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public double Amount { get; set; }
    }
}
