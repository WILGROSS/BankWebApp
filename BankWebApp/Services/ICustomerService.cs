using BankWebApp.ViewModels;

namespace BankWebApp.Services
{
    public interface ICustomerService
    {
        List<CustomerViewmodel> GetSuppliers(string sortColumn, string sortOrder);
    }
}
