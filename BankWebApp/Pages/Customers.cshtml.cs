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
        public List<CustomerViewmodel> _customers { get; set; }
        public CustomersModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public void OnGet(string sortColumn, string sortOrder)
        {
            var query = _context.Customers.Select(c => new CustomerViewmodel
            {

                CustomerId = c.CustomerId,
                Name = $"{c.Givenname}",
                City = c.City,
                Country = c.Country

            });
            if (sortColumn == "Name")
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.Name);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.Name);

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

            _customers = query.ToList();
        }
    }
}
