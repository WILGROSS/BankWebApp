﻿using AutoMapper;
using DataAccessLayer.Data;
using ViewModels;

namespace BankWebApp
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Customer, CustomersViewModel>();

			CreateMap<Customer, ViewCustomerViewModel>()
				.ForMember(dest => dest.Accounts, opt => opt.MapFrom(src => src.Dispositions.Select(d => d.Account)));

			CreateMap<Account, AccountViewModel>()
				.ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions));

			CreateMap<Transaction, TransactionViewModel>();
		}
	}
}
