using MarketingManagementSystem.Domain.Enums;
using MarketingManagementSystem.Domain.Primitives;

namespace MarketingManagementSystem.Domain.Entities;

public sealed class IdentityCardInfoEntity : Entity<int>
{
    public IdentityCardInfoEntity(
        DocumentType documentType, 
        string? documentSerialNumber, 
        string? documentNumber, 
        DateTime releaseDate, 
        string termOfDocument,
        string personalNumber, 
        string? issueAgency, 
        int distributorId)
    {
        DocumentType = documentType;
        DocumentSerialNumber = documentSerialNumber;
        DocumentNumber = documentNumber;
        ReleaseDate = releaseDate;
        TermOfDocument = termOfDocument;
        PersonalNumber = personalNumber;
        IssueAgency = issueAgency;
        DistributorId = distributorId;
    }

    public DocumentType DocumentType { get; set; }
    public string? DocumentSerialNumber { get; set; }
    public string? DocumentNumber { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string TermOfDocument { get; set; }
    public string PersonalNumber { get; set; }
    public string? IssueAgency { get; set; }
    public int DistributorId { get; set; }
    public DistributorEntity Distributor { get; set; }
}
