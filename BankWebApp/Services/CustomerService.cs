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
        public List<CustomerViewmodel> GetCustomers(string sortColumn, string sortOrder)
        {
            var query = _context.Customers.Select(c => new CustomerViewmodel
            {
                CustomerId = c.CustomerId,
                FirstName = c.Givenname,
                LastName = c.Surname,
                City = c.City,
                Country = c.Country
            });
            if (sortColumn == "Name")
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.LastName);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.LastName);

            if (sortColumn == "Country")
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.Country);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.Country);

            if (sortColumn == "City")
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.City);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.City);

            return query.ToList();
        }
    }
}
