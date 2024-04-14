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
				TempData["SuccessMessage"] = $"Succesfully updated {_customer.GivenName} {_customer.SurName}'s info!";
				return RedirectToPage("index", new { id });
			}

			_customer = _viewSingleCustomerService.GetCustomer(id);
			return Page();
		}
		public IActionResult OnPostAddAccount(int id)
		{
			if (_accountService.AddNewAccount(id, out int newAccountId))
			{
				TempData["SuccessMessage"] = $"Succesfully added account no. {newAccountId} for {_customer.GivenName} {_customer.SurName}!";
				return RedirectToPage("index", new { id });
			}

			_customer = _viewSingleCustomerService.GetCustomer(id);
			return Page();
		}
	}
}