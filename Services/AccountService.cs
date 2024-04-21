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

		public bool AddNewAccount(int customerId, out int newAccountId)
		{
			newAccountId = 0;
			var customer = _context.Customers.FirstOrDefault(x => x.CustomerId == customerId);
			if (customer == null)
			{
				return false;
			}

			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					var newAccount = new Account
					{
						Frequency = "Monthly",
						Created = DateOnly.FromDateTime(DateTime.Now),
						Balance = 0.0M
					};
					_context.Accounts.Add(newAccount);
					_context.SaveChanges();

					newAccountId = newAccount.AccountId;

					var newDisposition = new Disposition
					{
						CustomerId = customer.CustomerId,
						Type = "OWNER",
						AccountId = newAccountId
					};
					_context.Dispositions.Add(newDisposition);
					_context.SaveChanges();

					transaction.Commit();
					return true;
				}
				catch
				{
					transaction.Rollback();
					return false;
				}
			}
		}

		public AccountViewModel GetAccount(int id)
		{
			var account = _context.Accounts.Include(a => a.Transactions)
				.FirstOrDefault(x => x.AccountId == id);

			return _mapper.Map<AccountViewModel>(account);
		}

		public int GetCustomerIdFromAccountId(int accountId)
		{
			var disposition = _context.Dispositions.FirstOrDefault(d => d.AccountId == accountId);
			if (disposition == null)
			{
				throw new InvalidOperationException("No customer associated with this account.");
			}
			return disposition.CustomerId;
		}


		public List<TransactionViewModel> LoadMoreTransactions(int accountId, int offset)
		{
			var transactionsQuery = _context.Transactions
				.Where(t => t.AccountId == accountId)
				.OrderByDescending(t => t.TransactionId);

			var transactions = transactionsQuery
				.Skip(offset)
				.Take(20)
				.ToList();

			return _mapper.Map<List<TransactionViewModel>>(transactions);
		}

		public bool TryDeleteAccount(int accountId)
		{
			var account = _context.Accounts.FirstOrDefault(x => x.AccountId == accountId);

			try
			{
                if (account.Balance == 0)
                {
                    var disposition = _context.Dispositions.FirstOrDefault(x => x.AccountId == accountId);
                    var transactions = _context.Transactions.Where(x => x.AccountId == accountId);
                    var loans = _context.Loans.Where(x => x.AccountId == accountId);
                    var PermenentOrder = _context.Transactions.FirstOrDefault(x => x.AccountId == accountId);

                    _context.RemoveRange(transactions);
                    _context.RemoveRange(loans);

                    if (PermenentOrder != null)
                        _context.Remove(PermenentOrder);

                    if (disposition != null)
                        _context.Remove(disposition);

                    _context.Remove(account);
                    _context.SaveChanges();

                    return true;
                }
            }
			catch
			{
				return false;
			}

			return false;
		}
	}
}
