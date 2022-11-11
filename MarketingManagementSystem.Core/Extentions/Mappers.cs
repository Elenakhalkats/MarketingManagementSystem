using AutoMapper;
using MarketingManagementSystem.Core.Entities;

namespace MarketingManagementSystem.Core.Extentions;

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
