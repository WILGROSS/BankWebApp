namespace BankWebApp.ViewModels
{
    public class CustomersViewModel
    {
        public int CustomerId { get; set; }
        public string NationalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public decimal TotalBalance { get; set; }
    }
}
