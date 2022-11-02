using MarketingManagementSystem.Core.Enums;

namespace MarketingManagementSystem.Web.Models;

public class ContactInfo
{
    public ContactInfo(
        int? id,
        ContactType contactType,
        string contact)
    {
        Id = id;
        ContactType = contactType;
        Contact = contact;
    }

    public int? Id { get; set; }
    public ContactType ContactType { get; set; }
    public string Contact { get; set; }
}
