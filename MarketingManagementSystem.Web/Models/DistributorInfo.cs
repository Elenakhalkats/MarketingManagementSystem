using MarketingManagementSystem.Core.Enums;

namespace MarketingManagementSystem.Web.Models;

public class DistributorInfo
{
    public DistributorInfo(
        Distributor? distributor,
        IdentityCardInfo? identityCardInfo,
        ContactInfo? contactInfo,
        AddressInfo? addressInfo)
    {
        Distributor = distributor;
        IdentityCardInfo = identityCardInfo;
        ContactInfo = contactInfo;
        AddressInfo = addressInfo;
    }
    public Distributor? Distributor { get; set; }
    public IdentityCardInfo? IdentityCardInfo { get; set; }
    public ContactInfo? ContactInfo { get; set; }
    public AddressInfo? AddressInfo { get; set; }
}

public class UpdateDistributorInfo
{
    public UpdateDistributor? UpdateDistributor { get; set; }
    public UpdateIdentityCardInfo? UpdateIdentityCardInfo { get; set; }
    public UpdateContactInfo? UpdateContactInfo { get; set; }
    public UpdateAddressInfo? UpdateAddressInfo { get; set; }

}

public class UpdateDistributor
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public Gender? Gender { get; set; }
    public string? Img { get; set; }
}
public class UpdateIdentityCardInfo
{
    public DocumentType? DocumentType { get; set; }
    public string? DocumentSerialNumber { get; set; }
    public string? DocumentNumber { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string? TermOfDocument { get; set; }
    public string? PersonalNumber { get; set; }
    public string? IssueAgency { get; set; }
    public int? DistributorId { get; set; }
}
public class UpdateContactInfo
{
    public ContactType? ContactType { get; set; }
    public string? Contact { get; set; }
    public int? DistributorId { get; set; }
}
public class UpdateAddressInfo
{
    public AddressType? AddressType { get; set; }
    public string? Address { get; set; }
    public int? DistributorId { get; set; }
}