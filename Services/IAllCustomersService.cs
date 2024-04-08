namespace Services
{
    public interface IAllCustomersService
    {
        AllCustomersResult GetCustomers(string sortColumn, string sortOrder, string searchQuery, int loadedRows, List<string> selectedCountries, int currentPage);
        List<string> GetAllCountries();
    }
}
