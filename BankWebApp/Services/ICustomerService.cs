using BankWebApp.ViewModels;

namespace BankWebApp.Services
{
    public interface ICustomerService
    {
        List<CustomerViewmodel> GetCustomers(string sortColumn, string sortOrder, string searchQuery, int loadedRows);
    }
}
