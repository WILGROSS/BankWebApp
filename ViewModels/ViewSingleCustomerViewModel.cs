using System.ComponentModel;

namespace ViewModels
{
	public class ViewSingleCustomerViewModel
	{
		public int CustomerId { get; set; }
		public string? NationalId { get; set; }
		[DisplayName("Gender")]
		public string Gender { get; set; }
		[DisplayName("First name")]
		public string GivenName { get; set; }
		[DisplayName("Last name")]
		public string SurName { get; set; }
		[DisplayName("Date of birth")]
		public DateOnly? Birthday { get; set; }
		[DisplayName("Country")]
		public string Country { get; set; }
		public string CountryCode { get; set; }
		[DisplayName("City")]
		public string City { get; set; }
		[DisplayName("Street address")]
		public string StreetAddress { get; set; }
		[DisplayName("Zip code")]
		public string ZipCode { get; set; }
		[DisplayName("Phone country code")]
		public string? TelephoneCountryCode { get; set; }
		[DisplayName("Phone number")]
		public string? TelephoneNumber { get; set; }
		[DisplayName("Email address")]
		public string? EmailAddress { get; set; }
		public decimal TotalBalance { get; set; }
		public List<AccountViewModel> Accounts { get; set; }
	}
}
