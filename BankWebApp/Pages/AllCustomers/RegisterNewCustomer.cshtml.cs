using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;
using ViewModels;

namespace BankWebApp.Pages.AllCustomers
{
	[Authorize(Roles = "Cashier")]
	public class RegisterNewCustomerModel : PageModel
	{
		public readonly IViewSingleCustomerService _viewSingleCustomerService;
		public List<SelectListItem> _gendersList { get; set; }
		public List<SelectListItem> _countriesList { get; set; }
		public RegisterNewCustomerModel(IViewSingleCustomerService viewCustomerService)
		{
			_viewSingleCustomerService = viewCustomerService;
		}

		[BindProperty(SupportsGet = true)]
		public EditCustomerViewModel _newCustomer { get; set; }
		public void OnGet()
		{
			_newCustomer = _viewSingleCustomerService.GetNewCustomerViewModel();
			_gendersList = _viewSingleCustomerService.GetGenderSelectListItems();
			_countriesList = _viewSingleCustomerService.GetCountrySelectListItems();
		}

		public IActionResult OnPost()
		{
			if (ModelState.IsValid)
			{
				if (_viewSingleCustomerService.SaveNewCustomer(_newCustomer, out int newCustomerId))
				{
					TempData["SuccessMessage"] = $"Succesfully created a customer account for {_newCustomer.GivenName} {_newCustomer.SurName}";
					return RedirectToPage("/ViewSingleCustomer/Index", new { id = newCustomerId });
				}
			}
			_gendersList = _viewSingleCustomerService.GetGenderSelectListItems();
			_countriesList = _viewSingleCustomerService.GetCountrySelectListItems();
			return Page();
		}
	}
}
