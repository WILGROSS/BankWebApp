using BankAppWeb.ViewModels;
using BankWebApp.Data;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankWebApp.Pages
{
    public class CustomersModel : PageModel
    {
        public readonly ApplicationDbContext _context;
        private readonly ILogger<IndexModel> _logger;

        public CustomersViewmodel _customers;
        public CustomersModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public void OnGet()
        {
            _customers = _context.Customers.Select(s => new CustomerViewmodel
            {
                CustomerId = s.CustomerId,
                Name = s.Givenname,
                Surname = s.Surname,
                City = s.City,
                Country = s.Country

            }).ToList();
        }
    }
}
