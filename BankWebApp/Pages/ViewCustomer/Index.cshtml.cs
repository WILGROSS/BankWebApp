using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using ViewModels;

namespace BankWebApp.Pages.ViewCustomer
{
	public class IndexModel : PageModel
	{
		public readonly IViewCustomerService _viewCustomerService;
		public IndexModel(IViewCustomerService viewCustomerService)
		{
			_viewCustomerService = viewCustomerService;
		}
		public ViewCustomerViewModel _customer { get; set; }
		public void OnGet(int id)
		{
			_customer = _viewCustomerService.GetCustomer(id);
		}
	}
}