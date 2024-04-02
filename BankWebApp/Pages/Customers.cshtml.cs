using BankWebApp.Services;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankWebApp.Pages
{
    public class CustomersModel : PageModel
    {
        private readonly ICustomerService _customerService;
        public List<CustomerViewmodel> _customers { get; set; }
        public List<string> AllCountries { get; set; } = new List<string>();
        public List<string> SelectedCountries { get; set; } = new List<string>();
        public int TotalCount { get; set; }
        public int LoadedRows { get; set; }

        public CustomersModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public void OnGet(string sortColumn, string sortOrder, string searchQuery = "", int? loadedRows = null, List<string> selectedCountries = null, string action = "")
        {
            loadedRows ??= 16;
            LoadedRows = loadedRows.Value;

            if (action == "clear")
            {
                SelectedCountries = new List<string>();
                searchQuery = "";
            }
            else
            {
                SelectedCountries = selectedCountries ?? new List<string>();
            }

            AllCountries = _customerService.GetAllCountries();

            var customerResult = _customerService.GetCustomers(sortColumn, sortOrder, searchQuery, LoadedRows, SelectedCountries);
            _customers = customerResult.Customers;
            TotalCount = customerResult.TotalCount;
        }

    }
}
