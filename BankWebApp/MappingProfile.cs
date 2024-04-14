using AutoMapper;
using DataAccessLayer.Data;
using ViewModels;

namespace BankWebApp
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Customer, AllCustomersViewModel>();

			CreateMap<Customer, EditCustomerViewModel>().ReverseMap();

			CreateMap<Transaction, TransactionViewModel>().ReverseMap();

			CreateMap<TransactionViewModel, TransactionViewModel>();

			CreateMap<Customer, ViewSingleCustomerViewModel>()
				.ForMember(dest => dest.Accounts, opt => opt.MapFrom(src => src.Dispositions.Select(d => d.Account)));

			CreateMap<Account, AccountViewModel>()
				.ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions));
		}
	}
}
