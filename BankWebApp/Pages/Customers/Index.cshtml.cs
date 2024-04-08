using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using ViewModels;

namespace BankWebApp.Pages.Customers
{
	public class IndexModel : PageModel
	{
		public readonly ICustomersService _customersService;
		public List<CustomersViewModel> _customers { get; set; }
		public List<CustomersViewModel> _vipCustomers { get; set; }
		public List<string> AllCountries { get; set; } = new List<string>();
		public List<string> SelectedCountries { get; set; } = new List<string>();
		public int TotalPages { get; set; }
		public int TotalCount { get; set; }
		public int LoadedRows { get; set; }
		public int CurrentPage { get; set; }

		public IndexModel(ICustomersService customerService)
		{
			_customersService = customerService;
		}

		public void OnGet(string sortColumn = "Name", string sortOrder = "asc", string searchQuery = "", int? pageNo = 1, int? loadedRows = null, List<string> selectedCountries = null, string action = "")
		{
			CurrentPage = pageNo ?? 1;

			LoadedRows = loadedRows ?? 32;
			AllCountries = _customersService.GetAllCountries();

			if (action == "clear")
			{
				SelectedCountries = new List<string>();
			}
			else
			{
				SelectedCountries = selectedCountries ?? new List<string>();
			}

			var customerResult = _customersService.GetCustomers(sortColumn, sortOrder, searchQuery, LoadedRows, SelectedCountries, CurrentPage);
			_customers = customerResult.Customers;
			_vipCustomers = customerResult.VipCustomers;
			TotalCount = customerResult.TotalCount;
			TotalPages = customerResult.TotalPages;
		}
	}
}
