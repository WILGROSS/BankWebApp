using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
	public class MaximumAmountAttribute : ValidationAttribute
	{
		private readonly string _comparisonProperty;

		public MaximumAmountAttribute(string comparisonProperty)
		{
			_comparisonProperty = comparisonProperty;
		}

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var typePropertyInfo = validationContext.ObjectType.GetProperty("Type");
			if (typePropertyInfo != null && typePropertyInfo.GetValue(validationContext.ObjectInstance).ToString() == "Debit")
			{
				return ValidationResult.Success;
			}

			var comparisonPropertyInfo = validationContext.ObjectType.GetProperty(_comparisonProperty);
			var comparisonValue = (decimal?)comparisonPropertyInfo.GetValue(validationContext.ObjectInstance);

			if (comparisonValue == null)
				return new ValidationResult("The value must be a decimal");

			if (value is not decimal decimalValue)
				return new ValidationResult("The value must be a decimal");

			return decimalValue > comparisonValue
				? new ValidationResult("The account has insufficient funds for this amount")
				: ValidationResult.Success;
		}
	}
	public class TransactionViewModel
	{
		[Required]
		public int TransactionId { get; set; }
		[Required]
		public int AccountId { get; set; }
		[DisplayName("Receiving Account number")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a receiving account no.")]
		public int? ReceivingAccountId { get; set; } = null;
		[Required(ErrorMessage ="Please enter a message")]
		[DisplayName("Message")]
		[StringLength(50, MinimumLength = 2, ErrorMessage = "Message must be between 2 and 50 characters")]
		public string? Operation { get; set; }
		public string Type { get; set; }
        [Required]
        [DisplayName("Transaction Amount")]
        [MaximumAmount(nameof(Balance))]
        [Range(100, 100000, ErrorMessage = "Amount must be between 100 and 100000")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Amount must have a maximum of two decimal places")]
        public decimal Amount { get; set; }
		[Required]
		public decimal Balance { get; set; }
		[Required]
		public DateOnly Date { get; set; }
	}
}
