using MarketingManagementSystem.Core.Enums;
using MarketingManagementSystem.Core.Primitives;
using System.ComponentModel.DataAnnotations;

namespace MarketingManagementSystem.Core.Entities;

public sealed class AddressInfoEntity : Entity<int>
{
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
    [Required]
    public AddressType AddressType { get; set; }
    [MaxLength(100)]
    [Required]
    public string Address { get; set; }
    [Required]
    public int DistributorId { get; set; }
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