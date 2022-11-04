using AutoMapper;
using MarketingManagementSystem.Core.Entities;
using MarketingManagementSystem.Web.Models;

namespace MarketingManagementSystem.Web.Extentions;

public class Mappers : Profile
{
	public Mappers()
	{
		CreateMap<ProductEntity, Product>().ReverseMap();
		CreateMap<SaleEntity, Sale>().ReverseMap();
		CreateMap<DistributorEntity, Distributor>().ReverseMap();
        CreateMap<BonusEntity, Bonus>().ReverseMap();
		CreateMap<SaleEntity, Sale>().ReverseMap();
		CreateMap<IdentityCardInfoEntity, IdentityCardInfo>().ReverseMap();
		CreateMap<AddressInfoEntity, AddressInfo>().ReverseMap();
		CreateMap<ContactInfoEntity, ContactInfo>().ReverseMap();
    }
}
