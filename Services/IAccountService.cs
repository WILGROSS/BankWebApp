using ViewModels;

namespace Services
{
	public interface IAccountService
	{
		public AccountViewModel GetAccount(int id);
		public List<TransactionViewModel> LoadMoreTransactions(int id, int offset);
	}
}
