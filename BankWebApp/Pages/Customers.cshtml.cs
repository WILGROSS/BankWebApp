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
        public List<CustomersViewmodel> _customers { get; set; }
        public CustomersModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public void OnGet()
        {
            _customers = _context.Customers.Select(s => new CustomersViewmodel
            {
                CustomerId = s.CustomerId,
                Name = $"{s.Givenname} {s.Surname}",
                City = s.City,
                Country = s.Country
            }).ToList();
        }
    }
}
