using MarketingManagementSystem.Core.Enums;
using MarketingManagementSystem.Core.Primitives;
using System.ComponentModel.DataAnnotations;

namespace MarketingManagementSystem.Core.Entities;

public sealed class DistributorEntity : Entity<int>
{
    public DistributorEntity(
        string firstName, 
        string lastName, 
        DateTime birthDate, 
        Gender gender, 
        string? img, 
        bool recommendAccess   
        )
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Gender = gender;
        Img = img;
        RecommendAccess = recommendAccess;
    }
    public DistributorEntity(AddDistributor addDistributor)
    {
        FirstName = addDistributor.FirstName;
        LastName = addDistributor.LastName;
        BirthDate = addDistributor.BirthDate;
        Gender = addDistributor.Gender;
        Img = addDistributor.Img;
        RecommendAccess = true;
    }
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    [Required]
    public Gender Gender { get; set; }
    public string? Img { get; set; }
    [Required]
    public bool RecommendAccess { get; set; }
}
public class Distributor 
{
    public Distributor(
        int? id,
        string firstName,
        string lastName,
        DateTime birthDate,
        Gender gender,
        string? img,
        bool recommendAccess)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Gender = gender;
        Img = img;
        RecommendAccess = recommendAccess;
    }

    public int? Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? Img { get; set; }
    public bool RecommendAccess { get; set; }
}
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
public class AddDistributor
{
    public AddDistributor(
        string firstName,
        string lastName,
        DateTime birthDate,
        Gender gender,
        string? img)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Gender = gender;
        Img = img;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? Img { get; set; }
}
public class UpdateDistributor
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public Gender? Gender { get; set; }
    public string? Img { get; set; }
}
public class AddDistributorInfo
{
    public AddDistributorInfo(
        AddDistributor distributor,
        AddIdentityCardInfo? identityCardInfo,
        AddContactInfo? contactInfo,
        AddAddressInfo? addressInfo)
    {
        Distributor = distributor;
        IdentityCardInfo = identityCardInfo;
        ContactInfo = contactInfo;
        AddressInfo = addressInfo;
    }
    public AddDistributor Distributor { get; set; }
    public AddIdentityCardInfo? IdentityCardInfo { get; set; }
    public AddContactInfo? ContactInfo { get; set; }
    public AddAddressInfo? AddressInfo { get; set; }
}

public class UpdateDistributorInfo
{
    public UpdateDistributor? UpdateDistributor { get; set; }
    public UpdateIdentityCardInfo? UpdateIdentityCardInfo { get; set; }
    public UpdateContactInfo? UpdateContactInfo { get; set; }
    public UpdateAddressInfo? UpdateAddressInfo { get; set; }

}