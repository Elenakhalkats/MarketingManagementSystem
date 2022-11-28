using MarketingManagementSystem.Domain.Primitives;

namespace MarketingManagementSystem.Domain.Entities;

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
    public int DistributorId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public float CountedBonus { get; set; }
    public DistributorEntity Distributor { get; set; }
}
