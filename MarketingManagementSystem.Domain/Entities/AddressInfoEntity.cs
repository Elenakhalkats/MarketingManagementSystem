using MarketingManagementSystem.Domain.Enums;
using MarketingManagementSystem.Domain.Primitives;

namespace MarketingManagementSystem.Domain.Entities;

public sealed class AddressInfoEntity : Entity<int>
{
    public AddressInfoEntity()
    {

    }
    public AddressInfoEntity(
        AddressType addressType,
        string address,
        int distributorId)
    {
        AddressType = addressType;
        Address = address;
        DistributorId = distributorId;
    }
    public AddressType AddressType { get; set; }
    public string Address { get; set; }
    public int DistributorId { get; set; }
    public DistributorEntity Distributor { get; set; }

}