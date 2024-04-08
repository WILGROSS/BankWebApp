using ViewModels;
namespace Services
{
	public interface IViewCustomerService
	{
		public ViewCustomerViewModel GetCustomer(int customerId);
	}
}
