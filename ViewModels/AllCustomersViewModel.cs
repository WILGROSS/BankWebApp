namespace ViewModels
{
	public class AllCustomersViewModel
	{
		public int CustomerId { get; set; }
		public string NationalId { get; set; }
		public string GivenName { get; set; }
		public string SurName { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public string StreetAddress { get; set; }
		public decimal TotalBalance { get; set; }
	}
}
