using ViewModels;

namespace Services
{
    public class AllCustomersResult
    {
        public List<AllCustomersViewModel> Customers { get; set; }
        public List<AllCustomersViewModel> VipCustomers { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
