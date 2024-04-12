using AutoMapper;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using ViewModels;

namespace Services
{
	class TransactionService : ITransactionService
	{
		private readonly IMapper _mapper;
		private readonly ApplicationDbContext _context;

		public TransactionService(IMapper mapper, ApplicationDbContext context)
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

		public bool SaveNewTransaction(TransactionViewModel newTransactionViewModel, int id)
		{
			try
			{
				var account = _context.Accounts.Include(a => a.Transactions)
				.FirstOrDefault(x => x.AccountId == id);

				account.Transactions.Add(_mapper.Map<Transaction>(newTransactionViewModel));
				_context.Update(account);
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
