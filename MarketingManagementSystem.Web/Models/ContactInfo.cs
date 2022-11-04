using MarketingManagementSystem.Core.Enums;

namespace MarketingManagementSystem.Web.Models;

public class ContactInfo
{
    public ContactInfo(
        int? id,
        ContactType contactType,
        string contact,
        int distributorId)
    {
        Id = id;
        ContactType = contactType;
        Contact = contact;
        DistributorId = distributorId;
    }

    public int? Id { get; set; }
    public ContactType ContactType { get; set; }
    public string Contact { get; set; }
    public int DistributorId { get; set; }
}
