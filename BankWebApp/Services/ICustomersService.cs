namespace BankWebApp.Services
{
    public interface ICustomersService
    {
        CustomersResult GetCustomers(string sortColumn, string sortOrder, string searchQuery, int loadedRows, List<string> selectedCountries);
        List<string> GetAllCountries();
    }
}
