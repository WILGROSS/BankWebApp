using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankWebApp.Pages
{
	public class ViewCustomerModel : PageModel
	{
		public readonly IViewCustomerService _viewCustomerService;
		public ViewCustomersViewModel _customer { get; set; }
		public void OnGet(int customerId)
		{
			_customer = _viewCustomerService.GetCustomer(customerId);
		}
	}
}
