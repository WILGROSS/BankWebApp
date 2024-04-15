using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using ViewModels;

namespace BankWebApp.Pages.ViewSingleCustomer
{
	[Authorize(Roles = "Cashier")]
	public class IndexModel : PageModel
	{
		public readonly IViewSingleCustomerService _viewSingleCustomerService;
		public readonly IAccountService _accountService;

		public IndexModel(IViewSingleCustomerService viewCustomerService, IAccountService accountService)
		{
			_viewSingleCustomerService = viewCustomerService;
			_accountService = accountService;
		}
		[BindProperty(SupportsGet = true)]
		public ViewSingleCustomerViewModel _customer { get; set; }
		public EditCustomerViewModel _editableViewModel { get; set; }
		public void OnGet(int id)
		{
			_customer = _viewSingleCustomerService.GetCustomer(id);
		}

		public IActionResult OnPostToggleActive(int id)
		{
			_editableViewModel = _viewSingleCustomerService.GetEditableViewModel(id);
			if (_viewSingleCustomerService.ToggleCustomerActiveStatus(_editableViewModel))
			{
				var statusWord = _editableViewModel.IsDeleted ? "activated" : "deactivated";
				TempData["SuccessMessage"] = $"Succesfully {statusWord} customer's account";
				return RedirectToPage("index", new { id });
			}

			_customer = _viewSingleCustomerService.GetCustomer(id);
			return Page();
		}
		public IActionResult OnPostAddAccount(int id)
		{
			if (_accountService.AddNewAccount(id, out int newAccountId))
			{
				TempData["SuccessMessage"] = $"Succesfully added account {newAccountId} for {_customer.GivenName} {_customer.SurName}";
				return RedirectToPage("index", new { id });
			}

			_customer = _viewSingleCustomerService.GetCustomer(id);
			return Page();
		}

		public IActionResult OnPostDeleteAccount(int accountId, int id)
		{
			if (_accountService.TryDeleteAccount(accountId))
			{
				TempData["SuccessMessage"] = $"Succesfully deleted account {accountId}";
				return RedirectToPage("index", new { id });
			}

			ModelState.AddModelError($"BalanceNotZero{accountId}", $"Account {accountId} can only be deleted once it's balance is 0 kr");
			_customer = _viewSingleCustomerService.GetCustomer(id);
			return Page();
		}
	}
}