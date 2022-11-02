using MarketingManagementSystem.Core.Enums;

namespace MarketingManagementSystem.Web.Models;

public class Distributor
{
    public Distributor(
        int? id,
        string firstName,
        string lastName,
        DateTime birthDate,
        Gender gender,
        string img,
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
    public string Img { get; set; }
    public bool RecommendAccess { get; set; }
}
