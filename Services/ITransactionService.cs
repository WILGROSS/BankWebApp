using ViewModels;

public enum TransactionValidationCode
{
    Ok = 0,
    InvalidInput = 1,
    NullInput = 2
}

namespace Services
{
    public interface ITransactionService
    {
        public TransactionViewModel GetNewTransaction(AccountViewModel account, string? message, string type);
        public bool SaveNewTransaction(TransactionViewModel newTransactionViewModel);
        public TransactionValidationCode ValidateTransaction(TransactionViewModel newTransactionViewModel);
    }
}
