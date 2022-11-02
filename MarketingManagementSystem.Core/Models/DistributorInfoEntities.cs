using MarketingManagementSystem.Core.Entities;

namespace MarketingManagementSystem.Core.Models;

public class DistributorInfoEntities
{
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

