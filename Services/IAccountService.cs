using ViewModels;

namespace Services
{
	public interface IAccountService
	{
		bool AddNewAccount(int id, out int newAccountId);
		public AccountViewModel GetAccount(int id);
		public List<TransactionViewModel> LoadMoreTransactions(int id, int offset);
	}
}
