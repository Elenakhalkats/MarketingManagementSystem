using MarketingManagementSystem.Domain.Entities;

namespace MarketingManagementSystem.Application.ResponseModels;

public class DistributorInfoEntities
{
    public DistributorInfoEntities()
    {

    }
    public DistributorInfoEntities(
       DistributorEntity? distributorInfo,
       IdentityCardInfoEntity? identityCardInfo,
       ContactInfoEntity? contactInfo,
       AddressInfoEntity? addressInfo)
    {
        DistributorInfo = distributorInfo;
        IdentityCardInfo = identityCardInfo;
        ContactInfo = contactInfo;
        AddressInfo = addressInfo;
    }
    public DistributorEntity? DistributorInfo { get; set; }
    public IdentityCardInfoEntity? IdentityCardInfo { get; set; }
    public ContactInfoEntity? ContactInfo { get; set; }
    public AddressInfoEntity? AddressInfo { get; set; }
}

