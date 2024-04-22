using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
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
			_newDeposit = _transactionService.GetNewTransaction(_account, "Debit");
		}

		public IActionResult OnPost(int id)
		{
			var validationCodes = _transactionService.ValidateTransaction(_newDeposit, null, null);
			_newDeposit.Operation = "Credit in Cash";

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

				}
			}

			if (ModelState.IsValid)
			{
				if (_transactionService.SaveNewTransaction(_newDeposit))
				{
					TempData["SuccessMessage"] = $"Succesfully deposited {_newDeposit.Amount.ToString("C2", new CultureInfo("sv-SE"))} into account {id}";
					return RedirectToPage("ViewAccount", new { id });
				}
			}

			_account = _accountService.GetAccount(id);
			return Page();
		}
	}
}
