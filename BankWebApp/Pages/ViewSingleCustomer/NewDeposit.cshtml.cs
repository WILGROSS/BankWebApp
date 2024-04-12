using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using ViewModels;

namespace BankWebApp.Pages.ViewSingleCustomer
{
    [Authorize(Roles = "Cashier")]
    public class NewDepositModel : PageModel
    {
        public readonly IAccountService _accountService;
        public readonly ITransactionService _transactionService;

        public NewDepositModel(IAccountService accountService, ITransactionService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
        }
        public AccountViewModel _account { get; set; }
        [BindProperty(SupportsGet = true)]
        public TransactionViewModel _newDeposit { get; set; }
        public void OnGet(int id)
        {
            _account = _accountService.GetAccount(id);
            _newDeposit = _transactionService.GetNewTransaction(_account, "Credit in Cash", "Debit");
        }

        public IActionResult OnPost(int id)
        {
            var validationCode = _transactionService.ValidateTransaction(_newDeposit);

            switch (validationCode)
            {
                case TransactionValidationCode.InvalidInput:
                    ModelState.AddModelError("Amount", "Please enter a valid amount");
                    break;
                case TransactionValidationCode.NullInput:
                    ModelState.AddModelError("Amount", "Please enter an amount");
                    break;
            }

            if (ModelState.IsValid)
            {
                if (_transactionService.SaveNewTransaction(_newDeposit))
                {
                    TempData["SuccessMessage"] = $"Succesfully deposited {_newDeposit.Amount} into account {id}";
                    return RedirectToPage("ViewAccount", new { id });
                }
            }
            _account = _accountService.GetAccount(id);
            return Page();
        }
    }
}
