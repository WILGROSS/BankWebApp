using BankWebApp.ViewModels;

namespace BankAppWeb.ViewModels
{
    public class LandingPageViewModel
    {
        public List<CountryInfoViewModel> CountriesInfo { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalAccounts { get; set; }
        public decimal TotalBalance { get; set; }
    }
}