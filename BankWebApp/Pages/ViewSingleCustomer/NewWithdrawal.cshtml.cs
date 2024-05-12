using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using ViewModels;

namespace BankWebApp.Pages.ViewSingleCustomer
{
    [Authorize(Roles = "Cashier")]
    public class NewWithdrawalModel : PageModel
    {
        public readonly IAccountService _accountService;
        public readonly ITransactionService _transactionService;

        public NewWithdrawalModel(IAccountService accountService, ITransactionService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
        }
        public AccountViewModel _account { get; set; }
        [BindProperty]
        public TransactionViewModel _newWithdrawal { get; set; }
        public void OnGet(int id)
        {
            _account = _accountService.GetAccount(id);
            _newWithdrawal = _transactionService.GetNewTransaction(_account, "Credit");
            _newWithdrawal.Operation = "Withdrawal in Cash";
        }

        public IActionResult OnPost(int id)
        {
            _account = _accountService.GetAccount(id);
            var validationCodes = _transactionService.ValidateTransaction(_newWithdrawal, _account.Balance, null);

            foreach (var validationCode in validationCodes)
            {
                switch (validationCode)
                {
                    case TransactionValidationCode.NullInput:
                        ModelState.Clear();
                        ModelState.AddModelError("Amount", "Please enter an amount");
                        break;
                    case TransactionValidationCode.InvalidInput:
                        ModelState.AddModelError("Amount", "Please enter a valid amount");
                        break;
                    case TransactionValidationCode.InvalidPrecision:
                        ModelState.AddModelError("Amount", "Amount may not have more than two decimals");
                        break;
                    case TransactionValidationCode.AmountOutOfRange:
                        ModelState.AddModelError("Amount", "The amount must be between 100 and 100 000");
                        break;
                    case TransactionValidationCode.InsufficientFunds:
                        ModelState.AddModelError("Amount", "The account has insufficient funds for this amount");
                        break;
                }
            }

            if (ModelState.IsValid)
            {
                if (_transactionService.SaveNewTransaction(_newWithdrawal))
                {
                    TempData["SuccessMessage"] = $"Succesfully withdrew {(-_newWithdrawal.Amount).ToString("C2", new CultureInfo("sv-SE"))} from account {id}";
                    return RedirectToPage("ViewAccount", new { id });
                }
            }

            _account = _accountService.GetAccount(id);
            return Page();
        }
    }
}