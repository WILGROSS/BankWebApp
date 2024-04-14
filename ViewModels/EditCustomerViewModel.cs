using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
	public enum Genders
	{
		Select = 0,
		male = 1,
		female = 2,
		Other = 3
	}
	public enum Countries
	{
		Select = 0,
		Finland = 1,
		Sweden = 2,
		Norway = 3,
		Denmark = 4
	}
	public enum CountryCodes
	{
		FI = 1,
		SE = 2,
		NO = 3,
		DK = 4
	}
	public class MinimumAgeAttribute : ValidationAttribute
	{
		private readonly int _minimumAge;

		public MinimumAgeAttribute(int minimumAge)
		{
			_minimumAge = minimumAge;
			ErrorMessage = $"Customer must be at least {minimumAge} years old to register";
		}

		public override bool IsValid(object value)
		{
			if (value is DateOnly date)
			{
				int currentYear = DateOnly.FromDateTime(DateTime.Today).Year;
				int birthYear = date.Year;
				int age = currentYear - birthYear;
				if (new DateOnly(currentYear, date.Month, date.Day) > DateOnly.FromDateTime(DateTime.Today))
				{
					age--;
				}
				return age >= _minimumAge;
			}
			return false;
		}
	}
	public class EditCustomerViewModel
	{
		[Required]
		public int CustomerId { get; set; }
		public bool IsDeleted { get; set; } = false;
		[Required]
		[DisplayName("First name")]
		[StringLength(100, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 100 characters")]
		public string GivenName { get; set; }
		[Required]
		[DisplayName("Last name")]
		[StringLength(100, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 100 characters")]
		public string SurName { get; set; }

		[DisplayName("Date of birth")]
		[MinimumAge(18)]
		public DateOnly? Birthday { get; set; }
		[DisplayName("National ID")]
		public string NationalId { get; set; }
		[Required]
		[DisplayName("Gender")]
		[Range(1, 99, ErrorMessage = "Please select a gender")]
		public Genders Gender { get; set; }
		[Required]
		[DisplayName("Country")]
		[Range(1, 99, ErrorMessage = "Please select a country")]
		public Countries Country { get; set; }
		[Required]
		[DisplayName("City")]
		[StringLength(100, MinimumLength = 2, ErrorMessage = "City must be between 2 and 100 characters")]
		public string City { get; set; }
		[Required]
		[DisplayName("Street address")]
		[StringLength(100, MinimumLength = 2, ErrorMessage = "Street address must be between 2 and 100 characters")]
		public string StreetAddress { get; set; }
		[Required]
		[DisplayName("Zip code")]
		[StringLength(15, MinimumLength = 2, ErrorMessage = "Zip code must be between 2 and 15 characters")]
		public string ZipCode { get; set; }
		[DisplayName("Phone country code")]
		[StringLength(10, MinimumLength = 2, ErrorMessage = "Phone country code must be between 2 and 10 characters")]
		public string? TelephoneCountryCode { get; set; }

		[DisplayName("Phone number")]
		[StringLength(25, MinimumLength = 2, ErrorMessage = "Phone number must be between 2 and 25 characters")]
		public string? TelephoneNumber { get; set; }

		[DisplayName("Email address")]
		[StringLength(100, MinimumLength = 2, ErrorMessage = "Email address must be between 2 and 100 characters")]
		public string? EmailAddress { get; set; }
	}
}
