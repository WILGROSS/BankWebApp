namespace ViewModels
{
	public class AccountViewModel
	{
		public int AccountId { get; set; }
		public DateOnly DateCreated { get; set; }
		public decimal Balance { get; set; }
		public string Frequency { get; set; }
		public List<TransactionViewModel> Transactions { get; set; }
	}
}
