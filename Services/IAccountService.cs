using ViewModels;

namespace Services
{
	public interface IAccountService
	{
		public AccountViewModel GetAccount(int id);
	}
}
