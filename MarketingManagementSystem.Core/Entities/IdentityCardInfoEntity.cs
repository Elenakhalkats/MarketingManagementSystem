using MarketingManagementSystem.Core.Enums;
using MarketingManagementSystem.Core.Primitives;
using System.ComponentModel.DataAnnotations;

namespace MarketingManagementSystem.Core.Entities;

public sealed class IdentityCardInfoEntity : AggregateRoot
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

    [Required]
    public DocumentType DocumentType { get; set; }
    [MaxLength(10)]
    public string? DocumentSerialNumber { get; set; }
    [MaxLength(10)]
    public string? DocumentNumber { get; set; }
    [Required]
    public DateTime ReleaseDate { get; set; }
    [Required]
    public string TermOfDocument { get; set; }
    [Required]
    [MaxLength(50)]
    public string PersonalNumber { get; set; }
    [MaxLength(100)]
    public string? IssueAgency { get; set; }
    public int DistributorId { get; set; }
}
