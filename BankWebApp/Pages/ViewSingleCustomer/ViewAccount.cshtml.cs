using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using ViewModels;

namespace BankWebApp.Pages.ViewSingleCustomer
{
	public class ViewAccountModel : PageModel
	{
		public readonly IAccountService _accountService;

		public ViewAccountModel(IAccountService accountService)
		{
			_accountService = accountService;
		}
		[BindProperty(SupportsGet = true)]
		public AccountViewModel _account { get; set; }
		public void OnGet(int id)
		{
			_account = _accountService.GetAccount(id);
		}
	}
}
