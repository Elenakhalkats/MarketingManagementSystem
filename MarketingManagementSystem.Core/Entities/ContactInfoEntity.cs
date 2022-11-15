using MarketingManagementSystem.Core.Enums;
using MarketingManagementSystem.Core.Primitives;
using System.ComponentModel.DataAnnotations;

namespace MarketingManagementSystem.Core.Entities;

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
    [Required]
    public ContactType ContactType { get; set; }
    [Required]
    [MaxLength(100)]
    public string Contact { get; set; }
    [Required]
    public int DistributorId { get; set; }
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