using Microsoft.AspNetCore.Mvc.Rendering;
using ViewModels;
namespace Services
{
	public interface IViewSingleCustomerService
	{
		public ViewSingleCustomerViewModel GetCustomer(int customerId);
		public EditCustomerViewModel GetEditableViewModel(int id);
		public List<SelectListItem> GetGenderSelectListItems();
		public List<SelectListItem> GetCountrySelectListItems();
		public bool UpdateCustomer(EditCustomerViewModel model);
		public bool ToggleCustomerActiveStatus(EditCustomerViewModel model);
	}
}
