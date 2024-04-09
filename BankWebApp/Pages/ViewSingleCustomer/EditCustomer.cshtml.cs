using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using ViewModels;

namespace BankWebApp.Pages.ViewSingleCustomer
{
	[Authorize(Roles = "Cashier")]
	public class EditCustomerModel : PageModel
	{
		public readonly IViewSingleCustomerService _viewSingleCustomerService;
		public EditCustomerModel(IViewSingleCustomerService viewCustomerService)
		{
			_viewSingleCustomerService = viewCustomerService;
		}

		[BindProperty(SupportsGet = true)]
		public EditCustomerViewModel _customer { get; set; }
		public void OnGet(int id)
		{
			_customer = _viewSingleCustomerService.GetEditableViewModel(id);
		}

		public IActionResult OnPost(int id)
		{
			if (ModelState.IsValid)
			{
				if (_viewSingleCustomerService.UpdateCustomer(_customer))
				{
					TempData["SuccessMessage"] = $"Succesfully updated {_customer.GivenName} {_customer.SurName}'s info!";
					return RedirectToPage("index", new { id });
				}
			}

			return Page();
		}
	}
}
