using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using ViewModels;

namespace BankWebApp.Pages.AllUsers
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        public readonly IAllCustomersService _customersService;
        public List<AllCustomersViewModel> _customers { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int LoadedRows { get; set; }
        public int CurrentPage { get; set; }

        public IndexModel(IAllCustomersService customerService)
        {
            _customersService = customerService;
        }

        public void OnGet(string sortColumn = "Name", string sortOrder = "asc", string searchQuery = "", int? pageNo = 1, int? loadedRows = null, List<string> selectedCountries = null, string action = "")
        {
            //CurrentPage = pageNo ?? 1;
            //LoadedRows = loadedRows ?? 50;
            //
            //var customerResult = _customersService.GetCustomers(sortColumn, sortOrder, searchQuery, LoadedRows, CurrentPage);
            //_customers = customerResult.Customers;
            //TotalCount = customerResult.TotalCount;
            //TotalPages = customerResult.TotalPages;
        }
    }
}
