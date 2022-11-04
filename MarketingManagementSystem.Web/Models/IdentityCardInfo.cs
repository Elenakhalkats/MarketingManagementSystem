using MarketingManagementSystem.Core.Enums;

namespace MarketingManagementSystem.Web.Models;

public class IdentityCardInfo
{
    public IdentityCardInfo(
        int? id,
        DocumentType documentType,
        string? documentSerialNumber,
        string? documentNumber,
        DateTime releaseDate,
        string termOfDocument,
        string personalNumber,
        string? issueAgency,
        int distributorId)
    {
        Id = id;
        DocumentType = documentType;
        DocumentSerialNumber = documentSerialNumber;
        DocumentNumber = documentNumber;
        ReleaseDate = releaseDate;
        TermOfDocument = termOfDocument;
        PersonalNumber = personalNumber;
        IssueAgency = issueAgency;
        DistributorId = distributorId;
    }

    public int? Id { get; set; }
    public DocumentType DocumentType { get; set; }
    public string? DocumentSerialNumber { get; set; }
    public string? DocumentNumber { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string TermOfDocument { get; set; }
    public string PersonalNumber { get; set; }
    public string? IssueAgency { get; set; }
    public int DistributorId { get; set; }

}
