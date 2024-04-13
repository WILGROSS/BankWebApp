using System.Globalization;
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
		[BindProperty]
		public TransactionViewModel _newDeposit { get; set; }
		public void OnGet(int id)
		{
			_account = _accountService.GetAccount(id);
			_newDeposit = _transactionService.GetNewTransaction(_account, "Credit in Cash", "Debit");
		}

		public IActionResult OnPost(int id)
		{
			var validationCode = _transactionService.ValidateTransaction(_newDeposit.AmountInput, null);

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
				default:
					_newDeposit.AmountInput.Replace(',', '.');
					if (decimal.TryParse(_newDeposit.AmountInput, NumberStyles.Any, CultureInfo.InvariantCulture, out var amount))
					{
						_newDeposit.Amount = amount;
					}
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
