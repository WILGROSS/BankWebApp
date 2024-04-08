using AutoMapper;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using ViewModels;
namespace Services
{
	public class ViewCustomerService : IViewCustomerService
	{
		private readonly IMapper _mapper;
		private readonly ApplicationDbContext _context;

		public ViewCustomerService(IMapper mapper, ApplicationDbContext dbContext)
		{
			_mapper = mapper;
			_context = dbContext;
		}

		public ViewCustomerViewModel GetCustomer(int customerId)
		{
			var customer = _context.Customers.Include(c => c.Dispositions)
				.ThenInclude(d => d.Account)
				.ThenInclude(a => a.Transactions)
				.First(x => x.CustomerId == customerId);
			var viewModel = _mapper.Map<ViewCustomerViewModel>(customer);

			decimal bajskorv = 0m;

			foreach (var account in viewModel.Accounts)
			{
				foreach (var transaction in account.Transactions)
				{
					bajskorv += transaction.amount;
				}
				viewModel.TotalBalance += account.Balance;
			}

			return viewModel;
		}
	}
}
