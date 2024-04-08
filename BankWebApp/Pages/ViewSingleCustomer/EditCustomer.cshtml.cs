using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using ViewModels;

namespace BankWebApp.Pages.ViewSingleCustomer
{
	[Authorize(Roles = "Cashier")]
	public class EditCustomerModel : PageModel
	{
		public readonly IViewSingleCustomerService _viewCustomerService;
		public EditCustomerModel(IViewSingleCustomerService viewCustomerService)
		{
			_viewCustomerService = viewCustomerService;
		}
		public ViewSingleCustomerViewModel _customer { get; set; }
		public void OnGet(int id)
		{
			_customer = _viewCustomerService.GetCustomer(id);
		}
	}
}
