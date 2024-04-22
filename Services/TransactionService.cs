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

		public TransactionViewModel GetNewTransaction(AccountViewModel account, string type)
		{
			var newTransaction = new TransactionViewModel()
			{
				AccountId = account.AccountId,
				Date = DateOnly.FromDateTime(DateTime.Now),
				Balance = account.Balance,
				Type = type
			};

			return newTransaction;
		}

		public TransactionViewModel GetNewReceiverTransaction(TransactionViewModel newOutgoingTransfer, int receivingAccountId)
		{
			var receiverTransaction = _mapper.Map<TransactionViewModel>(newOutgoingTransfer);
			var receivingAccount = _context.Accounts.FirstOrDefault(x => x.AccountId == receivingAccountId);

			receiverTransaction.Amount = newOutgoingTransfer.Amount;
			receiverTransaction.AccountId = receivingAccountId;
			receiverTransaction.Balance = receivingAccount.Balance;
			receiverTransaction.Type = "Debit";

			return receiverTransaction;
		}

		public List<TransactionValidationCode> ValidateTransaction(TransactionViewModel newTransaction, decimal? accountBalance, int? receivingAccountId)
		{
			var validationCodes = new List<TransactionValidationCode>();

			decimal scaledResult = Math.Abs(newTransaction.Amount) * 100;
			if (scaledResult != Math.Floor(scaledResult))
			{
				validationCodes.Add(TransactionValidationCode.InvalidPrecision);
			}

			if (newTransaction.Amount < 100 || newTransaction.Amount > 100000)
			{
				validationCodes.Add(TransactionValidationCode.AmountOutOfRange);
			}

			if (newTransaction.Amount > accountBalance)
			{
				validationCodes.Add(TransactionValidationCode.InsufficientFunds);
			}

			if (!receivingAccountId.HasValue)
			{
				validationCodes.Add(TransactionValidationCode.ReceivingAccountFieldEmpty);
			}
			else if (_context.Accounts.FirstOrDefault(x => x.AccountId == receivingAccountId) == null)
			{
				validationCodes.Add(TransactionValidationCode.ReceivingAccountNotFound);
			}

			if (newTransaction.Operation.IsNullOrEmpty())
			{
				validationCodes.Add(TransactionValidationCode.NoPersonalMessage);
			}
			else if (newTransaction.Operation.Length > 50 || newTransaction.Operation.Length < 2)
			{
				validationCodes.Add(TransactionValidationCode.InvalidPersonalMessageLength);
			}

			if (validationCodes.Count == 0)
				validationCodes.Add(TransactionValidationCode.Ok);

			return validationCodes;
		}


		public bool SaveNewTransaction(TransactionViewModel newTransactionViewModel)
		{
			try
			{
				newTransactionViewModel.Amount = newTransactionViewModel.Type == "Credit" ? -newTransactionViewModel.Amount : newTransactionViewModel.Amount;

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

