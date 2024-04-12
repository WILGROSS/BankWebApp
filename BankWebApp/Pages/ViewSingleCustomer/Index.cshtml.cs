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
		public IndexModel(IViewSingleCustomerService viewCustomerService)
		{
			_viewSingleCustomerService = viewCustomerService;
		}
		[BindProperty(SupportsGet = true)]
		public ViewSingleCustomerViewModel _customer { get; set; }
		public EditCustomerViewModel _editableViewModel { get; set; }
		public void OnGet(int id)
		{
			_customer = _viewSingleCustomerService.GetCustomer(id);
		}

		public IActionResult OnPost(int id)
		{
			if (ModelState.IsValid)
			{
				_editableViewModel = _viewSingleCustomerService.GetEditableViewModel(id);
				if (_viewSingleCustomerService.ToggleCustomerActiveStatus(_editableViewModel))
				{
					TempData["SuccessMessage"] = $"Succesfully updated {_customer.GivenName} {_customer.SurName}'s info!";
					return RedirectToPage("index", new { id });
				}
			}
			return RedirectToPage("index", new { id });
		}
	}
}