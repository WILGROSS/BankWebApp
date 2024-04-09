using ViewModels;
namespace Services
{
	public interface IViewSingleCustomerService
	{
		public ViewSingleCustomerViewModel GetCustomer(int customerId);
		public EditCustomerViewModel GetEditableViewModel(int id);
		public bool UpdateCustomer(EditCustomerViewModel model);
	}
}
