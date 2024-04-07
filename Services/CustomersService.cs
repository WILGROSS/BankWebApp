using DataAccessLayer.Data;
using ViewModels;

namespace Services
{
	public class CustomersService : ICustomersService
	{
		private readonly ApplicationDbContext _context;

		public CustomersService(ApplicationDbContext dbContext)
		{
			_context = dbContext;
		}

		public CustomersResult GetCustomers(string sortColumn, string sortOrder, string searchQuery, int loadedRows, List<string> selectedCountries, int currentPage)
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
			int totalPages = (int)Math.Ceiling((double)totalCount / loadedRows);

			int skipCount = (currentPage - 1) * loadedRows;

			query = ApplySorting(query, sortColumn, sortOrder);

			var customers = query.Skip(skipCount).Take(loadedRows).Select(c => new CustomersViewModel
			{
				CustomerId = c.CustomerId,
				NationalId = c.NationalId,
				FirstName = c.Givenname,
				LastName = c.Surname,
				Address = c.Streetaddress,
				City = c.City,
				Country = c.Country
			}).ToList();

			var vipCustomers = _context.Customers
				.Select(c => new CustomersViewModel
				{
					FirstName = c.Givenname,
					LastName = c.Surname,
					TotalBalance = c.Dispositions.Sum(d => d.Account.Balance)
				})
				.OrderByDescending(c => c.TotalBalance)
				.Take(5)
				.ToList();

			return new CustomersResult { Customers = customers, TotalCount = totalCount, TotalPages = totalPages, VipCustomers = vipCustomers };
		}

		private IQueryable<Customer> ApplySorting(IQueryable<Customer> query, string sortColumn, string sortOrder)
		{
			switch (sortColumn)
			{
				case "Name":
					query = sortOrder == "asc" ? query.OrderBy(c => c.Surname) : query.OrderByDescending(c => c.Surname);
					break;
				case "City":
					query = sortOrder == "asc" ? query.OrderBy(c => c.City) : query.OrderByDescending(c => c.City);
					break;
				case "Country":
					query = sortOrder == "asc" ? query.OrderBy(c => c.Country) : query.OrderByDescending(c => c.Country);
					break;
				default:
					break;
			}

			return query;
		}

		public List<string> GetAllCountries()
		{
			return _context.Customers.Select(c => c.Country).Distinct().OrderBy(c => c).ToList();
		}
	}
}