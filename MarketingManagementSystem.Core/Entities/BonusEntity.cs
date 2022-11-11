using MarketingManagementSystem.Core.Primitives;
using System.ComponentModel.DataAnnotations;

namespace MarketingManagementSystem.Core.Entities;

public class BonusEntity : Entity<int>
{
    public BonusEntity(
        int distributorId,
        DateTime startDate,
        DateTime endDate,
        float countedBonus)
    {
        DistributorId = distributorId;
        StartDate = startDate;
        EndDate = endDate;
        CountedBonus = countedBonus;
    }
    [Required]
    public int DistributorId { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public float CountedBonus { get; set; }
}
public class Bonus
{
    public Bonus(
        int? id,
        DateTime startDate,
        DateTime endDate,
        float countedBonus)
    {
        Id = id;
        StartDate = startDate;
        EndDate = endDate;
        CountedBonus = countedBonus;
    }
    public int? Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public float CountedBonus { get; set; }
}