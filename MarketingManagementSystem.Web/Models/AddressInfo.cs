using MarketingManagementSystem.Core.Enums;

namespace MarketingManagementSystem.Web.Models;

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
