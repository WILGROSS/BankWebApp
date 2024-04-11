using AutoMapper;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using ViewModels;

namespace Services
{
	public class AccountService : IAccountService
	{
		private readonly IMapper _mapper;
		private readonly ApplicationDbContext _context;

		public AccountService(IMapper mapper, ApplicationDbContext context)
		{
			_mapper = mapper;
			_context = context;
		}
		public AccountViewModel GetAccount(int id)
		{
			var account = _context.Accounts.Include(a => a.Transactions)
				.FirstOrDefault(x => x.AccountId == id);

			return _mapper.Map<AccountViewModel>(account);
		}

		public List<TransactionViewModel> LoadMoreTransactions(int accountId, int offset)
		{
			var transactionsQuery = _context.Transactions
				.Where(t => t.AccountId == accountId)
				.OrderBy(t => t.Date);

			var transactions = transactionsQuery
				.Skip(offset)
				.Take(20)
				.ToList();

			return _mapper.Map<List<TransactionViewModel>>(transactions);
		}
	}
}
