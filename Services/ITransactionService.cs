using ViewModels;

public enum TransactionValidationCode
{
	NullInput = 0,
	InvalidInput = 1,
	InvalidPrecision = 2,
	AmountOutOfRange = 3,
	InsufficientFunds = 4,
	ReceivingAccountFieldEmpty = 5,
	ReceivingAccountNotFound = 6,
	NoPersonalMessage = 7,
	InvalidPersonalMessageLength = 8,
	NoOutgoingMessage = 9,
	InvalidOutgoingMessageLength = 10,
	Ok = 100
}

namespace Services
{
	public interface ITransactionService
	{
		public TransactionViewModel GetNewTransaction(AccountViewModel account, string type);
		public TransactionViewModel GetNewReceiverTransaction(TransactionViewModel newOutgoingTransfer, int receivingAccountId);
		public bool SaveNewTransaction(TransactionViewModel newTransactionViewModel);
		public List<TransactionValidationCode> ValidateTransaction(TransactionViewModel newTransaction, decimal? accountBalance, int? receivingAccountId);
	}
}
