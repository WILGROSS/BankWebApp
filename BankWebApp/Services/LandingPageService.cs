using BankAppWeb.ViewModels;
using BankWebApp.Data;
using BankWebApp.ViewModels;

namespace BankWebApp.Services
{
    public class LandingPageService : ILandingPageService
    {
        private readonly ApplicationDbContext _context;
        public LandingPageService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public LandingPageViewModel GetInfo()
        {
            var countries = _context.Customers
                        .Select(c => c.Country)
                        .Distinct()
                        .ToList();

            return new LandingPageViewModel
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
