namespace BankAppWeb.ViewModels
{
    public class LandingPageViewModel
    {
        public List<CountryInfoViewModel> CountriesInfo { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalAccounts { get; set; }
        public decimal TotalBalance { get; set; }
    }
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