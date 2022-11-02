using MarketingManagementSystem.Core.Enums;

namespace MarketingManagementSystem.Web.Models;

public class AddressInfo
{
    public AddressInfo(
        int? id,
        AddressType addressType,
        string address)
    {
        Id = id;
        AddressType = addressType;
        Address = address;
    }
    public int? Id { get; set; }
    public AddressType AddressType { get; set; }
    public string Address { get; set; }
}
