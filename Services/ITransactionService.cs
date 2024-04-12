using ViewModels;

namespace Services
{
	public interface ITransactionService
	{
		public TransactionViewModel GetNewTransaction(AccountViewModel account, string? message, string type);
		public bool SaveNewTransaction(TransactionViewModel newTransactionViewModel);
	}
}
