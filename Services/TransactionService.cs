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

        public TransactionValidationCode ValidateTransaction(TransactionViewModel newTransactionViewModel)
        {
            if (newTransactionViewModel.Amount.ToString().IsNullOrEmpty())
            {
                return TransactionValidationCode.NullInput;
            }
            //else if (put the check here!){
            //    return TransactionValidationCode.InvalidInput;
            //}

            return TransactionValidationCode.Ok;
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
