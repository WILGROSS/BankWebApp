using BankWebApp.Services;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankWebApp.Pages
{
    public class CustomersModel : PageModel
    {
        private readonly ICustomerService _customerService;
        public List<CustomerViewmodel> _customers { get; set; }
        public int TotalCount { get; set; }
        public int LoadedRows { get; set; }

        public CustomersModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public void OnGet(string sortColumn, string sortOrder, string searchQuery = "", int? loadedRows = null)
        {
            loadedRows ??= 16;
            LoadedRows = loadedRows.Value;
            var customerResult = _customerService.GetCustomers(sortColumn, sortOrder, searchQuery, LoadedRows);
            _customers = customerResult.Customers;
            TotalCount = customerResult.TotalCount;
        }
    }
}
