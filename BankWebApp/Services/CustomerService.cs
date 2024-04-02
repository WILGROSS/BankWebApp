using BankWebApp.Data;
using BankWebApp.ViewModels;

namespace BankWebApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public CustomerResult GetCustomers(string sortColumn, string sortOrder, string searchQuery, int loadedRows, List<string> selectedCountries)
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                var lowerSearchQuery = searchQuery.ToLower().Replace(" ", "");
                query = query.Where(c => (c.Givenname.ToLower() + c.Surname.ToLower()).Contains(lowerSearchQuery)
                                         || c.NationalId.Contains(lowerSearchQuery)
                                         || c.City.ToLower().Contains(lowerSearchQuery)
                                         || c.Country.ToLower().Contains(lowerSearchQuery));
            }

            if (selectedCountries != null && selectedCountries.Any())
            {
                query = query.Where(c => selectedCountries.Contains(c.Country));
            }

            int totalCount = query.Count();

            var finalQuery = query.Select(c => new CustomerViewmodel
            {
                CustomerId = c.CustomerId,
                NationalId = c.NationalId,
                FirstName = c.Givenname,
                LastName = c.Surname,
                Address = c.Streetaddress,
                City = c.City,
                Country = c.Country
            });

            if (sortColumn == "Name")
            {
                finalQuery = sortOrder == "asc" ? finalQuery.OrderBy(c => c.LastName) : finalQuery.OrderByDescending(c => c.LastName);
            }
            else if (sortColumn == "City")
            {
                finalQuery = sortOrder == "asc" ? finalQuery.OrderBy(c => c.City) : finalQuery.OrderByDescending(c => c.City);
            }
            else if (sortColumn == "Country")
            {
                finalQuery = sortOrder == "asc" ? finalQuery.OrderBy(c => c.Country) : finalQuery.OrderByDescending(c => c.Country);
            }

            var customers = finalQuery.Take(loadedRows).ToList();

            return new CustomerResult { Customers = customers, TotalCount = totalCount };
        }
        public List<string> GetAllCountries()
        {
            return _context.Customers.Select(c => c.Country).Distinct().OrderBy(c => c).ToList();
        }
    }
}