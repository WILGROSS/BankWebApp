namespace BankWebApp.ViewModels
{
    public class CountryInfoViewModel
    {
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalAccounts { get; set; }
        public decimal TotalBalance { get; set; }
        public List<decimal> TopBalances { get; set; }
    }
}
