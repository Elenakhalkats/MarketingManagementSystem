using MarketingManagementSystem.Domain.Enums;
using MarketingManagementSystem.Domain.Primitives;

namespace MarketingManagementSystem.Domain.Entities;

public sealed class ContactInfoEntity : Entity<int>
{
    public ContactInfoEntity(
        ContactType contactType, 
        string contact, 
        int distributorId)
    {
        ContactType = contactType;
        Contact = contact;
        DistributorId = distributorId;
    }
    public ContactInfoEntity(AddContactInfo addContactInfo, int distributorId)
    {
        ContactType = addContactInfo.ContactType;
        Contact = addContactInfo.Contact;
        DistributorId = distributorId;
    }
    public ContactType ContactType { get; set; }
    public string Contact { get; set; }
    public int DistributorId { get; set; }
    public DistributorEntity Distributor { get; set; }

}
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
public class AddContactInfo
{
    public ContactType ContactType { get; set; }
    public string Contact { get; set; }
}
public class UpdateContactInfo
{
    public ContactType? ContactType { get; set; }
    public string? Contact { get; set; }
}