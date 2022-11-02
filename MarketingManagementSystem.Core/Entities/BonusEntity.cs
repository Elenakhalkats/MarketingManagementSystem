using MarketingManagementSystem.Core.Primitives;
using System.ComponentModel.DataAnnotations;

namespace MarketingManagementSystem.Core.Entities;

public class BonusEntity : AggregateRoot
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
