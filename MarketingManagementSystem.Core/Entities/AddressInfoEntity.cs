using MarketingManagementSystem.Core.Enums;
using MarketingManagementSystem.Core.Primitives;
using System.ComponentModel.DataAnnotations;

namespace MarketingManagementSystem.Core.Entities;

public sealed class AddressInfoEntity : AggregateRoot
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
    [Required]
    public AddressType AddressType { get; set; }
    [MaxLength(100)]
    [Required]
    public string Address { get; set; }
    [Required]
    public int DistributorId { get; set; }
}
