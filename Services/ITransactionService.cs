using ViewModels;

public enum TransactionValidationCode
{
	NullInput = 0,
	InvalidInput = 1,
	InvalidPrecision = 2,
	AmountOutOfRange = 3,
	InsufficientFunds = 4,
	Ok = 100
}

namespace Services
{
	public interface ITransactionService
	{
		public TransactionViewModel GetNewTransaction(AccountViewModel account, string? message, string type);
		public bool SaveNewTransaction(TransactionViewModel newTransactionViewModel);
		public TransactionValidationCode ValidateTransaction(string input, decimal? accountBalance);
	}
}
