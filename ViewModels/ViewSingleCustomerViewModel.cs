namespace ViewModels
{
	public class ViewSingleCustomerViewModel
	{
		public int CustomerId { get; set; }
		public string NationalId { get; set; }
		public string Gender { get; set; }
		public string GivenName { get; set; }
		public string SurName { get; set; }
		public DateOnly DateOfBirth { get; set; }
		public string Country { get; set; }
		public string CountryCode { get; set; }
		public string City { get; set; }
		public string Address { get; set; }
		public string ZipCode { get; set; }
		public string PhoneCountryCode { get; set; }
		public string PhoneNumber { get; set; }
		public string EmailAddress { get; set; }
		public decimal TotalBalance { get; set; }
		public List<AccountViewModel> Accounts { get; set; }
	}
}
