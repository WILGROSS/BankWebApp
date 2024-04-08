using ViewModels;
namespace Services
{
	public interface IViewSingleCustomerService
	{
		public ViewSingleCustomerViewModel GetCustomer(int customerId);
	}
}
