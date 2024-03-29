using BankWebApp.ViewModels;

namespace ServicesLibrary.Services
{
    public interface ICustomerService
    {
        List<CustomerViewmodel> GetCustomers(string sortColumn, string sortOrder);
    }
}
