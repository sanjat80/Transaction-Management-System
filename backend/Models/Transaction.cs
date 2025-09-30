using System.Text.Json.Serialization;

namespace TransactionManagementSystem.Models
{
    public class Transaction
    {
        public DateTime TransactionDate { get; set; }
        public string AccountNumber { get; set; } = null!;
        public string AccountHolderName { get; set; } = null!;
        public double Amount { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; } 
    }
}