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
		[DisplayName("Receiving Account number")]
		public int? ReceivingAccountId { get; set; } = null;
		[DisplayName("Message")]
		public string? Operation { get; set; }
		public string Type { get; set; }
		[Required]
		public decimal Amount { get; set; }
		[DisplayName("Transaction Amount")]
		public string AmountInput { get; set; }
		[Required]
		public decimal Balance { get; set; }
		[Required]
		public DateOnly Date { get; set; }
	}
}
