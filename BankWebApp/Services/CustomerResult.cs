using BankWebApp.ViewModels;

namespace BankWebApp.Services
{
    public class CustomerResult
    {
        public List<CustomerViewmodel> Customers { get; set; }
        public int TotalCount { get; set; }
    }
}
