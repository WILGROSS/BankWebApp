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
		[BindProperty(SupportsGet = true)]
		public AccountViewModel _account { get; set; }
		public TransactionViewModel _newDeposit;
		public void OnGet(int id)
		{
			_account = _accountService.GetAccount(id);
			_newDeposit = _transactionService.GetNewTransaction(_account, "Credit in Cash", "Debit");
		}

		public IActionResult OnPost(int id)
		{
			if (ModelState.IsValid)
			{
				if (_transactionService.SaveNewTransaction(_newDeposit, id))
				{
					TempData["SuccessMessage"] = $"Succesfully deposited {_newDeposit.Amount} into account {id}";
					return RedirectToPage("index", new { id });
				}
			}
			return Page();
		}
	}
}
