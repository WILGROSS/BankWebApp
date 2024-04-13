using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using ViewModels;

namespace BankWebApp.Pages.ViewSingleCustomer
{
	public class NewTransferModel : PageModel
	{
		public readonly IAccountService _accountService;
		public readonly ITransactionService _transactionService;

		public NewTransferModel(IAccountService accountService, ITransactionService transactionService)
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
			_newWithdrawal = _transactionService.GetNewTransaction(_account, "Transfer between accounts", "Credit");
		}

		public IActionResult OnPost(int id)
		{
			_account = _accountService.GetAccount(id);
			var validationCode = _transactionService.ValidateTransaction(_newWithdrawal.AmountInput, _account.Balance);

			switch (validationCode)
			{
				case TransactionValidationCode.NullInput:
					ModelState.Clear();
					ModelState.AddModelError("AmountInput", "Please enter an amount");
					break;
				case TransactionValidationCode.InvalidInput:
					ModelState.AddModelError("AmountInput", "Please enter a valid amount");
					break;
				case TransactionValidationCode.InvalidPrecision:
					ModelState.AddModelError("AmountInput", "Amount may not have more than two decimals");
					break;
				case TransactionValidationCode.AmountOutOfRange:
					ModelState.AddModelError("AmountInput", "The amount must be between 100 and 100 000");
					break;
				case TransactionValidationCode.InsufficientFunds:
					ModelState.AddModelError("AmountInput", "The account has insufficient funds for this amount");
					break;
				default:
					_newWithdrawal.AmountInput.Replace(',', '.');
					if (decimal.TryParse(_newWithdrawal.AmountInput, NumberStyles.Any, CultureInfo.InvariantCulture, out var amount))
					{
						_newWithdrawal.Amount = amount;
					}
					break;
			}

			if (ModelState.IsValid)
			{
				if (_transactionService.SaveNewTransaction(_newWithdrawal))
				{
					TempData["SuccessMessage"] = $"Succesfully withdrew {_newWithdrawal.Amount} from account {id}";
					return RedirectToPage("ViewAccount", new { id });
				}
			}

			_account = _accountService.GetAccount(id);
			return Page();
		}
	}
}
