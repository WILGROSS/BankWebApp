using AutoMapper;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using ViewModels;

namespace Services
{
	public class AllCustomersService : IAllCustomersService
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public AllCustomersService(ApplicationDbContext dbContext, IMapper mapper)
		{
			_context = dbContext;
			_mapper = mapper;
		}

		public AllCustomersResult GetCustomers(string sortColumn, string sortOrder, string searchQuery, int loadedRows, List<string> selectedCountries, int currentPage)
		{
			var query = _context.Customers.AsQueryable();

			if (!string.IsNullOrEmpty(searchQuery))
			{
				var lowerSearchQuery = searchQuery.ToLower().Replace(" ", "");
				query = query.Where(c => (c.Givenname.ToLower() + c.Surname.ToLower()).Replace(" ", "").Contains(lowerSearchQuery)
										 || c.NationalId.Contains(lowerSearchQuery)
										 || c.City.ToLower().Contains(lowerSearchQuery)
										 || c.Country.ToLower().Contains(lowerSearchQuery)
										 || c.CustomerId.ToString().Contains(lowerSearchQuery));
			}

			if (selectedCountries != null && selectedCountries.Any())
			{
				query = query.Where(c => selectedCountries.Contains(c.Country));
			}

			int totalCount = query.Count();
			int totalPages = (int)Math.Ceiling((double)totalCount / loadedRows);
			int skipCount = (currentPage - 1) * loadedRows;

			query = ApplySorting(query, sortColumn, sortOrder);

			var pagedCustomers = query.Skip(skipCount).Take(loadedRows).ToList();
			var customers = _mapper.Map<IEnumerable<AllCustomersViewModel>>(pagedCustomers).ToList();

			var vipCustomers = _context.Customers
				.Select(c => new AllCustomersViewModel
				{
					CustomerId = c.CustomerId,
					GivenName = c.Givenname,
					SurName = c.Surname,
					TotalBalance = c.Dispositions.Sum(d => d.Account.Balance)
				})
				.OrderByDescending(c => c.TotalBalance)
				.Take(5)
				.ToList();

			return new AllCustomersResult { Customers = customers, TotalCount = totalCount, TotalPages = totalPages, VipCustomers = vipCustomers };
		}

		private IQueryable<Customer> ApplySorting(IQueryable<Customer> query, string sortColumn, string sortOrder)
		{
			string collation = "Finnish_Swedish_CI_AS";

			switch (sortColumn)
			{
				case "Name":
					query = sortOrder == "asc"
						? query.OrderBy(c => EF.Functions.Collate(c.Surname, collation))
						: query.OrderByDescending(c => EF.Functions.Collate(c.Surname, collation));
					break;
				case "City":
					query = sortOrder == "asc"
						? query.OrderBy(c => EF.Functions.Collate(c.City, collation))
						: query.OrderByDescending(c => EF.Functions.Collate(c.City, collation));
					break;
				case "Country":
					query = sortOrder == "asc"
						? query.OrderBy(c => EF.Functions.Collate(c.Country, collation))
						: query.OrderByDescending(c => EF.Functions.Collate(c.Country, collation));
					break;
				case "ActiveStatus":
					query = sortOrder == "asc"
						? query.OrderBy(c => c.isDeleted)
						: query.OrderByDescending(c => c.isDeleted);
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