using ViewModels;

namespace Services
{
	public interface ITransactionService
	{
		public AccountViewModel GetAccount(int id);
		public bool SaveNewTransaction(TransactionViewModel newTransactionViewModel, int id);
	}
}
