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
    public ContactType ContactType { get; set; }
    public string Contact { get; set; }
    public int DistributorId { get; set; }
    public DistributorEntity Distributor { get; set; }
}