using BankAppWeb.ViewModels;
using BankWebApp.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankWebApp.Pages
{
    public class IndexModel : PageModel
    {
        public readonly ApplicationDbContext _context;
        private readonly ILogger<IndexModel> _logger;

        public LandingPageViewModel _landingPageViewModel;
        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            _landingPageViewModel = new()
            {
                TotalCustomers = _context.Customers.Count(),
                TotalAccounts = _context.Accounts.Count(),
                TotalBalance = _context.Accounts.Sum(a => a.Balance)
            };
        }
    }
}
