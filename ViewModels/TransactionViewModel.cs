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
		[Required]
		public decimal Amount { get; set; }
		[Required]
		public decimal Balance { get; set; }
		[Required]
		public DateOnly Date { get; set; }
	}
}
