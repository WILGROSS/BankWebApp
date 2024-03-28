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
            var countries = _context.Customers
                                    .Select(c => c.Country)
                                    .Distinct()
                                    .ToList();

            _landingPageViewModel = new LandingPageViewModel
            {
                TotalCustomers = _context.Customers.Count(),
                TotalAccounts = _context.Accounts.Count(),
                TotalBalance = _context.Accounts.Sum(a => a.Balance),

                CountriesInfo = countries.Select(x => new CountryInfoViewModel
                {
                    Country = x,
                    TotalCustomers = _context.Customers.Count(c => c.Country == x),
                    TotalAccounts = _context.Dispositions
                                            .Where(d => d.Customer.Country == x)
                                            .Select(d => d.AccountId)
                                            .Distinct()
                                            .Count(),
                    TotalBalance = _context.Dispositions
                                           .Where(d => d.Customer.Country == x)
                                           .Select(d => d.Account)
                                           .Sum(a => a.Balance),
                    TopBalances = _context.Dispositions
                                          .Where(d => d.Customer.Country == x)
                                          .Select(d => d.Account)
                                          .OrderByDescending(a => a.Balance)
                                          .Take(10)
                                          .Select(a => a.Balance)
                                          .ToList()
                }).ToList()
            };
        }
    }
}
