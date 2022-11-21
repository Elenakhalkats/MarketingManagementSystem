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
    public AddressInfoEntity(AddAddressInfo addAddressInfo, int distributorId)
    {
        AddressType = addAddressInfo.AddressType;
        Address = addAddressInfo.Address;
        DistributorId = distributorId;
    }
    public AddressType AddressType { get; set; }
    public string Address { get; set; }
    public int DistributorId { get; set; }
    public DistributorEntity Distributor { get; set; }

}
public class AddressInfo
{
    public AddressInfo(
        int? id,
        AddressType addressType,
        string address,
        int distributorId)
    {
        Id = id;
        AddressType = addressType;
        Address = address;
        DistributorId = distributorId;
    }
    public int? Id { get; set; }
    public AddressType AddressType { get; set; }
    public string Address { get; set; }
    public int DistributorId { get; set; }
}
public class AddAddressInfo
{
    public AddressType AddressType { get; set; }
    public string Address { get; set; }
}
public class UpdateAddressInfo
{
    public AddressType? AddressType { get; set; }
    public string? Address { get; set; }
}