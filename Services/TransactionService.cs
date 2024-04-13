using System.Globalization;
using AutoMapper;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ViewModels;

namespace Services
{
	public class TransactionService : ITransactionService
	{
		private readonly IMapper _mapper;
		private readonly ApplicationDbContext _context;

		public TransactionService(IMapper mapper, ApplicationDbContext context)
		{
			_mapper = mapper;
			_context = context;
		}

		public TransactionViewModel GetNewTransaction(AccountViewModel account, string? message, string type)
		{
			var newTransaction = new TransactionViewModel()
			{
				AccountId = account.AccountId,
				Date = DateOnly.FromDateTime(DateTime.Now),
				Balance = account.Balance,
				Type = type
			};

			if (!message.IsNullOrEmpty())
				newTransaction.Operation = message;

			return newTransaction;
		}

		public TransactionValidationCode ValidateTransaction(string amountInput)
		{
			if (string.IsNullOrEmpty(amountInput))
			{
				return TransactionValidationCode.NullInput;
			}

			amountInput.Replace(',', '.');

			if (decimal.TryParse(amountInput, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
			{
				decimal scaledResult = Math.Abs(result) * 100;
				if (scaledResult != Math.Floor(scaledResult))
				{
					return TransactionValidationCode.InvalidPrecision;
				}

				if (result < 100 || result > 100000)
				{
					return TransactionValidationCode.AmountOutOfRange;
				}

				return TransactionValidationCode.Ok;
			}
			else
			{
				return TransactionValidationCode.InvalidInput;
			}
		}


		public bool SaveNewTransaction(TransactionViewModel newTransactionViewModel)
		{
			try
			{
				var account = _context.Accounts.Include(a => a.Transactions)
				.FirstOrDefault(x => x.AccountId == newTransactionViewModel.AccountId);

				newTransactionViewModel.Balance += newTransactionViewModel.Amount;
				account.Balance += newTransactionViewModel.Amount;
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

