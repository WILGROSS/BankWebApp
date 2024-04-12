using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class TransactionViewModel
    {
        [Required]
        public int TransactionId { get; set; }
        [Required]
        public int AccountId { get; set; }
        [Required(ErrorMessage = "Please enter a message")]
        [DisplayName("Message")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Message must be between 1 and 100 characters")]
        public string Operation { get; set; }
        [Required]
        public string Type { get; set; }
        [Required(ErrorMessage = "The amount must be between 100 and 100000")]
        [Range(typeof(decimal), "100", "100000", ErrorMessage = "The amount must be between 100 and 100000")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "The amount have more than 2 decimals")]
        public decimal Amount { get; set; }
        [Required]
        public decimal Balance { get; set; }
        [Required]
        public DateOnly Date { get; set; }
    }
}
