using MarketingManagementSystem.Domain.Enums;
using MarketingManagementSystem.Domain.Primitives;

namespace MarketingManagementSystem.Domain.Entities;

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
    public DistributorEntity(
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
        RecommendAccess = true;
    }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? Img { get; set; }
    public bool RecommendAccess { get; set; }

    public List<BonusEntity> Bonuses { get; set; } = new List<BonusEntity>();
    public List<ContactInfoEntity> ContactInfos { get; set; } = new List<ContactInfoEntity>();
    public List<IdentityCardInfoEntity> IdentityCardInfos { get; set; } = new List<IdentityCardInfoEntity>();
    public List<AddressInfoEntity> AddressInfos { get; set; } = new List<AddressInfoEntity>();
    public List<SaleEntity> Sales { get; set; } = new List<SaleEntity>();
    public List<RecommendationEntity> AsRecommendators { get; set; } = new List<RecommendationEntity>();
}