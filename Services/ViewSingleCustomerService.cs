using AutoMapper;
using DataAccessLayer.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ViewModels;
namespace Services
{
	public class ViewSingleCustomerService : IViewSingleCustomerService
	{
		private readonly IMapper _mapper;
		private readonly ApplicationDbContext _context;

		public ViewSingleCustomerService(IMapper mapper, ApplicationDbContext dbContext)
		{
			_mapper = mapper;
			_context = dbContext;
		}

		public ViewSingleCustomerViewModel GetCustomer(int customerId)
		{
			var customer = _context.Customers.Include(c => c.Dispositions)
				.ThenInclude(d => d.Account)
				.ThenInclude(a => a.Transactions)
				.First(x => x.CustomerId == customerId);
			var viewModel = _mapper.Map<ViewSingleCustomerViewModel>(customer);

			foreach (var account in viewModel.Accounts)
			{
				account.LatestTransactions = account.Transactions
					.OrderByDescending(t => t.TransactionId)
					.Take(10)
					.ToList();

				viewModel.TotalBalance += account.Balance;
			}
			return viewModel;
		}

		public EditCustomerViewModel GetEditableViewModel(int id)
		{
			var customer = _context.Customers.FirstOrDefault(x => x.CustomerId == id);
			return _mapper.Map<EditCustomerViewModel>(customer);
		}
		public List<SelectListItem> GetGenderSelectListItems()
		{
			var genderList = Enum.GetValues(typeof(Genders))
						  .Cast<Genders>()
						  .Select(g => new SelectListItem
						  {
							  Text = g.ToString(),
							  Value = ((int)g).ToString()
						  }).ToList();
			return genderList;
		}
		public List<SelectListItem> GetCountrySelectListItems()
		{
			var genderList = Enum.GetValues(typeof(Countries))
						  .Cast<Countries>()
						  .Select(c => new SelectListItem
						  {
							  Text = c.ToString(),
							  Value = ((int)c).ToString()
						  }).ToList();
			return genderList;
		}
		public bool UpdateCustomer(EditCustomerViewModel model)
		{
			try
			{
				var customer = _context.Customers.FirstOrDefault(x => x.CustomerId == model.CustomerId);

				switch (model.Country)
				{
					case Countries.Finland:
						customer.CountryCode = "FI";
						break;
					case Countries.Sweden:
						customer.CountryCode = "SE";
						break;
					case Countries.Norway:
						customer.CountryCode = "NO";
						break;
					case Countries.Denmark:
						customer.CountryCode = "DK";
						break;
					default:
						return false;
				}

				_mapper.Map(model, customer);
				_context.Update(customer);
				_context.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool ToggleCustomerActiveStatus(EditCustomerViewModel model)
		{
			try
			{
				var customer = _context.Customers.FirstOrDefault(x => x.CustomerId == model.CustomerId);

				_mapper.Map(model, customer);

				customer.isDeleted = !customer.isDeleted;
				_context.Update(customer);
				_context.SaveChanges();
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
