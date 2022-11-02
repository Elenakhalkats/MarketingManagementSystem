using MarketingManagementSystem.Core.Enums;
using MarketingManagementSystem.Core.Primitives;
using System.ComponentModel.DataAnnotations;

namespace MarketingManagementSystem.Core.Entities;

public sealed class DistributorEntity : AggregateRoot
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
