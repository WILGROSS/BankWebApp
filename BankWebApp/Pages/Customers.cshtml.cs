using BankWebApp.Services;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankWebApp.Pages
{
    public class CustomersModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<IndexModel> _logger;
        public List<CustomerViewmodel> _customers { get; set; }
        public CustomersModel(ILogger<IndexModel> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }
        public void OnGet(string sortColumn, string sortOrder, string searchQuery)
        {
            _customers = _customerService.GetCustomers(sortColumn, sortOrder, searchQuery);
        }
    }
}
