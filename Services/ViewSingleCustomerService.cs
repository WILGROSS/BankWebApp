using AutoMapper;
using DataAccessLayer.Data;
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

		public bool UpdateCustomer(EditCustomerViewModel model)
		{
			try
			{
				var customer = _context.Customers.FirstOrDefault(x => x.CustomerId == model.CustomerId);
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
	}
}
