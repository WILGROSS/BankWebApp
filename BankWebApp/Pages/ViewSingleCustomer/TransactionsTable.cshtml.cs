using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using ViewModels;

namespace BankWebApp.Pages.ViewSingleCustomer
{
	[Authorize(Roles = "Cashier")]
	public class TransactionsTableModel : PageModel
	{
		public readonly IAccountService _accountService;
		public List<TransactionViewModel> _transactions;
		public TransactionsTableModel(IAccountService accountService)
		{
			_accountService = accountService;
		}
		public void OnGet(int id, int offset)
		{
			_transactions = _accountService.LoadMoreTransactions(id, offset);
		}
	}
}
