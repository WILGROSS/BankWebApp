using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using ViewModels;

namespace BankWebApp.Pages.ViewSingleCustomer
{
	[Authorize(Roles = "Cashier")]
	public class ViewAccountModel : PageModel
	{
		public readonly IAccountService _accountService;

		public ViewAccountModel(IAccountService accountService, IViewSingleCustomerService viewSingleCustomerService)
		{
			_accountService = accountService;
		}
		public AccountViewModel _account { get; set; }
		public int _customerId { get; set; }
		public void OnGet(int id)
		{
			_account = _accountService.GetAccount(id);
			_customerId = _accountService.GetCustomerIdFromAccountId(id);
		}
	}
}
