﻿using BankWebApp.Data;
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
        public List<CustomerViewmodel> GetCustomers(string sortColumn, string sortOrder, string searchQuery)
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                var lowerSearchQuery = searchQuery.ToLower();
                query = query.Where(c => c.Givenname.ToLower().Contains(lowerSearchQuery)
                                      || c.Surname.ToLower().Contains(lowerSearchQuery)
                                      || c.City.ToLower().Contains(lowerSearchQuery)
                                      || c.Country.ToLower().Contains(lowerSearchQuery));
            }

            var finalQuery = query.Select(c => new CustomerViewmodel
            {
                CustomerId = c.CustomerId,
                FirstName = c.Givenname,
                LastName = c.Surname,
                City = c.City,
                Country = c.Country
            });

            if (sortColumn == "Name")
            {
                finalQuery = sortOrder == "asc" ? finalQuery
                    .OrderBy(c => c.LastName) : finalQuery
                    .OrderByDescending(c => c.LastName);
            }
            else if (sortColumn == "City")
            {
                finalQuery = sortOrder == "asc" ? finalQuery
                    .OrderBy(c => c.City) : finalQuery
                    .OrderByDescending(c => c.City);
            }
            else if (sortColumn == "Country")
            {
                finalQuery = sortOrder == "asc" ? finalQuery
                    .OrderBy(c => c.Country) : finalQuery
                    .OrderByDescending(c => c.Country);
            }

            return finalQuery.ToList();
        }
    }
}
