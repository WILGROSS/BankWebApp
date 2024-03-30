namespace BankWebApp.Services
{
    public interface ICustomerService
    {
        CustomerResult GetCustomers(string sortColumn, string sortOrder, string searchQuery, int loadedRows);
    }
}
