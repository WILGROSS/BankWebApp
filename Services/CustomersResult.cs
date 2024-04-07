using ViewModels;

namespace Services
{
    public class CustomersResult
    {
        public List<CustomersViewModel> Customers { get; set; }
        public List<CustomersViewModel> VipCustomers { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
