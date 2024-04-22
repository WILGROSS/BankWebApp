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
    private const int MinimumAge = 18;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not DateOnly birthDay)
            return new ValidationResult("Please enter a valid date format");

        if (birthDay.AddYears(MinimumAge) <= DateOnly.FromDateTime(DateTime.Now))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("You have to be at least 18 years old to register");
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
		[MinimumAge]
		public DateOnly? Birthday { get; set; }
		[DisplayName("National ID")]
		[StringLength(20, MinimumLength = 2, ErrorMessage = "Country code must be between 2 and 20 characters")]
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
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Email address must be between 2 and 100 characters")]
		public string? EmailAddress { get; set; }
	}
}
