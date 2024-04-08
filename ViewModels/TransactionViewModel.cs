namespace ViewModels
{
	public class TransactionViewModel
	{
		public string TransactionId { get; set; }
		public string Operation { get; set; }
		public decimal Amount { get; set; }
		public decimal Balance { get; set; }
		public DateOnly Date { get; set; }
	}
}
