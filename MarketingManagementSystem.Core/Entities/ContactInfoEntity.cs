using MarketingManagementSystem.Core.Enums;
using MarketingManagementSystem.Core.Primitives;
using System.ComponentModel.DataAnnotations;

namespace MarketingManagementSystem.Core.Entities;

public sealed class ContactInfoEntity : AggregateRoot
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
    [Required]
    public ContactType ContactType { get; set; }
    [Required]
    [MaxLength(100)]
    public string Contact { get; set; }
    [Required]
    public int DistributorId { get; set; }
}
