using AutoMapper;
using MarketingManagementSystem.Application.ResponseModels;
using MarketingManagementSystem.Domain.Entities;

namespace MarketingManagementSystem.Application.Extentions;

public class Mappers : Profile
{
	public Mappers()
	{
		CreateMap<ProductEntity, Product>();
		CreateMap<SaleEntity, Sale>();
		CreateMap<DistributorEntity, Distributor>();
		CreateMap<BonusEntity, Bonus>();
		CreateMap<SaleEntity, Sale>();
		CreateMap<IdentityCardInfoEntity, IdentityCardInfo>();
		CreateMap<AddressInfoEntity, AddressInfo>();
		CreateMap<ContactInfoEntity, ContactInfo>();
    }
}
