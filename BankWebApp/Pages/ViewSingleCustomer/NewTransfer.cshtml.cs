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
		public TransactionViewModel _newOutgoingTransfer { get; set; }
		public void OnGet(int id)
		{
			_account = _accountService.GetAccount(id);
			_newOutgoingTransfer = _transactionService.GetNewTransaction(_account, "Credit");
		}

		public IActionResult OnPost(int id)
		{
			_account = _accountService.GetAccount(id);
			var validationCodes = _transactionService.ValidateTransaction(_newOutgoingTransfer, _account.Balance, _newOutgoingTransfer.ReceivingAccountId);

			foreach (var validationCode in validationCodes)
			{
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
					case TransactionValidationCode.ReceivingAccountFieldEmpty:
						ModelState.AddModelError("ReceivingAccountId", "Please enter a receiving account no.");
						break;
					case TransactionValidationCode.ReceivingAccountNotFound:
						ModelState.AddModelError("ReceivingAccountId", $"Receiving account no. {_newOutgoingTransfer.ReceivingAccountId} not found");
						break;
					case TransactionValidationCode.NoPersonalMessage:
						ModelState.AddModelError("Operation", $"Please enter a message");
						break;
					case TransactionValidationCode.InvalidPersonalMessageLength:
						ModelState.AddModelError("Operation", $"Message must be between 2 and 50 characters");
						break;
					default:
						_newOutgoingTransfer.AmountInput = _newOutgoingTransfer.AmountInput.IsNullOrEmpty() ? _newOutgoingTransfer.AmountInput : _newOutgoingTransfer.AmountInput.Replace(',', '.');
						if (decimal.TryParse(_newOutgoingTransfer.AmountInput, NumberStyles.Any, CultureInfo.InvariantCulture, out var amount))
						{
							_newOutgoingTransfer.Amount = amount;
						}
						break;
				}
			}

			if (ModelState.IsValid)
			{
				var newIncomingTransfer = _transactionService.GetNewReceiverTransaction(_newOutgoingTransfer, (int)_newOutgoingTransfer.ReceivingAccountId);

				if (_transactionService.SaveNewTransaction(_newOutgoingTransfer) && _transactionService.SaveNewTransaction(newIncomingTransfer))
				{
					TempData["SuccessMessage"] = $"Succesfully transfered {(-_newOutgoingTransfer.Amount).ToString("C2", new CultureInfo("sv-SE"))} from account {id} to account {newIncomingTransfer.AccountId}";
					return RedirectToPage("ViewAccount", new { id });
				}
			}

			_account = _accountService.GetAccount(id);
			return Page();
		}
	}
}
